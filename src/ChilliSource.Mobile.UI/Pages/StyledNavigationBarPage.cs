#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public abstract class StyledNavigationBarPage : ContentPage
	{
		public static BindableProperty SubtitleProperty = BindableProperty.Create(nameof(SubTitle), typeof(string), typeof(StyledNavigationBarPage));

		public string SubTitle
		{
			get
			{
				return (string)this.GetValue(SubtitleProperty);
			}
			set
			{
				SetValue(SubtitleProperty, value);
			}
		}

		public virtual ExtendedFont TitleFont { get; set; }

		public virtual ExtendedFont TitleOnlyFont { get; set; }

		public virtual Color BarTintColor { get; set; }

		public virtual ExtendedFont SubTitleFont
		{
			get; set;
		}

		public static BindableProperty RightToolbarItemFontProperty = BindableProperty.Create(nameof(RightToolbarItemFont), typeof(ExtendedFont), typeof(StyledNavigationBarPage));

		public ExtendedFont RightToolbarItemFont
		{
			get { return (ExtendedFont)this.GetValue(RightToolbarItemFontProperty); }
			set
			{
				this.SetValue(RightToolbarItemFontProperty, value);
			}
		}

		public static BindableProperty LeftToolbarItemFontProperty = BindableProperty.Create(nameof(LeftToolbarItemFont), typeof(ExtendedFont), typeof(StyledNavigationBarPage));

		public ExtendedFont LeftToolbarItemFont
		{
			get { return (ExtendedFont)this.GetValue(LeftToolbarItemFontProperty); }
			set
			{
				this.SetValue(LeftToolbarItemFontProperty, value);
			}
		}

		public static BindableProperty RightToolbarItemVisibleProperty = BindableProperty.Create(nameof(RightToolbarItemVisible), typeof(bool), typeof(StyledNavigationBarPage), defaultValue: true);

		public bool RightToolbarItemVisible
		{
			get { return (bool)this.GetValue(RightToolbarItemVisibleProperty); }
			set
			{
				this.SetValue(RightToolbarItemVisibleProperty, value);
			}
		}

		public static BindableProperty LeftToolbarItemVisibleProperty = BindableProperty.Create(nameof(LeftToolbarItemVisible), typeof(bool), typeof(StyledNavigationBarPage), defaultValue: true);

		public bool LeftToolbarItemVisible
		{
			get { return (bool)this.GetValue(LeftToolbarItemVisibleProperty); }
			set
			{
				this.SetValue(LeftToolbarItemVisibleProperty, value);
			}
		}

		public bool HideBackButton { get; set; }

		private bool _isTransparentNavBar;
		public bool IsTransparentNavBar
		{
			get { return _isTransparentNavBar; }
			set
			{
				_isTransparentNavBar = value;
				NavigationPage.SetHasNavigationBar(this, !_isTransparentNavBar);
			}
		}
	}
}

