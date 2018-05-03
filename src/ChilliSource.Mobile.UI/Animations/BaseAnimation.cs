#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms;
using System.Threading.Tasks;
using ChilliSource.Mobile.Core;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Base abstract class for user-defined behaviors of animations.
    /// </summary>
    public abstract class BaseAnimation : Behavior<View>
    {
        #region Bindable Properties

        /// <summary>
        /// Backing store for the <see cref="Duration"/> bindable property.
        /// </summary>
        public static readonly BindableProperty DurationProperty =
            BindableProperty.CreateAttached(nameof(Duration), typeof(int), typeof(BaseAnimation), 1);

        /// <summary>
        /// Gets or sets the duration of the animation in milliseconds. This is a bindable property.
        /// </summary>
        /// <value>The duration of the animation; the default is 1.</value>
        public virtual int Duration
        {
            get { return (int)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// Backing store for the <see cref="Easing"/> bindable property.
        /// </summary>
        public static readonly BindableProperty EasingProperty =
            BindableProperty.CreateAttached(nameof(Easing), typeof(Easing), typeof(BaseAnimation), null);

        /// <summary>
        /// Gets or sets the <see cref="Xamarin.Forms.Easing"/> function of the animation. This is a bindable property.
        /// </summary>
        /// <value>The <see cref="Xamarin.Forms.Easing"/> function of the animation; the default is <c>null</c>.</value>
        public Easing Easing
        {
            get { return (Easing)GetValue(EasingProperty); }
            set { SetValue(EasingProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="ShouldRepeat"/> bindable property.
        /// </summary>
        public static readonly BindableProperty ShouldRepeatProperty =
            BindableProperty.CreateAttached(nameof(ShouldRepeat), typeof(bool), typeof(BaseAnimation), false);

        /// <summary>
        /// Gets or sets a value indicating whether this animation should repeat. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if the animation should repeat; otherwise, <c>false</c>. The default value is <c>false</c>.</value>
        public bool ShouldRepeat
        {
            get { return (bool)GetValue(ShouldRepeatProperty); }
            set { SetValue(ShouldRepeatProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="ShouldReset"/> bindable property.
        /// </summary>
        public static readonly BindableProperty ShouldResetProperty =
            BindableProperty.CreateAttached(nameof(ShouldReset), typeof(bool), typeof(BaseAnimation), false);

        /// <summary>
        /// Gets or sets a value indicating whether this animation should reset. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if the animation should reset; otherwise, <c>false</c>. The default value is <c>false</c>.</value>
        public bool ShouldReset
        {
            get { return (bool)GetValue(ShouldResetProperty); }
            set { SetValue(ShouldResetProperty, value); }
        }

        /// <summary>
        /// Backing store for the <see cref="ResetValue"/>  bindable property.
        /// </summary>
        protected static readonly BindableProperty ResetValueProperty =
            BindableProperty.CreateAttached(nameof(ResetValue), typeof(double), typeof(BaseAnimation), 0.0);

        /// <summary>
        /// Gets or sets the initial state of the property that is modified through the animation. This is a bindable property.
        /// </summary>
        /// <remarks>
        /// Once the animation is completed the property will be set to the ResetValue.
        /// </remarks>
        /// <value>The reset value of the animation; the default is 0. </value>
        protected double ResetValue
        {
            get { return (double)GetValue(ResetValueProperty); }
            set { SetValue(ResetValueProperty, value); }
        }

        #endregion

        protected View Control { get; set; }

        protected Animation Animation { get; set; }

        protected string AnimationId { get; set; }

        protected TaskCompletionSource<OperationResult<double>> AnimationTaskCompletionSource { get; set; }

        /// <summary>
        /// Override to specify custom animation execution logic.
        /// </summary>
        /// <returns>Returns an instance of <see cref="OperationResult"/> that represents the outcome of the animation 
        /// and stores a <c>finalValue</c> representing the completed state of the animation.</returns>
        public abstract Task<OperationResult<double>> PerformAnimation();

        /// <summary>
        /// Cancels the animation.
        /// </summary>
        public void CancelAnimation()
        {
            Control.AbortAnimation(AnimationId);
        }

        protected override void OnAttachedTo(View bindable)
        {
            Control = bindable;
            base.OnAttachedTo(bindable);

            //Map the Binding Context the view has been attached to
            bindable.BindingContextChanged += (sender, e) =>
            {
                BindingContext = ((BindableObject)sender).BindingContext;
            };
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
        }
    }
}

