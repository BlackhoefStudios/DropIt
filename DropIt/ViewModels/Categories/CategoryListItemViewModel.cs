using DropIt.ViewModels.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropIt.ViewModels.Categories
{
    public sealed class CategoryListItemViewModel : ObservableCollection<TaskItemViewModel>
    {
		public const string AddedMessage = "CategoryAdded";
        public string Id { get; set; }
		public Guid ModelId {get;set;}
        public string Name { get; set; }

		public override string ToString ()
		{
			return Name;
		}
    }
}
