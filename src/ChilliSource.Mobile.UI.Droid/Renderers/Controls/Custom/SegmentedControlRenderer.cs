#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Android.Views;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics.Drawables;
using System.Linq;
using Xamarin.Forms;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(SegmentedControlView), typeof(SegmentedControlRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class SegmentedControlRenderer : ViewRenderer<SegmentedControlView, RadioGroup>
	{
		public SegmentedControlRenderer()
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControlView> e)
		{
			base.OnElementChanged(e);
			var layoutInflater = LayoutInflater.From(Context);

			var radioGroup = new RadioGroup(Context);
			radioGroup.LayoutParameters = new LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
			radioGroup.Orientation = Orientation.Horizontal;

			var segmentedControlView = e.NewElement;

			for (var i = 0; i < segmentedControlView.Children.Count; i++)
			{
				var child = segmentedControlView.Children[i];
				var radioButton = (SegmentedControlButton)layoutInflater.Inflate(Resource.Layout.SegmentedControl, null);
				radioButton.Text = child.Text;
				radioButton.SetTextColor(i == segmentedControlView.SelectedItem ? segmentedControlView.TintColor.ToAndroid() : segmentedControlView.SelectedColor.ToAndroid());


				var drawable = new StateListDrawable();

				ShapeDrawable unselectedDrawable = new ShapeDrawable();
				unselectedDrawable.Shape = new RectShape();
				unselectedDrawable.Paint.Color = segmentedControlView.SelectedColor.ToAndroid();
				unselectedDrawable.Paint.StrokeWidth = 5;
				unselectedDrawable.Paint.SetStyle(Paint.Style.Stroke);
				ColorDrawable selectedDrawable = new ColorDrawable(segmentedControlView.SelectedColor.ToAndroid());

				drawable.AddState(new int[] { Android.Resource.Attribute.StateChecked }, selectedDrawable);
				drawable.AddState(new int[] { Android.Resource.Attribute.StatePressed }, selectedDrawable);
				drawable.AddState(StateSet.WildCard.ToArray(), unselectedDrawable);
				radioButton.SetBackground(drawable);

				radioGroup.AddView(radioButton);
			}

			(radioGroup.GetChildAt(e.NewElement.SelectedItem) as RadioButton).Checked = true;
			radioGroup.CheckedChange += HandleRadioGroupCheckedChange;

			SetNativeControl(radioGroup);
		}

		private void HandleRadioGroupCheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
		{
			RadioGroup radioGroup = (RadioGroup)sender;
			if (radioGroup.CheckedRadioButtonId != -1)
			{
				var id = radioGroup.CheckedRadioButtonId;
				var radioButton = radioGroup.FindViewById(id);
				var index = radioGroup.IndexOfChild(radioButton);

				Element.SelectedItem = index;

				for (var i = 0; i < Element.Children.Count; i++)
				{
					(radioGroup.GetChildAt(i) as RadioButton).SetTextColor(i == Element.SelectedItem ? Element.TintColor.ToAndroid() : Element.SelectedColor.ToAndroid());
				}
			}
		}
	}

}

