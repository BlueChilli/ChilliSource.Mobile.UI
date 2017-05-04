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
	/// Forms implementation of the FloatingLabelEntry.
	/// </summary>
	public class FloatingLabelEntry : ExtendedEntry
	{
		public static readonly BindableProperty FloatingLabelCustomFontProperty =
			BindableProperty.Create(nameof(FloatingLabelCustomFont), typeof(ExtendedFont), typeof(FloatingLabelEntry), null);

		public ExtendedFont FloatingLabelCustomFont
		{
			get { return (ExtendedFont)GetValue(FloatingLabelCustomFontProperty); }
			set { SetValue(FloatingLabelCustomFontProperty, value); }
		}
	}
}
