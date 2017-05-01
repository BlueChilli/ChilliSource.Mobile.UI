#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;

namespace ChilliSource.Mobile.UI.Core
{
	public class KeyboardVisibilityEventArgs : EventArgs
	{

		public KeyboardVisibilityEventArgs(bool isVisible, float height, int animationDuration)
		{
			Height = height;
			AnimationDuration = animationDuration;
			IsVisible = isVisible;
		}

		public float Height
		{
			get;
			private set;
		}

		public int AnimationDuration
		{
			get;
			private set;
		}

		public bool IsVisible
		{
			get;
			private set;
		}
	}
}

