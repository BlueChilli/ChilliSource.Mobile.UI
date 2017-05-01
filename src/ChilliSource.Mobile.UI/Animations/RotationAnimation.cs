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
	/// Rotation Animation
	/// </summary>
	public class RotationAnimation : BaseAnimation
	{
		public static readonly BindableProperty RotationAmountProperty =
			BindableProperty.CreateAttached(nameof(RotationAmount), typeof(double), typeof(RotationAnimation), 0.0);

		public double RotationAmount
		{
			get { return (double)GetValue(RotationAmountProperty); }
			set { SetValue(RotationAmountProperty, value); }
		}

		public override Task<Tuple<double, bool>> PerformAnimation()
		{
			AnimationTaskCompletionSource = new TaskCompletionSource<Tuple<double, bool>>();

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
			Animation = new Animation((r) => Control.Rotation = r, 0, RotationAmount, Easing);
			AnimationId = Guid.NewGuid().ToString();
		}
	}
}

