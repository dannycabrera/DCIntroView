using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace DCIntroView
{
	public partial class TemplateView : UIViewController
	{
		String _title = "";
		String _description = "";
		UIImage _img;
		UIColor _viewBackgroundColor;
		UIColor _titleTextColor;
		UIColor _descriptionTextColor;
		UIFont _titleFont;
		UIFont _descriptionFont;
		float _titleY;
		float _descriptionY;
		float _imgY;
		float _descriptionHeight;
		UITextAlignment _titleTextAlignment = UITextAlignment.Center;
		UITextAlignment _descriptionTextAlignment = UITextAlignment.Center;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public TemplateView ()
			: base (UserInterfaceIdiomIsPhone ? "TemplateView_iPhone" : "TemplateView_iPad", null)
		{
		}

		public TemplateView (String title, String description)
			: base (UserInterfaceIdiomIsPhone ? "TemplateView_iPhone" : "TemplateView_iPad", null)
		{
			_title = title;
			_description = description;
		}

		#region Public properties
		public UITextAlignment TitleTextAlignment
		{
			get { return _titleTextAlignment; }
			set { _titleTextAlignment = value; }
		}

		public UITextAlignment DescriptionTextAlignment
		{
			get { return _descriptionTextAlignment; }
			set { _descriptionTextAlignment = value; }
		}

		public float TitleY
		{
			get { return _titleY; }
			set { _titleY = value; }
		}

		public float DescriptionY
		{
			get { return _descriptionY; }
			set { _descriptionY = value; }
		}

		public float DescriptionHeight
		{
			get { return _descriptionHeight; }
			set { _descriptionHeight = value; }
		}

		public float ImageY
		{
			get { return _imgY; }
			set { _imgY = value; }
		}

		public UIFont TitleFont
		{
			get { return _titleFont; }
			set { _titleFont = value; }
		}

		public UIFont DescriptionFont
		{
			get { return _descriptionFont; }
			set { _descriptionFont = value; }
		}

		public UIImage Image
		{
			get { return _img; }
			set { _img = value; }
		}

		public UIColor TitleTextColor
		{
			get { return _titleTextColor; }
			set { _titleTextColor = value; }
		}

		public UIColor DescriptionTextColor
		{
			get { return _descriptionTextColor; }
			set { _descriptionTextColor = value; }
		}

		public UIColor ViewBackgroundColor
		{
			get { return _viewBackgroundColor; }
			set { _viewBackgroundColor = value; }
		}

		public void SetTitle(string value)
		{
			_title = value;
		}

		public void SetDescription(string value)
		{
			_description = value;
		}
		#endregion

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// View
			if (_viewBackgroundColor != null) {
				this.View.BackgroundColor = _viewBackgroundColor;
				descTextView.BackgroundColor = UIColor.Clear;
			}

			// Title
			if (_title == null || _title == "") {
				this.titleLabel.Hidden = true;
			} else {
				this.titleLabel.Hidden = false;
				if (_titleFont != null)
					this.titleLabel.Font = _titleFont;
				this.titleLabel.Text = _title;
				if (_titleTextColor != null)
					this.titleLabel.TextColor = _titleTextColor;
				if (_titleY != 0) {
					var frame = this.titleLabel.Frame;
					frame.Y = _titleY;
					this.titleLabel.Frame = frame;
				}
				if (_titleTextAlignment != null)
					this.titleLabel.TextAlignment = _titleTextAlignment;
			}

			// Description
			this.descTextView.Editable = false;
			this.descTextView.DataDetectorTypes = UIDataDetectorType.All;
			if (_description == null || _description == "") {
				this.descTextView.Hidden = true;
			} else {
				this.descTextView.Hidden = false;
				this.descTextView.Text = _description;
				if (_descriptionTextColor != null)
					this.descTextView.TextColor = _descriptionTextColor;
				if (_descriptionFont != null)
					this.descTextView.Font = _descriptionFont;

				if (_descriptionY != 0) {
					var frame = this.descTextView.Frame;
					frame.Y = _descriptionY;
					this.descTextView.Frame = frame;
				}
				if (_descriptionHeight != 0) {
					var frame = this.descTextView.Frame;
					frame.Height = _descriptionHeight;
					this.descTextView.Frame = frame;
				}
				if (_descriptionTextAlignment != null)
					this.descTextView.TextAlignment = _descriptionTextAlignment;
				else
					this.descTextView.TextAlignment = UITextAlignment.Center;
			}

			// Image
			if (_img == null) {
				this.imageView.Hidden = true;
			} else {
				this.imageView.Hidden = false;
				imageView.Image = _img;
				imageView.ContentMode = UIViewContentMode.ScaleAspectFit;

				if (_imgY != 0) {
					var frame = this.imageView.Frame;
					frame.Y = _imgY;
					this.imageView.Frame = frame;
				}
			}
		}
	}
}

