using System;
using DropIt.Data.Interfaces.Services;
using System.Threading.Tasks;
using DropIt.Data.Interfaces.Users;
using Auth0.SDK;
using Foundation;
using UIKit;
using DropIt.Data;
using DropIt.iOS.Dependencies;
using DropIt.Services;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;
using Xamarin.Forms;

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
	        var webAuth = await base.GetAuthenticator(connection, scope, title);
	        webAuth.AllowCancel = false;
	        webAuth.Title = "Sign into DropIt";
	        return webAuth;
	    }

	    public void SetUser(IUser user)
	    {
	        LoginService.CurrentUser = user;
	    }

	    public async Task Login (){
			var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;

            //show login
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

            var accountService = DependencyService.Get<IAccountService>();
            accountService.StoreCredentials(toReturn);
            SetUser(toReturn);

            MessagingCenter.Send(LoginService.CurrentUser as IUser, "LoggedIn");
        }
    }
}

