using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DropIt.ViewModels.Users;

namespace DropIt.Pages
{
	public partial class AddUserPage : ContentPage
	{
		public AddUserPage ()
		{
			BindingContext = new NewUserViewModel (Navigation);
			InitializeComponent ();
		}
	}
}

