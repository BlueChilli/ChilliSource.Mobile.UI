#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;

using Xamarin.Forms;
using Humanizer;

namespace Examples
{
	public enum MenuItemType
	{
		TermsAndConditions,
		ReportABug,
		Contact,
		Credits
	}

	public class MenuItem
	{
		public MenuItem(MenuItemType type)
		{
			ItemType = type;
			Title = ItemType.Humanize();
		}

		public string Title { get; private set; }

		public MenuItemType ItemType { get; private set; }
	}
}

