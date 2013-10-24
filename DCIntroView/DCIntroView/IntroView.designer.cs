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
	[Register ("IntroView")]
	partial class IntroView
	{
		[Outlet]
		MonoTouch.UIKit.UIPageControl pageControl { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView scrollView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton skipButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (scrollView != null) {
				scrollView.Dispose ();
				scrollView = null;
			}

			if (pageControl != null) {
				pageControl.Dispose ();
				pageControl = null;
			}

			if (skipButton != null) {
				skipButton.Dispose ();
				skipButton = null;
			}
		}
	}
}
