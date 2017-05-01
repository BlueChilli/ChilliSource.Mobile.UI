#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Examples
{
	public partial class IndexPage : BaseContentPage
	{
		ICommand _searchBarVisibilityCommand;

		List<IndexItem> _childItems;

		public IndexPage(string titlePrefix, List<IndexItem> childItems)
		{
			BindingContext = this;

			Title = titlePrefix + " Index";
			_childItems = childItems;

			_searchBarVisibilityCommand = new Command(() =>
			{
				SearchBarVisible = !SearchBarVisible;
				OnPropertyChanged(nameof(SearchBarVisible));
			});

			SetupToolBar();
			PopulateItems();
			InitializeComponent();
		}

		void SetupToolBar()
		{
			ToolbarItems.Add(new ToolbarItem()
			{
				Priority = 1,
				Icon = ThemeManager.SearchImage,
				Command = _searchBarVisibilityCommand
			});
		}

		void PopulateItems()
		{
			_childItems = _childItems.Where(i => i.ShouldBeShown()).ToList();

			Items = _childItems;

			OnPropertyChanged(nameof(Items));
		}


		void FilterItemsFromSearch()
		{
			if (searchBar?.Text != null && searchBar?.Text.Length > 0)
			{
				Items = _childItems.Where(i =>
				{
					return i.Title.Contains(searchBar.Text);

				}).ToList();
			}
			else
			{
				Items = _childItems;
			}

			OnPropertyChanged(nameof(Items));
		}

		public List<IndexItem> Items { get; private set; }

		public bool SearchBarVisibleProperty { get; set; }

		public bool SearchBarVisible
		{
			get
			{
				return SearchBarVisibleProperty;
			}
			set
			{
				SearchBarVisibleProperty = value;
				searchBar.Focus();
			}
		}

		async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			((ListView)sender).SelectedItem = null;

			var indexItem = e.Item as IndexItem;

			if (indexItem.PageType != null)
			{
				var page = Activator.CreateInstance(indexItem.PageType);
				await Navigation.PushAsync(page as Page);
			}
			else
			{
				var page = new IndexPage(indexItem.Title, indexItem.Children.ToList());
				await Navigation.PushAsync(page);
			}
		}

		void Handle_CancelButtonPressed(object sender, System.EventArgs e)
		{
			_searchBarVisibilityCommand.Execute(null);
		}

		void SearchBarTextChanged(object sender, TextChangedEventArgs e)
		{
			FilterItemsFromSearch();
		}
	}
}
