using System;
using BlackhoefStudios.Common.ViewModels.Bases;
using DropIt.Data;
using System.Windows.Input;
using Xamarin.Forms;
using DropIt.Services;

namespace DropIt.ViewModels.Projects
{
	public class AddProjectViewModel : ObservableViewModel
	{
		Project model;
		public string Name {
			get { return model.Name; }
			set {
				model.Name = value;
				Save.ChangeCanExecute ();
				OnPropertyChanged ("Name");
			}
		}

		public Command Save { get; private set; }

		public AddProjectViewModel ()
		{
			Save = new Command (async (sender) => {
				var service = ServiceResolver.Projects;
				var categories = ServiceResolver.Categories;

				var toAdd = new Category(){
					ParentForeignKey = model.Id,
					Name = "My Test Category"
				};

				model.NavigationIds.Add(toAdd.Id);

				var toShow = await service.SaveProject(model);
				await categories.SaveCategory(toAdd);

				MessagingCenter.Send<ProjectViewModel>(toShow, ProjectViewModel.AddedMessage);
				await App.Current.MainPage.Navigation.PopAsync();
			}, (sender) => !String.IsNullOrEmpty(Name));


			model = new Project ();
		}
	}
}

