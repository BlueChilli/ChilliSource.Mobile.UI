#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// ListView behavior that allows paging of data. Must be bound to a view model implementing <see cref="IPageable"/> 
    /// </summary>
    public class ListViewPagingBehavior : BaseBehavior<ListView>
    {
        int _lastPosition;

        /// <summary>
        /// Backing store for the <see cref="PreloadCount"/> bindable property.
        /// </summary>
        public static BindableProperty PreloadCountProperty =
            BindableProperty.Create(nameof(PreloadCount), typeof(int), typeof(ListViewPagingBehavior), 1, validateValue: (bindable, value) => (int)value > 0);

        /// <summary>
        /// Gets or sets the number of rows before the end of the list to start fetching the next page.
        /// </summary>
        /// <value>The preload count; the default is 1.</value>
        public int PreloadCount
        {
            get { return (int)GetValue(PreloadCountProperty); }
            set { SetValue(PreloadCountProperty, value); }
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

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemAppearing -= OnItemAppearing;
            bindable.PropertyChanged -= ListView_PropertyChanged;
            _lastPosition = 0;
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

        void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var bindable = (ListView)sender;
            var itemSource = bindable.ItemsSource as IList;
            int position = itemSource?.IndexOf(e.Item) ?? 0;

            if (itemSource == null)
            {
                return;
            }

            var fetchIndex = Math.Max(itemSource.Count - PreloadCount, 0);
            if (bindable.BindingContext is IPageable vm)
            {
                if (ShouldExecutePaging(position, _lastPosition, itemSource.Count - 1, fetchIndex))
                {
                    _lastPosition = position;

                    if (!vm.IsPaging && vm.HasMoreItems && !bindable.IsRefreshing)
                    {
                        LoadMore(vm.LoadMoreCommand);
                    }
                }
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (this.BindingContext != null && !(this.BindingContext is IPageable))
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(ListViewPagingBehavior)} BindingContext must implement {nameof(IPageable)}");
            }
        }

        static bool ShouldExecutePaging(int position, int lastPosition, int itemCount, int fetchIndex)
        {
            return (IsScrolled(position, lastPosition) || IsLastItem(position, itemCount))
                && ShouldFetchData(position, fetchIndex);
        }

        static bool ShouldFetchData(int position, int preCountIndex)
        {
            return position >= preCountIndex;
        }

        static bool IsScrolled(int position, int lastPosition)
        {
            return position > lastPosition;
        }

        static bool IsLastItem(int position, int itemCount)
        {
            return position == itemCount - 1;
        }
    }
}
