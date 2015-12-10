using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using DropIt.ViewModels.Tasks;
using DropIt.Data;

namespace DropIt.Pages
{
    public partial class TaskDetailsPage : ContentPage
    {

        public TaskDetailsPage(Guid projectId, TaskInfo toEdit = null)
        {
			var binding = new TaskDetailsViewModel (projectId, toEdit);
			BindingContext = binding;

			var save = new ToolbarItem ();
			save.Command = (BindingContext as TaskDetailsViewModel).SaveCommand;
			save.Text = "Save";

			ToolbarItems.Add(save);
            InitializeComponent();
        }
    }
}
