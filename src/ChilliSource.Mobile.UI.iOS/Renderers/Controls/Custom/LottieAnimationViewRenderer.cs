#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms.Platform.iOS;
using Airbnb.Lottie;
using Xamarin.Forms;
using ChilliSource.Mobile.UI;
using UIKit;
using Foundation;

[assembly: ExportRenderer(typeof(AfterEffectsAnimationView), typeof(LottieAnimationViewRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class LottieAnimationViewRenderer : ViewRenderer<AfterEffectsAnimationView, LAAnimationView>, ILottieAnimation
	{
		LAAnimationView _animationView;

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
			switch (Element.SourceType)
			{
				case LottieSourceType.LocalResource:
					_animationView = LAAnimationView.AnimationNamed(Element.Source);

					break;

				case LottieSourceType.URL:
					_animationView = new LAAnimationView(NSUrl.FromString(Element.Source));
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
					_animationView.ContentMode = UIViewContentMode.ScaleAspectFit;
					break;

				case Aspect.AspectFill:
					_animationView.ContentMode = UIViewContentMode.ScaleAspectFill;
					break;

				case Aspect.Fill:
					_animationView.ContentMode = UIViewContentMode.ScaleToFill;
					break;
			}

			_animationView.LoopAnimation = Element.LoopAnimation;
			SetNativeControl(_animationView);

			if (Element.ShouldPlayOnLoad)
			{
				_animationView.Play();
			}
		}

		#region Interface Implementation

		public void Play()
		{
			_animationView.Play();
		}

		public void Play(AnimationCompleted completed)
		{
			_animationView.PlayWithCompletion((comp) =>
			{
				completed.Invoke(comp);
			});
		}

		public void Pause()
		{
			_animationView.Pause();
		}

		public void Cancel()
		{
			//
		}

		public void MaskViewToAnimationLayer(View view, string layer)
		{
			_animationView.AddSubview(view.ConvertToNative(_animationView.Frame), layer);
		}

		public bool IsPlaying
		{
			get
			{
				return _animationView.IsAnimationPlaying;
			}
		}

		public float Progress
		{
			get
			{
				return (float)_animationView.AnimationProgress;
			}

			set
			{
				_animationView.AnimationProgress = value;
			}
		}

		public float Speed
		{
			get
			{
				return (float)_animationView.AnimationSpeed;
			}

			set
			{
				_animationView.AnimationSpeed = value;
			}
		}

		public float Duration
		{
			get
			{
				return (float)_animationView.AnimationDuration;
			}
		}

		#endregion
	}
}
