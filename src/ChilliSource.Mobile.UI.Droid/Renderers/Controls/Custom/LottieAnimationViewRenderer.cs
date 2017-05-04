#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Android.Widget;
using ChilliSource.Mobile.UI;
using Com.Airbnb.Lottie;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Runtime.InteropServices;
using Com.Airbnb.Lottie.Layers;

[assembly: ExportRenderer(typeof(AfterEffectsAnimationView), typeof(LottieAnimationViewRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class LottieAnimationViewRenderer : ViewRenderer<AfterEffectsAnimationView, LottieAnimationView>, ILottieAnimation
	{
		LottieAnimationView _animationView;
		AnimationCompleted _animationCompletedEvent;

		protected override void OnElementChanged(ElementChangedEventArgs<AfterEffectsAnimationView> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null || Element == null)
			{
				return;
			}

			SetupAnimationView();

			Element.Animation = this;
		}

		void SetupAnimationView()
		{
			_animationView = new LottieAnimationView(Context);

			switch (Element.SourceType)
			{
				case LottieSourceType.LocalResource:
					_animationView.SetAnimation(Element.Source);

					break;

				case LottieSourceType.URL:
					_animationView.SetAnimation(Element.Source, LottieAnimationView.CacheStrategy.Weak);
					break;
			}

			if (_animationView == null)
			{
				Console.WriteLine("LOTTIE ANIMATION - Could not load animation");
				throw new NullReferenceException();
			}

			switch (Element.ContentFit)
			{
				case Aspect.AspectFit:
					_animationView.SetScaleType(ImageView.ScaleType.FitCenter);
					break;

				case Aspect.AspectFill:
					_animationView.SetScaleType(ImageView.ScaleType.FitXy);
					break;

				case Aspect.Fill:
					_animationView.SetScaleType(ImageView.ScaleType.CenterCrop);
					break;
			}

			_animationView.Loop(Element.LoopAnimation);
			SetNativeControl(_animationView);

			if (Element.ShouldPlayOnLoad)
			{
				_animationView.PlayAnimation();
			}
		}


		public void Pause()
		{
			_animationView.PauseAnimation();
		}

		public void Play()
		{
			_animationView.PlayAnimation();
		}

		public void Play(AnimationCompleted completed)
		{
			_animationCompletedEvent = completed;
			_animationView.PlayAnimation();
		}

		public void Cancel()
		{
			_animationView.CancelAnimation();
		}

		protected override void OnAnimationEnd()
		{
			base.OnAnimationEnd();

			if (_animationCompletedEvent != null)
			{
				_animationCompletedEvent.Invoke(true);
			}
		}

		public void MaskViewToAnimationLayer(View view, string layer)
		{
			//
		}

		public float Duration
		{
			get
			{
				return _animationView.Duration;
			}
		}

		public bool IsPlaying
		{
			get
			{
				return _animationView.IsAnimating;
			}
		}

		public float Progress
		{
			get
			{
				return _animationView.Progress;
			}

			set
			{
				_animationView.Progress = value;
			}
		}

		public float Speed
		{
			get
			{
				return 1;
			}

			set
			{
			}
		}
	}
}
