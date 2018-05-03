#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ExtendedListView), typeof(ExtendedListViewRenderer))]
namespace ChilliSource.Mobile.UI
{    
    public class ExtendedListViewRenderer : ListViewRenderer
    {
        #region Lifecycle

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
           
            if (e.OldElement != null && ListView != null)
            {
                ListView.UIRefreshRequested -= RefreshUI;
            }

            if (e.NewElement != null && ListView != null)
            {
                ListView.UIRefreshRequested += RefreshUI;
                Control.AllowsSelection = ListView.SelectionAllowed;
                Control.Layer.CornerRadius = (nfloat)ListView.CornerRadius;
            }

            if (e.NewElement != null)
            {                
                Control.Delegate = new ListViewTableViewDelegate(this);                
            }
        }

        #endregion

        #region Properties

        public ExtendedListView ListView => Element as ExtendedListView;

        #endregion

        #region Helper Methods

        void RefreshUI(object sender, EventArgs ea)
        {
            Control.BeginUpdates();
            Control.EndUpdates();
        }
       
        #endregion

       
        #region Private Classes

        /// <summary>
        /// Custom <see cref="UITableViewDelegate"/> to use when <see cref="ListView.HasUnevenRows"/> is false.
        /// Note: This allows us to intercept base renderer lifecycle methods i.e. capture scrolling.
        /// </summary>
        private class ListViewTableViewDelegate : UITableViewDelegate
        {
            #region Fields

            private ListView _element;
            private UITableViewSource _source;

            #endregion

            #region Ctor

            /// <summary>
            /// Initializes a new instance of the <see cref="ListViewTableViewDelegate"/> class.
            /// </summary>
            /// <param name="renderer">The <see cref="ListViewRenderer"/>.</param> 
            public ListViewTableViewDelegate(ListViewRenderer renderer)
            {
                _element = renderer.Element;
                _source = renderer.Control.Source;
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// Handle the dragging ended behavior.
            /// </summary>
            /// <param name="scrollView">The scroll view.</param>
            /// <param name="willDecelerate">If set to <c>true</c> will decelerate.</param>
            public override void DraggingEnded(UIScrollView scrollView, bool willDecelerate)
            {
                _source.DraggingEnded(scrollView, willDecelerate);
            }

            /// <summary>
            /// Handle the dragging started behavior.
            /// </summary>
            /// <param name="scrollView">The scroll view.</param>
            public override void DraggingStarted(UIScrollView scrollView)
            {
                _source.DraggingStarted(scrollView);
            }

            /// <summary>
            /// Gets the height for a header.
            /// </summary>
            /// <returns>The height for the header.</returns>
            /// <param name="tableView">The <see cref="UITableView"/>.</param>
            /// <param name="section">The section index.</param>
            public override System.nfloat GetHeightForHeader(UITableView tableView, System.nint section)
            {
                return _source.GetHeightForHeader(tableView, section);
            }

            /// <summary>
            /// Gets the view for header.
            /// </summary>
            /// <returns>The view for header.</returns>
            /// <param name="tableView">The <see cref="UITableView"/>.</param>
            /// <param name="section">The section index.</param>
            public override UIView GetViewForHeader(UITableView tableView, System.nint section)
            {
                return _source.GetViewForHeader(tableView, section);
            }

            /// <summary>
            /// Handle the row deselected behavior.
            /// </summary>
            /// <param name="tableView">The <see cref="UITableView"/>.</param>
            /// <param name="indexPath">The index path for the row.</param>
            public override void RowDeselected(UITableView tableView, Foundation.NSIndexPath indexPath)
            {
                _source.RowDeselected(tableView, indexPath);
            }

            /// <summary>
            /// Handle the row selected behavior.
            /// </summary>
            /// <param name="tableView">The <see cref="UITableView"/>.</param>
            /// <param name="indexPath">The index path for the row.</param>
            public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
            {
                _source.RowSelected(tableView, indexPath);
            }

            /// <summary>
            /// Handle the scroll behavior.
            /// </summary>
            /// <param name="scrollView">The scroll view.</param>
            public override void Scrolled(UIScrollView scrollView)
            {
                _source.Scrolled(scrollView);

                bool isBouncingTop = scrollView.ContentOffset.Y < 0;
                bool isBouncingBottom = scrollView.ContentOffset.Y > (scrollView.ContentSize.Height - scrollView.Frame.Size.Height);

                if (!isBouncingTop && !isBouncingBottom)
                {
                    SendScrollEvent(scrollView.ContentOffset.Y);
                }                               
            }

            #endregion

            #region Private Methods

            /// <summary>
            /// Send the scrolled event to the portable event handler.
            /// </summary>
            /// <param name="y">The vertical content offset.</param>
            private void SendScrollEvent(double y)
            {
                var element = _element as ExtendedListView;
                var args = new ScrolledEventArgs(0, y);
                element?.RaiseScrollEvent(args);
            }

            #endregion
        }

        #endregion
    }
}
