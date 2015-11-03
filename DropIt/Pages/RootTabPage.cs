using System;
using Xamarin.Forms;

namespace DropIt.Pages
{
	public class RootTabPage : TabbedPage
	{
		public RootTabPage ()
		{
			Children.Add (new CustomNavigationPage (new ProjectListPage ()));
			Children.Add (new CustomNavigationPage (new UsersListPage ()));
		}
	}
}

