using System;
using BlackhoefStudios.Common.ViewModels.Bases;
using DropIt.Data;
using System.Windows.Input;
using Xamarin.Forms;
using DropIt.Services;
using DropIt.Pages;

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

		string defaultCategoryName;
		public string DefaultCategory {
			get { return defaultCategoryName; }
			set {
				defaultCategoryName = value;
				OnPropertyChanged ("DefaultCategory");
				Save.ChangeCanExecute ();
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
					Name = DefaultCategory
				};

				model.NavigationIds.Add(toAdd.Id);

				var toShow = await service.SaveProject(model);
				await categories.SaveCategory(toAdd);

				MessagingCenter.Send<ProjectViewModel>(toShow, ProjectViewModel.AddedMessage);
				await (App.Current.MainPage as RootTabPage).CurrentPage.Navigation.PopAsync();
			}, (sender) => !String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(DefaultCategory));


			model = new Project ();
		}
	}
}

