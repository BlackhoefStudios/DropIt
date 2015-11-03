using System;
using BlackhoefStudios.Common.ViewModels.Bases;
using System.Collections.ObjectModel;
using DropIt.ViewModels.Projects;
using BlackhoefStudios.Common.Interfaces.Pages;
using Xamarin.Forms;
using DropIt.Pages;

namespace DropIt.ViewModels.Users
{
	public class UserDetailsViewModel : BaseListViewModel<UserProjectViewModel>, ISubscriber
	{
		public string Email {get;set;}
		public string DateCreated { get; set; }


		public UserDetailsViewModel (UserListItemViewModel vm, IApplication app) : base(app)
		{
			Email = vm.Name;
			DateCreated = new DateTime (2015, 10, 15).ToString("d");

			DataSource.Add (new UserProjectViewModel (Email, false) {
				Name = "SRS"
			});

			DataSource.Add (new UserProjectViewModel(Email, true){
				Name = "Tests "
			});
		}

		public void Subscribe() {
			MessagingCenter.Subscribe<UserProjectViewModel> (this, UserProjectViewModel.SelectedMessage,
				(userProj) => {
					Selected = null;
				});

			MessagingCenter.Subscribe<UserProjectViewModel> (this, UserProjectViewModel.RemoveAccessSelected,
				(userProj) => {
					DataSource.Remove(userProj);
				});
		}

		public void Unsubscribe() {
			MessagingCenter.Unsubscribe<UserProjectViewModel> (this, UserProjectViewModel.SelectedMessage);
			MessagingCenter.Unsubscribe<UserProjectViewModel> (this, UserProjectViewModel.RemoveAccessSelected);
		}
	}
}

