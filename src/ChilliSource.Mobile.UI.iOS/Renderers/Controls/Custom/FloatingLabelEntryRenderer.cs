#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using ChilliSource.Mobile.UI;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(FloatingLabelEntry), typeof(FloatingLabelEntryRenderer))]

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Renderer for the Floating Label Entry
	/// </summary>
	public class FloatingLabelEntryRenderer : ViewRenderer<FloatingLabelEntry, FloatingLabelEntryNativeView>
	{
		FloatingLabelEntryNativeView _floatingLabelEntryView;
		bool _disposed;

		protected override void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			_disposed = true;

			if (disposing)
			{
				_floatingLabelEntryView = null;

				if (Control != null)
				{
					Control.EditingDidBegin -= OnEditingBegan;
					Control.EditingChanged -= OnEditingChanged;
					Control.EditingDidEnd -= OnEditingEnded;
				}
			}

			base.Dispose(disposing);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<FloatingLabelEntry> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement == null)
				return;

			if (Control == null)
			{
				_floatingLabelEntryView = new FloatingLabelEntryNativeView(new CoreGraphics.CGRect(Element.Bounds.X, Element.Bounds.Y, Element.Bounds.Width, Element.Bounds.Height), Element.FloatingLabelCustomFont);

				SetNativeControl(_floatingLabelEntryView);

				//Handle Textfield events
				_floatingLabelEntryView.EditingChanged += OnEditingChanged;
				_floatingLabelEntryView.ShouldReturn = OnShouldReturn;
				_floatingLabelEntryView.EditingDidBegin += OnEditingBegan;
				_floatingLabelEntryView.EditingDidEnd += OnEditingEnded;
			}

			//Implement Entry renderer
			UpdatePlaceholder();
			UpdatePassword();
			UpdateText();
			UpdateFont();
			UpdateKeyboard();
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Element == null)
			{
				return;
			}

			switch (e.PropertyName)
			{
				case nameof(Element.FloatingLabelCustomFont):
				case nameof(Element.Placeholder):
					{
						_floatingLabelEntryView.CustomFont = Element.FloatingLabelCustomFont;
						_floatingLabelEntryView.Placeholder = Element.Placeholder;
						_floatingLabelEntryView.SetNeedsDisplay();
						break;
					}
			}
		}

		void OnEditingBegan(object sender, EventArgs e)
		{
			IElementController ElementController = Element as IElementController;

			ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, true);
		}

		void OnEditingChanged(object sender, EventArgs eventArgs)
		{
			IElementController ElementController = Element as IElementController;

			ElementController.SetValueFromRenderer(Entry.TextProperty, Control.Text);
		}

		void OnEditingEnded(object sender, EventArgs e)
		{
			IElementController ElementController = Element as IElementController;

			if (Control.Text != Element.Text)
			{
				ElementController.SetValueFromRenderer(Entry.TextProperty, Control.Text);
			}

			ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
		}

		bool OnShouldReturn(UITextField view)
		{
			Control.ResignFirstResponder();
			((IEntryController)Element).SendCompleted();
			return false;
		}

		void UpdateKeyboard()
		{
			Control.ApplyKeyboard(Element.Keyboard);
			Control.ReloadInputViews();
		}
		void UpdateFont()
		{
			if (Element.CustomFont != null) Control.Font = Element.CustomFont.ToUIFont();
			Control.CustomFont = Element.FloatingLabelCustomFont;
			Control.CustomPlaceholderFont = Element.CustomPlaceholderFont;
		}

		void UpdatePassword()
		{
			if (Element.IsPassword && Control.IsFirstResponder)
			{
				Control.Enabled = false;
				Control.SecureTextEntry = true;
				Control.Enabled = Element.IsEnabled;
				Control.BecomeFirstResponder();
			}
			else
				Control.SecureTextEntry = Element.IsPassword;
		}

		void UpdatePlaceholder()
		{
			var formatted = (FormattedString)Element.Placeholder;

			if (formatted == null)
				return;

			var targetColor = Element.PlaceholderColor;

			// Placeholder default color is 70% gray
			// https://developer.apple.com/library/prerelease/ios/documentation/UIKit/Reference/UITextField_Class/index.html#//apple_ref/occ/instp/UITextField/placeholder

			var color = Element.IsEnabled && targetColor != Color.Default ? targetColor : new UIColor(0.7f, 0.7f, 0.7f, 1).ToColor();

			Control.Placeholder = Element.Placeholder;
			Control.AttributedPlaceholder = formatted.ToAttributed(Font.Default, color);
		}

		void UpdateText()
		{
			// ReSharper disable once RedundantCheckBeforeAssignment
			if (Control.Text != Element.Text)
				Control.Text = Element.Text;
		}
	}
}
