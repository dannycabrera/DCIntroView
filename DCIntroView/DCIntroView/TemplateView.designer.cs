// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace DCIntroView
{
	[Register ("TemplateView")]
	partial class TemplateView
	{
		[Outlet]
		MonoTouch.UIKit.UITextView descTextView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel titleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (imageView != null) {
				imageView.Dispose ();
				imageView = null;
			}

			if (titleLabel != null) {
				titleLabel.Dispose ();
				titleLabel = null;
			}

			if (descTextView != null) {
				descTextView.Dispose ();
				descTextView = null;
			}
		}
	}
}
