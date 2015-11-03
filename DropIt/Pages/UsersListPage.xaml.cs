using System;
using System.Collections.Generic;

using Xamarin.Forms;
using BlackhoefStudios.Common.Interfaces.Pages;
using DropIt.ViewModels.Users;

namespace DropIt.Pages
{
	public partial class UsersListPage : ContentPage
	{
		public UsersListPage ()
		{
			var vm = new UserListViewModel ((IApplication)Application.Current);
			BindingContext = vm;
			ToolbarItems.Add(new ToolbarItem("New User", "add-user.png", async () => {
				await Navigation.PushAsync(new AddUserPage());
			}));

			InitializeComponent ();
			Appearing += (object sender, EventArgs e) => vm.Refresh.Execute (sender);
		}
	}
}

