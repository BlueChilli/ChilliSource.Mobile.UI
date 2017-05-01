#region License
/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;

namespace Examples
{
	public partial class InputsExamplePage : BaseContentPage
	{
		public InputsExamplePage()
		{
			InitializeComponent();
		}

		void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.NewTextValue))
			{
				if (e.NewTextValue.Any(char.IsDigit))
				{
					LineEffect.SetLineColor(floatingLabelEntry, Color.Red);
				}
				else
				{
					LineEffect.SetLineColor(floatingLabelEntry, Color.Green);
				}
			}
			else
			{
				LineEffect.SetLineColor(floatingLabelEntry, Color.Green);
			}
		}
	}
}
