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
using ChilliSource.Mobile.Core;

namespace Examples
{
    public class NavigationViewModel : ObservableObject {

        public NavigationViewModel(string title, string subtitle) 
        {
            this.Title = title;
            this.Subtitle = subtitle;

			LeftButtonVisibilityCommand = new Command(() =>
			{
				_leftVisible = !_leftVisible;
				LeftButtionText = LeftButtonVisibilityText;
			});

			RightButtonVisibilityCommand = new Command(() =>
			{
				_rightVisible = !_rightVisible;
				RightButtonText = RightButtonVisibilityText;
			});

            ChangeTitleCommand = new Command(() =>
            {
                Title = "Title changed";
            });

            ChangeSubTitleCommand = new Command(() =>
            {
                Subtitle = "Subtitl changed";
            });
		}

        private string _title;
		public string Title
		{
			get { return _title; }
			set { this.SetProperty(ref _title, value); }
		}

		private string _subtitle;
        public string Subtitle 
        {
            get { return _subtitle; }
            set {this.SetProperty(ref _subtitle, value);}
        }


		bool _leftVisible = true;
		bool _rightVisible = true;

		private string _leftButtionText;
		public string LeftButtionText 
        {
			get { return _leftButtionText; }
			set { this.SetProperty(ref _leftButtionText, value); }
        }

		private string _rightButtonText;
		public string RightButtonText
		{
			get { return _rightButtonText; }
			set { this.SetProperty(ref _rightButtonText, value); }
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

        public ICommand ChangeTitleCommand { get; set; }

        public ICommand ChangeSubTitleCommand { get; set; }
	}

	public partial class StyledNavigationBarExamplePage : BaseContentPage
	{
		
		public StyledNavigationBarExamplePage()
		{
            BindingContext = new NavigationViewModel("subtitle", "title");

           
			RightToolbarItemFont = ThemeManager.CellTitleFont;
			LeftToolbarItemFont = ThemeManager.AdvancedActionSheetTitleFont;
			TitleOnlyFont = ThemeManager.AdvancedActionSheetCancelFont;
			TitleFont = ThemeManager.AdvancedActionSheetCancelFont;
			SubTitleFont = ThemeManager.CellSubtitleFont;

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

			InitializeComponent();
		}


	}
}
