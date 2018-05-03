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
using Android.Content;

namespace ChilliSource.Mobile.UI
{
    public static partial class ViewExtensions
    {
        public static Android.Views.View ConvertToNative(this Xamarin.Forms.View view, Context context, Rectangle size)
        {
            var vRenderer = Platform.CreateRendererWithContext(view, context);
            var viewGroup = vRenderer.View;
            vRenderer.Tracker.UpdateLayout();
            var layoutParams = new ViewGroup.LayoutParams((int)size.Width, (int)size.Height);
            viewGroup.LayoutParameters = layoutParams;
            view.Layout(size);
            viewGroup.Layout(0, 0, (int)view.WidthRequest, (int)view.HeightRequest);
            return viewGroup;
        }
    }
}
