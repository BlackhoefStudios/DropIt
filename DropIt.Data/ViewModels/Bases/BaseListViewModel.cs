using DropIt.Data.Interfaces.Pages;
using DropIt.Data.ViewModels.Bases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DropIt.Data.ViewModels.Bases
{
    public abstract class BaseListViewModel<T> : BaseViewModel where T : class
    {
        private ObservableCollection<T> dataSource;
        public ObservableCollection<T> DataSource
        {
            get { return dataSource; }
            set
            {
                dataSource = value;
                OnPropertyChanged("DataSource");
            }
        }

        public ICommand Refresh
        {
            get; protected set;
        }

        protected BaseListViewModel(IApplication app)
            : base(app)
        {
            DataSource = new ObservableCollection<T>();
        }

    }
}
