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
	public enum ShapeType
	{
		Circle,
		Rectangle,
		Triangle
	}

	public class ShapeView : ContentView
	{
		public static readonly BindableProperty ShapeTypeProperty =
			BindableProperty.Create(nameof(ShapeType), typeof(ShapeType), typeof(ShapeView), ShapeType.Rectangle);

		public ShapeType ShapeType
		{
			get { return (ShapeType)GetValue(ShapeTypeProperty); }
			set { SetValue(ShapeTypeProperty, value); }
		}

		public static readonly BindableProperty FillColorProperty =
			BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(ShapeView), Color.Transparent);

		public Color FillColor
		{
			get { return (Color)GetValue(FillColorProperty); }
			set { SetValue(FillColorProperty, value); }
		}

		public static readonly BindableProperty BorderWidthProperty =
			BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(ShapeView), 1);

		public int BorderWidth
		{
			get { return (int)GetValue(BorderWidthProperty); }
			set { SetValue(BorderWidthProperty, value); }
		}

		public static readonly BindableProperty BorderColorProperty =
			BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ShapeView), Color.Black);

		public Color BorderColor
		{
			get { return (Color)GetValue(BorderColorProperty); }
			set { SetValue(BorderColorProperty, value); }
		}

		public static readonly BindableProperty CornerRadiusProperty =
			BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(ShapeView), 0);

		public int CornerRadius
		{
			get { return (int)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}


	}
}
