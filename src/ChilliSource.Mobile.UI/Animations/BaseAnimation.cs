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
	/// Base abstract class for behavior animations
	/// </summary>
	public abstract class BaseAnimation : Behavior<View>
	{
		#region Bindable Properties

		public static readonly BindableProperty DurationProperty =
			BindableProperty.CreateAttached(nameof(Duration), typeof(int), typeof(BaseAnimation), 1);

		public int Duration
		{
			get { return (int)GetValue(DurationProperty); }
			set { SetValue(DurationProperty, value); }
		}

		public static readonly BindableProperty EasingProperty =
			BindableProperty.CreateAttached(nameof(Easing), typeof(Easing), typeof(BaseAnimation), null);

		public Easing Easing
		{
			get { return (Easing)GetValue(EasingProperty); }
			set { SetValue(EasingProperty, value); }
		}

		public static readonly BindableProperty ShouldRepeatProperty =
			BindableProperty.CreateAttached(nameof(ShouldRepeat), typeof(bool), typeof(BaseAnimation), false);

		public bool ShouldRepeat
		{
			get { return (bool)GetValue(ShouldRepeatProperty); }
			set { SetValue(ShouldRepeatProperty, value); }
		}

		public static readonly BindableProperty ShouldResetProperty =
			BindableProperty.CreateAttached(nameof(ShouldReset), typeof(bool), typeof(BaseAnimation), false);

		public bool ShouldReset
		{
			get { return (bool)GetValue(ShouldResetProperty); }
			set { SetValue(ShouldResetProperty, value); }
		}

		public static readonly BindableProperty ResetValueProperty =
			BindableProperty.CreateAttached(nameof(ResetValue), typeof(double), typeof(BaseAnimation), 0.0);

		public double ResetValue
		{
			get { return (double)GetValue(ResetValueProperty); }
			set { SetValue(ResetValueProperty, value); }
		}

		#endregion

		protected View Control { get; set; }

		protected Animation Animation { get; set; }

		protected string AnimationId { get; set; }

		protected TaskCompletionSource<Tuple<double, bool>> AnimationTaskCompletionSource { get; set; }

		public abstract Task<Tuple<double, bool>> PerformAnimation();

		public void CancelAnimation()
		{
			Control.AbortAnimation(AnimationId);
		}

		protected override void OnAttachedTo(View bindable)
		{
			Control = bindable;
			base.OnAttachedTo(bindable);

			//Map the Binding Context the view has been attached
			bindable.BindingContextChanged += (sender, e) =>
			{
				BindingContext = ((BindableObject)sender).BindingContext;
			};
		}

		protected override void OnDetachingFrom(View bindable)
		{
			base.OnDetachingFrom(bindable);
		}
	}
}

