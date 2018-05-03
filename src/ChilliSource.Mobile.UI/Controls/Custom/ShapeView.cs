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
    /// Specifies the shape type for the shape view.
    /// </summary>
    public enum ShapeType
    {
        Circle,
        Rectangle,
        Triangle
    }

    /// <summary>
    /// Control that renders as a shape defined by <see cref="ShapeType"/>
    /// </summary>
    public class ShapeView : ContentView
    {
        /// <summary>
        /// Backing store for the <c>ShapeType</c> bindable property.
        /// </summary>
        public static readonly BindableProperty ShapeTypeProperty =
            BindableProperty.Create(nameof(ShapeType), typeof(ShapeType), typeof(ShapeView), ShapeType.Rectangle);

        /// <summary>
        /// Gets or sets the shape of the view. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ShapeType"/> value that represents the shape of the shape view.</value>
        public ShapeType ShapeType
        {
            get { return (ShapeType)GetValue(ShapeTypeProperty); }
            set { SetValue(ShapeTypeProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>FillerColor</c> bindable property.
        /// </summary>
        public static readonly BindableProperty FillColorProperty =
            BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(ShapeView), Color.Transparent);

        /// <summary>
        /// Gets or sets the color to fill the shape view. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Color"/> value that fills the shape view. 
        /// the default value is <see cref="Color.Transparent"/>.</value> 
        public Color FillColor
        {
            get { return (Color)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>BorderWidth</c> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(ShapeView), 1);

        /// <summary>
        /// Gets or sets the width for the shape view's border. This is a bindable property.
        /// </summary>
        /// <value>The border width of the shape view; the default is 1.</value>
        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>BorderColor</c> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ShapeView), Color.Black);

        /// <summary>
        /// Gets or sets the color of the border. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the color of the shape view's border.
        /// The default value is <see cref="Color.Black"/>.</value>
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>CornerRadius</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(ShapeView), 0);

        /// <summary>
        /// Gets or sets the corner radius of the shape view. This is a bindable property.
        /// </summary>
        /// <value>The corner radius value for the shape view; the default is 0.</value>
        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }


    }
}
