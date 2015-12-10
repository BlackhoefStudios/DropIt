using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BlackhoefStudios.Common.ViewModels.Bases
{
    public abstract class ObservableViewModel : INotifyPropertyChanged
    {

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
}
