using System;
using System.Drawing;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace DCIntroView
{
	public partial class IntroView : UIViewController
	{
		List<UIViewController> _controllers = new List<UIViewController>();
		EventHandler _skipButtonHandler;
		SkipButtonAlignment _skipAlignment = SkipButtonAlignment.BottomRight;
		UIColor _pageIndicatorTintColor;
		UIColor _currentPageIndicatorTintColor;
		UIColor _skipButtonTitleColor;
		bool _showSkipButtonOnLastPage = false;
		bool _clearBackground = false;
		string _skipButtonTitle = "Skip";

		public enum SkipButtonAlignment
		{
			TopLeft,
			TopCenter,
			TopRight,
			BottomLeft,
			BottomCenter,
			BottomRight
		}

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public IntroView ()
			: base (UserInterfaceIdiomIsPhone ? "IntroView_iPhone" : "IntroView_iPad", null)
		{
		}

		#region Public properties
		public List<UIViewController> Controllers
		{
			get { return _controllers; }
			set { _controllers = value; }
		}

		public string SkipButtonTitle
		{
			get { return _skipButtonTitle; }
			set { _skipButtonTitle = value; }
		}

		public bool ClearBackground
		{
			get { return _clearBackground; }
			set { _clearBackground = value; }
		}

		public void AddViewToControllers(UIView view)
		{
			var controller = new UIViewController ();
			controller.Add (view);
			_controllers.Add (controller);
		}

		public void SetSkipButtonTitleColor(UIColor color)
		{
			_skipButtonTitleColor = color;
		}

		public void SetPageIndicatorTintColor(UIColor color)
		{
			_pageIndicatorTintColor = color;
		}

		public void SetCurrentPageIndicatorTintColor(UIColor color)
		{
			_currentPageIndicatorTintColor = color;
		}

		public Boolean ShowSkipButtonOnLastPage
		{
			get { return _showSkipButtonOnLastPage; }
			set { _showSkipButtonOnLastPage = value; }
		}

		public SkipButtonAlignment SetSkipButtonAlignment
		{
			get { return _skipAlignment; }
			set { _skipAlignment = value; }
		}

		public EventHandler SkipButtonHandler
		{
			get { return _skipButtonHandler; }
			set { _skipButtonHandler = value; }
		}
		#endregion

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		
			InitializeUI ();
			LoadControllers ();
		}

		private void InitializeUI()
		{
			//this.View.BackgroundColor = UIColor.Green;
			var height = UIScreen.MainScreen.Bounds.Height;
			var width = UIScreen.MainScreen.Bounds.Width;

			if (NavigationController != null)
				NavigationController.SetNavigationBarHidden (true, false);

			this.HidesBottomBarWhenPushed = true;

			pageControl.ValueChanged += HandlePageControlValueChanged;	

			if (_pageIndicatorTintColor != null)
				pageControl.PageIndicatorTintColor = _pageIndicatorTintColor;

			if (_currentPageIndicatorTintColor != null)
				pageControl.CurrentPageIndicatorTintColor = _currentPageIndicatorTintColor;

			scrollView.Scrolled += HandleScrolled;

			#region SkipButton
			skipButton.AllTouchEvents += _skipButtonHandler; // .TouchUpInside

			pageControl.Frame = new RectangleF (new PointF(pageControl.Frame.X, height-55), pageControl.Frame.Size);
			if (_skipAlignment == SkipButtonAlignment.BottomLeft)
			{
				skipButton.Frame = new RectangleF (20, ((height - skipButton.Frame.Height) - 20), skipButton.Frame.Width, skipButton.Frame.Height);
				skipButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			}
			else if (_skipAlignment == SkipButtonAlignment.BottomCenter)
			{
				var x = width/2 - skipButton.Frame.Width/2;
				var skipHeight = height-skipButton.Frame.Height - 20;
				skipButton.Frame = new RectangleF (x, skipHeight, skipButton.Frame.Width, skipButton.Frame.Height);
				skipButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
				pageControl.Frame = new RectangleF (new PointF(pageControl.Frame.X, skipHeight-30), pageControl.Frame.Size);
			}
			else if (_skipAlignment == SkipButtonAlignment.BottomRight)
			{
				skipButton.Frame = new RectangleF (width-skipButton.Frame.Width - 20, height-skipButton.Frame.Height - 20, skipButton.Frame.Width, skipButton.Frame.Height);
				skipButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
			}
			else if (_skipAlignment == SkipButtonAlignment.TopLeft)
			{
				skipButton.Frame = new RectangleF (20, 20, skipButton.Frame.Width, skipButton.Frame.Height);
				skipButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			}
			else if (_skipAlignment == SkipButtonAlignment.TopCenter)
			{
				skipButton.Frame = new RectangleF (width/2 - skipButton.Frame.Width/2, 20, skipButton.Frame.Width, skipButton.Frame.Height);
				skipButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
			}
			else if (_skipAlignment == SkipButtonAlignment.TopRight)
			{
				skipButton.Frame = new RectangleF (width-skipButton.Frame.Width - 20, 20, skipButton.Frame.Width, skipButton.Frame.Height);
				skipButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
			}

			if (_skipButtonTitleColor != null)
				skipButton.SetTitleColor(_skipButtonTitleColor, UIControlState.Normal);
			skipButton.Hidden = _showSkipButtonOnLastPage;
			skipButton.SetTitle (_skipButtonTitle, UIControlState.Normal);
			#endregion
		}

		private void LoadControllers ()
		{
			pageControl.Pages = _controllers.Count;

			if (_clearBackground)
				this.View.BackgroundColor = UIColor.Clear;

			// loop through each one
			for (int i = 0; i < _controllers.Count; i++)
			{
				// set their location and size, each one is moved to the 
				// right by the width of the previous
				RectangleF viewFrame = new RectangleF (
					scrollView.Frame.Width * i,
					scrollView.Frame.Y,
					scrollView.Frame.Width,
					scrollView.Frame.Height);
				_controllers[i].View.Frame = viewFrame;

				// add the view to the scrollview
				scrollView.AddSubview (_controllers[i].View);
			}

			// set our scroll view content size (width = number of pages * scroll view width)
			scrollView.ContentSize = new SizeF (scrollView.Frame.Width * _controllers.Count, scrollView.Frame.Height);
		}

		void HandleScrolled (object sender, EventArgs e)
		{
			int pageNumber = (int)(Math.Floor ((scrollView.ContentOffset.X - scrollView.Frame.Width / 2) / scrollView.Frame.Width) + 1);
			bool lastPage = (pageControl.CurrentPage == (_controllers.Count - 1));

			if (pageNumber >= 0 && pageNumber < _controllers.Count) {
				pageControl.CurrentPage = pageNumber;
				skipButton.Hidden = (_showSkipButtonOnLastPage ? !lastPage : false);
			}
		}

		void HandlePageControlValueChanged (object sender, EventArgs e)
		{
			scrollView.ScrollRectToVisible (_controllers [(sender as UIPageControl).CurrentPage].View.Frame, true);	
		}
	}
}

