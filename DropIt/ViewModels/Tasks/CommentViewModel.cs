using System;
using BlackhoefStudios.ViewModels.Bases;
using Xamarin.Forms;

namespace DropIt.ViewModels.Tasks
{
	public class CommentViewModel : BaseListItemViewModel
	{
		public const string CommentSelected = "CommentSelected";
		public CommentViewModel ()
		{
			Selected = new Command (() => {
				MessagingCenter.Send(this, CommentSelected); 
			});
		}
	}
}

