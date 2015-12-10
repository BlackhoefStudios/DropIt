using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using DropIt.ViewModels.Categories;
using DropIt.ViewModels.Projects;
using BlackhoefStudios.Common.Interfaces.Pages;
using DropIt.Data;

namespace DropIt.Pages
{
    public partial class CategoriesListPage : ContentPage
    {
		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			(BindingContext as CategoriesListViewModel).Refresh.Execute (null);
		}

        public CategoriesListPage(ProjectViewModel project)
        {
            //Set the view model of the page. Since this is the Projects List page, use that view model.
            var binding = new CategoriesListViewModel((IApplication)Application.Current, project);
            BindingContext = binding;

			var addButton = new ToolbarItem ();
			addButton.Text = "New Task";
			addButton.Icon = "add-task.png";
			addButton.Command = binding.OpenNewTask;


			ToolbarItems.Add(addButton);

            InitializeComponent();
        }
    }
}
