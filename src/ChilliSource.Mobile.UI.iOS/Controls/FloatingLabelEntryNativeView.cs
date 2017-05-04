#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

/*
Source: Based on FloatLabeledEntry (https://github.com/gshackles/FloatLabeledEntry/blob/master/src/iOS/FloatLabeledEntry.iOS/FloatLabeledTextField.cs)
Author: gshackles (https://github.com/gshackles)
License: MIT (https://github.com/gshackles/FloatLabeledEntry/blob/master/LICENSE)
*/

#endregion

using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
using Foundation;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Adds a floating placeholder to the textfield along with an optional effect that displays a line.
	/// </summary>
	public class FloatingLabelEntryNativeView : UITextField
	{
		private UILabel _floatingLabel;
		private bool _shouldFloat;
		private string _placeHolderText;
		private bool _animationInProgress;

		public ExtendedFont CustomFont { get; set; }

		public ExtendedFont CustomPlaceholderFont { get; set; }

		public EventHandler PlaceholderRectSet { get; set; }

		public FloatingLabelEntryNativeView(CGRect frame, ExtendedFont floatingLabelCustomFont)
			: base(frame)
		{
			CustomFont = floatingLabelCustomFont;
			InitializeLabel();
		}

		public override void AwakeFromNib() => InitializeLabel();

		private void InitializeLabel()
		{
			_floatingLabel = new UILabel
			{
				Alpha = this.Text.Length > 0 ? 1.0f : 0.0f,
				Font = CustomFont == null ? Xamarin.Forms.Font.Default.ToUIFont() : CustomFont.ToUIFont()
			};

			AddSubview(_floatingLabel);

			this.ShouldBeginEditing += EditingBegan;
			this.ShouldEndEditing += EditingEnded;
			this.ClipsToBounds = false;
			this.AutosizesSubviews = false;
			this.AutoresizingMask = UIViewAutoresizing.FlexibleBottomMargin;
		}

		public override string Placeholder
		{
			get { return base.Placeholder; }
			set
			{
				base.Placeholder = value;

				if (_placeHolderText == null)
				{
					_floatingLabel.Text = value;
					_floatingLabel.TextColor = CustomFont?.Color.ToUIColor();
					_floatingLabel.SizeToFit();
					_floatingLabel.Frame =
							new CGRect(
								0,
								1.0f,
								_floatingLabel.Frame.Size.Width,
								_floatingLabel.Frame.Size.Height);

					_placeHolderText = value;
				}
				else if (value != "")
				{
					_placeHolderText = value;
				}
			}
		}

		//Resize the various components to leave space for the floating label
		public override CGRect TextRect(CGRect forBounds)
		{
			if (_floatingLabel == null)
				return base.TextRect(forBounds);

			return InsetRect(base.TextRect(forBounds), new UIEdgeInsets(_floatingLabel.Font.LineHeight, 0, 0, 0));
		}

		public override CGRect EditingRect(CGRect forBounds)
		{
			if (_floatingLabel == null)
				return base.EditingRect(forBounds);

			return InsetRect(base.EditingRect(forBounds), new UIEdgeInsets(_floatingLabel.Font.LineHeight, 0, 0, 0));
		}

		public override CGRect PlaceholderRect(CGRect forBounds)
		{
			//If the placeholder should be floating the move to the floating lable position
			if (_shouldFloat)
			{
				return new CGRect(0, 1.0f,
								  forBounds.Width, _floatingLabel.Frame.Size.Height);
			}

			//Called to notify observers that the placeholders frame has been set
			PlaceholderRectSet?.Invoke(this, null);

			return base.PlaceholderRect(forBounds);
		}

		public override CGRect ClearButtonRect(CGRect forBounds)
		{
			var rect = base.ClearButtonRect(forBounds);

			if (_floatingLabel == null)
				return rect;

			return new CGRect(
				rect.X, rect.Y + _floatingLabel.Font.LineHeight / 2.0f,
				rect.Size.Width, rect.Size.Height);
		}
		//

		public bool IsFloating
		{
			get
			{
				return _floatingLabel.Alpha == 1f;
			}
		}

		//When the textfield begins editing float the placeholder
		bool EditingBegan(UITextField textField)
		{
			if (!_animationInProgress)
			{
				_shouldFloat = true;
				UpdateLabels();
			}

			return true;
		}

		//When the textfield ends editing and there is no text float the placeholder
		bool EditingEnded(UITextField textfield)
		{
			_shouldFloat = textfield.Text.Length > 0;
			UpdateLabels();
			return true;
		}

		//Handles the animation, fonts and placement of the floating label and placeholder
		void UpdateLabels()
		{
			if (!_shouldFloat) _floatingLabel.Alpha = 0.0f;

			AnimateNotify(0.5, 0.0, UIViewAnimationOptions.BeginFromCurrentState | UIViewAnimationOptions.CurveEaseOut, () =>
			 {
				 _animationInProgress = true;

				 if (_shouldFloat)
				 {
					 this.AttributedPlaceholder = CustomFont.BuildAttributedString(this.Placeholder);
				 }
				 else
				 {
					 Placeholder = _placeHolderText;

					 if (CustomPlaceholderFont != null)
					 {
						 this.AttributedPlaceholder = CustomPlaceholderFont.BuildAttributedString(this.Placeholder);
					 }
				 }

				 // Invokes the Rect Methods
				 this.LayoutIfNeeded();

			 }, (finished) =>
			  {
				  if (finished)
				  {
					  if (_shouldFloat)
					  {
						  _floatingLabel.Alpha = 1.0f;
						  Placeholder = "";
					  }
				  }

				  _animationInProgress = false;
			  });
		}

		private static CGRect InsetRect(CGRect rect, UIEdgeInsets insets) =>
			new CGRect(
				rect.X + insets.Left,
				rect.Y + insets.Top,
				rect.Width - insets.Left - insets.Right,
				rect.Height - insets.Top - insets.Bottom);
	}
}
