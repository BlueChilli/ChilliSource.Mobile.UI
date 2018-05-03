
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Displays a list of error messages
    /// </summary>
    public class ValidationErrorsView : RepeaterView<string>
    {
        /// <summary>
        /// Initializes a new instance of the <c>ValidationErrorsView</c> class.
        /// </summary>
        public ValidationErrorsView()
        {
            this.ItemTemplate = new DataTemplate(() =>
            {
                var validationMessage = new ExtendedLabel
                {
                    TextColor = this.ValidationMessageTextColor,
                    CustomFont = this.ValidationMessageFont
                };

                validationMessage.SetBinding(Label.TextProperty, ".");

                return validationMessage;
            });

        }

        /// <summary>
        /// Backing store for the <c>ValidationMessageTextColor</c> bindable property.
        /// </summary>
        public static BindableProperty ValidationMessageTextColorProperty =
            BindableProperty.Create(nameof(ValidationMessageTextColor), typeof(Color), typeof(ValidationErrorsView), Color.Red);

        /// <summary>
        /// Gets or sets the color for the validation message text. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the color of the validation message text. 
        /// The default value is <see cref="Color.Red"/>.</value>
        public Color ValidationMessageTextColor
        {
            get { return (Color)GetValue(ValidationMessageTextColorProperty); }
            set { SetValue(ValidationMessageTextColorProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>ValidationMessageBackgroundColor</c> bindbale property.
        /// </summary>
        public static BindableProperty ValidationMessageBackgroundColorProperty =
            BindableProperty.Create(nameof(ValidationMessageBackgroundColor), typeof(Color), typeof(ValidationErrorsView), Color.Default);

        /// <summary>
        /// Gets or sets the background color for the validation message. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the color of the validation view's background. 
        /// The default value is <see cref="Color.Default"/>.</value>
        public Color ValidationMessageBackgroundColor
        {
            get { return (Color)GetValue(ValidationMessageBackgroundColorProperty); }
            set { SetValue(ValidationMessageBackgroundColorProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>ValidationMessageFont</c> bindable property.
        /// </summary>
        public static BindableProperty ValidationMessageFontProperty =
            BindableProperty.Create(nameof(ValidationMessageFont), typeof(ExtendedFont), typeof(ValidationErrorsView));

        /// <summary>
        /// Gets or sets the font for the validation message text. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the font for the validation message text.</value>
        public ExtendedFont ValidationMessageFont
        {
            get { return (ExtendedFont)GetValue(ValidationMessageFontProperty); }
            set { SetValue(ValidationMessageFontProperty, value); }
        }


    }
}
