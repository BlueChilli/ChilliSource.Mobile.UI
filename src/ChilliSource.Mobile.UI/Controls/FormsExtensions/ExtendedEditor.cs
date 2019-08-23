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
    /// <summary>
    /// Xamarin Forms Editor extension
    /// </summary>
    public class ExtendedEditor : Editor
    {
        /// <summary>
        /// Initializes a new instance of the <c>ExtendedEditor</c> class.
        /// </summary>
        public ExtendedEditor()
        {
            ToolbarItems = new List<ToolbarItem>();
        }

        /// <summary>
        /// Occurs when the editor needs to be resized.
        /// </summary>
        /// <remarks>
        /// <c>AllowDynamicSizing</c> property should be <c>true</c> for resizing.
        /// </remarks>
        public event EventHandler ParentContainerSizeUpdateRequested;

        /// <summary>
        /// Identifies the <c>AllowDynamicSizing</c> bindable property.
        /// </summary>
        public static readonly BindableProperty AllowDynamicSizingProperty =
            BindableProperty.Create(nameof(AllowDynamicSizing), typeof(bool), typeof(ExtendedEditor), false);

        /// <summary>
        /// Gets or sets a value indicating whether the editor allows dynamic sizing. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if dynamic sizing is allowed; otherwise, <c>false</c>. The default value is <c>false</c>.</value>
		public bool AllowDynamicSizing
        {
            get { return (bool)GetValue(AllowDynamicSizingProperty); }
            set { SetValue(AllowDynamicSizingProperty, value); }
        }

        /// <summary>
        /// Backing store for the <c>CustomFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomFontProperty =
            BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(ExtendedEditor), null);

        /// <summary>
        /// Gets or sets the custom font for the content of the editor. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font for the content of the editor. 
        /// The default value is <c>null</c>.</value>
        public ExtendedFont CustomFont
        {
            get { return (ExtendedFont)GetValue(CustomFontProperty); }
            set { SetValue(CustomFontProperty, value); }
        }

        /// <summary>
        /// Identifies the <c>HasBorder</c> bindable property.
        /// </summary>
        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(ExtendedEditor), false);

        /// <summary>
        /// Gets or sets a value indicating whether this editor has border. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if the editor has border; otherwise, <c>false</c>. The default value is <c>false</c>.</value>
		public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

      
        /// <summary>
        /// Backing store for the <c>CustomPlaceholderFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomPlaceholderFontProperty =
            BindableProperty.Create(nameof(CustomPlaceholderFont), typeof(ExtendedFont), typeof(ExtendedEditor), null);

        /// <summary>
        /// Gets or sets the custom font for the placeholder text. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font for the placeholder text. 
        /// The default value is <c>null</c>.</value>
        public ExtendedFont CustomPlaceholderFont
        {
            get { return (ExtendedFont)GetValue(CustomPlaceholderFontProperty); }
            set { SetValue(CustomPlaceholderFontProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>ToolBarItemCustomFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty ToolBarItemCustomFontProperty =
            BindableProperty.Create(nameof(ToolBarItemCustomFont), typeof(ExtendedFont), typeof(ExtendedEntry), null);

        /// <summary>
        /// Gets or sets the custom font for the toolbar item. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font for the toolbar item. 
        /// The default value is <c>null</c>.</value>
        public ExtendedFont ToolBarItemCustomFont
        {
            get { return (ExtendedFont)GetValue(ToolBarItemCustomFontProperty); }
            set { SetValue(ToolBarItemCustomFontProperty, value); }
        }

        /// <summary>
        /// Identifies the <c>ShouldCancelEditing</c> bindable property.
        /// </summary>
        public static BindableProperty ShouldCancelEditingProperty = BindableProperty.Create(nameof(ShouldCancelEditing), typeof(bool), typeof(ExtendedEditor), true);

        /// <summary>
        /// Gets or sets a value indicating whether editing the editor's text should be cancelled. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if editing the content should be cancelled; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool ShouldCancelEditing
        {
            get { return (bool)GetValue(ShouldCancelEditingProperty); }
            set { SetValue(ShouldCancelEditingProperty, value); }
        }


        /// <summary>
        /// Gets a list of toolbar items to display inside the <see cref="MonoTouch.UIKit.UITextField.InputAccessoryView"/> on iOS.
        /// </summary>
        /// <value>A list of <see cref="ToolbarItem"/> to appear in the input accessory view of the editor.</value>
        public IList<ToolbarItem> ToolbarItems { get; }

        /// <summary>
        /// Gets or sets the position of the <c>MonoTouch.UIKit.UITextField.InputAccessoryView</c> toolbar.
        /// </summary>
        /// <value>A <see cref="EntryToolbarPosition"/> value that represents the posiotion of the editor's toolbar.</value>
        public EntryToolbarPosition ToolbarPosition { get; set; }

        /// <summary>
        /// Gets or sets the text shown on the keyboard's return key.
        /// </summary>
        /// <value>A <see cref="KeyboardReturnKeyType"/> value that represents the text of the keyboard's return key.</value>
        public KeyboardReturnKeyType KeyboardReturnType
        {
            get;
            set;
        }

        /// <summary>
        /// Resizes the editor when is unfocused after entering some text .
        /// </summary>
        public void ResizeEditor()
        {
            if (AllowDynamicSizing)
            {
                InvalidateMeasure();

                ParentContainerSizeUpdateRequested?.Invoke(this, new EventArgs());
            }
        }

    }
}


