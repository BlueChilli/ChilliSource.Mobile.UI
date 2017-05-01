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
namespace ChilliSource.Mobile.UI
{
	public static class TypefaceHelper
	{
		public static Typeface GetTypeface(string family)
		{
			const string fontFolder = "fonts";
			var context = Forms.Context;

			var aMan = context.Assets;
			var assets = aMan.List(String.Empty);

			var typeface = GetTypeFaceFromAsset(aMan, assets, String.Empty, family);

			if (typeface == null && assets.Contains(fontFolder))
			{
				typeface = GetTypeFaceFromAsset(aMan, aMan.List(fontFolder), fontFolder, family);
			}

			if (typeface == null)
			{
				throw new InvalidDataException();
			}

			return typeface;
		}

		private static Typeface GetTypeFaceFromAsset(AssetManager assetMgr, string[] assets, string folder, string family)
		{
			Typeface typeface = null;
			var path = !String.IsNullOrEmpty(folder) ? $"{folder}/" : folder;

			foreach (var asset in assets)
			{
				if (asset.Equals(family + ".otf"))
				{
					typeface = Typeface.CreateFromAsset(assetMgr, $"{path}{family}.otf");
					return typeface;

				}
				else if (asset.Equals(family + ".ttf"))
				{
					typeface = Typeface.CreateFromAsset(assetMgr, $"{path}{family}.ttf");
					return typeface;
				}
			}

			return typeface;

		}
	}
}
