using System;
using BlackhoefStudios.ViewModels.Bases;
using Xamarin.Forms;

namespace DropIt.ViewModels.Users
{
	public class UserListItemViewModel : BaseListItemViewModel
	{
		public const string SelectedMessage = "UserSelected";
		public const string DeletedMessage = "UserDeleted";

		public UserListItemViewModel ()
		{
			Selected = new Command (() => {
				MessagingCenter.Send(this, SelectedMessage);
			});

			Delete = new Command (() => {
				MessagingCenter.Send(this, DeletedMessage);
			});
		}
	}
}

