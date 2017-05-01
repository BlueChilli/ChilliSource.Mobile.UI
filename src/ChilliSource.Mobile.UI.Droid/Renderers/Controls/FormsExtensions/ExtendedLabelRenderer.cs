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
using System.ComponentModel;
using Android.Text;
using System.Linq;
using Android.Text.Style;
using Android.Text.Method;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ExtendedLabelRenderer : LabelRenderer
	{

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			if (Element == null)
			{
				return;
			}

			SetStyle();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = Element as ExtendedLabel;

			if (view != null &&
				e.PropertyName == Label.TextProperty.PropertyName ||
				e.PropertyName == Label.FormattedTextProperty.PropertyName ||
				e.PropertyName == ExtendedLabel.CustomFontProperty.PropertyName)
			{
				SetStyle();
			}
		}

		private void SetStyle()
		{
			var styledLabel = (ExtendedLabel)this.Element;

			if (styledLabel.Children != null && styledLabel.Children.Count > 0)
			{
				SpannableStringBuilder builder = new SpannableStringBuilder();
				styledLabel.Children.ToList().ForEach(child =>
				{
					var spannableText = new SpannableString(child.Text);
					spannableText.SetSpan(new ForegroundColorSpan(child.CustomFont.Color.ToAndroid()), 0, child.Text.Length, SpanTypes.Composing);
					spannableText.SetSpan(new CustomTypefaceSpan(child.CustomFont), 0, child.Text.Length, SpanTypes.Composing);
					if (child.Command != null)
					{
						var clickableSpan = new AwesomeClickableSpan();
						clickableSpan.Click += (Android.Views.View obj) =>
						{
							child.Command?.Execute(child.CommandParameter);
						};
						spannableText.SetSpan(clickableSpan, 0, child.Text.Length, SpanTypes.Composing);
					}
					builder.Append(spannableText);
				});

				Control.TextFormatted = builder;
				Control.MovementMethod = new LinkMovementMethod();
				return;
			}

			if (styledLabel.Text != null && styledLabel.CustomFont != null)
			{
				styledLabel.CustomFont.ApplyTo(Control);
			}
		}

		//This class does apsolutely nothing special. ClickableSpan is abstract so this had to be done.
		private class AwesomeClickableSpan : ClickableSpan
		{
			public Action<Android.Views.View> Click;

			public override void OnClick(Android.Views.View widget)
			{
				if (Click != null)
					Click(widget);
			}
		}
	}


}

