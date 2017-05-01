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
	public partial class StyledTabExamplePage : BaseTabPage
	{
		public StyledTabExamplePage()
		{
			BindingContext = this;

			SelectedTintColor = Color.Orange;

			TabItems = new List<ITabItem>()
			{
				new TabItem(){
					Title = "Item 1",
					BadgeCount = "8",
					Icon = @"Images/Misc/Question.png",
					SelectedIcon = @"Images/Misc/Question.png"
				},

				new TabItem(){
					Title = "Item 2",
					BadgeCount = "4",
					Icon = @"Images/Misc/Information.png",
					SelectedIcon = @"Images/Misc/Information.png"
				},

				new TabItem(){
					Title = "Item 3",
					Icon = @"Images/Misc/Edit.png",
					SelectedIcon = @"Images/Misc/Edit.png"
				}
			};

			IncreaseBadgeCommand = new Command(() =>
			{
				TabItems[0].BadgeCount = (Convert.ToInt32(TabItems[0].BadgeCount) + 1).ToString();
				OnPropertyChanged(nameof(TabItems));
			});

			DecreaseBadgeCommand = new Command(() =>
			{
				TabItems[0].BadgeCount = (Convert.ToInt32(TabItems[0].BadgeCount) - 1).ToString();
				OnPropertyChanged(nameof(TabItems));
			});

			InitializeComponent();

		}

		public ICommand IncreaseBadgeCommand { get; private set; }
		public ICommand DecreaseBadgeCommand { get; private set; }

	}
}
