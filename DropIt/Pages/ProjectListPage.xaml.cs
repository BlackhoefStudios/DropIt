using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DropIt.Services;
using Xamarin.Forms;
using DropIt.Data.ViewModels.Projects;
using DropIt.Data.Interfaces.Pages;

namespace DropIt.Pages
{
    public partial class ProjectListPage : ContentPage
    {
        public ProjectListPage()
        {
            //Set the view model of the page. Since this is the Projects List page, use that view model.
            var binding = new ProjectsListViewModel((IApplication)Application.Current);
            BindingContext = binding;
            InitializeComponent();
            
            //when the page first appears, check if the user needs to login or not.
            Appearing += (sender, args) =>
            {
                //if signed in already, the CurrentUser will not be null.
                if (LoginService.CurrentUser == null)
                {
                    //send a message saying we need to login.
                    MessagingCenter.Send(binding, "NeedsLogIn");
                }
            };
        }
    }
}
