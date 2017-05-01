#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
namespace Examples
{
	public partial class MainPage : BaseContentPage
	{
		List<RootItem> _items;

		public MainPage()
		{
			Title = "ChilliSource.Mobile";
			BindingContext = this;
			_items = ContentFactory.BuildIndexHierarchy();
			InitializeComponent();
			PopulateGrid();
		}

		void PopulateGrid()
		{
			_items = _items.Where(i => i.ShouldBeShown()).ToList();

			int x = 0, y = -1;

			for (int i = 0; i < _items.Count; i++)
			{

				if (i % 2 == 0)
				{
					x = 0;
					y++;
				}
				else
				{
					x = 1;
				}

				grid.Children.Add(new RootItemView(_items[i]), x, y);
			}
		}
	}
}
