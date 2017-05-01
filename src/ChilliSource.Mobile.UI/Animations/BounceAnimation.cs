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
	/// Bounce Animation
	/// </summary>
	public class BounceAnimation : BaseAnimation
	{
		int _numberOfBouncesPerformed = 0;

		public static readonly BindableProperty NumberOfBouncesProperty =
			BindableProperty.CreateAttached(nameof(NumberOfBounces), typeof(int), typeof(BounceAnimation), 1);

		public int NumberOfBounces
		{
			get { return (int)GetValue(NumberOfBouncesProperty); }
			set { SetValue(NumberOfBouncesProperty, value); }
		}

		public static readonly BindableProperty BounceHeightProperty =
			BindableProperty.CreateAttached(nameof(BounceHeight), typeof(double), typeof(BounceAnimation), 20.0);

		public double BounceHeight
		{
			get { return (double)GetValue(BounceHeightProperty); }
			set { SetValue(BounceHeightProperty, value); }
		}

		public override Task<Tuple<double, bool>> PerformAnimation()
		{
			AnimationTaskCompletionSource = new TaskCompletionSource<Tuple<double, bool>>();

			Animation.Commit(Control, AnimationId, length: (uint)Duration / 2, finished: FirstAnimationCompleted, repeat: () => false);

			return AnimationTaskCompletionSource.Task;
		}

		public void FirstAnimationCompleted(double arg1, bool arg2)
		{
			new Animation((r) => Control.TranslationY = r, Control.TranslationY, 0, Easing.BounceOut).Commit(Control, AnimationId + "Reset", length: (uint)Duration / 2, finished: (resetArg1, resetArg2) =>
				{
					_numberOfBouncesPerformed++;

					if (!ShouldRepeat && _numberOfBouncesPerformed >= NumberOfBounces)
					{
						_numberOfBouncesPerformed = 0;
						CancelAnimation();

						AnimationTaskCompletionSource.SetResult(new Tuple<double, bool>(arg1, arg2));
					}
					else
					{
						PerformAnimation();
					}
				});
		}

		protected override void OnAttachedTo(View bindable)
		{
			base.OnAttachedTo(bindable);

			Animation = new Animation((r) => Control.TranslationY = r, Control.TranslationY, -BounceHeight, Easing.SinOut);
			AnimationId = Guid.NewGuid().ToString();
		}
	}
}
