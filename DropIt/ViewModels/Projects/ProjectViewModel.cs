using DropIt.ViewModels.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DropIt.ViewModels.Projects
{
    public sealed class ProjectViewModel : BaseListItemViewModel
    {
        public const string DeleteMessage = "DeleteProject";
        public const string SelectedMessage = "SelectedProject";

        public ProjectViewModel()
        {
            Delete = new Command(() =>
            {
                MessagingCenter.Send(this, DeleteMessage);
            });

            Selected = new Command(() =>
            {
                MessagingCenter.Send(this, SelectedMessage);
            });
        }
    }
}
