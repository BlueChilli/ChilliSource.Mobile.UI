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
    /// Rotation animation with configurable <see cref="RotationDegrees"/> 
    /// in addition to the default <see cref="BaseAnimation"/> properties
    /// </summary>
    public class RotationAnimation : BaseAnimation
    {
        /// <summary>
        /// Backing store for the <see cref="RotationDegrees"/> bindable property.
        /// </summary>
        public static readonly BindableProperty RotationDegreesProperty =
            BindableProperty.CreateAttached(nameof(RotationDegrees), typeof(double), typeof(RotationAnimation), 0.0);

        /// <summary>
        /// Gets or sets the total number of degrees that the parent object should be rotated by. 
        /// A positive value will result in a clockwise rotation, and a negative value in a anti-clockwise rotation. 
        /// This is a bindable property.
        /// </summary>
        /// <value>The rotation degrees; The default value is 0.</value>
        public double RotationDegrees
        {
            get { return (double)GetValue(RotationDegreesProperty); }
            set { SetValue(RotationDegreesProperty, value); }
        }

        /// <summary>
        /// Performs rotation animation
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
                    Control.Rotation = ResetValue;
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
            Animation = new Animation((r) => Control.Rotation = r, 0, RotationDegrees, Easing);
            AnimationId = Guid.NewGuid().ToString();
        }
    }
}

