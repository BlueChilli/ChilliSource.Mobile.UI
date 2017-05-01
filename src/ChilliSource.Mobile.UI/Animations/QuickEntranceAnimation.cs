#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Quick Entrance Animation
	/// </summary>
	public class QuickEntranceAnimation : BaseAnimation
	{
		static readonly Easing CustomEasing = new Easing(x => x < 0.5f ? Math.Pow(x * 2.0f, 3.0f) / 2.0f : Math.Pow(x - 1.0f, 3.0f) + 1.0f);

		public static readonly BindableProperty EntranceDirectionProperty =
			BindableProperty.CreateAttached(nameof(EntranceDirection), typeof(AnimationQuickEntranceDirection), typeof(QuickEntranceAnimation), AnimationQuickEntranceDirection.Left);

		public AnimationQuickEntranceDirection EntranceDirection
		{
			get { return (AnimationQuickEntranceDirection)GetValue(EntranceDirectionProperty); }
			set { SetValue(EntranceDirectionProperty, value); }
		}

		public static readonly BindableProperty ParentViewBoundsProperty =
			BindableProperty.CreateAttached(nameof(ParentViewBounds), typeof(Rectangle), typeof(QuickEntranceAnimation), new Rectangle(0, 0, 0, 0));

		public Rectangle ParentViewBounds
		{
			get { return (Rectangle)GetValue(ParentViewBoundsProperty); }
			set
			{
				SetupAnimation();
				SetValue(ParentViewBoundsProperty, value);
			}
		}

		public override Task<Tuple<double, bool>> PerformAnimation()
		{
			AnimationTaskCompletionSource = new TaskCompletionSource<Tuple<double, bool>>();

			Control.Opacity = 1;

			Animation.Commit(Control, AnimationId, length: (uint)Duration, finished: (arg1, arg2) =>
			{
				if (ShouldReset)
				{
					Control.Rotation = ResetValue;
				}

				AnimationTaskCompletionSource.SetResult(new Tuple<double, bool>(arg1, arg2));

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
				Control.TranslationX = GetInitialValue();
				Animation = new Animation((r) => Control.TranslationX = r, Control.TranslationX, 0, CustomEasing);
			}
			else
			{
				Control.TranslationY = GetInitialValue();
				Animation = new Animation((r) => Control.TranslationY = r, Control.TranslationY, 0, CustomEasing);
			}
		}

		double GetInitialValue()
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
