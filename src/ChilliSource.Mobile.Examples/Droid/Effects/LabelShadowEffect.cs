#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Examples;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("ChilliSource")]
[assembly: ExportEffect(typeof(LabelShadowEffect), "LabelShadowEffect")]
namespace Examples
{
	public class LabelShadowEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			try
			{
				var control = Control as Android.Widget.TextView;
				float radius = 5;
				float distanceX = 5;
				float distanceY = 5;
				Android.Graphics.Color color = Color.White.ToAndroid();
				control.SetShadowLayer(radius, distanceX, distanceY, color);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}

		protected override void OnDetached()
		{
		}
	}
}
