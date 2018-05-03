#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Xamarin Forms Label extension
    /// </summary>
	public class ExtendedLabel : Label, IViewContainer<StyledTextPart>
    {
        /// <summary>
        /// Initializes a new instance of the <c>ExtendedLabel</c> class.
        /// </summary>
        public ExtendedLabel()
        {
            Children = new List<StyledTextPart>();
        }

        /// <summary>
        /// Backing store for the <c>CustomFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomFontProperty =
            BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(ExtendedLabel), null);

        /// <summary>
        /// Gets or sets the custom font for the label's text. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font for the content of the label. 
        /// The default value is <c>null</c>.</value>
        public ExtendedFont CustomFont
        {
            get { return (ExtendedFont)GetValue(CustomFontProperty); }
            set { SetValue(CustomFontProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>Children</c> bindable property.
        /// </summary>
        public static readonly BindableProperty ChildrenProperty =
            BindableProperty.Create(nameof(Children), typeof(List<StyledTextPart>), typeof(ExtendedLabel), null);

        /// <summary>
        /// Gets or sets the children of the label. This is a bindable property.
        /// </summary>
        /// <value>A list of <c>StyledTextPart</c> values that represent label's children.</value>
        public IList<StyledTextPart> Children
        {
            get { return (IList<StyledTextPart>)GetValue(ChildrenProperty); }
            set
            {
                SetValue(ChildrenProperty, value);
            }
        }


        /// <summary>
        /// Identifies the <c>AdjustFontSizeToFit</c> bindable property.
        /// </summary>
        public static readonly BindableProperty AdjustFontSizeToFitProperty =
            BindableProperty.Create(nameof(AdjustFontSizeToFit), typeof(bool), typeof(ExtendedLabel), false);

        /// <summary>
        /// Gets or sets a value indicating whether the font size of the content should be adjusted to fit inside the label. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if the font size should be adjusted to fit; otherwise, <c>false</c>. The default value is <c>false</c>.</value>
        public bool AdjustFontSizeToFit
        {
            get { return (bool)GetValue(AdjustFontSizeToFitProperty); }
            set { SetValue(AdjustFontSizeToFitProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>NumberOfLines</c> bindable property.
        /// </summary>
        public static readonly BindableProperty NumberOfLinesProperty =
            BindableProperty.Create(nameof(NumberOfLines), typeof(int), typeof(ExtendedLabel), 0);

        /// <summary>
        /// Gets or sets the number of lines for the label. This is a bindable property.
        /// </summary>
        /// <value>The number of lines in the label; the default is 0.</value>
        public int NumberOfLines
        {
            get { return (int)GetValue(NumberOfLinesProperty); }
            set { SetValue(NumberOfLinesProperty, value); }
        }

       
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (Children != null && Children.Count > 0)
            {
                Children.ToList().ForEach(child => child.BindingContext = BindingContext);
            }
        }
    }


    /// <summary>
    /// Represents a block of text with distinct formatting, to be used in a child collection for <see cref="ExtendedLabel"/> 
    /// </summary>
    public class StyledTextPart : View
    {
        /// <summary>
        /// Initializes a new instance of the <c>StyledTextPart</c> class.
        /// </summary>
        public StyledTextPart()
        {
            SetBinding(TextProperty, new Binding(nameof(Text)));
            SetBinding(CustomFontProperty, new Binding(nameof(CustomFont)));
            SetBinding(CommandProperty, new Binding(nameof(Command)));
            SetBinding(CommandParameterProperty, new Binding(nameof(CommandParameter)));
        }

        /// <summary>
        /// Backing store for the <c>Text</c> bindable property.
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(ExtendedLabel), string.Empty);

        /// <summary>
        /// Gets or sets the styled text. This is a bindable property.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }


        /// <summary>
        /// BackingStore for the <c>CustomFont</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CustomFontProperty =
            BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(ExtendedLabel), null);

        /// <summary>
        /// Gets or sets the custom font for the styled text. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the custom font for the styled text part of the label. 
        /// The default value is <c>null</c>.</value>
        public ExtendedFont CustomFont
        {
            get { return (ExtendedFont)GetValue(CustomFontProperty); }
            set { SetValue(CustomFontProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>Command</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ExtendedLabel), default(ICommand));

        /// <summary>
        /// Gets or sets the command to invoke when the label is tapped. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ICommand"/> value that represents the command invoked whenever the label is tapped.</value>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>CommandParameter</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ExtendedLabel));

        /// <summary>
        /// Gets or sets the parameter of the command. This is a bindable property.
        /// </summary>
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
    }
}

