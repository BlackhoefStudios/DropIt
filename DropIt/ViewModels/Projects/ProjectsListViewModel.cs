using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Pages;
using DropIt.Data.Interfaces.Services;
using DropIt.Data.Interfaces.Users;
using Xamarin.Forms;
using DropIt.Data.Services;
using DropIt.ViewModels.Bases;
using System.Windows.Input;
using DropIt.Pages;

namespace DropIt.ViewModels.Projects
{
    /// <summary>
    /// A class for the ProjectsList page. 
    /// </summary>
    public class ProjectsListViewModel : BaseListViewModel<ProjectViewModel>, ISubscriber
    {
        private bool isLoggingIn;
        /// <summary>
        /// Used to indicate if the screen is currently loading the Auth0 dialog.
        /// </summary>
        public bool IsLoggingIn
        {
            get
            {
                return isLoggingIn;
            }
            set
            {
                isLoggingIn = value;
                OnPropertyChanged("IsLoggingIn");
            }
        }

        public ProjectsListViewModel(IApplication app) : base(app)
        {
            Refresh = new Command(() =>
            {
                IsBusy = true;

                DataSource.Clear();
                //update list view
                DataSource.Add(new ProjectViewModel()
                {
                    Name = "DropIt Authentication",
                    Id = Guid.NewGuid().ToString(),
                    Subtitle = 5.ToString()
                });
                DataSource.Add(new ProjectViewModel()
                {
                    Name = "SRS",
                    Id = Guid.NewGuid().ToString(),
                    Subtitle = 0.ToString()
                });
                DataSource.Add(new ProjectViewModel()
                {
                    Name = "Diagrams",
                    Id = Guid.NewGuid().ToString(),
                    Subtitle = 150.ToString()
                });
                DataSource.Add(new ProjectViewModel()
                {
                    Name = "Tests",
                    Id = Guid.NewGuid().ToString(),
                    Subtitle = 20.ToString()
                });
                IsBusy = false;

            }, () => !IsBusy);
        }

        /// <summary>
        /// Setup any messaging the app should respond too.
        /// </summary>
        public void Subscribe()
        {
            MessagingCenter.Subscribe<ProjectsListViewModel>(this, "NeedsLogIn", async (app) =>
            {
                //This function gets called when the user needs to login, or it is the first run of the application.
                IsLoggingIn = true;

                //get the platform code for opening the login dialog
                var loginService = DependencyService.Get<ILoginService>();

                //get access to the devices secure storage.
                var actService = new AccountHelper(DependencyService.Get<ISecureStorage>());
                //get a new instance of a user to populate values for.
                var newUser = DependencyService.Get<IUser>();

                //try loading values into the user. If null, we need to login.
                var currentUser = actService.LoadCredentials(newUser);
                if (currentUser == null)
                {
                    // need to login, so trigger that workflow on the device.
                    await loginService.Login();
                }
                else
                {
                    // no login needed, so just set this user to the current one.
                    loginService.SetUser(currentUser);
                }

                //disable loading spinner
                IsLoggingIn = false;

                //refresh the items in the list.
                Refresh.Execute(null);
            });

            MessagingCenter.Subscribe<ProjectViewModel>(this, ProjectViewModel.DeleteMessage, (project) =>
            {
                //user chose to delete a project, so simply remove from the list.
                DataSource.Remove(project);
            });

            MessagingCenter.Subscribe<ProjectViewModel>(this, ProjectViewModel.SelectedMessage, (project) =>
            {
                //user selected an item, so push a new page (the categories view page).
                //also, deselect the project so it doesn't remain highlighted.
                Selected = null;
                Navigation.PushAsync(new CategoriesListPage(project));
            });
        }

        /// <summary>
        /// Unsubscribe from all events. This prevents duplication of event firing.
        /// </summary>
        public void Unsubscribe()
        {
            MessagingCenter.Unsubscribe<ProjectsListViewModel>(this, "NeedsLogIn");
            MessagingCenter.Unsubscribe<ProjectViewModel>(this, ProjectViewModel.DeleteMessage);
            MessagingCenter.Unsubscribe<ProjectViewModel>(this, ProjectViewModel.SelectedMessage);
        }
    }
}
