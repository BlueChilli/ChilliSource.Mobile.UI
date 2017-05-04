#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;
using System;
using System.Threading.Tasks;
using Foundation;
using CoreGraphics;

[assembly: ExportRenderer(typeof(StyledTabbedPage), typeof(StyledTabbedPageRenderer))]
namespace ChilliSource.Mobile.UI
{
	public class StyledTabbedPageRenderer : TabbedRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				RemoveCollectionEvent(e.OldElement as StyledTabbedPage);

				e.OldElement.PropertyChanged -= OnElementPropertyChanged;
			}

			if (e.NewElement != null)
			{
				AddCollectionEvent(e.NewElement as StyledTabbedPage);

				e.NewElement.PropertyChanged += OnElementPropertyChanged;
			}
		}

		private void RemoveCollectionEvent(StyledTabbedPage tabs)
		{
			var tabItems = tabs?.TabItems as ObservableCollection<ITabItem>;

			if (tabItems != null)
			{
				tabItems.CollectionChanged -= OnCollectionChanged;
			}
		}

		private void AddCollectionEvent(StyledTabbedPage tabs)
		{
			var tabItems = tabs?.TabItems as ObservableCollection<ITabItem>;

			if (tabItems != null)
			{
				tabItems.CollectionChanged += OnCollectionChanged;
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (!disposing) return;

			if (Element == null) return;

			Element.PropertyChanged -= OnElementPropertyChanged;
			RemoveCollectionEvent(Element as StyledTabbedPage);
		}

		private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (!e.PropertyName.Equals(StyledTabbedPage.TabItemsProperty.PropertyName)) return;

			RemoveCollectionEvent(Element as StyledTabbedPage);
			AddCollectionEvent(Element as StyledTabbedPage);

			if (this.TabPage?.TabItems != null)
			{
				UpdateTabBarItems(this.TabPage.TabItems);
			}
		}


		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (this.TabPage?.TabItems != null)
			{
				UpdateTabBarItems(this.TabPage.TabItems);
			}

		}

		public StyledTabbedPage TabPage => Element as StyledTabbedPage;

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			if (TabPage == null) return;

			TabBar.Opaque = TabPage.IsOpaque;
			TabBar.TintColor = TabPage.TintColor.ToUIColor();
			TabBar.SelectedImageTintColor = TabPage.SelectedTintColor.ToUIColor();
			UpdateTabBarItems(TabPage.TabItems);
		}

		private void UpdateTabBarItems(IList<ITabItem> tabItems)
		{
			if (TabBar.Items == null) return;

			foreach (var tabbarItem in TabBar.Items)
			{
				var tabItem = GetTabItem(tabbarItem, tabItems);
				if (tabItem == null) continue;
				tabbarItem.Enabled = tabItem.IsEnabled;
				if (tabItem.Icon != null) tabbarItem.Image = UIImage.FromBundle(tabItem.Icon);
				if (tabItem.SelectedIcon != null) tabbarItem.SelectedImage = UIImage.FromBundle(tabItem.SelectedIcon);
				tabbarItem.BadgeValue = tabItem.BadgeCount;
			}
		}

		private ITabItem GetTabItem(UITabBarItem item, IList<ITabItem> tabItems)
		{
			return tabItems.FirstOrDefault(m => m.Title.Equals(item.Title));
		}
	}
}
