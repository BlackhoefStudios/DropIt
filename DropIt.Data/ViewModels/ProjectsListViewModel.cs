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
using DropIt.Data.ViewModels.Bases;
using System.Windows.Input;

namespace DropIt.Data.ViewModels
{
    public class ProjectsListViewModel : BaseListViewModel<ProjectViewModel>, ISubscriber
    {
        private bool isLoggingIn;
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
                    TaskCount = 5
                });
                DataSource.Add(new ProjectViewModel()
                {
                    Name = "SRS",
                    Id = Guid.NewGuid().ToString(),
                    TaskCount = 0
                });
                DataSource.Add(new ProjectViewModel()
                {
                    Name = "Diagrams",
                    Id = Guid.NewGuid().ToString(),
                    TaskCount = 150
                });
                DataSource.Add(new ProjectViewModel()
                {
                    Name = "Tests",
                    Id = Guid.NewGuid().ToString(),
                    TaskCount = 20
                });
                IsBusy = false;

            }, () => !IsBusy);
        }

        public void Subscribe()
        {
            MessagingCenter.Subscribe<ProjectsListViewModel>(this, "NeedsLogIn", async (app) =>
            {
                IsLoggingIn = true;
                var loginService = DependencyService.Get<ILoginService>();
                var actService = new AccountHelper(DependencyService.Get<ISecureStorage>());

                var newUser = DependencyService.Get<IUser>();

                var currentUser = actService.LoadCredentials(newUser);
                if (currentUser == null)
                {
                    await loginService.Login();
                }
                else
                {
                    loginService.SetUser(currentUser);
                }
                IsLoggingIn = false;
                Refresh.Execute(null);
            });

            MessagingCenter.Subscribe<ProjectViewModel>(this, "DeleteProject", (project) =>
            {
                DataSource.Remove(project);
            });
        }

        public void Unsubscribe()
        {
            MessagingCenter.Unsubscribe<ProjectsListViewModel>(this, "NeedsLogIn");
            MessagingCenter.Unsubscribe<ProjectViewModel>(this, "DeleteProject");
        }
    }
}
