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
using System.Text;

namespace Examples
{
	public partial class MenuPage : ContentPage
	{
		public MenuPage()
		{
			BindingContext = this;
			Icon = ThemeManager.HamburgerMenuImage;
			PopulateItems();
			BuildVersionString();

			InitializeComponent();
		}

		public string Version { get; private set; }

		public List<MenuItem> Items { get; private set; }

		void PopulateItems()
		{
			Items = new List<MenuItem>()
			{
				new MenuItem(MenuItemType.TermsAndConditions),
				new MenuItem(MenuItemType.ReportABug),
				new MenuItem(MenuItemType.Credits),
				new MenuItem(MenuItemType.Contact),
			};
		}

		void BuildVersionString()
		{
			Version = string.Format("Version {0} ({1})", Global.Instance.DeviceService.GetAppVersion(), Global.Instance.DeviceService.GetAppBuild());
		}

		void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			((ListView)sender).SelectedItem = null;

			var menuItem = e.Item as MenuItem;

			switch (menuItem.ItemType)
			{
				case MenuItemType.Contact:
					Device.OpenUri(new Uri("https://www.bluechilli.com"));
					break;
			}
		}
	}
}
