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

namespace DropIt.Data.ViewModels
{
    public class HomePageViewModel : BaseViewModel, ISubscriber
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

        public HomePageViewModel(IApplication app) : base(app)
        {
            Subscribe();
        }

        public void Subscribe()
        {
            MessagingCenter.Subscribe<HomePageViewModel>(this, "NeedsLogIn", async (app) =>
            {
                IsLoggingIn = true;
                var loginService = DependencyService.Get<ILoginService>();
                var actService = DependencyService.Get<IAccountService>();

                var currentUser = actService.LoadCredentials();
                if (currentUser == null)
                {
                    await loginService.Login();
                }
                else
                {
                    loginService.SetUser(currentUser);
                    IsLoggingIn = false;
                }
            });

            MessagingCenter.Subscribe<IUser>(this, "LoggedIn", (user) =>
            {
                IsLoggingIn = false;
            });
        }

        public void Unsubscribe()
        {
            throw new NotImplementedException();
        }
    }
}
