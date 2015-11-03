using System;
using DropIt.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: Xamarin.Forms.ExportRenderer(typeof(TextCell), typeof(TappableCellRenderer))]
namespace DropIt.iOS.Renderers
{
	public class TappableCellRenderer : TextCellRenderer
	{
		public override UITableViewCell GetCell (Cell item, 
			UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell (item, reusableCell, tv);

			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

			return cell;
		}	
	}
}

