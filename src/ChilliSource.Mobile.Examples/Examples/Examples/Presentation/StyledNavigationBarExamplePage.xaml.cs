#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;

namespace Examples
{
    public partial class StyledNavigationBarExamplePage : BaseContentPage
	{
	    public StyledNavigationBarExamplePage(IndexItem indexItem)
		{
            InitializeComponent();
        	RightToolbarItemFont = ThemeManager.CellTitleFont;
			LeftToolbarItemFont = ThemeManager.AdvancedActionSheetTitleFont;
			TitleOnlyFont = ThemeManager.AdvancedActionSheetCancelFont;
			TitleFont = ThemeManager.AdvancedActionSheetCancelFont;
			SubTitleFont = ThemeManager.CellSubtitleFont;

            // Doesn't seems to work binding from xaml why????
		    SetBinding(StyledNavigationBarPage.SubtitleProperty, new Binding("SubTitle"));

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

		    BindingContext = new StyledNavigationBarExamplePageViewModel("subtitle", "title");
        }
	}
}
