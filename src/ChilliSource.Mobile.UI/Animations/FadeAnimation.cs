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
	/// Fade Animation
	/// </summary>
	public class FadeAnimation : BaseAnimation
	{
		public static readonly BindableProperty FadeProperty =
			BindableProperty.CreateAttached(nameof(Fade), typeof(AnimationFadeType), typeof(FadeAnimation), AnimationFadeType.Out);

		public AnimationFadeType Fade
		{
			get { return (AnimationFadeType)GetValue(FadeProperty); }
			set { SetValue(FadeProperty, value); }
		}

		public override Task<Tuple<double, bool>> PerformAnimation()
		{
			AnimationTaskCompletionSource = new TaskCompletionSource<Tuple<double, bool>>();

			Animation.Commit(Control, AnimationId, length: (uint)Duration, finished: (arg1, arg2) =>
			{
				if (ShouldReset)
				{
					Control.Opacity = ResetValue;
				}

				AnimationTaskCompletionSource.SetResult(new Tuple<double, bool>(arg1, arg2));

			}, repeat: () => ShouldRepeat);

			return AnimationTaskCompletionSource.Task;
		}

		protected override void OnAttachedTo(View bindable)
		{
			base.OnAttachedTo(bindable);

			switch (Fade)
			{
				case AnimationFadeType.In:
					Animation = new Animation((r) => Control.Opacity = r, 0, 1, Easing);
					ResetValue = 0;
					break;

				case AnimationFadeType.Out:
					Animation = new Animation((r) => Control.Opacity = r, 1, 0, Easing);
					ResetValue = 1;
					break;
			}

			AnimationId = Guid.NewGuid().ToString();
		}
	}
}
