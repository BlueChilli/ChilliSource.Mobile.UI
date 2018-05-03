#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace ChilliSource.Mobile.UI
{
	public static class ImageSourceExtensions
	{
		public static Task<UIImage> ToUIImage(this ImageSource imageSource)
		{
			IImageSourceHandler handler = null;

			if (imageSource is FileImageSource)
			{
				handler = new FileImageSourceHandler();
			}
			else if (imageSource is StreamImageSource)
			{
				handler = new StreamImagesourceHandler();
			}
			else if (imageSource is UriImageSource)
			{
				handler = new ImageLoaderSourceHandler();
			}
			else
			{
				throw new NotImplementedException();
			}

			return handler.LoadImageAsync(imageSource);
		}
	}
}
