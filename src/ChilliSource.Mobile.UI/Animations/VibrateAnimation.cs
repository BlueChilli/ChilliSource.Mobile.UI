#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Vibrate Animation
	/// </summary>
	public class VibrateAnimation : BaseAnimation
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

			Animation.Commit(Control, AnimationId, length: GetVibrateDuration(), finished: (arg1, cancelled) =>
			{
				Control.TranslationX = cancelled ? 0 : -GetVibrateXChange();

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
			Animation = new Animation((r) => Control.TranslationX = r, 0, GetVibrateXChange(), Easing.CubicOut);
			AnimationId = Guid.NewGuid().ToString();
		}

		uint GetVibrateDuration()
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

		int GetVibrateXChange()
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
