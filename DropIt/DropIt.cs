using System;
using System.Diagnostics;
using Xamarin.Forms;
using DropIt.Pages;
using DropIt.Services;
using BlackhoefStudios.Common.Interfaces.Pages;

namespace DropIt
{
	public class App : Application, IApplication
	{
	    public App()
	    {
            // The root page of your application
			MainPage = new RootTabPage();
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
