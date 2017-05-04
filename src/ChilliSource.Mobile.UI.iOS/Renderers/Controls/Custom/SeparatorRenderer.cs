#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;
using CoreGraphics;

[assembly: ExportRenderer(typeof(SeparatorView), typeof(SeparatorRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class SeparatorRenderer : ViewRenderer<SeparatorView, SeparatorNativeView>
	{
		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<SeparatorView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control == null)
				{
					BackgroundColor = Color.Transparent.ToUIColor();
					SetNativeControl(new SeparatorNativeView(Bounds));
				}
			}

			SetProperties();
		}

		/// <summary>
		/// Handles the <see cref="E:ElementPropertyChanged" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			SetProperties();
		}

		/// <summary>
		/// Sets the properties.
		/// </summary>
		private void SetProperties()
		{
			if (Control == null || Element == null)
				return;

			var separator = Control;
			separator.Thickness = Element.Thickness;
			separator.StrokeColor = Element.Color.ToUIColor();
			separator.StrokeType = Element.StrokeType;
			separator.Orientation = Element.Orientation;
			separator.SpacingBefore = Element.SpacingBefore;
			separator.SpacingAfter = Element.SpacingAfter;
			separator.Height = Element.HeightRequest;
			separator.Width = Element.WidthRequest;
		}
	}
}
