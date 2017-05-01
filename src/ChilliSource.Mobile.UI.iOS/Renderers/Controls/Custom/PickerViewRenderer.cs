#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using System.ComponentModel;
using UIKit;
using System.Collections.Generic;
using Foundation;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ChilliSource.Mobile.UI.PickerView), typeof(PickerViewRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class PickerViewRenderer : ViewRenderer<PickerView, UIPickerView>
	{
		private PickerView _styledPicker;

		private List<double> _componentWidths = new List<double>();

		protected override void OnElementChanged(ElementChangedEventArgs<PickerView> e)
		{
			base.OnElementChanged(e);

			if (Element == null)
			{
				return;
			}

			_styledPicker = Element as PickerView;

			CalculateComponentWidths();

			if (Control == null)
			{
				var picker = new UIPickerView();
				picker.DataSource = new PickerDataSource(_styledPicker);
				picker.Delegate = new PickerDelegate(_styledPicker, picker, _componentWidths);
				SetNativeControl(picker);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (_styledPicker != null &&
				e.PropertyName == PickerView.ItemsSourceProperty.PropertyName)
			{
				CalculateComponentWidths();
				Control.ReloadAllComponents();
			}
		}

		private class PickerDataSource : UIPickerViewDataSource
		{
			private PickerView _styledPicker;

			public PickerDataSource(PickerView styledPicker)
			{
				_styledPicker = styledPicker;
			}

			public override nint GetComponentCount(UIPickerView pickerView)
			{
				return _styledPicker.ItemsSource == null ? 0 : _styledPicker.ItemsSource.Count;
			}

			public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
			{
				return _styledPicker.ItemsSource == null ? 0 : _styledPicker.ItemsSource[(int)component].Count;
			}
		}

		private class PickerDelegate : UIPickerViewDelegate
		{
			private PickerView _styledPicker;

			private List<double> _componentWidths;

			private double _rowHeight;

			private nint _currentSelected;

			public PickerDelegate(PickerView styledPicker, UIPickerView picker, List<double> componentWidths)
			{
				_styledPicker = styledPicker;
				_componentWidths = componentWidths;
				_rowHeight = styledPicker.RowHeight;

				if (_styledPicker.SelectedItems == null)
				{
					_styledPicker.SelectedItems = new List<string>();
					for (int i = 0; i < _styledPicker.ItemsSource.Count; i++)
					{
						_styledPicker.SelectedItems.Add(_styledPicker.ItemsSource[i][0]);
					}
				}
				else
				{
					for (int i = 0; i < _styledPicker.SelectedItems.Count; i++)
					{
						var index = _styledPicker.ItemsSource[i].IndexOf(_styledPicker.SelectedItems[i]);
						picker.Select(index, i, false);
					}
				}
			}

			public override Foundation.NSAttributedString GetAttributedTitle(UIPickerView pickerView, nint row, nint component)
			{
				var fontToApply = _styledPicker.CustomFont;

				if (_currentSelected == row && _styledPicker.CustomSelectedFont != null)
				{
					fontToApply = _styledPicker.CustomSelectedFont;
				}

				var attributedString = fontToApply.BuildAttributedString(_styledPicker.ItemsSource[(int)component][(int)row]);
				return attributedString;
			}

			public override void Selected(UIPickerView pickerView, nint row, nint component)
			{
				if (_styledPicker.SelectedItems.Count > 0 && _styledPicker.SelectedItems.Count >= (int)component)
				{
					_styledPicker.SelectedItems[(int)component] = (_styledPicker.ItemsSource[(int)component][(int)row]);
				}
				else
				{
					_styledPicker.SelectedItems.Add(_styledPicker.ItemsSource[(int)component][(int)row]);
				}

				_currentSelected = row;

				pickerView.ReloadAllComponents();
			}

			public override nfloat GetComponentWidth(UIPickerView pickerView, nint component)
			{
				return (nfloat)_componentWidths[(int)component];
			}

			public override nfloat GetRowHeight(UIPickerView pickerView, nint component)
			{
				return (nfloat)_rowHeight;
			}
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			if (_styledPicker.ComponentFixedTextItems == null)
			{
				return;
			}

			nfloat accumulatedWidth = 0;
			var ms = new CoreGraphics.CGSize(500, 500);
			for (int i = 0; i < _styledPicker.ComponentFixedTextItems.Count; i++)
			{
				CoreGraphics.CGSize sizeMax = new CoreGraphics.CGSize(0, 0);
				for (int j = 0; j < _styledPicker.ItemsSource[i].Count; j++)
				{
					NSString text = new NSString(_styledPicker.ItemsSource[i][j]);
					var size = text.StringSize(_styledPicker.CustomFont.ToUIFont(), ms, UILineBreakMode.WordWrap);
					if (size.Width > sizeMax.Width)
					{
						sizeMax = size;
					}
				}
				NSString fixedText = new NSString(_styledPicker.ComponentFixedTextItems[i]);
				var fixedTextSize = fixedText.StringSize(_styledPicker.CustomFont.ToUIFont(), ms, UILineBreakMode.WordWrap);
				var label = new UILabel();
				label.Frame = new CoreGraphics.CGRect(accumulatedWidth + Control.RowSizeForComponent(i).Width / 2 + sizeMax.Width / 2 + 30, Control.Frame.Size.Height / 2 - fixedTextSize.Height / 2, fixedTextSize.Width + 10, fixedTextSize.Height);
				accumulatedWidth += Control.RowSizeForComponent(i).Width + 5;
				label.AttributedText = _styledPicker.CustomFont.BuildAttributedString(_styledPicker.ComponentFixedTextItems[i]);
				Control.AddSubview(label);
			}

		}

		private void CalculateComponentWidths()
		{
			_componentWidths.Clear();

			var ms = new CoreGraphics.CGSize(500, 500);

			for (int i = 0; i < _styledPicker.ItemsSource.Count; i++)
			{
				CoreGraphics.CGSize sizeMax = new CoreGraphics.CGSize(0, 0);
				for (int j = 0; j < _styledPicker.ItemsSource[i].Count; j++)
				{
					NSString text = new NSString(_styledPicker.ItemsSource[i][j]);
					var size = text.StringSize(_styledPicker.CustomFont.ToUIFont(), ms, UILineBreakMode.WordWrap);
					if (size.Width > sizeMax.Width)
					{
						sizeMax = size;
					}
				}
				_componentWidths.Add(sizeMax.Width);
			}

			var screenWidth = UIScreen.MainScreen.Bounds.Size.Width;
			for (int i = 0; i < _componentWidths.Count; i++)
			{
				_componentWidths[i] += 20;
			}
			double totalCalculatedWith = 0;
			_componentWidths.ForEach(componentWidth => totalCalculatedWith += componentWidth);
			if (screenWidth / totalCalculatedWith > 1)
			{
				for (int i = 0; i < _componentWidths.Count; i++)
				{
					_componentWidths[i] *= (screenWidth - 40) / totalCalculatedWith;
				}
			}
		}

	}
}

