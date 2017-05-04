#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public delegate void AnimationCompleted(bool completed);

	/// <summary>
	/// Lottie Controls Interface.
	/// </summary>
	public interface ILottieAnimation
	{
		void Play();

		void Play(AnimationCompleted completed);

		void Pause();

		void Cancel();

		void MaskViewToAnimationLayer(View view, string layer);

		bool IsPlaying { get; }

		float Progress { get; set; }

		float Speed { get; set; }

		float Duration { get; }
	}
}
