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
	public class ImageBlurEffect : RoutingEffect
	{
		public ImageBlurEffect() : this("ChilliSource.Mobile.UI.ImageBlurEffect")
		{

		}

		public ImageBlurEffect(string effectID, float radius = 0.0f, bool hideImageUntilBlurred = false, float transitionDuration = 0.0f) : base(effectID)
		{
			Radius = radius;
			HideImageUntilBlurred = hideImageUntilBlurred;
			BlurTransitionDuration = transitionDuration;
		}

		public float Radius { get; set; }

		public bool HideImageUntilBlurred { get; set; }

		public float BlurTransitionDuration { get; set; } = 0.0f;
	}
}

