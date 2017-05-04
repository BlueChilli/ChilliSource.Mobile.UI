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
	public enum LottieSourceType
	{
		LocalResource,
		URL
	}

	/// <summary>
	/// Implementation of Lottie animation view. Lottie displays an animation from a JSON file.
	/// </summary>
	public class AfterEffectsAnimationView : View
	{
		public ILottieAnimation Animation { get; set; }

		public static readonly BindableProperty SourceProperty =
			BindableProperty.Create(nameof(Source), typeof(string), typeof(AfterEffectsAnimationView), defaultValue: "");

		public string Source
		{
			get { return (string)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}

		public bool ShouldPlayOnLoad
		{
			get;
			set;
		}

		public LottieSourceType SourceType
		{
			get; set;
		}

		public Aspect ContentFit
		{
			get; set;
		}

		public bool LoopAnimation
		{
			get; set;
		}
	}
}

