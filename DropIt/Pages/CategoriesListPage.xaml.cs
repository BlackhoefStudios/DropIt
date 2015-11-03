using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using DropIt.ViewModels.Categories;
using DropIt.ViewModels.Projects;
using BlackhoefStudios.Common.Interfaces.Pages;

namespace DropIt.Pages
{
    public partial class CategoriesListPage : ContentPage
    {
        public CategoriesListPage(ProjectViewModel project)
        {
            //Set the view model of the page. Since this is the Projects List page, use that view model.
            var binding = new CategoriesListViewModel((IApplication)Application.Current, project);
            BindingContext = binding;

			ToolbarItems.Add(new ToolbarItem("NewTask", "add-task.png", async () => {
				await Navigation.PushAsync(new TaskDetailsPage());
			}));

            InitializeComponent();
        }
    }
}
