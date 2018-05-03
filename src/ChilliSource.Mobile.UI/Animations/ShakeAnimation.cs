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
    /// Shake animation with configurable <see cref="Intensity"/>, <see cref="RepeatCount"/> 
    /// in addition to the default <see cref="BaseAnimation"/> properties
    /// </summary>
    public class ShakeAnimation : BaseAnimation
    {
        int _repeatCount;

        /// <summary>
        /// Backing store for <see cref="Intensity"/> bindable property.
        /// </summary>
        public static readonly BindableProperty IntensityProperty =
            BindableProperty.CreateAttached(nameof(Intensity), typeof(AnimationIntensityType), typeof(ShakeAnimation), AnimationIntensityType.Default);

        /// <summary>
        /// Gets or sets the intensity type of the shake animation. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="AnimationIntensityType"/> value that represents the intensity of the shake animation. 
        /// The default value is <see cref="AnimationIntensityType.Default"/>.</value>
        public AnimationIntensityType Intensity
        {
            get { return (AnimationIntensityType)GetValue(IntensityProperty); }
            set { SetValue(IntensityProperty, value); }
        }

        /// <summary>
        /// Backing store for the <see cref="RepeatCount"/> bindable property.
        /// </summary>
        public static readonly BindableProperty RepeatCountProperty =
            BindableProperty.CreateAttached(nameof(RepeatCount), typeof(int), typeof(ShakeAnimation), 5);

        /// <summary>
        /// Gets or sets the number of times that the animation should be repeated. This is a bindable proeprty.
        /// </summary>
        /// <value>The number of times to repeat the animation; the default is 5.</value>
        public int RepeatCount
        {
            get { return (int)GetValue(RepeatCountProperty); }
            set { SetValue(RepeatCountProperty, value); }
        }

        /// <summary>
        /// Returns the duration value represented by the <see cref="Intensity"/> property.		
        /// </summary>
        public override int Duration
        {
            get
            {
                return GetShakeDuration();
            }
        }

        /// <summary>
        /// Performs shake animation
        /// </summary>
        /// <returns>Returns an instance of <see cref="OperationResult"/> that represents the outcome of the animation 
        /// and stores a <c>finalValue</c> representing the completed state of the animation.</returns>
        public override Task<OperationResult<double>> PerformAnimation()
        {
            AnimationTaskCompletionSource = new TaskCompletionSource<OperationResult<double>>();

            _repeatCount = 0;


            Animation.Commit(Control, AnimationId, length: (uint)GetShakeDuration(), finished: (finalValue, wasCancelled) =>
           {

               if (_repeatCount == RepeatCount)
               {
                   Control.Rotation = 0;
                   AnimationTaskCompletionSource.SetResult(OperationResult<double>.AsSuccess(finalValue));
               }
               else
               {
                   Control.Rotation = -GetShakeAngleChange();
               }

               if (wasCancelled)
               {
                   Control.Rotation = 0;
                   AnimationTaskCompletionSource.SetResult(OperationResult<double>.AsCancel());
               }


           }, repeat: () => ++_repeatCount < RepeatCount);

            return AnimationTaskCompletionSource.Task;
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            Animation = new Animation((r) => Control.Rotation = r, 0, GetShakeAngleChange(), Easing.CubicOut);
            AnimationId = Guid.NewGuid().ToString();
        }

        int GetShakeDuration()
        {
            switch (Intensity)
            {
                case AnimationIntensityType.Default:
                    return 100;

                case AnimationIntensityType.Vigorous:
                    return 60;

                case AnimationIntensityType.Soft:
                    return 140;
            }

            return 100;
        }

        int GetShakeAngleChange()
        {
            switch (Intensity)
            {
                case AnimationIntensityType.Default:
                    return 5;

                case AnimationIntensityType.Vigorous:
                    return 8;

                case AnimationIntensityType.Soft:
                    return 2;
            }

            return 5;
        }
    }
}
