using System;
using System.Diagnostics;
using DropIt.Data.Interfaces.Pages;
using DropIt.Data.Interfaces.Services;
using Xamarin.Forms;
using DropIt.Pages;
using DropIt.Services;

namespace DropIt
{
	public class App : Application, IApplication
	{
	    public App()
	    {
            // The root page of your application
            MainPage = new CustomNavigationPage(new HomePage());
	    }

	    protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
