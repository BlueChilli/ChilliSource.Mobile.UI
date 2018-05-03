using ChilliSource.Mobile.UI;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// User input validation behavior that triggers validation of an <see cref="IValidatableObject"/> 
    /// when the parent editing view loses focus.
    /// </summary>
    public class ValidateOnUnFocusBehavior : BaseBehavior<View>
    {
        /// <summary>
        /// Backing store for the <see cref="ValidatableObject"/> bindable property.
        /// </summary>
        public static BindableProperty ValidatableObjectProperty =
            BindableProperty.Create(nameof(ValidatableObject), typeof(IValidatableObject), typeof(ValidateOnUnFocusBehavior));

        /// <summary>
        /// Gets or sets the object that should be validated. This is a bindable property.
        /// </summary>
        /// <value>An instance of <see cref="IValidatableObject"/>class that can be validated.</value>
        public IValidatableObject ValidatableObject
        {
            get { return (IValidatableObject)GetValue(ValidatableObjectProperty); }
            set { SetValue(ValidatableObjectProperty, value); }
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.Unfocused += OnUnFocused;
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Unfocused -= OnUnFocused;
        }

        private void OnUnFocused(object sender, FocusEventArgs e)
        {
            if (ValidatableObject != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await ValidatableObject.ValidateAsync();
                });
            }
        }
    }
}
