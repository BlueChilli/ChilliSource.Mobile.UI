#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using ChilliSource.Mobile.Core;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Specifies the fading type for <see cref="FadeAnimation"/>.
    /// </summary>
    public enum AnimationFadeType
    {
        In,
        Out
    }

    /// <summary>
    /// Fade animation with configurable <see cref="AnimationFadeType"/>
    /// in addition to the default <see cref="BaseAnimation"/> properties
    /// </summary>
    public class FadeAnimation : BaseAnimation
    {
        /// <summary>
        /// Backing store for the <c>Fade</c> bindable property.
        /// </summary>
        public static readonly BindableProperty FadeProperty =
            BindableProperty.CreateAttached(nameof(Fade), typeof(AnimationFadeType), typeof(FadeAnimation), AnimationFadeType.Out);

        /// <summary>
        /// Gets or sets the fading type of the animation. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="AnimationFadeType"/> value that represents the fade type of the animation. 
        /// The default value is <see cref="AnimationFadeType.Out"/>.</value>
        public AnimationFadeType Fade
        {
            get { return (AnimationFadeType)GetValue(FadeProperty); }
            set { SetValue(FadeProperty, value); }
        }

        /// <summary>
        /// Performs fade animation.
        /// </summary>
        /// <returns>Returns an instance of <see cref="OperationResult"/> that represents the outcome of the animation 
        /// and stores a <c>finalValue</c> representing the completed state of the animation.</returns>
        public override Task<OperationResult<double>> PerformAnimation()
        {
            AnimationTaskCompletionSource = new TaskCompletionSource<OperationResult<double>>();

            Animation.Commit(Control, AnimationId, length: (uint)Duration, finished: (finalValue, wasCancelled) =>
            {
                if (ShouldReset)
                {
                    Control.Opacity = ResetValue;
                }
                if (wasCancelled)
                {
                    AnimationTaskCompletionSource.SetResult(OperationResult<double>.AsCancel());
                }
                else
                {
                    AnimationTaskCompletionSource.SetResult(OperationResult<double>.AsSuccess(finalValue));
                }


            }, repeat: () => ShouldRepeat);

            return AnimationTaskCompletionSource.Task;
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);

            switch (Fade)
            {
                case AnimationFadeType.In:
                    Animation = new Animation((r) => Control.Opacity = r, 0, 1, Easing);
                    ResetValue = 0;
                    break;

                case AnimationFadeType.Out:
                    Animation = new Animation((r) => Control.Opacity = r, 1, 0, Easing);
                    ResetValue = 1;
                    break;
            }

            AnimationId = Guid.NewGuid().ToString();
        }
    }
}
