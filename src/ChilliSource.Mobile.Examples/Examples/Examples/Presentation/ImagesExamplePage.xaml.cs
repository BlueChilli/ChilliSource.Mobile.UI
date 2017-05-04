#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Examples
{
	public partial class ImagesExamplePage : BaseContentPage
	{
		public ImagesExamplePage(IndexItem indexItem)
		{
			Item = indexItem;
			BindingContext = this;
			SetupCommands();
			InitializeComponent();
		}

		void SetupCommands()
		{
			ToolbarItems[1].Command = new Command(() =>
			{
				PopupNavigation.PushAsync(new HelpDescriptionPopupPage(Title, Item.LongDescription));

			});
		}

		public IndexItem Item { get; set; }
	}
}
