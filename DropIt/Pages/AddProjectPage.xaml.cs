using System;
using System.Collections.Generic;

using Xamarin.Forms;
using DropIt.Data;
using DropIt.Services;
using DropIt.ViewModels.Projects;

namespace DropIt.Pages
{
	public partial class AddProjectPage : ContentPage
	{
		public AddProjectPage ()
		{
			BindingContext = new Project ();
			var save = new ToolbarItem ("Save", string.Empty, 
		           async () => {
					var service = new StorageService();
					var newProject = await service.SaveProject(BindingContext as Project);
					MessagingCenter.Send<ProjectViewModel>(newProject, ProjectViewModel.AddedMessage);
					await Navigation.PopAsync();
				});

			ToolbarItems.Add (save);

			InitializeComponent ();
		}
	}
}

