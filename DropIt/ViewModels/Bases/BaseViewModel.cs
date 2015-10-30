using System;
using System.ComponentModel;
using DropIt.Data.Interfaces.Pages;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace DropIt.ViewModels.Bases
{
	public abstract class BaseViewModel : INotifyPropertyChanged 
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
				if (app != null && app.MainPage != null)
					app.MainPage.IsBusy = value;

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
						return ((TabbedPage)mainPage).Children [0].Navigation;
					}
					return app.MainPage.Navigation;
				}
				return null;
			}
		}

		#region Property Changed Implementation

        /// <summary>
        /// The event for when a property changes.
        /// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify any bound items that a property value has been changed.
        /// </summary>
        /// <param name="propertyName">The property that had changed.</param>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
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

