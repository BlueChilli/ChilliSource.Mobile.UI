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
using System.Windows.Input;

namespace Examples
{
	public class BaseContentPage : StyledNavigationBarPage
	{
		public BaseContentPage()
		{
			NavigationPage.SetBackButtonTitle(this, "");
			BarTintColor = ThemeManager.OrangePink;

			AddBackButton();
		}

		void AddBackButton()
		{
			HideBackButton = true;

			ToolbarItems.Add(new ToolbarItem
			{
				Icon = ThemeManager.BackImage,
				Order = ToolbarItemOrder.Primary,
				Priority = 0,
				Command = new Command(() =>
			   {
				   if (BackCommand == null)
				   {
					   Navigation.PopAsync();
				   }
				   else
				   {
					   BackCommand.Execute(null);
				   }
			   })
			});
		}

		public ICommand BackCommand { get; set; }
	}
}
