#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/* Based on code from BlurredImageRenderer.cs
 * Project: BlurredImageTest (https://github.com/TheRealAdamKemp/BlurredImageTest)
 * Author: Adam Kemp (https://github.com/TheRealAdamKemp)
 */

using System;
using System.Linq;
using CoreImage;
using ChilliSource.Mobile.UI;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//see here for Android version: http://blog.adamkemp.com/2015/05/blurred-image-renderer-for-xamarinforms.html

//Note: this effect only works correctly if the UIImageView already has an Image
using System.Threading.Tasks;
using System.Drawing;

[assembly: ExportEffect(typeof(ImageBlurPlatformEffect), "ImageBlurEffect")]
namespace ChilliSource.Mobile.UI
{
	public class ImageBlurPlatformEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			var imageView = Control as UIImageView;
			var effect = (ImageBlurEffect)Element.Effects.FirstOrDefault(e => e is ImageBlurEffect);

			if (effect.HideImageUntilBlurred) imageView.Hidden = true;

			BlurImage(imageView, imageView.Image, effect.Radius, effect.BlurTransitionDuration);
		}

		void BlurImage(UIImageView imageView, UIImage image, float radius, float transitionDuration)
		{
			Task.Run(() =>
			{
				using (var context = CIContext.Create())
				using (var inputImage = CIImage.FromCGImage(image.CGImage))
				using (var filter = new CIGaussianBlur() { Image = inputImage, Radius = radius })
				using (var resultImage = context.CreateCGImage(filter.OutputImage, inputImage.Extent))
				{
					var blurimage = new UIImage(resultImage);

					Device.BeginInvokeOnMainThread(() =>
					{
						imageView.Hidden = false;

						if (transitionDuration > 0.0f)
						{
							UIView.Transition(imageView, transitionDuration, UIViewAnimationOptions.TransitionCrossDissolve, animation: () =>
							{
								imageView.Image = blurimage;

							}, completion: null);
						}
						else
						{
							imageView.Image = blurimage;
						}
					});
				}
			});
		}

		protected override void OnDetached()
		{
		}
	}
}

