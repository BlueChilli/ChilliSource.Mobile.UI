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
using System.Windows.Input;
using ChilliSource.Mobile.UI;

namespace Examples
{
	public partial class IntroPage : BaseContentPage
	{
		public IntroPage()
		{
			NavigationPage.SetHasNavigationBar(this, false);

			TitleOnlyFont = ThemeManager.NavigationTitleFont;
			TitleFont = ThemeManager.NavigationTitleFont;

			BindingContext = this;

			LetsGoCommand = new Command(() =>
		  	{
				  App.CurrentApp.MainPage = new RootPage();
			  });

			InitializeComponent();
		}

		public ICommand LetsGoCommand { get; set; }
	}
}
