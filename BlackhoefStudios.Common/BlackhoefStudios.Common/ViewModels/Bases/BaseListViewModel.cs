using BlackhoefStudios.Common.Interfaces.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlackhoefStudios.ViewModels.Bases
{
    /// <summary>
    /// A base class that provides common interfaces for ListViews.
    /// </summary>
    public abstract class BaseListViewModel<T> : BaseViewModel where T : class
    {
        private ObservableCollection<T> dataSource;

        /// <summary>
        /// Represents a list of objects to be used as the ListView Source.
        /// </summary>
        public ObservableCollection<T> DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                OnPropertyChanged("DataSource");
            }
        }

        private T selected;
        /// <summary>
        /// The currently selected ListView item.
        /// </summary>
        public T Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                OnPropertyChanged("Selected");
            }
        }

        /// <summary>
        /// The command to call when the Swipe to Refresh (or Pull to Refresh) happens.
        /// </summary>
        public ICommand Refresh
        {
            get; protected set;
        }
        
        /// <summary>
        /// Creates an object for interactions with a ListView.
        /// </summary>
        /// <param name="app"></param>
        protected BaseListViewModel(IApplication app)
            : base(app)
        {
            DataSource = new ObservableCollection<T>();
        }

    }
}
