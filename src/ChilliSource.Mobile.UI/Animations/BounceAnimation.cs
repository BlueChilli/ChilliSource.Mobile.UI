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
    /// Bounce animantion with configurable <see cref="NumberOfBounces"/> and <see cref="BounceHeight"/>
    /// in addition to the default <see cref="BaseAnimation"/> properties
    /// </summary>
    public class BounceAnimation : BaseAnimation
    {
        int _numberOfBouncesPerformed = 0;

        /// <summary>
        /// Backing store for the <see cref="NumberOfBounces"/> bindable property.
        /// </summary>
        public static readonly BindableProperty NumberOfBouncesProperty =
            BindableProperty.CreateAttached(nameof(NumberOfBounces), typeof(int), typeof(BounceAnimation), 1);

        /// <summary>
        /// Gets or sets the number of bounces during the animation. This is a bindable property.
        /// </summary>
        /// <value>The number of bounces; the default is 1.</value>
        public int NumberOfBounces
        {
            get { return (int)GetValue(NumberOfBouncesProperty); }
            set { SetValue(NumberOfBouncesProperty, value); }
        }

        /// <summary>
        /// Backing store for the <see cref="BounceHeight"/> bindable property.
        /// </summary>
        public static readonly BindableProperty BounceHeightProperty =
            BindableProperty.CreateAttached(nameof(BounceHeight), typeof(double), typeof(BounceAnimation), 20.0);

        /// <summary>
        /// Gets or sets the height of bounces during the animation. This is a bindable property.
        /// </summary>
        /// <value>A <c>double</c> that represents the height of bounces; the default is 20.</value>
        public double BounceHeight
        {
            get { return (double)GetValue(BounceHeightProperty); }
            set { SetValue(BounceHeightProperty, value); }
        }

        /// <summary>
        /// Performs bounce animation.
        /// </summary>
        /// <remarks>
        /// Initializes a new instance of the animation every time that it is called.
        /// </remarks>
        /// <returns>Returns an instance of <see cref="OperationResult"/> that represents the outcome of the animation 
        /// and stores a <c>finalValue</c> representing the completed state of the animation.</returns>
        public override Task<OperationResult<double>> PerformAnimation()
        {
            AnimationTaskCompletionSource = new TaskCompletionSource<OperationResult<double>>();
            CreateAndStartBounceAnimation();
            return AnimationTaskCompletionSource.Task;
        }

        private Animation CreateBounceAnimation()
        {
            var animation = new Animation();
            animation.Add(0, 0.5, new Animation((r) => { Control.TranslationY = r; }, 0, -BounceHeight, Easing.SinOut));
            animation.Add(0.5, 1, new Animation((r) => { Control.TranslationY = r; }, -BounceHeight, 0, Easing.BounceOut));
            return animation;
        }

        private void CreateAndStartBounceAnimation()
        {
            Animation = CreateBounceAnimation();
            Animation.Commit(Control, AnimationId, length: (uint)Duration, finished: OnAnimationCompleted, repeat: () => false);
            _numberOfBouncesPerformed++;
        }

        private void OnAnimationCompleted(double finalValue, bool wasCancelled)
        {
            if (wasCancelled)
            {
                _numberOfBouncesPerformed = 0;
                Control.TranslationY = 0;
                AnimationTaskCompletionSource.SetResult(OperationResult<double>.AsCancel());
            }
            else if (_numberOfBouncesPerformed == NumberOfBounces)
            {
                _numberOfBouncesPerformed = 0;
                AnimationTaskCompletionSource.SetResult(OperationResult<double>.AsSuccess(finalValue));
            }
            else
            {
                CreateAndStartBounceAnimation();
            }


        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            AnimationId = Guid.NewGuid().ToString();
        }
    }
}
