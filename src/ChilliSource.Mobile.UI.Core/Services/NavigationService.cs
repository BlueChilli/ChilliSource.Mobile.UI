#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/*
 * Based on
 * Source: SimpleIoCApp (https://github.com/Clancey/SimpleIoCApp)
 * Author:  James Clancey (https://github.com/Clancey)
 * License: Apache 2.0 (https://github.com/Clancey/SimpleIoCApp/blob/master/LICENSE)
*/

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI.Core
{
	/// <summary>
	/// Provides viewmodel-first navigation methods in conjunction with the <see cref="T:ChilliSource.Mobile.UI.Core.NavigationLocator"/> class.
	/// </summary>
	public static partial class NavigationService
	{
		static INavigation Navigation
		{
			get
			{
				//If the tab page has Navigation controllers as the contents, we need to use those.
				var tabbed = Application.Current.MainPage as TabbedPage;
				return tabbed?.CurrentPage?.Navigation ?? Application.Current.MainPage.Navigation;
			}
		}

		public static async Task PushAsync(BaseViewModel viewModel)
		{
			var page = BuildPage(viewModel, false);
			await Navigation.PushAsync(page);
		}

		public static async Task PopAsync()
		{
			await Navigation.PopAsync();
		}

		public static async Task PushModalAsync(BaseViewModel viewModel, bool wrapInNavigation = true)
		{
			var page = BuildPage(viewModel, wrapInNavigation);
			await Navigation.PushModalAsync(page);
		}

		public static async Task PopModalAsync()
		{
			await Navigation.PopModalAsync();
		}

		/// <summary>
		/// Sets the app root page to the page instance associated with the provided view model
		/// </summary>
		/// <param name="viewModel">View model.</param>
		/// <param name="wrapInNavigation">If set to <c>true</c> wraps the root page in a NavigationPage.</param>
		public static void SetRoot(object viewModel, bool wrapInNavigation = true)
		{
			Application.Current.MainPage = BuildPage(viewModel, wrapInNavigation);
		}

		/// <summary>
		/// Returns the page instance associated with the provided viewmodel
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="viewModel">View model.</param>
		/// <param name="wrapInNavigation">If set to <c>true</c> wraps the returned page in a NavigationPage.</param>
		public static Page BuildPage(object viewModel, bool wrapInNavigation = true)
		{
			var view = NavigationLocator.GetPage(viewModel.GetType());
			view.BindingContext = viewModel;
			return wrapInNavigation ? new NavigationPage(view) : view;
		}
	}
}

