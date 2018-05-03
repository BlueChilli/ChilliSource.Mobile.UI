#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Fully customizable popup control that displays an iOS like action sheet.
    /// </summary>
    public partial class AdvancedActionSheet : PopupPage
    {
        List<AdvancedActionSheetAction> Actions;

        /// <summary>
        /// Gets or sets the font for the title.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the title's font.</value>
        public ExtendedFont TitleFont { get; set; }

        /// <summary>
        /// Gets or sets the text for the title.
        /// </summary>
        public string TitleText { get; set; }

        /// <summary>
        /// Gets a value indicating whether the title of this <c>AdvancedActionSheet</c> should be visible.
        /// </summary>
        /// <value><c>true</c> if the title is visible; otherwise, <c>false</c>.</value>
        public bool TitleVisible { get { return !String.IsNullOrEmpty(TitleText); } }

        /// <summary>
        /// Gets or sets a value indicating whether the cancel button of this <c>AdvancedActionSheet</c> should be visible.
        /// </summary>
        /// <value><c>true</c> if the cancel button is visible; otherwise, <c>false</c>.</value>
        public bool CancelButtonVisible { get; set; }

        /// <summary>
        /// Gets or sets the cancel command for the cancel button.
        /// </summary>
        /// <value>A <see cref="ICommand"/> value that represent the cancel button's command.</value>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Gets or sets the text for the cancel button.
        /// </summary>
        public string CancelText { get; set; }

        /// <summary>
        /// Gets or sets the font for the cancel button.
        /// </summary>
        /// <value>A <see cref="ExtendedFont"/> value that represents the cancel button's font.</value>
        public ExtendedFont CancelFont { get; set; }


        /// <summary>
        /// Shows the advanced action sheet.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> that shows the action sheet.</returns>
        /// <param name="actions">Action buttons.</param>
        /// <param name="title">Title.</param>
        /// <param name="titleFont">Title font.</param>
        public static async Task ShowActionSheet(List<AdvancedActionSheetAction> actions, string title = "", ExtendedFont titleFont = null)
        {
            var view = new AdvancedActionSheet(actions, title, titleFont);
            await Application.Current.MainPage.Navigation.PushPopupAsync(view);
        }

        /// <summary>
        /// Initializes a new instance of this <c>AdvancedActionSheet</c> class.
        /// </summary>
        /// <param name="actions">Action buttons.</param>
        /// <param name="title">Title.</param>
        /// <param name="titleFont">Title font.</param>
        public AdvancedActionSheet(List<AdvancedActionSheetAction> actions, string title = "", ExtendedFont titleFont = null)
        {
            BindingContext = this;

            Actions = actions;
            TitleText = title;
            TitleFont = titleFont;

            Animation = new AdvancedActionSheetAnimation();

            HasSystemPadding = false;
            CloseWhenBackgroundIsClicked = true;

            InitializeComponent();

            CreateButtons();
        }

        /// <summary>
        /// Adds individual action buttons to the alert view.
        /// </summary>
        /// <param name="action">Action.</param>
        public void AddAction(AdvancedActionSheetAction action)
        {
            var ExtendedButton = new ExtendedButton()
            {
                Style = AdvancedActionSheetStyles.StandardButtonStyle,
                Command = new Command(async () =>
                {
                    await Navigation.PopPopupAsync();
                    action.Command?.Execute(null);
                }),

                CustomFont = action.Font,
                Text = action.Title
            };

            if (action.ActionType == ActionType.Cancel)
            {
                CancelButtonVisible = true;
                CancelText = action.Title;
                CancelFont = action.Font;
                CancelCommand = new Command(() =>
                {
                    action.Command?.Execute(null);
                });

                OnPropertyChanged(nameof(CancelButtonVisible));
                OnPropertyChanged(nameof(CancelCommand));
                OnPropertyChanged(nameof(CancelText));
                OnPropertyChanged(nameof(CancelFont));
            }
            else
            {
                buttonStack.Children.Add(ExtendedButton);
            }
        }

        void CreateButtons()
        {
            foreach (var action in Actions)
            {
                AddAction(action);
            }
        }
    }
}
