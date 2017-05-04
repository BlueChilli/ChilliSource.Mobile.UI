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
	public class CarouselItem
	{
		public CarouselItem(int index, string itemText)
		{
			Index = index;
			ItemText = itemText;
		}
		public int Index { get; set; }
		public string ItemText { get; set; }
	}

	public partial class CarouselViewExamplePage : BaseContentPage
	{
		CarouselItem _currentItem;

		public CarouselViewExamplePage(IndexItem indexItem)
		{
			Item = indexItem;
			SetupCommands();
			BuildData();
			BindingContext = this;
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}

		void BuildData()
		{
			Items = new List<CarouselItem>();
			Items.Add(new CarouselItem(1, "Item 1"));
			Items.Add(new CarouselItem(2, "Item 2"));
			Items.Add(new CarouselItem(3, "Item 3"));

			CurrentItem = Items[0];
		}

		void SetupCommands()
		{
			ToolbarItems[1].Command = new Command(() =>
			{
				PopupNavigation.PushAsync(new HelpDescriptionPopupPage(Title, Item.LongDescription));

			});
		}

		public List<CarouselItem> Items { get; set; }


		public CarouselItem CurrentItem
		{
			get
			{
				return _currentItem;
			}
			set
			{
				_currentItem = value;
				OnPropertyChanged(nameof(CurrentItem));
			}
		}

		public IndexItem Item { get; set; }
	}
}
