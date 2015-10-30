using DropIt.ViewModels.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropIt.ViewModels.Tasks
{
    public sealed class TaskItemViewModel : BaseListItemViewModel
    {
        public string CategoryId { get; set; }
    }
}
