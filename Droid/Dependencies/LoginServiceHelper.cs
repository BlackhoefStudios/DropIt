using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Auth0.SDK;
using DropIt.Data;
using DropIt.Data.Interfaces.Services;
using DropIt.Data.Interfaces.Users;
using DropIt.Data.Services;
using DropIt.Droid.Dependencies;
using DropIt.Services;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;
using Xamarin.Forms;
using Application = Android.App.Application;

//need this line so we can use the DependencyService.Get<ILoginService>()
[assembly: Xamarin.Forms.Dependency(typeof(LoginServiceHelper))]

namespace DropIt.Droid.Dependencies
{
    public class LoginServiceHelper : Auth0Client, ILoginService
    {
        const string Auth0Domain = "xamandor.auth0.com";
        const string Auth0ClientId = "7LCDnjxDsJTh9fSvrDTNiUcUJ7u3iFHY";

        public LoginServiceHelper() : base(Auth0Domain, Auth0ClientId)
        {
                
        }

        protected override async Task<WebRedirectAuthenticator> GetAuthenticator(string connection, string scope, string title = null)
        {
            var baseAuthenticator = await base.GetAuthenticator(connection, scope, title);
            //get the base web view to show for logging in.
            baseAuthenticator.AllowCancel = false;
            //set the title
            baseAuthenticator.Title = "Sign into DropIt";
            return baseAuthenticator;
        }

        public async System.Threading.Tasks.Task Login()
        {
            //show login
            var user = await LoginAsync(Forms.Context, withRefreshToken: true, title: "Sign In");

            //after login, create user object.
            User toReturn = new User()
            {
                IdToken = user.IdToken,
                RefreshToken = user.RefreshToken,
                Email = user.Profile["email"].Value<string>()
            };

            //create account service and store the credentials on the device.
            var accountService = new AccountHelper(new KeyVaultStorage());
            accountService.StoreCredentials(toReturn);
            SetUser(toReturn);
        }

        public void SetUser(IUser loggedInUser)
        {
            //setup the current user to be the one that signed in.
            LoginService.CurrentUser = loggedInUser;
        }
    }
}