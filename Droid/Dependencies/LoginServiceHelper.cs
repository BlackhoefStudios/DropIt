using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Auth0.SDK;
using DropIt.Data.Interfaces.Services;
using DropIt.Data.Interfaces.Users;

namespace DropIt.Droid.Dependencies
{
    public class LoginServiceHelper : Auth0Client
    {
        const string Auth0Domain = "xamandor.auth0.com";
        const string Auth0ClientId = "7LCDnjxDsJTh9fSvrDTNiUcUJ7u3iFHY";

        public LoginServiceHelper() : base(Auth0Domain, Auth0ClientId)
        {
                
        }
    }
}