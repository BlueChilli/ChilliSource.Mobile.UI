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
using System.Linq;
using ChilliSource.Mobile.UI;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Specifies the type of the alert.
    /// </summary>
    public enum AlertType
    {
        Preset,
        AccentOnly,
        CustomImage,
        CustomImageAndAccent,
        Waiting
    }

    /// <summary>
    /// Specifies the preset types for the alert.
    /// </summary>
    public enum PresetType
    {
        Positive,
        Negative,
        Info
    }

    /// <summary>
    /// Fully customizable popup control that displays an iOS like alert view.
    /// </summary>
    public partial class AdvancedAlertView : PopupPage
    {
        AlertType _alertType;
        PresetType _presetType;

        /// <summary>
        /// The default color of the alert view.
        /// </summary>
        public static Color DefaultColor = Color.FromRgb(114, 115, 117);

        /// <summary>
        /// The color of the success alert.
        /// </summary>
        public static Color SuccessColor = Color.FromRgb(57, 181, 115);

        /// <summary>
        /// The color of the negative alert.
        /// </summary>
        public static Color NegativeColor = Color.FromRgb(255, 38, 0);

        #region Static Helpers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="views">A list of views to display in the alert.</param>
        /// <param name="accentColor">Color of the circle.</param>
        /// <param name="title"></param>
        /// <param name="titleFont"></param>
        /// <param name="content"></param>
        /// <param name="contentFont"></param>
        /// <param name="alertType"></param>
        /// <param name="customImage">Image to be displayed in the circle.</param>
        /// <returns></returns>
        public static async Task ShowCustomAlert(List<View> views, Color accentColor, string title = "", ExtendedFont titleFont = null,
                                                 string content = "", ExtendedFont contentFont = null, AlertType alertType = AlertType.AccentOnly,
                                                 ImageSource customImage = null)
        {
            var view = new AdvancedAlertView(views, accentColor, title, titleFont, content, contentFont, alertType, customImage);
            await Application.Current.MainPage.Navigation.PushPopupAsync(view);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accentColor"></param>
        /// <param name="title"></param>
        /// <param name="titleFont"></param>
        /// <param name="content"></param>
        /// <param name="contentFont"></param>
        /// <param name="duration">How long the alert should be visible</param>
        /// <returns></returns>
        public static async Task ShowWaitingAlert(Color accentColor, string title = "", ExtendedFont titleFont = null,
                                                  string content = "", ExtendedFont contentFont = null, int duration = 0)
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
        /// 
        /// </summary>
        /// <param name="buttons">A list of buttons to appear in the alert view.</param>
        /// <param name="presetType"></param>
        /// <param name="title"></param>
        /// <param name="titleFont"></param>
        /// <param name="content"></param>
        /// <param name="contentFont"></param>
        /// <param name="buttonFont"></param>
        /// <param name="okCommand"></param>
        /// <returns></returns>
        public static async Task ShowPresetAlert(List<ExtendedButton> buttons, PresetType presetType, string title = "", ExtendedFont titleFont = null,
                                                 string content = "", ExtendedFont contentFont = null, ExtendedFont buttonFont = null, Command okCommand = null)
        {
            var view = new AdvancedAlertView(buttons.Cast<View>().ToList(), DefaultColor, title, titleFont, content, contentFont, AlertType.Preset, presetType: presetType);
            await Application.Current.MainPage.Navigation.PushPopupAsync(view);
        }

        /// <summary>
        /// Hides the alert view.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> that hides the alert.</returns>
        public static async Task HideAlert()
        {
            await Application.Current.MainPage.Navigation.PopPopupAsync(true);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of this <c>AdvancedAlertView</c> class.
        /// </summary>
        /// <param name="buttons">Buttons.</param>
        /// <param name="accentColor">Color of the circle.</param>
        /// <param name="title">Title.</param>
        /// <param name="titleFont">Title's font.</param>
        /// <param name="content">Content.</param>
        /// <param name="contentFont">Content's font.</param>
        /// <param name="alertType">Alert type.</param>
        /// <param name="customImage">Image to be displayed in the circle.</param>
        /// <param name="presetType">Preset type of the alert.</param>
        public AdvancedAlertView(List<View> buttons, Color accentColor, string title = "", ExtendedFont titleFont = null,
                                 string content = "", ExtendedFont contentFont = null, AlertType alertType = AlertType.AccentOnly, ImageSource customImage = null,
                                 PresetType presetType = PresetType.Positive)
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

        /// <summary>
        /// Gets the title of the alert view.
        /// </summary>
        public string TitleString { get; private set; }

        /// <summary>
        /// Gets the content of the alert view.
        /// </summary>
        public string ContentString { get; private set; }

        /// <summary>
        /// Gets the font for the alert view's title.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the title's font.</value>
        public ExtendedFont TitleFont { get; private set; }

        /// <summary>
        /// Gets the font for the alert view's content.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the content's font.</value>
        public ExtendedFont ContentFont { get; private set; }

        /// <summary>
        /// Gets the color of the circle in the alert view.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the color of the alert's circle.</value>
        public Color AccentColor { get; private set; }

        /// <summary>
        /// Gets the source for the custom image of the alert view.
        /// </summary>
        /// <value>A <see cref="ImageSource"/> that provides the source for the custom image.</value>
        public ImageSource CustomImageSource { get; private set; }

        /// <summary>
        /// Gets a value indicating whether only the image should be visible in this <c>AdvancedAlertView</c>.
        /// </summary>
        /// <value><c>true</c> if only the custom image should be visible; otherwise, <c>false</c>.</value>
        public bool CustomImageOnlyVisible { get { return _alertType == AlertType.CustomImage; } }

        #endregion

        void SetRelativeLayoutConstraints()
        {
            circleLayout.SetConstraints(topLevelLayout,
                                           xConstraint: ExposedConstraint.RelativeToParent((parent) => { return parent.Width / 2 - 37.5; }),
                                           yConstraint: ExposedConstraint.RelativeToView(mainFrame, (parent, sibling) => { return mainFrame.Y - 37.5; }));

            defaultLayout.SetConstraints(circleLayout,
                                            ExposedConstraint.RelativeToView(outerCircle, (parent, sibling) => { return outerCircle.X + 5; }),
                                            ExposedConstraint.RelativeToView(outerCircle, (parent, sibling) => { return outerCircle.Y + 5; }),
                                            ExposedConstraint.RelativeToParent((parent) => { return parent.Width - 10; }),
                                            ExposedConstraint.RelativeToParent((parent) => { return parent.Height - 10; }));
        }

        void AddImage(bool isCustom)
        {
            var defaultImage = new CircledImage { Aspect = Aspect.AspectFit };

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
            Func<RelativeLayout, double> getImageWidth = (p) => defaultImage.Measure(defaultLayout.Width, defaultLayout.Height).Request.Width;
            Func<RelativeLayout, double> getImageHeight = (p) => defaultImage.Measure(defaultLayout.Width, defaultLayout.Height).Request.Height;

            defaultLayout.Children.Add(defaultImage,
                Constraint.RelativeToParent((parent) => parent.Width / 2 - getImageWidth(parent) / 2),
                Constraint.RelativeToParent((parent) => parent.Height / 2 - getImageHeight(parent) / 2)
                );

        }

        void AddSpinner()
        {
            var spinner = new ActivityIndicator()
            {
                Color = Color.White,
                IsRunning = true,
                IsVisible = true,
                Scale = 1.5
            };
            Func<RelativeLayout, double> getSpinnerWidth = (p) => spinner.Measure(defaultLayout.Width, defaultLayout.Height).Request.Width;
            Func<RelativeLayout, double> getSpinnerHeight = (p) => spinner.Measure(defaultLayout.Width, defaultLayout.Height).Request.Height;

            defaultLayout.Children.Add(spinner,
            Constraint.RelativeToParent((parent) => parent.Width / 2 - getSpinnerWidth(parent) / 2),
            Constraint.RelativeToParent((parent) => parent.Height / 2 - getSpinnerHeight(parent) / 2)
            );
        }

        /// <summary>
        /// Adds the button to the alert view.
        /// </summary>
        /// <param name="button">Button.</param>
        public void AddButton(View button)
        {
            contentLayout.Children.Add(button);
        }
    }
}
