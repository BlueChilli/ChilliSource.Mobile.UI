#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using AndroidHUD;
using ChilliSource.Mobile.UI;

[assembly: Dependency(typeof(HudService))]

namespace ChilliSource.Mobile.UI
{
	public class HudService : IHudService
	{
		public void Show()
		{
			AndHUD.Shared.Show(Forms.Context, null, -1, MaskType.Black);
		}

		public void Show(string message)
		{
			AndHUD.Shared.Show(Forms.Context, message, -1, MaskType.Black);
		}

		public void Show(string message, ExtendedFont font)
		{
			//TODO: add support for custom font
			AndHUD.Shared.Show(Forms.Context, message, -1, MaskType.Black);
		}

		public void Dismiss()
		{
			AndHUD.Shared.Dismiss(Forms.Context);
		}
	}
}

