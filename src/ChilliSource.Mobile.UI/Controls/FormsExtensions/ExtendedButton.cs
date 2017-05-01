#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Windows.Input;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public enum ButtonHorizontalContentAlignment
	{
		Left,
		Right,
		Center
	}

	public enum LongPressDirection
	{
		LeftToRight,
		TopToBotton,
		RightToLeft,
		BottomToTop
	}

	public class ExtendedButton : Button
	{
		public static readonly BindableProperty PressedBackgroundColorProperty =
			BindableProperty.Create(nameof(PressedBackgroundColor), typeof(Color), typeof(ExtendedButton), Color.Default);

		public Color PressedBackgroundColor
		{
			get { return (Color)GetValue(PressedBackgroundColorProperty); }
			set { SetValue(PressedBackgroundColorProperty, value); }
		}

		public static readonly BindableProperty DisabledBackgroundColorProperty =
			BindableProperty.Create(nameof(DisabledBackgroundColor), typeof(Color), typeof(ExtendedButton), Color.Default);

		public Color DisabledBackgroundColor
		{
			get { return (Color)GetValue(DisabledBackgroundColorProperty); }
			set { SetValue(DisabledBackgroundColorProperty, value); }
		}
		public static readonly BindableProperty EnabledProperty =
			BindableProperty.Create(nameof(Enabled), typeof(bool), typeof(ExtendedButton), true);

		public bool Enabled
		{
			get { return (bool)GetValue(EnabledProperty); }
			set { SetValue(EnabledProperty, value); }
		}

		public static readonly BindableProperty IsCustomButtonProperty =
				BindableProperty.Create(nameof(IsCustomButton), typeof(bool), typeof(ExtendedButton), false);

		/// <summary>
		/// Enable custom button type for iOS
		/// </summary>
		public bool IsCustomButton
		{
			get { return (bool)GetValue(IsCustomButtonProperty); }
			set { SetValue(IsCustomButtonProperty, value); }
		}

		public static readonly BindableProperty CustomFontProperty =
			BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(ExtendedButton), null);

		public ExtendedFont CustomFont
		{
			get { return (ExtendedFont)GetValue(CustomFontProperty); }
			set { SetValue(CustomFontProperty, value); }
		}

		public static readonly BindableProperty PressedCustomFontProperty =
			BindableProperty.Create(nameof(PressedCustomFont), typeof(ExtendedFont), typeof(ExtendedButton), null);

		public ExtendedFont PressedCustomFont
		{
			get { return (ExtendedFont)GetValue(PressedCustomFontProperty); }
			set { SetValue(PressedCustomFontProperty, value); }
		}

		public static readonly BindableProperty ImageRightAlignedProperty =
			BindableProperty.Create(nameof(ImageRightAligned), typeof(bool), typeof(ExtendedButton), false);

		public bool ImageRightAligned
		{
			get { return (bool)GetValue(ImageRightAlignedProperty); }
			set { SetValue(ImageRightAlignedProperty, value); }
		}

		public static readonly BindableProperty ImageVisibleProperty =
			BindableProperty.Create(nameof(ImageVisible), typeof(bool), typeof(ExtendedButton), true);

		public bool ImageVisible
		{
			get { return (bool)GetValue(ImageVisibleProperty); }
			set { SetValue(ImageVisibleProperty, value); }
		}

		public static readonly BindableProperty HorizontalContentAlignmentProperty =
			BindableProperty.Create(nameof(HorizontalContentAlignment), typeof(ButtonHorizontalContentAlignment), typeof(ExtendedButton), ButtonHorizontalContentAlignment.Center);

		public ButtonHorizontalContentAlignment HorizontalContentAlignment
		{
			get { return (ButtonHorizontalContentAlignment)GetValue(HorizontalContentAlignmentProperty); }
			set { SetValue(HorizontalContentAlignmentProperty, value); }
		}

		public static readonly BindableProperty ImageHorizontalOffsetProperty =
			BindableProperty.Create(nameof(ImageHorizontalOffset), typeof(int), typeof(ExtendedButton), 5);

		public int ImageHorizontalOffset
		{
			get { return (int)GetValue(ImageHorizontalOffsetProperty); }
			set { SetValue(ImageHorizontalOffsetProperty, value); }
		}

		public static readonly BindableProperty EnableLongPressProperty =
			BindableProperty.Create(nameof(EnableLongPress), typeof(bool), typeof(ExtendedButton), defaultValue: false);

		#region Long Press Properties

		public bool EnableLongPress
		{
			get { return (bool)GetValue(EnableLongPressProperty); }
			set { SetValue(EnableLongPressProperty, value); }
		}

		public static readonly BindableProperty LongPressDurationProperty =
			BindableProperty.Create(nameof(LongPressDuration), typeof(int), typeof(ExtendedButton), defaultValue: 2);

		public int LongPressDuration
		{
			get { return (int)GetValue(LongPressDurationProperty); }
			set { SetValue(LongPressDurationProperty, value); }
		}

		public static readonly BindableProperty TouchUpAfterLongPressCommandProperty =
			BindableProperty.Create(nameof(TouchUpAfterLongPressCommand), typeof(ICommand), typeof(ExtendedButton), defaultValue: null);

		public ICommand TouchUpAfterLongPressCommand
		{
			get { return (ICommand)GetValue(TouchUpAfterLongPressCommandProperty); }
			set { SetValue(TouchUpAfterLongPressCommandProperty, value); }
		}

		public static readonly BindableProperty LongPressFillDirectionProperty =
			BindableProperty.Create(nameof(LongPressFillDirection), typeof(LongPressDirection), typeof(ExtendedButton), defaultValue: LongPressDirection.BottomToTop);

		public LongPressDirection LongPressFillDirection
		{
			get { return (LongPressDirection)GetValue(LongPressFillDirectionProperty); }
			set { SetValue(LongPressFillDirectionProperty, value); }
		}

		#endregion

		public delegate void TouchedMovedEventHandler(float forceTouchPercentage);
		public event TouchedMovedEventHandler OnTouchesMoved;

		public void InvokeTouchesMovedEvent(float forceTouchPercentage)
		{
			OnTouchesMoved?.Invoke(forceTouchPercentage);
		}

		public delegate void TouchesBeganEventHandler();
		public event TouchesBeganEventHandler OnTouchesBegan;

		public void InvokeTouchesBeganEvent()
		{
			OnTouchesBegan?.Invoke();
		}

		public delegate void TouchesEndedEventHandler(bool invokedFromForceTouch);
		public event TouchesEndedEventHandler OnTouchesEnded;

		public void InvokeTouchesEndedEvent(bool invokedFromForceTouch)
		{
			OnTouchesEnded?.Invoke(invokedFromForceTouch);
		}

		public delegate void TouchesCancelledEventHandler();
		public event TouchesCancelledEventHandler OnTouchesCancelled;

		public void InvokeTouchesCancelledEvent()
		{
			OnTouchesCancelled?.Invoke();
		}

		public delegate void ControlDidLayoutSubviewsEventHandler();
		public event ControlDidLayoutSubviewsEventHandler OnDidLayoutSubviews;

		public void InvokeSubviewsDidLayoutEvent()
		{
			OnDidLayoutSubviews?.Invoke();
		}
	}
}

