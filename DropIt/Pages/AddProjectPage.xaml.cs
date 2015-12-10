using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DropIt.Data;
using DropIt.Services;
using DropIt.ViewModels.Projects;
using DropIt.ViewModels.Categories;

namespace DropIt.Pages
{
	public partial class AddProjectPage : ContentPage
	{
		public AddProjectPage ()
		{
			BindingContext = new AddProjectViewModel ();

			var save = new ToolbarItem();
			save.Text = "Save";
			save.Icon = string.Empty;
			save.Command = (BindingContext as AddProjectViewModel).Save;
			save.CommandParameter = BindingContext;

			ToolbarItems.Add (save);

			InitializeComponent ();
		}
	}
}

