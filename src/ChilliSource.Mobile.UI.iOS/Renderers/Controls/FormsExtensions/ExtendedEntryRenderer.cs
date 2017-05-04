#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ExtendedEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Element == null)
			{
				return;
			}

			SetStyle();
			SetMaxLength();
			SetKeyboardReturnKey();
			SetToolbars();

			if (e.OldElement != null)
			{
				this.Control.ShouldEndEditing -= ShouldEndEditing;
			}

			if (e.NewElement != null)
			{
				this.Control.ShouldEndEditing += ShouldEndEditing;
			}

		}

		private bool ShouldEndEditing(UITextField textField)
		{
			if (Element == null) return true;
			var editor = (Element as ExtendedEntry);
			return editor.ShouldCancelEditing;
		}

		private static UIBarButtonItem[] SetCustomFontsToToolBars(
			List<UIBarButtonItem> navigationBarItems,
			ExtendedFont toolbarFont)
		{
			navigationBarItems.ForEach(nativeItem =>
			{
				if (toolbarFont != null)
				{
					var textAttributes = new UITextAttributes()
					{
						Font = UIFont.FromName(toolbarFont.Family, toolbarFont.Size),
						TextColor = toolbarFont.Color.ToUIColor(),
					};
					nativeItem.SetTitleTextAttributes(textAttributes, UIControlState.Normal);
					nativeItem.TintColor = toolbarFont.Color.ToUIColor();
				}
			});

			return navigationBarItems.ToArray();
		}


		private void SetToolbars()
		{
			var styledEntry = (ExtendedEntry)this.Element;

			if (!styledEntry.ToolbarItems.Any()) return;

			var toolbar = new UIToolbar(new CGRect(0.0f, 0.0f, Control.Frame.Size.Width, 44.0f));

			var toolbarItems = new List<UIBarButtonItem>();

			foreach (var toolbarItem in styledEntry.ToolbarItems)
			{
				toolbarItems.Add(toolbarItem.ToUIBarButtonItem());
			}

			if (toolbarItems.Any())
			{
				if (styledEntry.ToolbarPosition == EntryToolbarPosition.RightToLeft)
				{
					toolbarItems.Insert(0, new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace));
				}
			}

			toolbar.Items = SetCustomFontsToToolBars(toolbarItems, styledEntry.ToolBarItemCustomFont);

			if (toolbarItems.Any())
			{
				this.Control.InputAccessoryView = toolbar;
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = Element as ExtendedEntry;

			if (view != null &&
				//e.PropertyName == Entry.TextProperty.PropertyName || //this should not be needed and causes problems
				e.PropertyName == Entry.PlaceholderProperty.PropertyName ||
				e.PropertyName == ExtendedEntry.IsValidProperty.PropertyName ||
				e.PropertyName == ExtendedEntry.CustomFontProperty.PropertyName ||
				e.PropertyName == ExtendedEntry.CustomPlaceholderFontProperty.PropertyName)
			{
				SetStyle();
			}
		}

		private void SetStyle()
		{
			var styledEntry = (ExtendedEntry)this.Element;

			switch (styledEntry.KeyboardTheme)
			{
				case KeyboardTheme.Light:
					{
						Control.KeyboardAppearance = UIKeyboardAppearance.Light;
						break;
					}
				case KeyboardTheme.Dark:
					{
						Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
						break;
					}
			}

			if (styledEntry.IsValid)
			{
				Control.BackgroundColor = styledEntry.NormalBackgroundColor.ToUIColor();
			}
			else
			{
				Control.BackgroundColor = styledEntry.ErrorBackgroundColor.ToUIColor();

				if (styledEntry.ShouldFocusWhenInvalid && !styledEntry.IsFocused)
				{
					styledEntry.Focus();
				}
			}

			if (!styledEntry.HasBorder)
			{
				Control.BorderStyle = UITextBorderStyle.None;
			}

			if (styledEntry.TextIndentation > 0)
			{
				Control.Layer.SublayerTransform = CATransform3D.MakeTranslation(styledEntry.TextIndentation, 0, 0);
			}

			if (styledEntry.CustomFont != null)
			{
				ExtendedFont fontToApply;

				if (!styledEntry.IsValid && styledEntry.CustomErrorFont != null)
				{
					fontToApply = styledEntry.CustomErrorFont;
				}
				else
				{
					fontToApply = styledEntry.CustomFont;
				}

				this.Control.TextColor = fontToApply.Color.ToUIColor();
				this.Control.TintColor = fontToApply.Color.ToUIColor();

				if (styledEntry.Text != null)
				{
					var attributedString = fontToApply.BuildAttributedString(styledEntry.Text);
					this.Control.AttributedText = attributedString;
				}

				if (styledEntry.Placeholder != null)
				{
					var font = styledEntry.IsValid ? styledEntry.CustomPlaceholderFont : styledEntry.CustomPlaceholderErrorFont;
					this.Control.AttributedPlaceholder = font.BuildAttributedString(styledEntry.Placeholder, this.Control.TextAlignment);
				}
			}
		}

		private void SetMaxLength()
		{
			var styledEntry = (ExtendedEntry)this.Element;

			Control.ShouldChangeCharacters = (textField, range, replacementString) =>
			{
				var newLength = textField.Text.Length + replacementString.Length - range.Length;
				textField.SizeToFit();
				return newLength <= styledEntry.MaxLength;
			};
		}

		private void SetKeyboardReturnKey()
		{
			var view = Element as ExtendedEntry;
			switch (view.KeyboardReturnType)
			{
				case KeyboardReturnKeyType.Done:
					{
						this.Control.ReturnKeyType = UIKit.UIReturnKeyType.Done;
						break;
					}
				case KeyboardReturnKeyType.Go:
					{
						this.Control.ReturnKeyType = UIKit.UIReturnKeyType.Go;
						break;
					}
				case KeyboardReturnKeyType.Next:
					{
						this.Control.ReturnKeyType = UIKit.UIReturnKeyType.Next;
						break;
					}
			}
		}
	}
}

