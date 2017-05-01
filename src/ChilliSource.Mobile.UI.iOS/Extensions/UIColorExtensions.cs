#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using CoreGraphics;
using UIKit;

namespace ChilliSource.Mobile.UI
{
	public static class UIColorExtensions
	{
		public static UIImage GetImageFromColor(UIColor color)
		{
			var rect = new CGRect(0, 0, 1, 1);
			UIGraphics.BeginImageContext(rect.Size);
			var context = UIGraphics.GetCurrentContext();
			context.SetFillColor(color.CGColor);
			UIGraphics.RectFill(rect);
			var image = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

			return image;
		}
	}
}
