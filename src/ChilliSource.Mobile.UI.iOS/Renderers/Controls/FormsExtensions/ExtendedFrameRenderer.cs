#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ExtendedFrame), typeof(ExtendedFrameRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ExtendedFrameRenderer : FrameRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || Element == null)
			{
				return;
			}

			Layer.CornerRadius = (Element as ExtendedFrame).CornerRadius;
		}
	}
}

