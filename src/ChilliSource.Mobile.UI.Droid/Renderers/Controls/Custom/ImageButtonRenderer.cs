#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Views;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ImageButtonView), typeof(ImageButtonRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ImageButtonRenderer : ViewRenderer<ImageButtonView, Android.Views.View>, Android.Views.View.IOnTouchListener
	{
		public ImageButtonRenderer() : base()
		{
			SetOnTouchListener(this);
		}

		public bool OnTouch(Android.Views.View v, MotionEvent e)
		{
			switch (e.Action)
			{
				case MotionEventActions.Down:
					((ImageButtonView)Element).OnPressed(true);
					break;
				case MotionEventActions.Cancel:
					((ImageButtonView)Element).OnPressed(false);
					break;
				case MotionEventActions.Up:
					((ImageButtonView)Element).OnPressed(false);
					break;
			}
			return true;
		}
	}
}

