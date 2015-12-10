using BlackhoefStudios.ViewModels.Bases;
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
		public const string AddedMessage = "ProjectAdded";
        public const string DeleteMessage = "DeleteProject";
		public const string SelectedMessage = "SelectedProject";
        public const string EditMessage = "EditProject";

		public ICommand Edit { get; private set; }

        public ProjectViewModel()
        {
			Subtitle = 0.ToString ();

			Edit = new Command (() => {
				MessagingCenter.Send(this, EditMessage);
			});
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
