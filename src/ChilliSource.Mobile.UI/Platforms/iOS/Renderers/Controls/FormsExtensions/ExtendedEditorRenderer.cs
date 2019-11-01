#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ExtendedEditor), typeof(ExtendedEditorRenderer))]

namespace ChilliSource.Mobile.UI
{
    public class ExtendedEditorRenderer : EditorRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
            {
                return;
            }

            var styledEditor = (ExtendedEditor)this.Element;

            SetStyle();
            SetKeyboardReturnKey();
            SetToolbars();


            styledEditor.TextChanged += (sender, evt) =>
            {
                var limitEntry = styledEditor.Text.Length > styledEditor.MaxLength;

                if (limitEntry)
                {
                    styledEditor.Text = styledEditor.Text.Remove(styledEditor.Text.Length - 1);
                }
            };

            
            styledEditor.Unfocused += (sender, evt) =>
            {
                styledEditor.ResizeEditor();
            };

            if (e.OldElement != null)
            {
                this.Control.ShouldEndEditing -= ShouldEndEditing;
            }

            if (e.NewElement != null)
            {
                this.Control.ShouldEndEditing += ShouldEndEditing;
            }
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
                        Font = toolbarFont.ToUIFont(),
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
            var styledEntry = (ExtendedEditor)this.Element;

            if (!styledEntry.ToolbarItems.Any())
            {
                return;
            }

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

            var view = Element as ExtendedEditor;

            if (view != null &&
                e.PropertyName == Entry.TextProperty.PropertyName ||
                e.PropertyName == Entry.PlaceholderProperty.PropertyName)
            {
                SetStyle();
            }

        }

        private void SetStyle()
        {
            var styledEditor = (ExtendedEditor)this.Element;

            Control.TintColor = styledEditor.CustomFont.Color.ToUIColor();

            if (styledEditor.Text != null && styledEditor.CustomFont != null)
            {
                var attributedString = styledEditor.CustomFont.BuildAttributedString(styledEditor.Text, this.Control.TextAlignment);
                this.Control.AttributedText = attributedString;
            }

            if (styledEditor.HasBorder)
            {
                Control.Layer.BorderColor = UIColor.Gray.ColorWithAlpha(0.5f).CGColor;
                Control.Layer.BorderWidth = 0.5f;

                Control.Layer.CornerRadius = 5;
                Control.ClipsToBounds = true;

                var defaultInset = Control.TextContainerInset;
                defaultInset.Left = 2;
                Control.TextContainerInset = defaultInset;
            }
        }

        private void SetKeyboardReturnKey()
        {
            var view = Element as ExtendedEditor;
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

        private bool ShouldEndEditing(UITextView textView)
        {
            if (Element == null) return true;
            var editor = (Element as ExtendedEditor);
            return editor.ShouldCancelEditing;
        }
    }
}

