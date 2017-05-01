#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Android.Content;
using Android.Util;
using Android.Widget;

namespace ChilliSource.Mobile.UI
{
	public class SegmentedControlButton : RadioButton
	{
		public SegmentedControlButton(Context context) : this(context, null)
		{
		}

		public SegmentedControlButton(Context context, IAttributeSet attributes) : this(context, attributes, Android.Graphics.Color.Red)
		{
		}

		public SegmentedControlButton(Context context, IAttributeSet attributes, int defStyle) : base(context, attributes, defStyle)
		{
		}
	}
}
