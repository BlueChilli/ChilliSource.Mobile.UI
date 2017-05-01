#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ChilliSource.Mobile.UI
{
	public static class ViewExtensions
	{
		public static ViewGroup ConvertToNative(this Xamarin.Forms.View view, Rectangle size)
		{
			var vRenderer = Platform.CreateRenderer(view);
			var viewGroup = vRenderer.ViewGroup;
			vRenderer.Tracker.UpdateLayout();
			var layoutParams = new ViewGroup.LayoutParams((int)size.Width, (int)size.Height);
			viewGroup.LayoutParameters = layoutParams;
			view.Layout(size);
			viewGroup.Layout(0, 0, (int)view.WidthRequest, (int)view.HeightRequest);
			return viewGroup;
		}
	}
}
