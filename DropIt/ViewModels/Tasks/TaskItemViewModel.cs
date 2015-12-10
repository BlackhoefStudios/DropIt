using BlackhoefStudios.ViewModels.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DropIt.ViewModels.Tasks
{
    public sealed class TaskItemViewModel : BaseListItemViewModel
    {
        public TaskItemViewModel()
        {
            Selected = new Command(() =>
            {
                MessagingCenter.Send(this, "TaskTapped");
            });
        }
        public Guid ModelId { get; set; }
    }
}
