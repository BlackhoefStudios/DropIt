using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DropIt.Data.Interfaces.Services;
using DropIt.Data.Interfaces.Users;
using System.Threading.Tasks;
using DropIt.Droid.Dependencies;
using DropIt.Data;
using Xamarin.Forms;
using Application = Android.App.Application;
using DropIt.Data.Services;
using DropIt.Services;

namespace DropIt.Droid
{
	[Activity (Label = "DropIt.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
        protected override void OnCreate (Bundle bundle)
		{
            KeyVaultStorage.Password = "<YOUR PASSWORD KEY HERE>";
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());
		}
	}
}

