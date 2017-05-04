#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class TransitionOptions
	{
		public float TransitionDuration { get; set; } = 1.0f;

		public SwipeDirection PushSwipeTransitionDirection { get; set; }

		public SwipeDirection PopSwipeTransitionDirection { get; set; }

		public bool ApplyPopSwipeTransitionToOrigin { get; set; }

		public bool FromHasNavigationBar { get; set; } = true;

		public bool ToHasNavigationBar { get; set; } = true;
	}
}

