#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using UIKit;
using Xamarin.Forms;
using ChilliSource.Mobile.UI;
using BigTed;

[assembly: Dependency(typeof(HudService))]

namespace ChilliSource.Mobile.UI
{
	public class HudService : IHudService
	{
		ProgressHUD _hud;

		public void Show()
		{
			_hud = new ProgressHUD();
			_hud.Show(null, -1, ProgressHUD.MaskType.Black);
		}

		public void Show(string message)
		{
			_hud = new ProgressHUD();
			_hud.Show(message, -1, ProgressHUD.MaskType.Black);
		}

		public void Show(string message, ExtendedFont font)
		{
			_hud = new ProgressHUD();
			_hud.HudFont = font.ToUIFont();
			_hud.Show(message, -1, ProgressHUD.MaskType.Black);
		}

		public void Dismiss()
		{
			if (_hud != null)
			{
				_hud.Dismiss();
				_hud.Dispose();
				_hud = null;
			}
		}
	}
}

