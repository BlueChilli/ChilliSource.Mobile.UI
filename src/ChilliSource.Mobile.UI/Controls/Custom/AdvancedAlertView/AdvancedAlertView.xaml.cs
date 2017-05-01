#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using ChilliSource.Mobile.UI.Core;

namespace ChilliSource.Mobile.UI
{
	public enum AlertType
	{
		Preset,
		AccentOnly,
		CustomImage,
		CustomImageAndAccent,
		Waiting
	}

	public enum PresetType
	{
		Positive,
		Negative,
		Info
	}

	/// <summary>
	/// Popup control that displays an iOS like alert view that can be fully customized.
	/// </summary>
	public partial class AdvancedAlertView : PopupPage
	{
		AlertType _alertType;
		PresetType _presetType;

		public static Color DefaultColor = Color.FromRgb(114, 115, 117);
		public static Color SuccessColor = Color.FromRgb(57, 181, 115);
		public static Color NegativeColor = Color.FromRgb(255, 38, 0);

		#region Static Helpers

		/// <summary>
		/// Show alert
		/// </summary>
		/// <param name="views">A list of Views to display in the alert</param>
		/// <param name="accentColor">Color of the circle</param>
		/// <param name="customImage">Image to be displayed in the circle</param>
		/// <param name="alertType">Type of alert</param>
		public static async Task ShowCustomAlert(List<View> views, Color accentColor, string title = "", ExtendedFont titleFont = null, string content = "", ExtendedFont contentFont = null, AlertType alertType = AlertType.AccentOnly, ImageSource customImage = null)
		{
			var view = new AdvancedAlertView(views, accentColor, title, titleFont, content, contentFont, alertType, customImage);
			await Application.Current.MainPage.Navigation.PushPopupAsync(view);
		}

		/// <summary>
		/// Show waiting alert
		/// </summary>
		/// <param name="duration">How long the alert should appear for</param>
		public static async Task ShowWaitingAlert(Color accentColor, string title = "", ExtendedFont titleFont = null, string content = "", ExtendedFont contentFont = null, int duration = 0)
		{
			var view = new AdvancedAlertView(null, accentColor, title, titleFont, content, contentFont, AlertType.Waiting);
			await Application.Current.MainPage.Navigation.PushPopupAsync(view);

			if (duration > 0)
			{
				await Task.Delay(duration);
				await view.Navigation.PopPopupAsync();
			}
		}

		/// <summary>
		/// Show preset alert
		/// </summary>
		/// <param name="buttons">A list of ExtendedButtons to appear in the alert></param>
		/// <param name="presetType">The type of preset</param>
		public static async Task ShowPresetAlert(List<ExtendedButton> buttons, PresetType presetType, string title = "", ExtendedFont titleFont = null, string content = "", ExtendedFont contentFont = null, ExtendedFont buttonFont = null, Command okCommand = null)
		{
			var view = new AdvancedAlertView(buttons.Cast<View>().ToList(), DefaultColor, title, titleFont, content, contentFont, AlertType.Preset, presetType: presetType);
			await Application.Current.MainPage.Navigation.PushPopupAsync(view);
		}

		public static async Task HideAlert()
		{
			await PopupNavigation.PopAsync();
		}

		#endregion

		#region Constructor

		public AdvancedAlertView(List<View> buttons, Color accentColor, string title = "", ExtendedFont titleFont = null, string content = "", ExtendedFont contentFont = null, AlertType alertType = AlertType.AccentOnly, ImageSource customImage = null, PresetType presetType = PresetType.Positive)
		{
			_alertType = alertType;
			_presetType = presetType;

			BindingContext = this;

			HasSystemPadding = false;
			CloseWhenBackgroundIsClicked = false;

			CustomImageSource = customImage;
			AccentColor = accentColor;

			Animation = new AdvancedAlertViewAnimation();

			InitializeComponent();

			switch (alertType)
			{
				case AlertType.Preset:
					AddImage(false);
					break;

				case AlertType.Waiting:
					AddSpinner();
					break;

				case AlertType.CustomImageAndAccent:
					AddImage(true);
					break;
			}

			if (buttons != null && buttons.Count > 0)
			{
				buttons.ForEach(AddButton);
			}

			TitleString = title;
			ContentString = content;

			TitleFont = titleFont;
			ContentFont = contentFont;

			SetRelativeLayoutConstraints();

			OnPropertyChanged(nameof(TitleString));
			OnPropertyChanged(nameof(ContentString));
			OnPropertyChanged(nameof(TitleFont));
			OnPropertyChanged(nameof(ContentFont));
			OnPropertyChanged(nameof(AccentColor));
			OnPropertyChanged(nameof(CustomImageSource));
		}

		#endregion

		#region Binding Properties

		public string TitleString { get; private set; }

		public string ContentString { get; private set; }

		public ExtendedFont TitleFont { get; private set; }

		public ExtendedFont ContentFont { get; private set; }

		public Color AccentColor { get; private set; }

		public ImageSource CustomImageSource { get; private set; }

		public bool CustomImageOnlyVisible { get { return _alertType == AlertType.CustomImage; } }

		#endregion

		void SetRelativeLayoutConstraints()
		{
			circleRelLayout.SetConstraints(topLevelRelLayout,
										   xConstraint: ExposedConstraint.RelativeToParent((parent) => { return parent.Width / 2 - 37.5; }),
										   yConstraint: ExposedConstraint.RelativeToView(mainFrame, (parent, sibling) => { return mainFrame.Y - 37.5; }));

			defaultRelLayout.SetConstraints(circleRelLayout,
											ExposedConstraint.RelativeToView(outerCircle, (parent, sibling) => { return outerCircle.X + 5; }),
											ExposedConstraint.RelativeToView(outerCircle, (parent, sibling) => { return outerCircle.Y + 5; }),
											ExposedConstraint.RelativeToParent((parent) => { return parent.Width - 10; }),
											ExposedConstraint.RelativeToParent((parent) => { return parent.Height - 10; }));
		}

		void AddImage(bool isCustom)
		{
			var defaultImage = new Image { Aspect = Aspect.AspectFit };

			if (isCustom)
			{
				defaultImage.Source = CustomImageSource;
			}
			else
			{
				switch (_presetType)
				{
					case PresetType.Positive:
						defaultImage.Source = "Success";
						AccentColor = SuccessColor;
						break;

					case PresetType.Negative:
						defaultImage.Source = "Error";
						AccentColor = NegativeColor;
						break;

					case PresetType.Info:
						defaultImage.Source = "Information";
						AccentColor = DefaultColor;
						break;
				}
			}

			//Dynamicaly resizes the layout depending on the relative width and height of the set image.
			Func<RelativeLayout, double> getImageWidth = (p) => defaultImage.Measure(defaultRelLayout.Width, defaultRelLayout.Height).Request.Width;
			Func<RelativeLayout, double> getImageHeight = (p) => defaultImage.Measure(defaultRelLayout.Width, defaultRelLayout.Height).Request.Height;

			defaultRelLayout.Children.Add(defaultImage,
				Constraint.RelativeToParent((parent) => parent.Width / 2 - getImageWidth(parent) / 2),
				Constraint.RelativeToParent((parent) => parent.Height / 2 - getImageHeight(parent) / 2)
				);
			//
		}

		void AddSpinner()
		{
			var spinner = new ActivityIndicator();
			spinner.Color = Color.White;
			spinner.IsRunning = true;
			spinner.IsVisible = true;
			spinner.Scale = 1.5;

			Func<RelativeLayout, double> getSpinnerWidth = (p) => spinner.Measure(defaultRelLayout.Width, defaultRelLayout.Height).Request.Width;
			Func<RelativeLayout, double> getSpinnerHeight = (p) => spinner.Measure(defaultRelLayout.Width, defaultRelLayout.Height).Request.Height;

			defaultRelLayout.Children.Add(spinner,
			Constraint.RelativeToParent((parent) => parent.Width / 2 - getSpinnerWidth(parent) / 2),
			Constraint.RelativeToParent((parent) => parent.Height / 2 - getSpinnerHeight(parent) / 2)
			);
		}

		public void AddButton(View button)
		{
			mainContentStack.Children.Add(button);
		}
	}
}
