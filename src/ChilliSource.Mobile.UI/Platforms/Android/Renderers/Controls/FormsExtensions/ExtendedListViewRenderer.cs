using System;
using global::Android.Runtime;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AndroidListView = global::Android.Widget.AbsListView;
using AndroidScrollState = global::Android.Widget.ScrollState;
using ChilliSource.Mobile.UI;
using Android.Content;

[assembly: ExportRenderer(typeof(ExtendedListView), typeof(ExtendedListViewRenderer))]

namespace ChilliSource.Mobile.UI
{
    class ExtendedListViewRenderer: ListViewRenderer
    {
        public ExtendedListViewRenderer(Context context) : base(context)
        {

        }

        #region Protected Methods

        /// <summary>
        /// Called when the portable element changes.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var element = e.NewElement;

                Control.SetOnScrollListener(new PixelScrollDetector(this));
            }
        }

        #endregion

        #region Private Classes

        /// <summary>
        /// Pixel scroll detector which can be attached to a <see cref="AndroidListView"/>.
        /// </summary>
        private class PixelScrollDetector : Java.Lang.Object, AndroidListView.IOnScrollListener
        {
            #region Fields

            private ListView _element;
            private float _density;
            private int _contentOffset = 0;

            private TrackElement[] _trackElements =
            {
                new TrackElement(0),    // Top view, bottom Y
                new TrackElement(1),    // Mid view, bottom Y
                new TrackElement(2),    // Mid view, top Y
                new TrackElement(3)     // Bottom view, top Y
            };

            #endregion

            #region Ctor

            /// <summary>
            /// Initializes a new instance of the <see cref="PixelScrollDetector"/> class.
            /// </summary>
            /// <param name="renderer">The <see cref="ListViewRenderer"/>.</param>
            public PixelScrollDetector(ListViewRenderer renderer)
            {
                _element = renderer.Element;
                _density = renderer.Context.Resources.DisplayMetrics.Density;
            }

            #endregion

            #region IOnScrollListener Methods 

            /// <summary>
            /// Called when the <see cref="AndroidListView"/> has been scrolled.
            /// </summary>
            /// <param name="view">The <see cref="AndroidListView"/> whose scroll state is being reported.</param>
            /// <param name="scrollState">The current scroll state.</param>
            public void OnScrollStateChanged(AndroidListView view, [GeneratedEnum] AndroidScrollState scrollState)
            {
                // Initialize the values every time the list is moving.
                if (scrollState == AndroidScrollState.TouchScroll || scrollState == AndroidScrollState.Fling)
                {
                    foreach (var t in _trackElements)
                    {
                        t.SyncState(view);
                    }
                }
            }

            /// <summary>
            /// Called when the <see cref="AndroidListView"/> is being scrolled. 
            /// If the view is being scrolled, this method will be called before the next frame of the scroll is rendered.
            /// </summary>
            /// <param name="view">The <see cref="AndroidListView"/> whose scroll state is being reported.</param>
            /// <param name="firstVisibleItem">The index of the first visible cell.</param>
            /// <param name="visibleItemCount">The number of visible cells.</param>
            /// <param name="totalItemCount">The number of items in the list adaptor.</param>
            public void OnScroll(AndroidListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
            {
                var wasTracked = false;
                foreach (var t in _trackElements)
                {
                    if (!wasTracked)
                    {
                        if (t.IsSafeToTrack(view))
                        {
                            wasTracked = true;
                            _contentOffset += t.GetDeltaY();
                            SendScrollEvent(_contentOffset);
                            t.SyncState(view);
                        }
                        else
                        {
                            t.Reset();
                        }
                    }
                    else
                    {
                        t.SyncState(view);
                    }
                }
            }

            #endregion

            #region Private Methods

            /// <summary>
            /// Send the scrolled event to the portable event handler.
            /// </summary>
            /// <param name="y">The raw vertical content offset.</param>
            private void SendScrollEvent(double y)
            {
                var element = _element as ExtendedListView;

                // Calculate vertical offset in device-independent pixels (DIPs).
                var offset = Math.Abs(y) / _density;
                var args = new ScrolledEventArgs(0, offset);
                element?.RaiseScrollEvent(args);
            }

            #endregion

            #region Private Classes

            /// <summary>
            /// A wrapper to track a <see cref="Android.Views.View"/> within the native list view.
            /// </summary>
            private class TrackElement
            {
                #region Fields

                private readonly int _position;

                private global::Android.Views.View _trackedView;
                private int _trackedViewPrevPosition;
                private int _trackedViewPrevTop;

                #endregion

                #region Ctor

                /// <summary>
                /// Initializes a new instance of the <see cref="TrackElement"/> class.
                /// </summary>
                /// <param name="position">The element position.</param>
                public TrackElement(int position)
                {
                    _position = position;
                }

                #endregion

                #region Public Methods

                /// <summary>
                /// Synchronize the state for the tracked view.
                /// </summary>
                /// <param name="view">The native list view.</param>
                public void SyncState(AndroidListView view)
                {
                    if (view.ChildCount > 0)
                    {
                        _trackedView = GetChild(view);
                        _trackedViewPrevTop = GetY();
                        _trackedViewPrevPosition = view.GetPositionForView(_trackedView);
                    }
                }

                /// <summary>
                /// Reset the tracked view.
                /// </summary>
                public void Reset()
                {
                    _trackedView = null;
                }

                /// <summary>
                /// Determine if it is safe to track the nested view.
                /// </summary>
                /// <param name="view">The native list view.</param>
                /// <returns>True if safe, false if not.</returns>
                public bool IsSafeToTrack(AndroidListView view)
                {
                    return _trackedView != null
                        && _trackedView.Parent == view
                        && view.GetPositionForView(_trackedView) == _trackedViewPrevPosition;
                }

                /// <summary>
                /// Get the delta Y-movement for the tracked view.
                /// </summary>
                /// <returns>The delta (change) along the Y-axis.</returns>
                public int GetDeltaY()
                {
                    return GetY() - _trackedViewPrevTop;
                }

                #endregion

                #region Private Methods

                /// <summary>
                /// Get a child view for the specified position.
                /// </summary>
                /// <param name="view">The native list view.</param>
                /// <returns>A child view for the specified position.</returns>
                private global::Android.Views.View GetChild(AndroidListView view)
                {
                    switch (_position)
                    {
                        case 0:
                            return view.GetChildAt(0);
                        case 1:
                        case 2:
                            return view.GetChildAt(view.ChildCount / 2);
                        case 3:
                            return view.GetChildAt(view.ChildCount - 1);
                        default:
                            return null;
                    }
                }

                /// <summary>
                /// Get the Y-anchor point for the tracked view.
                /// </summary>
                /// <returns>The Y anchor point.</returns>
                private int GetY()
                {
                    return _position <= 1
                        ? _trackedView.Bottom
                        : _trackedView.Top;
                }

                #endregion
            }

            #endregion
        }

        #endregion
    }
}
