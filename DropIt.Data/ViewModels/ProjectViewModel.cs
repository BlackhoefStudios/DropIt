using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DropIt.Data.ViewModels
{
    public sealed class ProjectViewModel
    {
        public ProjectViewModel()
        {
            Delete = new Command(() =>
            {
                MessagingCenter.Send(this, "DeleteProject");
            });
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public int TaskCount { get; set; }

        public ICommand Delete { get; private set; }
    }
}
