#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using ChilliSource.Mobile.UI.Core;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public static class KeyboardHelper
	{
		private static IKeyboardService keyboardHelper = null;

		public static void Init()
		{
			if (keyboardHelper == null)
			{
				keyboardHelper = DependencyService.Get<IKeyboardService>();
			}
		}

		public static event EventHandler<KeyboardVisibilityEventArgs> KeyboardChanged
		{
			add
			{
				Init();
				keyboardHelper.KeyboardVisibilityChanged += value;
			}
			remove
			{
				Init();
				keyboardHelper.KeyboardVisibilityChanged -= value;
			}
		}
	}
}

