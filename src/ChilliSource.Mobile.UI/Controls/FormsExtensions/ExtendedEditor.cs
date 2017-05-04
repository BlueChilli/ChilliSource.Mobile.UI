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
	public class ExtendedEditor : Editor
	{
		public ExtendedEditor()
		{
			ToolbarItems = new List<ToolbarItem>();
		}

		public event EventHandler ParentContainerSizeUpdateRequested;

		public static readonly BindableProperty AllowDynamicSizingProperty =
			BindableProperty.Create(nameof(AllowDynamicSizing), typeof(bool), typeof(ExtendedEditor), false);


		public bool AllowDynamicSizing
		{
			get { return (bool)GetValue(AllowDynamicSizingProperty); }
			set { SetValue(AllowDynamicSizingProperty, value); }
		}

		public static readonly BindableProperty CustomFontProperty =
			BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(ExtendedEditor), null);


		public static readonly BindableProperty HasBorderProperty =
			BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(ExtendedEditor), false);

		public bool HasBorder
		{
			get { return (bool)GetValue(HasBorderProperty); }
			set { SetValue(HasBorderProperty, value); }
		}

		public ExtendedFont CustomFont
		{
			get { return (ExtendedFont)GetValue(CustomFontProperty); }
			set { SetValue(CustomFontProperty, value); }
		}

		public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ExtendedEditor), string.Empty);


		public string Placeholder
		{
			get { return (string)GetValue(PlaceholderProperty); }
			set { SetValue(PlaceholderProperty, value); }
		}

		public static readonly BindableProperty CustomPlaceholderFontProperty =
			BindableProperty.Create(nameof(CustomPlaceholderFont), typeof(ExtendedFont), typeof(ExtendedEditor), null);


		public ExtendedFont CustomPlaceholderFont
		{
			get { return (ExtendedFont)GetValue(CustomPlaceholderFontProperty); }
			set { SetValue(CustomPlaceholderFontProperty, value); }
		}

		public KeyboardReturnKeyType KeyboardReturnType
		{
			get;
			set;
		}

		public static readonly BindableProperty MaxLengthProperty =
			BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(ExtendedEntry), 500);


		public int MaxLength
		{
			get { return (int)GetValue(MaxLengthProperty); }
			set { SetValue(MaxLengthProperty, value); }
		}

		public void ResizeEditor()
		{
			if (AllowDynamicSizing)
			{
				InvalidateMeasure();

				if (ParentContainerSizeUpdateRequested != null)
				{
					ParentContainerSizeUpdateRequested(this, new EventArgs());
				}
			}
		}

		public IList<ToolbarItem> ToolbarItems { get; }

		public EntryToolbarPosition ToolbarPosition { get; set; }

		public static readonly BindableProperty ToolBarItemCustomFontProperty =
			BindableProperty.Create(nameof(ToolBarItemCustomFont), typeof(ExtendedFont), typeof(ExtendedEntry), null);


		public ExtendedFont ToolBarItemCustomFont
		{
			get { return (ExtendedFont)GetValue(ToolBarItemCustomFontProperty); }
			set { SetValue(ToolBarItemCustomFontProperty, value); }
		}

		public static BindableProperty ShouldCancelEditingProperty = BindableProperty.Create(nameof(ShouldCancelEditing), typeof(bool), typeof(ExtendedEditor), true);

		public bool ShouldCancelEditing
		{
			get { return (bool)this.GetValue(ShouldCancelEditingProperty); }
			set { this.SetValue(ShouldCancelEditingProperty, value); }
		}
	}
}


