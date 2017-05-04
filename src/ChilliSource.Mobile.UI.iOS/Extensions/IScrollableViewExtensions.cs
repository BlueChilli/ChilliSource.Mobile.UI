#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using CoreGraphics;
using UIKit;

namespace ChilliSource.Mobile.UI
{
	public static class IScrollableViewExtensions
	{
		public static void ForceUpdateTableViewContentSize(this IScrollableView view)
		{
			var t = view as UITableView;
			if (t != null)
			{
				t.ContentSize = t.SizeThatFits(new CGSize(t.Frame.Width, nfloat.MaxValue));
			}
		}

		// Animation duration used for setContentOffset:
		public const double KPbInfiniteScrollAnimationDuration = 0.35;

		// Keys for values in associated dictionary
		public const string KpbInfiniteScrollStateKey = "kPBInfiniteScrollStateKey";
	}
}
