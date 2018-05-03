#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Android.Graphics;
using System.IO;
using Xamarin.Forms;
using Android.Content.Res;
using System.Linq;
using Android.Content;

namespace ChilliSource.Mobile.UI
{
	public static class TypefaceHelper
	{
	    private static Context Context => Android.App.Application.Context;

		public static Typeface GetTypeface(string family)
		{
			const string fontFolder = "fonts";			

			var assetsFolder = Context.Assets;						
			var typeface = GetTypeFaceFromAsset(assetsFolder, assetsFolder.List(fontFolder), fontFolder, family);
						
			return typeface ?? Typeface.Default;
		}

		private static Typeface GetTypeFaceFromAsset(AssetManager assetMgr, string[] assets, string folder, string family)
		{			
			var path = !String.IsNullOrEmpty(folder) ? $"{folder}/" : folder;

            if (family.StartsWith("."))
            {
                family = family.Substring(1, family.Length - 1);
            }

            var font = assets.FirstOrDefault(a => System.IO.Path.GetFileNameWithoutExtension(a).Equals(family));
            if (font != null)
            {
                return Typeface.CreateFromAsset(assetMgr, $"{path}{font}");
            }
            else
            {
                return null;
            }			
		}
	}
}
