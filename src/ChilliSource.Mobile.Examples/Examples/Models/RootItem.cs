#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using ChilliSource.Mobile.UI;

namespace Examples
{
	public class RootItem : IndexItem
	{
		public RootItem(string title, ImageSource image, ImageSource selectedImage, Type pageType = null) : base(title, null, pageType)
		{
			DefaultImage = image;
			SelectedImage = selectedImage;
			Image = DefaultImage;

			OnPropertyChanged(nameof(Image));
			OnPropertyChanged(nameof(Title));
		}

		ImageSource DefaultImage { get; set; }

		ImageSource SelectedImage { get; set; }

		bool SelectedProperty { get; set; }

		public bool Selected
		{
			get
			{
				return SelectedProperty;
			}

			set
			{
				SelectedProperty = value;
				HandleSelectedChanged();
			}
		}

		void HandleSelectedChanged()
		{
			if (Selected)
			{
				BackgroundColor = ThemeManager.OrangePink;
				Image = SelectedImage;
				CustomFont = ThemeManager.CellTitleSelectedFont;
			}
			else
			{
				BackgroundColor = Color.White;
				Image = DefaultImage;
				CustomFont = ThemeManager.CellTitleFont;
			}

			OnPropertyChanged(nameof(BackgroundColor));
			OnPropertyChanged(nameof(Image));
			OnPropertyChanged(nameof(CustomFont));
		}

		public Color BackgroundColor { get; set; } = Color.White;

		public ImageSource Image { get; set; }

		public ExtendedFont CustomFont { get; set; } = ThemeManager.CellTitleFont;

	}
}
