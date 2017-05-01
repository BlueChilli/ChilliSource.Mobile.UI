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
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Popup control that displays an iOS like action sheet that can be fully customized.
	/// </summary>
	public partial class AdvancedActionSheet : PopupPage
	{
		List<AdvancedActionSheetAction> Actions;

		public ExtendedFont TitleFont { get; set; }

		public string TitleText { get; set; }

		public bool TitleVisible { get { return !String.IsNullOrEmpty(TitleText); } }

		public bool CancelButtonVisible { get; set; }

		public ICommand CancelCommand { get; set; }

		public string CancelText { get; set; }

		public ExtendedFont CancelFont { get; set; }

		public static async Task ShowActionSheet(List<AdvancedActionSheetAction> actions, string title = "", ExtendedFont titleFont = null)
		{
			var view = new AdvancedActionSheet(actions, title, titleFont);
			await Application.Current.MainPage.Navigation.PushPopupAsync(view);
		}

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

			CreateButons();
		}

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

		void CreateButons()
		{
			foreach (var action in Actions)
			{
				AddAction(action);
			}
		}
	}
}
