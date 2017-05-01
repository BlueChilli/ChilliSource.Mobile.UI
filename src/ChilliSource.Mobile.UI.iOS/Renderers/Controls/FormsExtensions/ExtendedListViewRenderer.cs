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
using ChilliSource.Mobile.UI.Core;

[assembly: ExportRenderer(typeof(ExtendedListView), typeof(ExtendedListViewRenderer))]
namespace ChilliSource.Mobile.UI
{

	public class ExtendedListViewRenderer : ListViewRenderer, IUITableViewDelegate, IUIScrollViewDelegate
	{
		UITableViewSource _originalDelegate;
		ExtendedListView _listView;

		bool _keyboardVisible;
		bool _shouldScrollToCell;

		UIView activeView;

		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);

			_listView = (ExtendedListView)Element;

			if (new DeviceService().IsScreenSizeFourInchesOrLess())
			{
				Control.AlwaysBounceVertical = false;
				if (_originalDelegate == null &&
					Control.WeakDelegate != null)
				{
					_originalDelegate = (UITableViewSource)Control.WeakDelegate;
				}

				Control.WeakDelegate = this;
			}

			if (e.OldElement != null)
			{
				if (_listView != null)
				{
					_listView.UIRefreshRequested -= RefreshUI;
				}
			}

			if (e.NewElement != null)
			{
				if (_listView != null)
				{
					_listView.UIRefreshRequested += RefreshUI;
				}
			}
		}

		public override void MovedToWindow()
		{
			base.MovedToWindow();
			RegisterForKeyboardNotifications();
		}

		private void RegisterForKeyboardNotifications()
		{
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
		}

		private void RefreshUI(object sender, EventArgs ea)
		{
			Control.BeginUpdates();
			Control.EndUpdates();
		}

		private void OnKeyboardNotification(NSNotification notification)
		{
			_keyboardVisible = notification.Name == UIKeyboard.WillShowNotification;
			var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);


			if (_listView != null)
			{

				if (_listView.Parent != null && (_listView.Parent is View))
				{
					if (_keyboardVisible)
					{
						activeView = Control.FindFirstResponder();

						if (activeView != null && activeView.IsKeyboardOverlapping(Control, keyboardFrame))
						{
							((View)_listView.Parent).TranslateTo(0, -_listView.Bounds.Y, 200, Easing.Linear);
							_shouldScrollToCell = true;
						}

					}
					else
					{
						((View)_listView.Parent).TranslateTo(0, 0, 200, Easing.Linear);
						_shouldScrollToCell = false;
					}
				}
			}
		}

		[Export("tableView:didSelectRowAtIndexPath:")]
		public void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			_originalDelegate.RowSelected(tableView, indexPath);
		}

		[Export("tableView:heightForHeaderInSection:")]
		public Single GetHeightForHeader(UITableView tableView, int section)
		{
			return (float)_originalDelegate.GetHeightForHeader(tableView, section);
		}

		[Export("tableView:viewForHeaderInSection:")]
		public UIView GetViewForHeader(UITableView tableView, int section)
		{
			return _originalDelegate.GetViewForHeader(tableView, section);
		}

		[Export("scrollViewDidScroll:")]
		public void Scrolled(UIScrollView scrollView)
		{
			if (_keyboardVisible && _shouldScrollToCell)
			{
				var cellIndexPath = Control.IndexPathForRowAtPoint(new CGPoint(Control.Center.X, Convert.ToSingle(activeView.GetViewRelativeBottom(Control))));
				if (cellIndexPath != null)
				{
					Control.ScrollToRow(cellIndexPath, UITableViewScrollPosition.Top, false);
				}
			}
		}
	}
}
