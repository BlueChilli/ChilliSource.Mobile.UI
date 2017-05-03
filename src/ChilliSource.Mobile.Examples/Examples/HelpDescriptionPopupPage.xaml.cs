#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Examples
{
	public partial class HelpDescriptionPopupPage : PopupPage
	{
		//public string Title { get; set; }
		public string LongDescription { get; set; }

		public HelpDescriptionPopupPage(string title, string longDescription)
		{

			BindingContext = this;
			Title = title;
			LongDescription = longDescription;

			InitializeComponent();
			this.BackgroundColor = Color.FromRgba(0, 0, 0, 0.85f);
		}
	}
}
