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

		public override string ToString ()
		{
			return string.Format ("{0}: {1}", Name, Subtitle);
		}
	}
}

