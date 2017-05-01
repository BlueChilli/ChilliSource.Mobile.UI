#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;

using Xamarin.Forms;

namespace Examples
{
	public class RootPage : MasterDetailPage
	{
		public RootPage()
		{
			Master = new MenuPage();
			Detail = new NavigationPage(new MainPage());
		}
	}
}

