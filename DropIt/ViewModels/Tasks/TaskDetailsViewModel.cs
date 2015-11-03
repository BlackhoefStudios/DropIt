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

namespace DropIt.ViewModels.Tasks
{
	public class TaskDetailsViewModel : ObservableViewModel, ISubscriber
	{
		public const string SaveMessage = "SaveTask";

		DateTime created;
		public DateTime DateCreated {
			get { return created; }
			set {
				created = value;
				OnPropertyChanged ("DateCreated");
			}
		}

		string description;
		public string Description {
			get { return description; }
			set {
				description = value;
				OnPropertyChanged ("Description");
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

		string assignedTo;
		public string AssignedTo 
		{
			get {
				return assignedTo;
			}
			set {
				assignedTo = value;
				OnPropertyChanged ("AssignedTo");
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

		public ICommand SaveCommand { get; private set; }

		public TaskDetailsViewModel ()
		{
			DateCreated = DateTime.Now;
			Users = new List<string> () {
				"chris.willette@wsu.edu",
				LoginService.CurrentUser.Email
			};

			SetupNewComment ();
			Comments = new ObservableCollection<CommentViewModel> ();
			SaveCommand = new Command (() => {
				MessagingCenter.Send(this, SaveMessage);
			});

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
					SetupNewComment();
				});
		}

		public void Unsubscribe() {
			MessagingCenter.Unsubscribe<CommentViewModel> (this, CommentViewModel.CommentSelected);
		}
	}
}

