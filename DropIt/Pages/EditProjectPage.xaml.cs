using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DropIt.Services;
using BlackhoefStudios.Common.Interfaces.Pages;

namespace DropIt.ViewModels.Projects
{
	public partial class EditProjectPage : ContentPage
	{
		public EditProjectPage (Guid projectId)
		{
			BindingContext = new EditProjectViewModel(Application.Current as IApplication,
				projectId);
			InitializeComponent ();

			var save = new ToolbarItem ();
			save.Command = (BindingContext as EditProjectViewModel).Save;
			save.Text = "Save";

			ToolbarItems.Add (save);
		}
	}
}

