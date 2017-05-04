#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ExtendedSearchBar), typeof(ExtendedSearchBarRenderer))]


namespace ChilliSource.Mobile.UI
{
	public class ExtendedSearchBarRenderer : SearchBarRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> args)
		{
			base.OnElementChanged(args);

			if (Element == null)
			{
				return;
			}

			var styledSearchBar = (ExtendedSearchBar)Element;

			switch (styledSearchBar.KeyboardTheme)
			{
				case KeyboardTheme.Light:
					Control.KeyboardAppearance = UIKeyboardAppearance.Light;
					break;

				case KeyboardTheme.Dark:
					Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
					break;
			}

			Control.BarTintColor = styledSearchBar.BarTint.ToUIColor();
			Control.TintColor = styledSearchBar.CancelButtonColor.ToUIColor();

			if (styledSearchBar.PersistCancelButton)
			{
				styledSearchBar.TextChanged += ((sender, e) =>
				{
					if (Control != null)
					{
						Control.ShowsCancelButton = true;
					}
				});

				Control.ShowsCancelButton = true;
			}

			Control.BackgroundImage = new UIImage();
			Control.BarStyle = (UIBarStyle)Enum.Parse(typeof(UIBarStyle), styledSearchBar.BarStyle);
			Control.SearchBarStyle = (UISearchBarStyle)Enum.Parse(typeof(UISearchBarStyle), styledSearchBar.SearchStyle);
			Control.CancelButtonClicked += (object sender, EventArgs e) => styledSearchBar.OnCancelButtonClicked();
		}
	}
}

