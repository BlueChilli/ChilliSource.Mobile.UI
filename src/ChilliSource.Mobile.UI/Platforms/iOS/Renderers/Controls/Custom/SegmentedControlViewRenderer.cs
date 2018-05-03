#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(SegmentedControlView), typeof(SegmentedControlViewRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class SegmentedControlViewRenderer : ViewRenderer<SegmentedControlView, UISegmentedControl>
	{

		protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControlView> e)
		{
			base.OnElementChanged(e);

			var nativeControl = new UISegmentedControl();

			if (e.NewElement == null)
			{
				return;
			}

			var segmentedControlView = e.NewElement;

			for (var i = 0; i < segmentedControlView.Children.Count; i++)
			{
				nativeControl.InsertSegment(segmentedControlView.Children[i].Text, i, false);
			}

			if (segmentedControlView.TintColor != Color.Default)
			{
				nativeControl.TintColor = segmentedControlView.TintColor.ToUIColor();
			}

			if (segmentedControlView.CustomFont != null)
			{
				var segmentedControlAttributes = new UITextAttributes()
				{
					TextColor = segmentedControlView.CustomFont.Color.ToUIColor(),
				};

				var font = segmentedControlView.CustomFont;
				segmentedControlAttributes.Font = font.ToUIFont();

				nativeControl.SetTitleTextAttributes(segmentedControlAttributes, UIControlState.Normal);
			}

			Color selectedColor = Color.Default;

			if (segmentedControlView.CustomSelectedFont != null)
			{
				selectedColor = segmentedControlView.CustomSelectedFont.Color;
			}
			else if (segmentedControlView.SelectedColor != Color.Default)
			{
				selectedColor = segmentedControlView.SelectedColor;
			}

			if (selectedColor != Color.Default)
			{
				var segmentedControlAttributes = new UITextAttributes()
				{
					TextColor = selectedColor.ToUIColor(),
				};

				var font = segmentedControlView.CustomSelectedFont;

				if (!string.IsNullOrEmpty(font.Family) && font.Size > 0)
				{
					segmentedControlAttributes.Font = UIFont.FromName(font.Family, font.Size);
				}

				nativeControl.SetTitleTextAttributes(segmentedControlAttributes, UIControlState.Highlighted);
				nativeControl.SetTitleTextAttributes(segmentedControlAttributes, UIControlState.Selected);
			}

			nativeControl.SelectedSegment = e.NewElement.SelectedItem;

			SetNativeControl(nativeControl);

			Control.ValueChanged += HandleControlValueChanged;
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == "SelectedItem")
			{
				Control.SelectedSegment = Element.SelectedItem;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Control.ValueChanged -= HandleControlValueChanged;
			}

			base.Dispose(disposing);
		}

		private void HandleControlValueChanged(object sender, EventArgs e)
		{
			Element.SelectedItem = (int)Control.SelectedSegment;
		}
	}
}

