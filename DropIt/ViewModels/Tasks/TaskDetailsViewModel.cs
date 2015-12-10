using System;
using BlackhoefStudios.Common.ViewModels.Bases;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using BlackhoefStudios.Common.Interfaces.Pages;
using DropIt.Services;
using System.Windows.Input;
using System.Collections.Generic;
using DropIt.Data.Interfaces.Users;
using DropIt.Data;
using DropIt.ViewModels.Categories;
using System.Diagnostics;
using System.Linq;

namespace DropIt.ViewModels.Tasks
{
	public class TaskDetailsViewModel : BaseViewModel<TaskInfo>, ISubscriber
	{
		public const string SaveMessage = "SaveTask";

		public DateTime DateCreated {
			get { return DataSource.DateCreated; }
			set {
				DataSource.DateCreated = value;
				OnPropertyChanged ("DateCreated");
			}
		}

		public string Description {
			get { return DataSource.Description; }
			set {
				DataSource.Description = value;
				OnPropertyChanged ("Description");
				SaveCommand.ChangeCanExecute ();

			}
		}

		ObservableCollection<CommentViewModel> comments;
		public ObservableCollection<CommentViewModel> Comments {
			get { return comments; }
			set {
				comments = value;
				OnPropertyChanged ("Comments");
			}
		}

		public List<string> Users { get; set; }

		public string AssignedTo 
		{
			get {
				return DataSource.AssignedTo;
			}
			set {
				DataSource.AssignedTo = value;
				OnPropertyChanged ("AssignedTo");
				SaveCommand.ChangeCanExecute ();

			}
		}

		IEnumerable<CategoryListItemViewModel> categories;
		public IEnumerable<CategoryListItemViewModel> Categories {
			get { return categories; }
			set {
				categories = value; 
				OnPropertyChanged ("Categories");
			}
		}

		CategoryListItemViewModel category;
		public CategoryListItemViewModel Category 
		{
			get {
				return category;
			}
			set {
				category = value;
				DataSource.ParentForeignKey = category.ModelId;
				OnPropertyChanged ("Category");
				SaveCommand.ChangeCanExecute ();

			}
		}

		CommentViewModel currentComment;
		public CommentViewModel CurrentComment {
			get {
				return currentComment;
			}
			set {
				currentComment = value;
				OnPropertyChanged ("CurrentComment");
			}
		}

		public bool IsComplete {
			get { return DataSource.IsComplete; } 
			set {
				DataSource.IsComplete = value;
				OnPropertyChanged ("IsComplete");
			}
		}

		public bool IsValid {
			get {
				return !String.IsNullOrEmpty (Description)
				&& !String.IsNullOrEmpty (AssignedTo)
				&& Category != null
				&& Category.ModelId != Guid.Empty;
			}
		}

		public Command SaveCommand { get; private set; }

		bool IsNew { get; set; }

		public TaskDetailsViewModel (Guid projectId, TaskInfo info) : base(null)
		{
			IsNew = info == null;

			DataSource = info ?? new TaskInfo();
			DateCreated = DateTime.UtcNow;
			Users = new List<string> () {
				"chris.willette@wsu.edu",
				LoginService.CurrentUser.Email
			};

			Command loadCategories = new Command (async() => {
				Categories = await ServiceResolver.Categories.GetCategories(projectId, false);
				if (DataSource.ParentForeignKey == Guid.Empty)
					Category = Categories.ElementAt(0);
				else {
					Category = Categories.First(cat => cat.ModelId == DataSource.ParentForeignKey);
				}
			});

			SetupNewComment ();
			Comments = new ObservableCollection<CommentViewModel>(DataSource.Comments.Select(c => new CommentViewModel {
				Id = c.Id,
				Name = c.CommenterName,
				Subtitle = c.Description
			}));

			SaveCommand = new Command (async (sender) => {
				var service = ServiceResolver.Tasks;
				await service.SaveTask(DataSource);

				if(IsNew){
					//add to category
					await ServiceResolver.Categories.AddTask(DataSource);
					if(!IsComplete){
						//update the task count on the project list view
						MessagingCenter.Send<TaskDetailsViewModel, Guid>(this, "IncrementTaskCount", projectId);
					}
				}

				MessagingCenter.Send(this, SaveMessage);
			}, (sender) => IsValid);
			loadCategories.Execute (null);
		}

		void SetupNewComment(){
			CurrentComment = new CommentViewModel () {
				Name = LoginService.CurrentUser.Email
			};
		}

		public void Subscribe() {
			MessagingCenter.Subscribe<CommentViewModel> (this, CommentViewModel.CommentSelected,
				(comment) => {
					if(String.IsNullOrEmpty(CurrentComment.Subtitle)){
						return;
					}

					DataSource.Comments.Add(new Comment(){
						CommenterName = CurrentComment.Name,
						Description = CurrentComment.Subtitle
					});
					
					Comments.Add(CurrentComment);

					SetupNewComment();
				});
		}

		public void Unsubscribe() {
			MessagingCenter.Unsubscribe<CommentViewModel> (this, CommentViewModel.CommentSelected);
		}
	}
}

