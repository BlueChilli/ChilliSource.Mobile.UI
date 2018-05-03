#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion


using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Renders a dashed, dotted, or solid line
    /// </summary>
    public class SeparatorView : View
    {

        /// <summary>
        /// Initializes a new instance of the <c>SeparatorView</c> class.
        /// </summary>
        public SeparatorView()
        {
            UpdateRequestedSize();
        }


        /// <summary>
        /// Backing store for the <c>Orientation</c> bindable property.
        /// </summary>
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(nameof(Orientation), typeof(SeparatorOrientation), typeof(SeparatorView),
                                    SeparatorOrientation.Horizontal, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// Gets the orientation of the separator. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="SeparatorOrientation"/> value that represents the orientation of the separator.
        ///  The default value is <see cref="SeparatorOrientation.Horizontal"/>.</value>
        public SeparatorOrientation Orientation
        {
            get
            {
                return (SeparatorOrientation)GetValue(OrientationProperty);
            }

            private set
            {
                SetValue(OrientationProperty, value);
            }
        }



        /// <summary>
        /// Backing store for the <c>Color</c> bindable property.
        /// </summary>
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(nameof(Color), typeof(Color), typeof(SeparatorView), Color.Default, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// Gets or sets the color of the separator. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the color of the separator. The default value is <see cref="Color.Default"/>.</value>
        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }



        /// <summary>
        /// Backing store for the <c>SpacingBefore</c> bindable property.
        /// </summary>
        public static readonly BindableProperty SpacingBeforeProperty =
            BindableProperty.Create(nameof(SpacingBefore), typeof(double), typeof(SeparatorView), (double)1, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// Gets or sets the padding amount before the separator. This is a bindable property.
        /// </summary>
        /// <value>The space before the seperator; the default is 1.</value>
        public double SpacingBefore
        {
            get
            {
                return (double)GetValue(SpacingBeforeProperty);
            }
            set
            {
                SetValue(SpacingBeforeProperty, value);
            }
        }



        /// <summary>
        /// Backing store for the <c>SpacingAfter</c> bindable property.
        /// </summary>
        public static readonly BindableProperty SpacingAfterProperty =
            BindableProperty.Create(nameof(SpacingAfter), typeof(double), typeof(SeparatorView), (double)1, BindingMode.OneWay, null, null, null, null);

        ///<summary>
        /// Gets or sets the padding amount after the separator. This is a bindable property.
        /// </summary>
        /// <value>The space after the separator; the default is 1.</value>
        public double SpacingAfter
        {
            get
            {
                return (double)GetValue(SpacingAfterProperty);
            }
            set
            {
                SetValue(SpacingAfterProperty, value);
            }
        }



        /// <summary>
        /// Backing store for the <c>Thickness</c> bindable property.
        /// </summary>
        public static readonly BindableProperty ThicknessProperty =
            BindableProperty.Create(nameof(Thickness), typeof(double), typeof(SeparatorView), (double)1, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// Gets or sets the thickness of the separator. This is a bindable property.
        /// </summary>
        /// <value>The thickness of the separator; the default is 1.</value>
        public double Thickness
        {
            get
            {
                return (double)GetValue(ThicknessProperty);
            }
            set
            {
                SetValue(ThicknessProperty, value);
            }
        }



        /// <summary>
        /// Backing store for the <c>StrokeType</c> bindable property.
        /// </summary>
        public static readonly BindableProperty StrokeTypeProperty =
            BindableProperty.Create(nameof(StrokeType), typeof(StrokeType), typeof(SeparatorView), StrokeType.Solid, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// Gets or sets the stroke type for the separator. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="StrokeType"/> value that represents the type of the separator's stroke. the default value is <see cref="StrokeType.Solid"/>.</value>
        public StrokeType StrokeType
        {
            get
            {
                return (StrokeType)GetValue(StrokeTypeProperty);
            }
            set
            {
                SetValue(StrokeTypeProperty, value);
            }
        }



        /// <summary>
        /// Call this method from a child class to notify that a change happened on a property.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        /// <remarks>
        /// A <see cref="T:Xamarin.Forms.BindableProperty" /> triggers this by itself. An inheritor only needs to call this for 
        /// properties without <see cref="Xamarin.Forms.BindableProperty" /> as the backend store.
        /// </remarks>
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == ThicknessProperty.PropertyName ||
               propertyName == SpacingBeforeProperty.PropertyName ||
               propertyName == SpacingAfterProperty.PropertyName ||
               propertyName == OrientationProperty.PropertyName)
            {
                UpdateRequestedSize();
            }
        }


        private void UpdateRequestedSize()
        {
            var minSize = Thickness;
            var optimalSize = SpacingBefore + Thickness + SpacingAfter;
            if (Orientation == SeparatorOrientation.Horizontal)
            {
                MinimumHeightRequest = minSize;
                HeightRequest = optimalSize;
                HorizontalOptions = LayoutOptions.FillAndExpand;
            }
            else
            {
                MinimumWidthRequest = minSize;
                WidthRequest = optimalSize;
                VerticalOptions = LayoutOptions.FillAndExpand;
            }
        }
    }
}
