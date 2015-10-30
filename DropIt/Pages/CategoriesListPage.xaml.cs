using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Pages;

using Xamarin.Forms;
using DropIt.ViewModels.Categories;
using DropIt.ViewModels.Projects;

namespace DropIt.Pages
{
    public partial class CategoriesListPage : ContentPage
    {
        public CategoriesListPage(ProjectViewModel project)
        {
            //Set the view model of the page. Since this is the Projects List page, use that view model.
            var binding = new CategoriesListViewModel((IApplication)Application.Current, project);
            BindingContext = binding;

            InitializeComponent();
        }
    }
}
