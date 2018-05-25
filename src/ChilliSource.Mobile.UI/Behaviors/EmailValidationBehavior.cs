#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using ChilliSource.Mobile.UI;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Email validation behavior.
    /// </summary>
    public class EmailValidationBehavior : Behavior<Entry>
    {
        EmailRule<string> _validationRule;

        public EmailValidationBehavior()
        {
            _validationRule = new EmailRule<string>("Email is invalid");
        }

        /// <summary>
        /// Identifies the <see cref="IsValid"/> bindable property.
        /// </summary>
        public static readonly BindableProperty IsValidProperty =
        BindableProperty.CreateAttached(nameof(IsValid), typeof(bool), typeof(EmailValidationBehavior), false);

        /// <summary>
        /// Gets a value indicating whether the email address is valid. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if the email address is valid; otherwise, <c>false</c>. The default value is <c>false</c>.</value>
        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            private set { SetValue(IsValidProperty, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            IsValid = _validationRule.Validate(e.NewTextValue);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
        }
    }
}
