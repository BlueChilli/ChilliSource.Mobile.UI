#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using UIKit;

namespace ChilliSource.Mobile.UI.Core
{
	public static partial class NavigationService
	{
		/// <summary>
		/// Returns the top-most presented view controller in the navigation stack
		/// </summary>
		/// <returns>The active view controller.</returns>
		public static UIViewController GetActiveViewController()
		{
			var viewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
			while (viewController.PresentedViewController != null)
			{
				viewController = viewController.PresentedViewController;
			}
			return viewController;
		}

	}
}
