#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion


using System;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Examples
{
	public partial class ShapeExamplePage : BaseContentPage
	{

		int _borderWidth;

		public ShapeExamplePage(IndexItem indexItem)
		{
			_borderWidth = 5;
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

		public int BorderWidth
		{
			get
			{
				return _borderWidth;
			}

			set
			{
				_borderWidth = value;
				OnPropertyChanged();
			}
		}

		void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
		{
			BorderWidth = (int)(e.NewValue * 10);
		}

		public IndexItem Item { get; set; }
	}
}
