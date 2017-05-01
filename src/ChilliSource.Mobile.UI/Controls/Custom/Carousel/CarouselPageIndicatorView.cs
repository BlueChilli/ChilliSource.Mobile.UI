#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using System.Collections;
using System.Linq;

namespace ChilliSource.Mobile.UI
{
	public class CarouselPageIndicatorView : StackLayout
	{

		public CarouselPageIndicatorView()
		{
			HorizontalOptions = LayoutOptions.CenterAndExpand;
			VerticalOptions = LayoutOptions.Center;
			Orientation = StackOrientation.Horizontal;
		}

		#region Properties

		public Color SelectedDotColor { get; set; }
		public Color DotBorderColor { get; set; }
		public Color SelectedDotBorderColor { get; set; }
		public double DotSize { get; set; }

		public static BindableProperty ItemsSourceProperty =
			BindableProperty.Create(
				nameof(ItemsSource),
				typeof(IList),
				typeof(CarouselPageIndicatorView),
				null,
				BindingMode.OneWay,
				propertyChanged: (bindable, oldValue, newValue) =>
				{
					((CarouselPageIndicatorView)bindable).ItemsSourceChanged();
				}
			);

		public IList ItemsSource
		{
			get
			{
				return (IList)GetValue(ItemsSourceProperty);
			}
			set
			{
				SetValue(ItemsSourceProperty, value);
			}
		}

		public static BindableProperty SelectedItemProperty =
			BindableProperty.Create(
				nameof(SelectedItem),
				typeof(object),
				typeof(CarouselPageIndicatorView),
				null,
				BindingMode.TwoWay,
				propertyChanged: (bindable, oldValue, newValue) =>
				{
					((CarouselPageIndicatorView)bindable).SelectedItemChanged();
				});

		public object SelectedItem
		{
			get
			{
				return GetValue(SelectedItemProperty);
			}
			set
			{
				SetValue(SelectedItemProperty, value);
			}
		}

		#endregion

		#region Event Handlers

		void ItemsSourceChanged()
		{
			if (ItemsSource == null)
			{
				return;
			}

			var countDelta = ItemsSource.Count - Children.Count;

			if (countDelta > 0)
			{
				for (var i = 0; i < countDelta; i++)
				{
					CreateDot();
				}
			}
			else if (countDelta < 0)
			{
				for (var i = 0; i < -countDelta; i++)
				{
					Children.RemoveAt(0);
				}
			}
		}

		void SelectedItemChanged()
		{

			var selectedIndex = ItemsSource.IndexOf(SelectedItem);
			var pagerIndicators = Children.Cast<ShapeView>().ToList();

			foreach (var pi in pagerIndicators)
			{
				UnselectDot(pi);
			}

			if (selectedIndex > -1)
			{
				SelectDot(pagerIndicators[selectedIndex]);
			}
		}

		#endregion

		#region Helper Methods

		void CreateDot()
		{
			var dot = new ShapeView
			{
				CornerRadius = Convert.ToInt32(DotSize / 2),
				HeightRequest = DotSize,
				WidthRequest = DotSize,
				BorderWidth = 1,
				BorderColor = DotBorderColor,
				FillColor = Color.Transparent,
			};
			Children.Add(dot);
		}


		void UnselectDot(ShapeView dot)
		{
			dot.FillColor = Color.Transparent;
			dot.BorderColor = DotBorderColor;
		}

		void SelectDot(ShapeView dot)
		{
			dot.FillColor = SelectedDotColor;
			dot.BorderColor = SelectedDotBorderColor;
		}

		#endregion
	}
}

