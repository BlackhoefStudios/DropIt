using BlackhoefStudios.Common.ViewModels.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;

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
		[JsonIgnore]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the item. Typically used as the Title of a cell.
        /// </summary>
		[JsonIgnore]
        public string Name { get; set; }

        /// <summary>
        /// The subtitle for the object. Typically used to show extra informatin of the item.
        /// </summary>
		[JsonIgnore]
        public string Subtitle { get; set; }

        /// <summary>
        /// A command that is normally used when the item has been selected in a list.
        /// </summary>
		[JsonIgnore]
        public ICommand Selected { get; protected set; }

        /// <summary>
        /// A command that is normally used when the item has been deleted from a list.
        /// </summary>
		[JsonIgnore]
        public ICommand Delete { get; protected set; }

        private bool dragging;
		[JsonIgnore]
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
