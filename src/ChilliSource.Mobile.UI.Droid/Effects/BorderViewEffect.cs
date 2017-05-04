#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Linq;
using Android.Graphics.Drawables;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(BorderViewEffect), "BorderEffect")]
namespace ChilliSource.Mobile.UI
{

	public class BorderViewEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			try
			{
				var effect = (BorderEffect)this.Element.Effects.FirstOrDefault(m => m is BorderEffect);

				if (effect != null)
				{
					var drawable = this.Control.Background as GradientDrawable;
					if (drawable != null)
					{
						drawable.SetCornerRadius(effect.Radius);
						drawable.SetStroke((int)effect.BorderWidth, effect.BorderColor.ToAndroid());
					}
				}


			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set Property on attached control {0}", ex.ToString());
			}
		}

		protected override void OnDetached()
		{

		}
	}
}
