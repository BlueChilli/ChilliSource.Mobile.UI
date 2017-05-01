#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;

namespace Examples
{

	public class CellViewModel : ObservableObject
	{
		private string _text;
		public string Text
		{
			get { return _text; }
			set { SetProperty(ref _text, value); }
		}

	}

	public class PageViewModel : ObservableObject, IPageable
	{

		private Command _loadMoreCommad;
		private ObservableRangeCollection<CellViewModel> _items;
		private static Random rand = new Random(30000);
		public PageViewModel()
		{
			_loadMoreCommad = new Command(async () =>
			{
				IsPaging = true;
				await Task.Delay(TimeSpan.FromSeconds(5));
				var list = new List<CellViewModel>();
				for (var i = 0; i < PageSize; i++)
				{
					list.Add(new CellViewModel()
					{
						Text = $"RandomText - {rand.Next()}"
					});
				}

				_items.AddRange(list);
				CurrentPage++;
				IsPaging = false;
			});

			_items = new ObservableRangeCollection<CellViewModel>();

		}

		public ObservableRangeCollection<CellViewModel> Items => _items;

		public int PageSize { get; set; } = 10;
		public int CurrentPage { get; set; } = 1;
		public int TotalPage { get; } = 40;

		public bool HasMoreItems => CurrentPage < TotalPage;

		private bool _isPaging;
		public bool IsPaging
		{
			get { return _isPaging; }
			set { SetProperty(ref _isPaging, value); }
		}

		public ICommand LoadMoreCommand => _loadMoreCommad;

		public int PreloadCount => 1;
	}

	public partial class PagingBehaviorExamplePage : ContentPage
	{
		PageViewModel _vm;

		public PagingBehaviorExamplePage()
		{
			InitializeComponent();

			_vm = new PageViewModel();

			this.BindingContext = _vm;

			listView.Behaviors.Add(new ListViewPagingBehavior()
			{
				PreloadCount = _vm.PreloadCount
			});

			_vm.LoadMoreCommand.Execute(null);

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}
	}
}
