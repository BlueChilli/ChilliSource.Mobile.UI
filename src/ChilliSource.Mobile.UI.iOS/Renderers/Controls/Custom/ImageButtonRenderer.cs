#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ImageButtonView), typeof(ImageButtonRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ImageButtonRenderer : ViewRenderer<ImageButtonView, UIView>
	{
		public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);
			Element.OnPressed(true);
		}

		public override void TouchesCancelled(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);
			Element.OnPressed(false);
		}

		public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			Element.OnPressed(false);
		}
	}
}

