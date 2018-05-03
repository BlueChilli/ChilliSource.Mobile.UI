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
    /// Image rendered in the shape of a circle
    /// </summary>
    public class CircledImage : Image
    {
        /// <summary>
        /// Backing store for the <c>BorderWidth</c> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(CircledImage), default(double));

        /// <summary>
        /// Gets or sets the border width for the image. This is a bindable property.
        /// </summary>
        public double BorderWidth
        {
            get { return (double)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>BorderColor</c> bindable property.
        /// </summary>
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CircledImage), Color.Default);

        /// <summary>
        /// Gets or sets the color of the image's border. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the color of the border. 
        /// The default value is <see cref="Color.Default"/>.</value>
        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }
    }
}

