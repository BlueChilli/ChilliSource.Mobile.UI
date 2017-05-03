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
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.UI;
using System.Linq;
using Rg.Plugins.Popup.Services;

namespace Examples
{
	public partial class MiscellaneousExamplePage : BaseContentPage
	{
		public MiscellaneousExamplePage(IndexItem indexItem)
		{
			Item = indexItem;
			BindingContext = this;
			SetupCommands();
			InitializeComponent();
		}

		public int SelectedItem { get; set; }

		public SeparatorOrientation Separator1Orientation { get; set; } = SeparatorOrientation.Vertical;

		public SeparatorOrientation Separator2Orientation { get; set; } = SeparatorOrientation.Vertical;

		public SeparatorOrientation Separator3Orientation { get; set; } = SeparatorOrientation.Vertical;

		public IndexItem Item { get; set; }

		void SetupCommands()
		{
			ToolbarItems[1].Command = new Command(() =>
			{
				PopupNavigation.PushAsync(new HelpDescriptionPopupPage(Title, Item.LongDescription));

			});
		}

		void SegmentedControlItemChanged(object sender, System.EventArgs e)
		{
			var segmentControl = sender as SegmentedControlView;

			switch (segmentControl.SelectedItem)
			{
				case 0:
					Separator1Orientation = SeparatorOrientation.Horizontal;
					Separator2Orientation = SeparatorOrientation.Vertical;
					Separator3Orientation = SeparatorOrientation.Vertical;
					break;

				case 1:
					Separator2Orientation = SeparatorOrientation.Horizontal;
					Separator1Orientation = SeparatorOrientation.Vertical;
					Separator3Orientation = SeparatorOrientation.Vertical;
					break;

				case 2:
					Separator3Orientation = SeparatorOrientation.Horizontal;
					Separator1Orientation = SeparatorOrientation.Vertical;
					Separator2Orientation = SeparatorOrientation.Vertical;
					break;
			}

			OnPropertyChanged("Separator1Orientation");
			OnPropertyChanged("Separator2Orientation");
			OnPropertyChanged("Separator3Orientation");

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
