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
	public class TaskDetailsViewModel : ObservableViewModel, ISubscriber
	{
		TaskInfo data;

		public const string SaveMessage = "SaveTask";

		public DateTime DateCreated {
			get { return data.DateCreated; }
			set {
				data.DateCreated = value;
				OnPropertyChanged ("DateCreated");
			}
		}

		public string Description {
			get { return data.Description; }
			set {
				data.Description = value;
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
				return data.AssignedTo;
			}
			set {
				data.AssignedTo = value;
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
				data.ParentForeignKey = category.ModelId;
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

		public bool IsValid {
			get {
				return !String.IsNullOrEmpty (Description)
				&& !String.IsNullOrEmpty (AssignedTo)
				&& Category.ModelId != Guid.Empty;
			}
		}

		public Command SaveCommand { get; private set; }

		public TaskDetailsViewModel (Guid projectId, TaskInfo info)
		{
			data = info ?? new TaskInfo();
			DateCreated = DateTime.UtcNow;
			Users = new List<string> () {
				"chris.willette@wsu.edu",
				LoginService.CurrentUser.Email
			};

			Command c = new Command (async() => {
				Categories = await ServiceResolver.Categories.GetCategories(projectId);
				if (data.ParentForeignKey == Guid.Empty)
					Category = Categories.ElementAt(0);
			});

			SetupNewComment ();
			Comments = new ObservableCollection<CommentViewModel> ();
			SaveCommand = new Command (async (sender) => {
				var service = ServiceResolver.Tasks;
				await service.SaveTask(data);

				await ServiceResolver.Categories.AddTask(data);

				MessagingCenter.Send(this, SaveMessage);
			}, (sender) => IsValid);
			c.Execute (null);
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

					CurrentComment.Name = string.Format("{0}: {1}", 
						currentComment.Name,
						currentComment.Subtitle);
					
					Comments.Add(CurrentComment);
					data.Comments.Add(new Comment(){
						CommenterName = CurrentComment.Name,
						Descriptions = CurrentComment.Subtitle
					});
					SetupNewComment();
				});
		}

		public void Unsubscribe() {
			MessagingCenter.Unsubscribe<CommentViewModel> (this, CommentViewModel.CommentSelected);
		}
	}
}

