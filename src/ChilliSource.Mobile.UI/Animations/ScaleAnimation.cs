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

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Scale Animation
	/// </summary>
	public class ScaleAnimation : BaseAnimation
	{
		public static readonly BindableProperty ScaleAmountProperty =
			BindableProperty.CreateAttached(nameof(ScaleAmount), typeof(double), typeof(ScaleAnimation), 0.0);

		public double ScaleAmount
		{
			get { return (double)GetValue(ScaleAmountProperty); }
			set { SetValue(ScaleAmountProperty, value); }
		}

		public override Task<Tuple<double, bool>> PerformAnimation()
		{
			AnimationTaskCompletionSource = new TaskCompletionSource<Tuple<double, bool>>();

			Animation.Commit(Control, AnimationId, length: (uint)Duration, finished: (arg1, arg2) =>
			{
				if (ShouldReset)
				{
					Control.Scale = ResetValue;
				}

				AnimationTaskCompletionSource.SetResult(new Tuple<double, bool>(arg1, arg2));

			}, repeat: () => ShouldRepeat);

			return AnimationTaskCompletionSource.Task;
		}

		protected override void OnAttachedTo(View bindable)
		{
			base.OnAttachedTo(bindable);
			Animation = new Animation((r) => Control.Scale = r, 0, ScaleAmount, Easing);
			AnimationId = Guid.NewGuid().ToString();
		}
	}
}
