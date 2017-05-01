#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class ListViewPagingBehavior : BaseBehavior<ListView>
	{
		public static BindableProperty PreloadCountProperty = BindableProperty.Create(nameof(PreloadCount), typeof(int), typeof(ListViewPagingBehavior), 1, validateValue: (bindable, value) => (int)value > 0);

		private int lastPosition;
		/// <summary>
		/// Number of rows before the end of the list to start fetching the next page
		/// </summary>
		/// <value>1</value>
		public int PreloadCount
		{
			get { return (int)this.GetValue(PreloadCountProperty); }
			set { this.SetValue(PreloadCountProperty, value); }
		}

		void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
		{
			var bindable = (ListView)sender;
			var itemSource = bindable.ItemsSource as IList;
			int position = itemSource?.IndexOf(e.Item) ?? 0;

			if (itemSource != null)
			{
				var fetchIndex = Math.Max(itemSource.Count - PreloadCount, 0);
				var vm = bindable.BindingContext as IPageable;
				if (vm != null)
				{
					if (ShouldExecutePaging(position, lastPosition, itemSource.Count - 1, fetchIndex))
					{
						lastPosition = position;

						if (!vm.IsPaging && vm.HasMoreItems && !bindable.IsRefreshing)
						{
							LoadMore(vm.LoadMoreCommand);
						}
					}

				}
			}

		}

		public static bool ShouldExecutePaging(int position, int lastPosition, int itemCount, int fetchIndex)
		{
			return (IsScrolled(position, lastPosition) || IsLastItem(position, itemCount))
				&& ShouldFetchData(position, fetchIndex);
		}

		public static bool ShouldFetchData(int position, int preCountIndex)
		{
			return position >= preCountIndex;
		}

		public static bool IsScrolled(int position, int lastPosition)
		{
			return position > lastPosition;
		}

		public static bool IsLastItem(int position, int itemCount)
		{
			return position == itemCount - 1;
		}


		void LoadMore(ICommand pagingCommand)
		{
			if (pagingCommand != null && pagingCommand.CanExecute(null))
			{
				pagingCommand.Execute(null);
			}
		}

		protected override void OnAttachedTo(ListView bindable)
		{
			base.OnAttachedTo(bindable);
			bindable.ItemAppearing += OnItemAppearing;
			bindable.PropertyChanged += ListView_PropertyChanged;
		}

		void ListView_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var bindable = (ListView)sender;

			if (e.PropertyName.Equals(nameof(bindable.ItemsSource)))
			{
				if (bindable.ItemsSource != null && !(bindable.ItemsSource is IList))
				{
					System.Diagnostics.Debug.WriteLine($"{nameof(ListView)} ItemSource must implement {nameof(IList)}");
				}
			}
		}

		protected override void OnDetachingFrom(ListView bindable)
		{
			base.OnDetachingFrom(bindable);
			bindable.ItemAppearing -= OnItemAppearing;
			bindable.PropertyChanged -= ListView_PropertyChanged;
			lastPosition = 0;
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (this.BindingContext != null && !(this.BindingContext is IPageable))
			{
				System.Diagnostics.Debug.WriteLine($"{nameof(ListViewPagingBehavior)} BindingContext must implement {nameof(IPageable)}");
			}
		}

	}
}
