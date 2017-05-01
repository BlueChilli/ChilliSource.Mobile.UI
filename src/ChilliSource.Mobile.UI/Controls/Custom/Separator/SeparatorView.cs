#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
/// <summary>
	/// Class Separator.
	/// </summary>
	public class SeparatorView : View
	{

		/**
		 * Orientation property
		 */
		/// <summary>
		/// The orientation property
		/// </summary>
		public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(SeparatorOrientation), typeof(SeparatorView), SeparatorOrientation.Horizontal, BindingMode.OneWay, null, null, null, null);

		/**
		 * Orientation of the separator. Only
		 */
		/// <summary>
		/// Gets the orientation.
		/// </summary>
		/// <value>The orientation.</value>
		public SeparatorOrientation Orientation
		{
			get
			{
				return (SeparatorOrientation)base.GetValue(SeparatorView.OrientationProperty);
			}

			private set
			{
				base.SetValue(SeparatorView.OrientationProperty, value);
			}
		}

		/**
		 * Color property
		 */
		/// <summary>
		/// The color property
		/// </summary>
		public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(SeparatorView), Color.Default, BindingMode.OneWay, null, null, null, null);

		/**
		 * Color of the separator. Black is a default color
		 */
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Color Color
		{
			get
			{
				return (Color)base.GetValue(SeparatorView.ColorProperty);
			}
			set
			{
				base.SetValue(SeparatorView.ColorProperty, value);
			}
		}


		/**
		 * SpacingBefore property
		 */

		/// <summary>
		/// The spacing before property
		/// </summary>
		public static readonly BindableProperty SpacingBeforeProperty = BindableProperty.Create(nameof(SpacingBefore), typeof(double), typeof(SeparatorView), (double)1, BindingMode.OneWay, null, null, null, null);

		/**
		 * Padding before the separator. Default is 1.
		 */
		/// <summary>
		/// Gets or sets the spacing before.
		/// </summary>
		/// <value>The spacing before.</value>
		public double SpacingBefore
		{
			get
			{
				return (double)base.GetValue(SeparatorView.SpacingBeforeProperty);
			}
			set
			{
				base.SetValue(SeparatorView.SpacingBeforeProperty, value);
			}
		}

		/**
		 * Spacing After property
		 */
		/// <summary>
		/// The spacing after property
		/// </summary>
		public static readonly BindableProperty SpacingAfterProperty = BindableProperty.Create(nameof(SpacingAfter), typeof(double), typeof(SeparatorView), (double)1, BindingMode.OneWay, null, null, null, null);

		/**
		 * Padding after the separator. Default is 1.
		 */
		/// <summary>
		/// Gets or sets the spacing after.
		/// </summary>
		/// <value>The spacing after.</value>
		public double SpacingAfter
		{
			get
			{
				return (double)base.GetValue(SeparatorView.SpacingAfterProperty);
			}
			set
			{
				base.SetValue(SeparatorView.SpacingAfterProperty, value);
			}
		}

		/**
		 * Thickness property
		 */
		/// <summary>
		/// The thickness property
		/// </summary>
		public static readonly BindableProperty ThicknessProperty = BindableProperty.Create(nameof(Thickness), typeof(double), typeof(SeparatorView), (double)1, BindingMode.OneWay, null, null, null, null);


		/**
		 * How thick should the separator be. Default is 1
		 */

		/// <summary>
		/// Gets or sets the thickness.
		/// </summary>
		/// <value>The thickness.</value>
		public double Thickness
		{
			get
			{
				return (double)base.GetValue(SeparatorView.ThicknessProperty);
			}
			set
			{
				base.SetValue(SeparatorView.ThicknessProperty, value);
			}
		}


		/**
		 * Stroke type property
		 */
		/// <summary>
		/// The stroke type property
		/// </summary>
		public static readonly BindableProperty StrokeTypeProperty = BindableProperty.Create(nameof(StrokeType), typeof(StrokeType), typeof(SeparatorView), StrokeType.Solid, BindingMode.OneWay, null, null, null, null);

		/**
		 * Stroke style of the separator. Default is Solid.
		 */
		/// <summary>
		/// Gets or sets the type of the stroke.
		/// </summary>
		/// <value>The type of the stroke.</value>
		public StrokeType StrokeType
		{
			get
			{
				return (StrokeType)base.GetValue(SeparatorView.StrokeTypeProperty);
			}
			set
			{
				base.SetValue(SeparatorView.StrokeTypeProperty, value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SeparatorView"/> class.
		/// </summary>
		public SeparatorView()
		{
			UpdateRequestedSize();
		}

		/// <summary>
		/// Call this method from a child class to notify that a change happened on a property.
		/// </summary>
		/// <param name="propertyName">The name of the property that changed.</param>
		/// <remarks>A <see cref="T:Xamarin.Forms.BindableProperty" /> triggers this by itself. An inheritor only needs to call this for properties without <see cref="T:Xamarin.Forms.BindableProperty" /> as the backend store.</remarks>
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


		/// <summary>
		/// Updates the size of the requested.
		/// </summary>
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
			else {
				MinimumWidthRequest = minSize;
				WidthRequest = optimalSize;
				VerticalOptions = LayoutOptions.FillAndExpand;
			}
		}
	}
}
