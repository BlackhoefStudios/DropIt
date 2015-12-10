
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

        public void Subscribe()
        {
            MessagingCenter.Subscribe<TaskItemViewModel>(this, "TaskTapped",
                async (task) =>
                {
					var taskToEdit = await ServiceResolver.Tasks.GetTask(task.ModelId);
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

			Refresh = new Command (async () => {
				IsBusy = IsFetchingData = true;

				var categoryService = ServiceResolver.Categories;
				var categories = await categoryService.GetCategories (project.Id);

				DataSource = new ObservableCollection<CategoryListItemViewModel>(categories);

				IsBusy = IsFetchingData = false;
			});

        }
    }
}
