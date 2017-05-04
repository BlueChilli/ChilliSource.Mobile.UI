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
using System.Timers;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Shake Animation
	/// </summary>
	public class ShakeAnimation : BaseAnimation
	{
		public static readonly BindableProperty IntensityProperty =
			BindableProperty.CreateAttached(nameof(Intensity), typeof(AnimationIntensityType), typeof(ShakeAnimation), AnimationIntensityType.Default);

		public AnimationIntensityType Intensity
		{
			get { return (AnimationIntensityType)GetValue(IntensityProperty); }
			set { SetValue(IntensityProperty, value); }
		}

		public override Task<Tuple<double, bool>> PerformAnimation()
		{
			AnimationTaskCompletionSource = new TaskCompletionSource<Tuple<double, bool>>();

			Animation.Commit(Control, AnimationId, length: GetShakeDuration(), finished: (arg1, cancelled) =>
			{
				Control.Rotation = cancelled ? 0 : -GetShakeAngleChange();

				if (cancelled)
				{
					AnimationTaskCompletionSource.SetResult(new Tuple<double, bool>(arg1, cancelled));
				}

			}, repeat: () => true);

			var timer = new Timer(Duration);
			timer.Elapsed += (sender, e) =>
			{
				CancelAnimation();
			};
			timer.Start();

			return AnimationTaskCompletionSource.Task;
		}

		protected override void OnAttachedTo(View bindable)
		{
			base.OnAttachedTo(bindable);
			Animation = new Animation((r) => Control.Rotation = r, 0, GetShakeAngleChange(), Easing.CubicOut);
			AnimationId = Guid.NewGuid().ToString();
		}

		uint GetShakeDuration()
		{
			switch (Intensity)
			{
				case AnimationIntensityType.Default:
					return (uint)100;

				case AnimationIntensityType.Vigorous:
					return (uint)60;

				case AnimationIntensityType.Soft:
					return (uint)140;
			}

			return (uint)100;
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
