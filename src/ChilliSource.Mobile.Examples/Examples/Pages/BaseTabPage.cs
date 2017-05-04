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

namespace Examples
{
	public class BaseTabPage : StyledTabbedPage
	{
		public BaseTabPage()
		{
			SetupToolbar();
		}

		void SetupToolbar()
		{
			var right = new ToolbarItem
			{
				Text = "Help",
				Order = ToolbarItemOrder.Primary,
				Priority = 1
			};

			ToolbarItems.Add(right);

		}
	}
}

