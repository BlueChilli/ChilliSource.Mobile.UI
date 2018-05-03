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
    /// Specifies the direction of entry for <see cref="QuickEntranceAnimation"/>
    /// </summary>
    public enum AnimationQuickEntranceDirection
    {
        Left,
        Top,
        Right,
        Bottom
    }

    /// <summary>
    /// Quick entrance animation with configurable <see cref="AnimationQuickEntranceDirection"/> 
    /// and <see cref="ParentViewBounds"/> in addition to the default <see cref="BaseAnimation"/> properties
    /// </summary>
    public class QuickEntranceAnimation : BaseAnimation
    {
        static readonly Easing CustomEasing = new Easing(x => x < 0.5f ? Math.Pow(x * 2.0f, 3.0f) / 2.0f : Math.Pow(x - 1.0f, 3.0f) + 1.0f);

        /// <summary>
        /// Backing store for the <c>EntranceDirection</c> bindable property.
        /// </summary>
        public static readonly BindableProperty EntranceDirectionProperty =
            BindableProperty.CreateAttached(nameof(EntranceDirection), typeof(AnimationQuickEntranceDirection), typeof(QuickEntranceAnimation), AnimationQuickEntranceDirection.Left);

        /// <summary>
        /// Gets or sets the entrance direction of the quick entrance animation. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="AnimationQuickEntranceDirection"/> value that represents the entrance direction of the animation. 
        /// The default value is <see cref="AnimationQuickEntranceDirection.Left"/>.</value>
        public AnimationQuickEntranceDirection EntranceDirection
        {
            get { return (AnimationQuickEntranceDirection)GetValue(EntranceDirectionProperty); }
            set { SetValue(EntranceDirectionProperty, value); }
        }

        /// <summary>
        /// Backing store for the <c>ParentViewBounds</c> bindable property.
        /// </summary>
        public static readonly BindableProperty ParentViewBoundsProperty =
            BindableProperty.CreateAttached(nameof(ParentViewBounds), typeof(Rectangle), typeof(QuickEntranceAnimation), new Rectangle(0, 0, 0, 0));

        /// <summary>
        /// Gets or sets the dimensions of the parent view of the control to be animated. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Rectangle"/> object that represents bounds to the parent view. The default value is 0.</value>
        public Rectangle ParentViewBounds
        {
            get { return (Rectangle)GetValue(ParentViewBoundsProperty); }
            set
            {
                SetupAnimation();
                SetValue(ParentViewBoundsProperty, value);
            }
        }

        /// <summary>
        /// Performs quick entrance animation
        /// </summary>
        /// <returns>Returns an instance of <see cref="OperationResult"/> that represents the outcome of the animation 
        /// and stores a <c>finalValue</c> representing the completed state of the animation.</returns>
        public override Task<OperationResult<double>> PerformAnimation()
        {
            AnimationTaskCompletionSource = new TaskCompletionSource<OperationResult<double>>();

            Control.Opacity = 1;

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
            AnimationId = Guid.NewGuid().ToString();
            Control.Opacity = 0;
        }


        void SetupAnimation()
        {
            if (ShouldTranslateOnXAxis())
            {
                Control.TranslationX = GetInitialTranslationValue();
                Animation = new Animation((r) => Control.TranslationX = r, Control.TranslationX, 0, CustomEasing);
            }
            else
            {
                Control.TranslationY = GetInitialTranslationValue();
                Animation = new Animation((r) => Control.TranslationY = r, Control.TranslationY, 0, CustomEasing);
            }
        }


        double GetInitialTranslationValue()
        {
            if (ShouldTranslateOnXAxis())
            {
                if (EntranceDirection == AnimationQuickEntranceDirection.Left)
                {
                    return -(Control.X + Control.Width * 2);
                }
                else
                {
                    return ParentViewBounds.Size.Width + Control.X;
                }
            }
            else
            {
                if (EntranceDirection == AnimationQuickEntranceDirection.Top)
                {
                    return -(Control.Y + Control.Height * 2);
                }
                else
                {
                    return ParentViewBounds.Size.Height + Control.Y;
                }
            }
        }

        bool ShouldTranslateOnXAxis()
        {
            return EntranceDirection == AnimationQuickEntranceDirection.Left || EntranceDirection == AnimationQuickEntranceDirection.Right;
        }
    }
}
