using System;
using DropIt.Data;
using Xamarin.Forms;
using BlackhoefStudios.Common.ViewModels.Bases;
using BlackhoefStudios.Common.Interfaces.Pages;
using DropIt.Services;
using System.Collections.ObjectModel;
using DropIt.ViewModels.Categories;
using System.Collections.Generic;

namespace DropIt.ViewModels.Projects
{
	public class EditProjectViewModel : BaseViewModel<Project>
	{
		public string Name {
			get { return DataSource.Name; }
			set {
				DataSource.Name = value;
				Save.ChangeCanExecute ();
				OnPropertyChanged ("Name");
			}
		}

		ObservableCollection<EditCategoryViewModel> categories;

		ObservableCollection<EditCategoryViewModel> Categories {
			get { return categories; }
			set {
				categories = value;
				OnPropertyChanged ("Categories");
			}
		}

		public Command Save { get; private set; }

		public EditProjectViewModel (IApplication app, Guid projectId) : base(app)
		{
			DataSource = new Project ();
			Categories = new ObservableCollection<EditCategoryViewModel> ();
			IsBusy = true;

			var fetchModelCommand = new Command (async () => {

				DataSource = await ServiceResolver.Projects.GetProject(projectId);

				var temp = new List<EditCategoryViewModel>();
				//now get categories for editing
				foreach (var categoryId in DataSource.NavigationIds) {
					var category = await ServiceResolver.Categories.GetCategory(categoryId);
					if(category != null){
						var editModel = new EditCategoryViewModel(category);
						temp.Add(editModel);
					}
				}
				Categories = new ObservableCollection<EditCategoryViewModel>(temp);
				OnPropertyChanged("Name");
				OnPropertyChanged("Categories");
				IsBusy = false;
			});

			Save = new Command (async (sender) => {
				var projectService = ServiceResolver.Projects;
				var categoryService = ServiceResolver.Categories;

				var project = await projectService.GetProject(DataSource.Id);

				var toAdd = new List<Category>();
				var toRemove = new List<Category>();

				foreach (var category in toAdd) {
					category.ParentForeignKey = project.Id;
					await categoryService.SaveCategory(category);
				}

				foreach (var category in toRemove) {
					category.ParentForeignKey = project.Id;
					//await categoryService.RemoveCategory(category);
				}

				await projectService.SaveProject(project);

			}, (sender) => !String.IsNullOrEmpty(Name));
			fetchModelCommand.Execute (null);
		}
	}
}

