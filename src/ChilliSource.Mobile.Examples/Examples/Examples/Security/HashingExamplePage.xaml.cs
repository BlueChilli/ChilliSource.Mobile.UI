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
using ChilliSource.Mobile.Security;
using ChilliSource.Mobile.UI;
using Rg.Plugins.Popup.Services;

namespace Examples
{
	public partial class HashingExamplePage : KeyboardOverlapContentPage
	{
		public HashingExamplePage(IndexItem indexItem)
		{
			Item = indexItem;
			BindingContext = this;
			SetupToolbar();
			InitializeComponent();
		}

		void SetupToolbar()
		{
			var right = new ToolbarItem
			{
				Text = "Help",
				Order = ToolbarItemOrder.Primary,
				Priority = 1,
				Command = new Command(() =>
				{
					PopupNavigation.PushAsync(new HelpDescriptionPopupPage(Title, Item.LongDescription));

				})
			};

			ToolbarItems.Add(right);
		}

		public string HashString { get; set; }
		public IndexItem Item { get; set; }

		void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			HashString = AesEncryptionProvider.HashString((sender as ExtendedEditor).Text);
			OnPropertyChanged(nameof(HashString));
		}
	}
}
