using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace DCIntroView.Sample
{
	public partial class SampleViewController : UIViewController
	{
		DCIntroView.IntroView _introView;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public SampleViewController ()
			: base (UserInterfaceIdiomIsPhone ? "SampleViewController_iPhone" : "SampleViewController_iPad", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			displayIntoButton.TouchUpInside += delegate {
				DisplayIntro();
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			DisplayIntro();
		}

		private void DisplayIntro()
		{
			if (_introView == null) {
				_introView = new IntroView();
				_introView.ClearBackground = true;
				_introView.SetSkipButtonTitleColor(UIColor.White);


				// First view
				_introView.AddViewToControllers(new TemplateView ("Xamarin Monkeys", "Here is my family picture.") {
					Image = UIImage.FromFile("Images/monkeys.png"),
					ViewBackgroundColor = UIColor.Black.ColorWithAlpha(.8f),
					DescriptionTextColor = UIColor.White,
					TitleTextColor = UIColor.White,
					TitleFont = UIFont.SystemFontOfSize(20),
					DescriptionFont = UIFont.FromName("Georgia-BoldItalic", 15),
					TitleY = (UserInterfaceIdiomIsPhone ? 50 : 100),
					DescriptionY = (UserInterfaceIdiomIsPhone ? 90 : 200),
					ImageY = (UserInterfaceIdiomIsPhone ? 150 : 300)
				}.View);

				// Second view
				var img = UIImage.FromFile ("Images/monkey.png").Scale(new SizeF(UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height));
				_introView.AddViewToControllers(new TemplateView ("Xamarin Monkey", "Me by the water...") {
					ViewBackgroundColor =  UIColor.FromPatternImage (img),
					DescriptionTextColor = UIColor.White,
					TitleTextColor = UIColor.White
				}.View);

				// Third view (custom view)
				UIView customView = new UIView (this.View.Bounds);
				int customImageWidth = 200;
				int screenWidth = (int)UIScreen.MainScreen.Bounds.Width;
				UIImageView custImg = new UIImageView(new Rectangle(screenWidth/2 - customImageWidth/2, 100, customImageWidth, 200));
				custImg.ContentMode = UIViewContentMode.ScaleAspectFit;
				custImg.Image = UIImage.FromFile("Images/monkey2.png");
				customView.AddSubview (custImg);
				customView.BackgroundColor = UIColor.FromRGBA (1.0f, 0.58f, 0.21f, 1.0f);

				int labelWidth = 200;
				var lbl = new UILabel(new Rectangle(screenWidth/2 - labelWidth/2, ((int)custImg.Frame.Y+(int)custImg.Frame.Height + 30), labelWidth, 40));
				lbl.Text = "This is my custom view.";

				customView.AddSubview (lbl);
				_introView.AddViewToControllers(customView);

				// Fourth view
				var sb = new System.Text.StringBuilder ("");
				sb.AppendLine ("\u2022 Customizable\r");
				sb.AppendLine ("\u2022 iOS 7 support\r");
				sb.AppendLine ("\u2022 iPhone, iPod & iPad support\r");
				sb.AppendLine ("\u2022 Use template or create your own view\r");
				sb.AppendLine ("\r\r");
				sb.AppendLine ("Example UITextView features:\r\r\r");
				sb.AppendLine ("Phone: (888) 555-5512\r\r\r");
				sb.AppendLine ("Email: John-Appleseed@mac.com\r\r\r");
				sb.AppendLine ("Url: www.Apple.com");

				_introView.AddViewToControllers(new TemplateView ("Features", sb.ToString()) {
					ViewBackgroundColor = UIColor.Black.ColorWithAlpha(.8f),
					DescriptionTextColor = UIColor.White,
					TitleTextColor = UIColor.White,
					TitleFont = UIFont.SystemFontOfSize(20),
					TitleY = (UserInterfaceIdiomIsPhone ? 50 : 100),
					DescriptionY = (UserInterfaceIdiomIsPhone ? 90 : 200),
					DescriptionTextAlignment = UITextAlignment.Left,
					DescriptionHeight = (UserInterfaceIdiomIsPhone ? 300 : 600),
				}.View);

				// Optional
				_introView.SetPageIndicatorTintColor(UIColor.DarkGray);
				_introView.SetCurrentPageIndicatorTintColor(UIColor.FromRGB (23, 112, 255));
				//_introView.ShowSkipButtonOnLastPage = true;
				//_introView.SkipButtonTitle = "Skip";
				//_introView.SetSkipButtonTitleColor(UIColor.DarkGray);
				//_introView.SetSkipButtonAlignment = IntroView.SkipButtonAlignment.TopCenter;

				_introView.SkipButtonHandler = delegate{
					_introView.View.RemoveFromSuperview();
				};
			}

			this.View.AddSubview(_introView.View);
		}
	}
}