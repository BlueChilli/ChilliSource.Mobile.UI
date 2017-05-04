#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public enum BannerPosition
	{
		Top,
		Bottom
	}

	public partial class BannerView : PopupPage
	{
		public static async Task DisplayToast(string title, ExtendedFont titlefont, ImageSource image, Color backgroundColor, INavigation navigation, BannerPosition position = BannerPosition.Top)
		{
			var view = new BannerView();
			view.Text = title;
			view.TitleFont = titlefont;
			view.Image = image;
			view.Color = backgroundColor;
			view.Position = position;

			await navigation.PushPopupAsync(view);
			await Task.Delay(2000);
			await PopupNavigation.PopAsync();
		}

		public BannerView()
		{
			BindingContext = this;
			Animation = new BannerViewAnimation();
			HasSystemPadding = false;
			CloseWhenBackgroundIsClicked = false;

			//as of version 1.0.4 the property below does not seem to exist anymore
			//IsBackgroundAnimating = true;

			BackgroundColor = Color.Transparent;

			InitializeComponent();
		}

		private ImageSource ImageProperty { get; set; }

		public ImageSource Image
		{
			get
			{
				return ImageProperty;
			}
			set
			{
				ImageProperty = value;
				OnPropertyChanged("Image");
				OnPropertyChanged("ImageVisible");
			}
		}

		public bool ImageVisible { get { return Image != null; } }

		private Color ColorProperty { get; set; }

		public Color Color
		{
			get
			{
				return ColorProperty;
			}
			set
			{
				ColorProperty = value;
				OnPropertyChanged("Color");
			}
		}

		private string TextProperty { get; set; }

		public string Text
		{
			get
			{
				return TextProperty;
			}
			set
			{
				TextProperty = value;
				OnPropertyChanged("Text");
			}
		}

		private ExtendedFont TitleFontProperty { get; set; }

		public ExtendedFont TitleFont
		{
			get
			{
				return TitleFontProperty;
			}
			set
			{
				TitleFontProperty = value;
				OnPropertyChanged("TitleFont");
			}
		}

		private BannerPosition PositionProperty { get; set; }

		public BannerPosition Position
		{
			get
			{
				return PositionProperty;
			}
			set
			{
				PositionProperty = value;
				OnPropertyChanged("Position");
				OnPropertyChanged("MainVerticalOption");
			}
		}

		public LayoutOptions MainVerticalOption
		{
			get { return Position == BannerPosition.Top ? LayoutOptions.Start : LayoutOptions.End; }
		}
	}
}
