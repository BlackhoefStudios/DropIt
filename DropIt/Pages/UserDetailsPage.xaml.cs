using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DropIt.ViewModels.Users;
using BlackhoefStudios.Common.Interfaces.Pages;

namespace DropIt.Pages
{
	public partial class UserDetailsPage : ContentPage
	{
		public UserDetailsPage (UserListItemViewModel vm)
		{
			BindingContext = new UserDetailsViewModel (vm, (IApplication)Application.Current);
			InitializeComponent ();
		}
	}
}

