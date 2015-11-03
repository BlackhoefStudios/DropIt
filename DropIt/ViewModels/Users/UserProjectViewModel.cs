using System;
using BlackhoefStudios.ViewModels.Bases;
using Xamarin.Forms;

namespace DropIt.ViewModels.Users
{
	public class UserProjectViewModel : BaseListItemViewModel
	{
		public const string SelectedMessage = "UserProjectSelected";
		public const string RemoveAccessSelected = "UserProjectRemoveAccess";

		bool isAdmin;
		public bool IsAdminOfProject {
			get { return isAdmin; }
			set {
				isAdmin = value;
				OnPropertyChanged ("IsAdminOfProject");
			}
		}
		public String UserEmail {get;set;}

		public UserProjectViewModel (string userEmail, bool isProjectAdmin)
		{
			IsAdminOfProject = isProjectAdmin;
			Subtitle = isProjectAdmin ? "yes" : "no";
			UserEmail = userEmail;

			Selected = new Command (() => {
				MessagingCenter.Send(this, SelectedMessage);
			});

			Delete = new Command (() => {
				MessagingCenter.Send(this, RemoveAccessSelected);
			});
		}
	}
}

