#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Windows.Input;
using ChilliSource.Mobile.UI;

namespace Examples
{
	public partial class ExtendedButtonExamplePage : BaseContentPage
	{
		bool _largeImageVisible = false;

		public ExtendedButtonExamplePage()
		{
			BindingContext = this;
			AligmentButtonCommand = new Command(() =>
				{
					switch (ContentAligment)
					{
						case ButtonHorizontalContentAlignment.Center:
							ContentAligment = ButtonHorizontalContentAlignment.Right;
							break;

						case ButtonHorizontalContentAlignment.Right:
							ContentAligment = ButtonHorizontalContentAlignment.Left;
							break;

						case ButtonHorizontalContentAlignment.Left:
							ContentAligment = ButtonHorizontalContentAlignment.Center;
							break;
					}

					OnPropertyChanged(nameof(ContentAligment));
				});

			ImageButtonCommand = new Command(() =>
			{
				if (_largeImageVisible)
				{
					ImageButtonSource = "Images/Misc/dogesmall";
					_largeImageVisible = false;
				}
				else
				{
					ImageButtonSource = "Images/Misc/doge";
					_largeImageVisible = true;
				}

				OnPropertyChanged(nameof(ImageButtonSource));
			});

			LongPressCommand = new Command(() =>
			{
				switch (FillDirection)
				{
					case LongPressDirection.LeftToRight:
						FillDirection = LongPressDirection.TopToBotton;
						LongPressText = "Long Press - Top To Bottom";
						break;

					case LongPressDirection.TopToBotton:
						FillDirection = LongPressDirection.RightToLeft;
						LongPressText = "Long Press - Right To Left";

						break;

					case LongPressDirection.RightToLeft:
						FillDirection = LongPressDirection.BottomToTop;
						LongPressText = "Long Press - Bottom To Top";

						break;

					case LongPressDirection.BottomToTop:
						FillDirection = LongPressDirection.LeftToRight;
						LongPressText = "Long Press - Left To Right";

						break;
				}

				OnPropertyChanged(nameof(FillDirection));
				OnPropertyChanged(nameof(LongPressText));
			});

			InitializeComponent();
		}

		public ButtonHorizontalContentAlignment ContentAligment { get; set; } = ButtonHorizontalContentAlignment.Center;

		public ImageSource ImageButtonSource { get; set; } = "Images/Misc/dogesmall";

		public ICommand AligmentButtonCommand { get; private set; }

		public ICommand ImageButtonCommand { get; private set; }

		public ICommand LongPressCommand { get; private set; }

		public LongPressDirection FillDirection { get; set; } = LongPressDirection.LeftToRight;

		public string LongPressText { get; set; } = "Long Press - Left To Right";
	}
}
