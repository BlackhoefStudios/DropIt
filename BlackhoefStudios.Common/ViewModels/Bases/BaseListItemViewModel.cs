using BlackhoefStudios.Common.ViewModels.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlackhoefStudios.ViewModels.Bases
{
    /// <summary>
    /// Represents the basic structure for a ListView cell.
    /// </summary>
    public abstract class BaseListItemViewModel : ObservableViewModel
    {
        /// <summary>
        /// A unique Id for the given item.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the item. Typically used as the Title of a cell.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The subtitle for the object. Typically used to show extra informatin of the item.
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// A command that is normally used when the item has been selected in a list.
        /// </summary>
        public ICommand Selected { get; protected set; }

        /// <summary>
        /// A command that is normally used when the item has been deleted from a list.
        /// </summary>
        public ICommand Delete { get; protected set; }

        private bool dragging;
        public bool Dragging
        {
            get { return dragging; }
            set
            {
                dragging = value;
                OnPropertyChanged("Dragging");
            }
        }
    }
}
