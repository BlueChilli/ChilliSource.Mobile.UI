#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Linq;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(BorderPlatformEffect), nameof(BorderEffect))]
namespace ChilliSource.Mobile.UI
{
    public class BorderPlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var effect = (BorderEffect)Element.Effects.FirstOrDefault(m => m is BorderEffect);
            var view = Control;

            if (effect != null && view != null)
            {
                view.Layer.CornerRadius = effect.BorderRadius;
                view.Layer.BorderWidth = effect.BorderWidth;
                view.Layer.BorderColor = effect.BorderColor.ToCGColor();
                view.Layer.MasksToBounds = true;
            }
        }

        protected override void OnDetached()
        {

        }
    }
}
