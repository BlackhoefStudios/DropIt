using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using DropIt.iOS.Renderers;
using UIKit;
using DropIt.Controls;


[assembly: Xamarin.Forms.ExportRenderer(typeof(BorderedEditor), typeof(BorderedEditorRenderer))]
namespace DropIt.iOS.Renderers
{
	public class BorderedEditorRenderer : EditorRenderer
	{
		protected override void UpdateNativeWidget ()
		{
			Layer.BorderWidth = 1;
			Layer.BorderColor = UIColor.Gray.CGColor;
			Layer.CornerRadius = 5;
			base.UpdateNativeWidget ();
		}
	}
}

