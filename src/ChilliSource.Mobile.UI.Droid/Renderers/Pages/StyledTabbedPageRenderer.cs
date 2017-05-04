#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Android.Content.Res;
using Android.Support.Design.Widget;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(StyledTabbedPage), typeof(StyledTabbedPageRenderer))]
namespace ChilliSource.Mobile.UI
{
	public class StyledTabbedPageRenderer : TabbedPageRenderer
	{
		private TabLayout _tabLayout;


		protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
		{

			base.OnElementChanged(e);


			if (e.OldElement != null)
			{
				RemoveCollectionEvent(e.OldElement as StyledTabbedPage);

				_tabLayout = null;
			}

			if (e.NewElement != null)
			{
				var activity = Context as BaseActivity;

				if (activity != null)
				{
					_tabLayout = this.ViewGroup.FindViewById<TabLayout>(activity.TabLayoutResourceId);
				}


				if (_tabLayout != null)
				{
					UpdateTabMode(_tabLayout);
					UpdateGravity(_tabLayout);
					UpdateTextColors(_tabLayout);
					UpdateColors(_tabLayout);
					UpdateTabBarItems(this.TabPage.TabItems);


				}

				AddCollectionEvent(e.NewElement as StyledTabbedPage);


			}
		}

		private void UpdateTextColors(TabLayout tabLayout)
		{
			if (this.TabPage.SelectedBarTextColor != null)
			{
				tabLayout.SetTabTextColors(this.TabPage.BarTextColor.ToAndroid(), this.TabPage.SelectedBarTextColor.Value.ToAndroid());
			}




		}


		private void UpdateColors(TabLayout tabLayout)
		{
			tabLayout.SetSelectedTabIndicatorColor(this.TabPage.SelectedTintColor.ToAndroid());

		}

		private void UpdateGravity(TabLayout tabLayout)
		{
			switch (this.TabPage.Gravity)
			{
				case Gravity.Center:
					tabLayout.TabGravity = TabLayout.GravityCenter;
					break;
				case Gravity.Fill:
					tabLayout.TabGravity = TabLayout.GravityFill;
					break;
			}
		}

		private void UpdateTabMode(TabLayout tabLayout)
		{
			switch (this.TabPage.TabMode)
			{
				case Mode.Fixed:
					tabLayout.TabMode = TabLayout.ModeFixed;
					break;
				case Mode.Scrollable:
					tabLayout.TabGravity = TabLayout.ModeScrollable;
					break;
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

			RemoveCollectionEvent(Element as StyledTabbedPage);
		}

		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (this.TabPage?.TabItems != null)
			{
				UpdateTabBarItems(this.TabPage.TabItems);
			}
		}

		public StyledTabbedPage TabPage => Element as StyledTabbedPage;

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == nameof(TabbedPage.CurrentPage))
			{
				UpdateTabBarItems(this.TabPage.TabItems);
			}
			else if (e.PropertyName == NavigationPage.BarTextColorProperty.PropertyName)
			{
				UpdateColors(_tabLayout);
			}

			if (!e.PropertyName.Equals(StyledTabbedPage.TabItemsProperty.PropertyName)) return;

			RemoveCollectionEvent(Element as StyledTabbedPage);
			AddCollectionEvent(Element as StyledTabbedPage);

			if (this.TabPage?.TabItems != null)
			{
				UpdateTabBarItems(this.TabPage.TabItems);
			}
		}



		private void UpdateTabBarItems(IList<ITabItem> tabItems)
		{
			if (_tabLayout == null) return;

			if (_tabLayout.TabCount == 0) return;


			var selectedTabIndex = 0;

			if (this.TabPage.CurrentPage != null)
			{
				selectedTabIndex = this.TabPage.Children.IndexOf(this.TabPage.CurrentPage);

			}


			for (var i = 0; i < _tabLayout.TabCount; i++)
			{
				var tab = _tabLayout.GetTabAt(i);

				var tabItem = GetTabItem(tab, tabItems);
				if (tabItem == null) continue;

				if (i == selectedTabIndex)
				{
					if (!String.IsNullOrWhiteSpace(tabItem.SelectedIcon))
					{
						tab.SetIcon(Resources.GetIdentifier(tabItem.SelectedIcon, "drawable", this.Context.PackageName));
					}
				}
				else
				{
					if (!String.IsNullOrWhiteSpace(tabItem.Icon))
					{
						tab.SetIcon(Resources.GetIdentifier(tabItem.Icon, "drawable", this.Context.PackageName));
					}
				}



			}
		}

		private ITabItem GetTabItem(TabLayout.Tab item, IList<ITabItem> tabItems)
		{
			return tabItems.FirstOrDefault(m => m.Title.Equals(item.Text));
		}


	}
}
