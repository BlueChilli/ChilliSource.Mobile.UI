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
using ChilliSource.Mobile.UI;
using System.Windows.Input;

namespace Examples
{
	public partial class StyledNavigationBarExamplePage : BaseContentPage
	{
		bool _leftVisible = true;
		bool _rightVisible = true;

		public StyledNavigationBarExamplePage()
		{
			BindingContext = this;

			RightToolbarItemFont = ThemeManager.CellTitleFont;
			LeftToolbarItemFont = ThemeManager.AdvancedActionSheetTitleFont;
			TitleOnlyFont = ThemeManager.AdvancedActionSheetCancelFont;
			TitleFont = ThemeManager.AdvancedActionSheetCancelFont;
			SubTitleFont = ThemeManager.CellSubtitleFont;
			SubTitle = "subtitle";
			Title = "title";

			ToolbarItems.Remove(ToolbarItems[0]);

			var left = new ToolbarItem
			{
				Text = "Back",
				Order = ToolbarItemOrder.Primary,
				Priority = 0,
				Command = new Command(async () =>
				{
					await Navigation.PopAsync();
				})
			};

			ToolbarItems.Add(left);

			var right = new ToolbarItem
			{
				Text = "Right Item",
				Order = ToolbarItemOrder.Primary,
				Priority = 1,
			};

			ToolbarItems.Add(right);

			LeftButtonVisibilityCommand = new Command(() =>
			{
				_leftVisible = !_leftVisible;
				LeftToolbarItemVisible = _leftVisible;
				OnPropertyChanged(nameof(LeftButtonVisibilityText));
			});

			RightButtonVisibilityCommand = new Command(() =>
			{
				_rightVisible = !_rightVisible;
				RightToolbarItemVisible = _rightVisible;
				OnPropertyChanged(nameof(RightButtonVisibilityText));

			});

			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			Title = "this is the new title";
			SubTitle = "This is the new subtitle";
		}

		public string LeftButtonVisibilityText
		{
			get
			{
				return _leftVisible ? "Hide Left Toolbar" : "Show Left Toolbar";
			}
		}

		public string RightButtonVisibilityText
		{
			get
			{
				return _rightVisible ? "Hide Right Toolbar" : "Show Right Toolbar";
			}
		}

		public ICommand LeftButtonVisibilityCommand { get; set; }

		public ICommand RightButtonVisibilityCommand { get; set; }

	}
}
