using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Pages;
using DropIt.Data.ViewModels;
using DropIt.Services;
using Xamarin.Forms;

namespace DropIt.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            var binding = new ProjectsListViewModel((IApplication)Application.Current);
            BindingContext = binding;
            InitializeComponent();
            
            Appearing += (sender, args) =>
            {
                if (LoginService.CurrentUser == null)
                {
                    MessagingCenter.Send(binding, "NeedsLogIn");
                }
            };
        }
    }
}
