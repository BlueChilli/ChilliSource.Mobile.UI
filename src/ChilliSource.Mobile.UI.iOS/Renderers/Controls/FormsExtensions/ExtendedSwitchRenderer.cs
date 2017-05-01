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

[assembly: ExportRenderer(typeof(ExtendedSwitch), typeof(ExtendedSwitchRenderer))]

namespace ChilliSource.Mobile.UI
{
	class ExtendedSwitchRenderer : SwitchRenderer
	{

		protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
		{
			base.OnElementChanged(e);

			if (Element == null)
			{
				return;
			}

			SetStyle();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = Element as ExtendedSwitch;

			if (view != null &&
				e.PropertyName == ExtendedSwitch.TintColorProperty.PropertyName)
			{
				SetStyle();
			}
		}

		private void SetStyle()
		{
			Control.OnTintColor = (Element as ExtendedSwitch).TintColor.ToUIColor();
		}
	}
}
