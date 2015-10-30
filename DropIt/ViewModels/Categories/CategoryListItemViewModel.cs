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
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
