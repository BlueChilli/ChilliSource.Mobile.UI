#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using ChilliSource.Mobile.Core;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Scaling animation with configurable <see cref="ScaleRatio"/>
    /// in addition to the default <see cref="BaseAnimation"/> properties
    /// </summary>
    public class ScaleAnimation : BaseAnimation
    {
        /// <summary>
        /// Backing store for <see cref="ScaleRatio"/> bindable property.
        /// </summary>
        public static readonly BindableProperty ScaleRatioProperty =
            BindableProperty.CreateAttached(nameof(ScaleRatio), typeof(double), typeof(ScaleAnimation), 0.0);

        /// <summary>
        /// Gets or sets the ratio for the scaling. E.g. a value of 2 will double the size of the parent object, 
        /// and a value of 0.5 will halve the size. This is a bindable property.
        /// </summary>
        /// <value>The scale amount of the animation; the default is 0.</value>
        public double ScaleRatio
        {
            get { return (double)GetValue(ScaleRatioProperty); }
            set { SetValue(ScaleRatioProperty, value); }
        }

        /// <summary>
        /// Performs scaling animation
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
                    Control.Scale = ResetValue;
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
            Animation = new Animation((r) => Control.Scale = r, 0, ScaleRatio, Easing);
            AnimationId = Guid.NewGuid().ToString();
        }
    }
}
