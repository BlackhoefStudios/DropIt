using System;
using DropIt.Data.Interfaces.Services;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Users;
using Auth0.SDK;
using Foundation;
using UIKit;
using DropIt.Data;
using DropIt.Data.Services;
using DropIt.iOS.Dependencies;
using DropIt.Services;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;
using Xamarin.Forms;

//need this line so we can use the DependencyService.Get<ILoginService>()
[assembly: Xamarin.Forms.Dependency(typeof(LoginServiceHelper))]

namespace DropIt.iOS.Dependencies
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
            //get the base web view to show for logging in.
	        var webAuth = await base.GetAuthenticator(connection, scope, title);
            //force them to login and not cancel.
	        webAuth.AllowCancel = false;
            //set the title
	        webAuth.Title = "Sign into DropIt";
	        return webAuth;
	    }

	    public void SetUser(IUser user)
	    {
            //setup the current user to be the one that signed in.
	        LoginService.CurrentUser = user;
	    }

	    public async System.Threading.Tasks.Task Login (){
            //get the main controller
			var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;

            //show login page.
            var result = await LoginAsync(
                controller,
                withRefreshToken: true);

            //after login, create user object.
	        User toReturn = new User()
	        {
                IdToken = result.IdToken,
                RefreshToken = result.RefreshToken,
                Email = result.Profile["email"].Value<string>()
	        };

            //create account service and store the credentials on the device.
            var accountService =new AccountHelper(new SecureStorage());
            accountService.StoreCredentials(toReturn);

            //set the current user.
            SetUser(toReturn);
        }
    }
}

