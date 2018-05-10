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
    /// <summary>
    /// Paging indicator for <see cref="CarouselView"/>
    /// </summary>
    public class CarouselPageIndicatorView : StackLayout
    {
        /// <summary>
        /// Initializes a new instance of this <c>CarouselPageIndicatorView</c> class.
        /// </summary>
        public CarouselPageIndicatorView()
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            VerticalOptions = LayoutOptions.Center;
            Orientation = StackOrientation.Horizontal;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the color of the indicator dot.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the color of the dot.</value>
        public Color DotColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the indicator dot when is selected.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the color of the selected dot.</value>
        public Color SelectedDotColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the indicator dot's border.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the color of the dot's border.</value>
        public Color DotBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the indicator dot's border when is selected.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the color of the selected dot's border.</value>
        public Color SelectedDotBorderColor { get; set; }

        /// <summary>
        /// Gets or sets the size of the indicator dot.
        /// </summary>
        public double DotSize { get; set; }

         /// <summary>
        /// Gets or sets the size of the indicator dot border width.
        /// </summary>
        public int DotBorderSize { get; set; } = 1;

        /// <summary>
        /// Backing store for the <c>ItemSource</c> bindable property.
        /// </summary>
        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(CarouselPageIndicatorView), null, BindingMode.OneWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((CarouselPageIndicatorView)bindable).ItemsSourceChanged();
                    ((CarouselPageIndicatorView)bindable).SelectedItemChanged();
                });

        /// <summary>
        /// Gets or sets the source for the items of the page indicator. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="IList"/> providing the source for the items.</value>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>SelectedItem</c> bindable property.
        /// </summary>
        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(CarouselPageIndicatorView), null, BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((CarouselPageIndicatorView)bindable).SelectedItemChanged();
                });

        /// <summary>
        /// Gets or sets the page indicator's selected item. This is a bindable property.
        /// </summary>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        
        /// <summary>
        /// Backing store for the <c>SelectedIndex</c> bindable property.
        /// </summary>
        public static BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(CarouselPageIndicatorView), -1, BindingMode.OneWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((CarouselPageIndicatorView)bindable).SelectedItemChanged();
                });

     
        /// <summary>
        /// Gets or sets the page indicator's selected index. This is a bindable property.
        /// </summary>
        public int SelectedIndex
        {
            get { return (int) GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        #endregion

        #region Event Handlers

        void ItemsSourceChanged()
        {
            if (ItemsSource == null || ItemsSource.Count == 0)
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
            if (ItemsSource == null || ItemsSource.Count == 0 || SelectedIndex < 0 && SelectedItem == null)
            {
                return;
            }

            int index = SelectedIndex;

            if (index < 0)
            {
                index = ItemsSource.IndexOf(SelectedItem);
            }

            if (index < 0)
            {
                return;
            }

            var pagerIndicators = Children.Cast<ShapeView>().ToList();

            foreach (var pi in pagerIndicators)
            {
                UnselectDot(pi);
            }

            if(index > pagerIndicators.Count) 
            {
                index = 0;    
            }

            SelectDot(pagerIndicators[index]);
            SelectedItem = ItemsSource[index];
        }

        #endregion

        #region Helper Methods

        void CreateDot()
        {
            var dot = new ShapeView
            {
                ShapeType = ShapeType.Circle,
                HeightRequest = DotSize,
                WidthRequest = DotSize,
                BorderWidth = DotBorderSize,
                BorderColor = DotBorderColor,
                FillColor = DotColor,
            };
            Children.Add(dot);
        }

        void UnselectDot(ShapeView dot)
        {
            dot.FillColor = DotColor;
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

