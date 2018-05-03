#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Xamarin Forms ListView extension
    /// </summary>
	public class ExtendedListView : ListView
    {
        /// <summary>
        /// Creates and initializes a new instance of the <c>ExtendedListView</c> class.
        /// </summary>
        public ExtendedListView() : this(ListViewCachingStrategy.RetainElement)
        {

        }

        /// <summary>
        /// Creates and initializes a new instance of the <c>ExtendedListView</c> class with the specified caching strategy.
        /// </summary>
        /// <param name="cacheStrategy">Cache strategy.</param>
        public ExtendedListView(ListViewCachingStrategy cacheStrategy) : base(cacheStrategy)
        {
        }

        /// <summary>
        /// Identifies the <c>SelectionAllowed</c> bindable property.
        /// </summary>
        public static readonly BindableProperty SelectionAllowedProperty =
            BindableProperty.Create(nameof(SelectionAllowed), typeof(bool), typeof(ExtendedListView), true);

        /// <summary>
        /// Gets or sets a value indicating whether the items in the list view can be selected. This is a bindable property.
        /// </summary>		
        public bool SelectionAllowed
        {
            get { return (bool)GetValue(SelectionAllowedProperty); }
            set { SetValue(SelectionAllowedProperty, value); }
        }

        /// <summary>
        /// Identifies the <c>CornerRadius</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(ExtendedListView), 0.0);

        /// <summary>
        /// Gets or sets the corner radius of the list view. This is a bindable property.
        /// </summary>		
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// Method that raises the <c>UIRefreshRequested</c> event and refreshes the user interface.
        /// </summary>
        /// <remarks>
        /// Instead of calling RefreshUI, a better approach is to call <see cref="BaseCellViewModel.ForceUpdateSize"/> on the cell that its height should be recalculated.
        /// If the ListView is jumping to the top make sure to give the <see cref="MonoTouch.UIKit.UITableView.EstimatedRowHeight"/> property a value in the <see cref="UI.ExtendedListViewRenderer"/>.
        /// </remarks>
        public void RefreshUI()
        {
            UIRefreshRequested?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Method to be called after a scroll completes.
        /// </summary>
        /// <param name="args">The scroll event arguments.</param>
        public void RaiseScrollEvent(ScrolledEventArgs args)
        {
            Scrolled?.Invoke(this, args);
        }


        /// <summary>
        /// Occurs when the user interface should be refreshed.
        /// </summary>
		public event EventHandler UIRefreshRequested;


        /// <summary>
        /// Event that is raised after a scroll completes.
        /// </summary>
        public event EventHandler<ScrolledEventArgs> Scrolled;

    }
}

