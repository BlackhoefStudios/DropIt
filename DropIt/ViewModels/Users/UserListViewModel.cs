using System;
using BlackhoefStudios.Common.ViewModels.Bases;
using BlackhoefStudios.Common.Interfaces.Pages;
using Xamarin.Forms;
using DropIt.Services;
using DropIt.Pages;

namespace DropIt.ViewModels.Users
{
	public class UserListViewModel : BaseListViewModel<UserListItemViewModel>, ISubscriber
	{
		public UserListViewModel (IApplication app) : base(app)
		{
			Refresh = new Command ((obj) => {
				IsFetchingData = true;

				DataSource.Clear();

				DataSource.Add(new UserListItemViewModel(){
					Id = Guid.NewGuid(),
					Name = "chris.willette@wsu.edu",
				});

				DataSource.Add(new UserListItemViewModel(){
					Id = Guid.NewGuid(),
					Name = LoginService.CurrentUser.Email,
				});

				IsFetchingData = false;
			});
		}

		public void Subscribe() {
			MessagingCenter.Subscribe<UserListItemViewModel> (this, UserListItemViewModel.SelectedMessage,
				async (user) => {
					Selected = null;
					await Navigation.PushAsync(new UserDetailsPage(user));
				});

			MessagingCenter.Subscribe<UserListItemViewModel> (this, UserListItemViewModel.DeletedMessage,
				(user) => {
					DataSource.Remove(user);
				});
		}

		public void Unsubscribe() {
			MessagingCenter.Unsubscribe<UserListViewModel> (this, UserListItemViewModel.SelectedMessage);
			MessagingCenter.Unsubscribe<UserListViewModel> (this, UserListItemViewModel.DeletedMessage);
		}
	}
}

