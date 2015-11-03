using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using DropIt.ViewModels.Tasks;

namespace DropIt.Pages
{
    public partial class TaskDetailsPage : ContentPage
    {
        public TaskDetailsPage()
        {
			BindingContext = new TaskDetailsViewModel ();
            InitializeComponent();
        }
    }
}
