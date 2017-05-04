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
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;

namespace Examples
{
	public partial class RootItemView : ExtendedFrame
	{
		RootItem _viewModel;

		public RootItemView(RootItem item)
		{
			_viewModel = item;

			BindingContext = _viewModel;
			InitializeComponent();

			AddTapGesture();
		}

		void AddTapGesture()
		{
			var tapGesture = new TapGestureRecognizer();
			tapGesture.Command = new Command(async () =>
			{
				_viewModel.Selected = true;

				if (_viewModel.PageType != null)
				{
					var page = Activator.CreateInstance(_viewModel.PageType);
					await Navigation.PushAsync(page as Page);
				}
				else
				{
					var page = new IndexPage(_viewModel.Title, _viewModel.Children);
					await Navigation.PushAsync(page);
				}

				_viewModel.Selected = false;
			});

			this.GestureRecognizers.Add(tapGesture);
		}
	}
}
