#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ExtendedButton), typeof(ExtendedButtonRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ExtendedButtonRenderer : ButtonRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			if (Element == null)
			{
				return;
			}

			SetStyle();
		}

		private void SetStyle()
		{
			var styledButton = (ExtendedButton)this.Element;

			switch (styledButton.HorizontalContentAlignment)
			{
				case ButtonHorizontalContentAlignment.Left:
					this.Control.Gravity = GravityFlags.Left;
					break;

				case ButtonHorizontalContentAlignment.Right:
					this.Control.Gravity = GravityFlags.Right;
					break;

				case ButtonHorizontalContentAlignment.Center:
					this.Control.Gravity = GravityFlags.Center;
					break;
			}
			if (!String.IsNullOrWhiteSpace(styledButton.Text) && styledButton.CustomFont != null)
			{
				styledButton.CustomFont.ApplyTo(Control);

			}
		}
	}
}

