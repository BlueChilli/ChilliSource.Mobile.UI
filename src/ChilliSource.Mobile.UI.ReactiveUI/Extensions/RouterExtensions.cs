#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using ReactiveUI;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
	public static class RouterExtensions
	{
		public static void NavigationToRoot(this RoutingState router)
		{
			var count = router.NavigationStack.Count;
			router.NavigationStack.RemoveRange(1, count - 1);
		}
	}
}
