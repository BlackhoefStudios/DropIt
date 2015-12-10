
using BlackhoefStudios.Common.Interfaces.Pages;
using BlackhoefStudios.ViewModels.Bases;
using DropIt.Pages;
using DropIt.ViewModels.Projects;
using DropIt.ViewModels.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BlackhoefStudios.Common.ViewModels.Bases;
using DropIt.Services;
using System.Collections.ObjectModel;

namespace DropIt.ViewModels.Categories
{
    public sealed class CategoriesListViewModel : BaseListViewModel<CategoryListItemViewModel>, ISubscriber
    {
		ProjectViewModel data;

		public Command OpenNewTask { get; private set; }

        public void Subscribe()
        {
            MessagingCenter.Subscribe<TaskItemViewModel>(this, "TaskTapped",
                async (task) =>
                {
					var taskToEdit = await ServiceResolver.Tasks.GetTask(task.Id);
					await base.Navigation.PushAsync(new TaskDetailsPage(data.Id, taskToEdit));
					Selected = null;
                });

			MessagingCenter.Subscribe<TaskDetailsViewModel> (this, TaskDetailsViewModel.SaveMessage,
				async (saved) => {
					var newListItem = new TaskItemViewModel{
						Name = saved.Description,
						Subtitle = saved.AssignedTo
					};

					var category = DataSource.FirstOrDefault(c => c.ModelId == saved.Category.ModelId);

					category.Add(newListItem);
					await base.Navigation.PopAsync();
				});
        }

        public void Unsubscribe()
        {
            MessagingCenter.Unsubscribe<TaskItemViewModel>(this, "TaskTapped");
        }

        public CategoriesListViewModel(IApplication app, ProjectViewModel project) : base(app)
        {
			data = project;
            Title = project.Name;
			IsBusy = IsFetchingData = true;

			OpenNewTask = new Command (async () => {
				await Navigation.PushAsync (new TaskDetailsPage (project.Id, null));
			}, () => !IsFetchingData);

			Refresh = new Command (async () => {
				IsBusy = IsFetchingData = true;
				OpenNewTask.ChangeCanExecute();

				if(DataSource.Count > 0) {
					IsBusy = IsFetchingData = false;
					OpenNewTask.ChangeCanExecute();

					return;
				}

				var categoryService = ServiceResolver.Categories;

				var categories = await categoryService.GetCategories (project.Id, true);

				DataSource = new ObservableCollection<CategoryListItemViewModel>(categories);

				IsBusy = IsFetchingData = false;
				OpenNewTask.ChangeCanExecute();
			});

        }
    }
}
