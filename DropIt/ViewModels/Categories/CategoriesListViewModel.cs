
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

namespace DropIt.ViewModels.Categories
{
    public sealed class CategoriesListViewModel : BaseListViewModel<CategoryListItemViewModel>, ISubscriber
    {
        public void Subscribe()
        {
            MessagingCenter.Subscribe<TaskItemViewModel>(this, "TaskTapped",
                async (task) =>
                {
                    await base.Navigation.PushAsync(new TaskDetailsPage());
					Selected = null;
                });

			MessagingCenter.Subscribe<TaskDetailsViewModel> (this, TaskDetailsViewModel.SaveMessage,
				async (saved) => {

					var newListItem = new TaskItemViewModel{
						Name = saved.Description,
						Subtitle = LoginService.CurrentUser.Email
					};

					DataSource[0].Add(newListItem);
					await base.Navigation.PopAsync();
				});
        }

        public void Unsubscribe()
        {
            MessagingCenter.Unsubscribe<TaskItemViewModel>(this, "TaskTapped");
        }
        public CategoriesListViewModel(IApplication app, ProjectViewModel project) : base(app)
        {
            Title = project.Name;

            var cat = new CategoryListItemViewModel()
            {
                Id = "Backlog",
                Name = "Backlog 1"
            };
            cat.Add(new TaskItemViewModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Translate app",
                Subtitle = "Assigned To: Tyler Vanderhoef"
            });

            var cat2 = new CategoryListItemViewModel()
            {
                Id = "In Process",
                Name = "In Process 1"
            };
            cat2.Add(new TaskItemViewModel
            {
                Id = "Categories View",
                Name = "Categories UI",
                Subtitle = "Assigned To: Chris Willette"
            });

            DataSource.Add(cat);
            DataSource.Add(cat2);

            DataSource.Add(new CategoryListItemViewModel()
            {
                Id = "Category 2",
                Name = "Category 2"
            });
            DataSource.Add(new CategoryListItemViewModel()
            {
                Id = "Category 3",
                Name = "Category 3"
            });
            DataSource.Add(new CategoryListItemViewModel()
            {
                Id = "Category 4",
                Name = "Category 4"
            });
        }
    }
}
