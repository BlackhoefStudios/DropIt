using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using BlackhoefStudios.Common.Interfaces.Pages;

namespace BlackhoefStudios.Common.ViewModels.Bases
{
	public abstract class BaseViewModel : ObservableViewModel 
	{
		IApplication app;

        /// <summary>
        /// Creates an instance for a typical view model.
        /// </summary>
        /// <param name="app">The base application for interactions with the navigation of an app.</param>
		protected BaseViewModel (IApplication app)
		{
			this.app = app;
		}

		bool isBusy;

        /// <summary>
        /// When set, it changes the Network icon to show active.
        /// </summary>
		public bool IsBusy {
			get { 
				return isBusy;
			}
			set { 
				if (app != null && app.MainPage != null) {
					var tabber = app.MainPage as TabbedPage;

					if (tabber != null) {
						var navigationPage = tabber.Children [0] as NavigationPage;
						if (navigationPage != null) {
							navigationPage.CurrentPage.IsBusy = value;
						} else {
							tabber.Children [0].IsBusy = value;
						}
					} else {
						app.MainPage.IsBusy = value;
					}
				}

				isBusy = value;
				OnPropertyChanged ("IsBusy");
			}
		}

        string title;

        /// <summary>
        /// When set, it changes the Title of the page.
        /// </summary>
		public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (app != null && app.MainPage != null)
                    app.MainPage.Title = value;

                title = value;
                OnPropertyChanged("Title");
            }
        }

		string icon;

		/// <summary>
		/// When set, it changes the Icon of the page.
		/// </summary>
		public string Icon
		{
			get
			{
				return icon;
			}
			set
			{
				if (app != null && app.MainPage != null)
					app.MainPage.Icon = value;

				icon = value;
				OnPropertyChanged("Icon");
			}
		}

        /// <summary>
        /// Gets the navigation item for the given application. Usefull for when wanting to push or pop pages.
        /// </summary>
		public INavigation Navigation {
			get { 
				var mainPage = app.MainPage;
				if (mainPage != null) {
					if (mainPage is NavigationPage) {
						return mainPage.Navigation;
					}
					if (mainPage is TabbedPage) {
						return ((TabbedPage)mainPage).CurrentPage.Navigation;
					}
					return app.MainPage.Navigation;
				}
				return null;
			}
		}
	}

    /// <summary>
    /// Provides a more generic view model for a page. This is usefull when you have a 
    /// data item backing the view model. For example, if on an edit screen, the DataSource
    /// could possibly be the item you are changing values on.
    /// </summary>
    /// <typeparam name="T">The item type to use for a DataSource.</typeparam>
	public abstract class BaseViewModel<T> : BaseViewModel where T : class
	{
		protected BaseViewModel (IApplication app) 
			:base(app)
		{

		}
		private T dataSource;
        /// <summary>
        /// The Object that is usually used for edits/creates on a page.
        /// </summary>
		public T DataSource {
			get { return dataSource; }
			set {
				dataSource = value;
				OnPropertyChanged ("DataSource");
			}
		}
	}

}

