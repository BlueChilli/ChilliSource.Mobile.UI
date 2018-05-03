#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Android.Text.Style;
using Android.Graphics;


namespace ChilliSource.Mobile.UI
{
	public class CustomTypefaceSpan : TypefaceSpan
	{
		private Typeface _typeface;

		public CustomTypefaceSpan(ExtendedFont font) : base(font.Family)
		{
			_typeface = TypefaceHelper.GetTypeface(font.Family);
		}

		public override void UpdateDrawState(Android.Text.TextPaint tp)
		{
			ApplyCustomTypeface(tp, _typeface);
		}

		public override void UpdateMeasureState(Android.Text.TextPaint p)
		{
			ApplyCustomTypeface(p, _typeface);
		}

		private void ApplyCustomTypeface(Paint paint, Typeface typeface)
		{
			paint.SetTypeface(typeface);
		}
	}
}

