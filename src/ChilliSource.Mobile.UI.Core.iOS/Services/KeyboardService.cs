#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Foundation;
using UIKit;
using ChilliSource.Mobile.UI.Core;

[assembly: Xamarin.Forms.Dependency(typeof(KeyboardService))]

namespace ChilliSource.Mobile.UI.Core
{
	public class KeyboardService : IKeyboardService
	{
		public KeyboardService()
		{
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
		}

		public event EventHandler<KeyboardVisibilityEventArgs> KeyboardVisibilityChanged;

		private void OnKeyboardNotification(NSNotification notification)
		{
			NSNumber duration = (NSNumber)notification.UserInfo[UIKeyboard.AnimationDurationUserInfoKey];

			int animationDuration = (int)(duration.FloatValue * 1000);

			var visible = notification.Name == UIKeyboard.WillShowNotification;
			var keyboardFrame = visible
				? UIKeyboard.FrameEndFromNotification(notification)
				: UIKeyboard.FrameBeginFromNotification(notification);

			if (KeyboardVisibilityChanged != null)
			{
				KeyboardVisibilityChanged(this, new KeyboardVisibilityEventArgs(visible, (float)keyboardFrame.Height, animationDuration));
			}
		}
	}
}

