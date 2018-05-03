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
using Android.Content;

[assembly: ExportRenderer(typeof(ExtendedTabbedPage), typeof(ExtendedTabbedPageRenderer))]
namespace ChilliSource.Mobile.UI
{
    public class ExtendedTabbedPageRenderer : TabbedPageRenderer
    {
        private TabLayout _tabLayout;

        public ExtendedTabbedPageRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                RemoveCollectionEvent(e.OldElement as ExtendedTabbedPage);

                _tabLayout = null;
            }

            if (e.NewElement != null)
            {

                if (Context is BaseActivity activity)
                {
                    _tabLayout = ViewGroup.FindViewById<TabLayout>(activity.TabLayoutResourceId);
                }

                if (_tabLayout != null)
                {
                    UpdateTabMode(_tabLayout);
                    UpdateGravity(_tabLayout);
                    UpdateTextColors(_tabLayout);
                    UpdateColors(_tabLayout);
                    UpdateTabBarItems(TabPage.TabItems);
                }

                AddCollectionEvent(e.NewElement as ExtendedTabbedPage);
            }
        }

        private void UpdateTextColors(TabLayout tabLayout)
        {
            if (TabPage.SelectedTabItemTextColor.HasValue)
            {
                tabLayout.SetTabTextColors(TabPage.BarTextColor.ToAndroid(), TabPage.SelectedTabItemTextColor.Value.ToAndroid());
            }
        }

        private void UpdateColors(TabLayout tabLayout)
        {
            tabLayout.SetSelectedTabIndicatorColor(TabPage.SelectedTabItemTintColor.ToAndroid());
        }

        private void UpdateGravity(TabLayout tabLayout)
        {
            switch (TabPage.Gravity)
            {
                case StyledTabBarGravity.Center:
                    {
                        tabLayout.TabGravity = TabLayout.GravityCenter;
                        break;
                    }
                case StyledTabBarGravity.Fill:
                    {
                        tabLayout.TabGravity = TabLayout.GravityFill;
                        break;
                    }
            }
        }

        private void UpdateTabMode(TabLayout tabLayout)
        {
            switch (TabPage.TabMode)
            {
                case StyledTabBarLayoutMode.Fixed:
                    {
                        tabLayout.TabMode = TabLayout.ModeFixed;
                        break;
                    }
                case StyledTabBarLayoutMode.Scrollable:
                    {
                        tabLayout.TabGravity = TabLayout.ModeScrollable;
                        break;
                    }
            }
        }

        private void RemoveCollectionEvent(ExtendedTabbedPage tabs)
        {
            if (tabs?.TabItems is ObservableCollection<ITabItem> tabItems)
            {
                tabItems.CollectionChanged -= OnCollectionChanged;
            }
        }

        private void AddCollectionEvent(ExtendedTabbedPage tabs)
        {
            if (tabs?.TabItems is ObservableCollection<ITabItem> tabItems)
            {
                tabItems.CollectionChanged += OnCollectionChanged;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!disposing) { return; }

            if (Element == null) { return; }

            RemoveCollectionEvent(Element as ExtendedTabbedPage);
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (TabPage?.TabItems != null)
            {
                UpdateTabBarItems(TabPage.TabItems);
            }
        }

        public ExtendedTabbedPage TabPage => Element as ExtendedTabbedPage;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(TabbedPage.CurrentPage))
            {
                UpdateTabBarItems(TabPage.TabItems);
            }
            else if (e.PropertyName == NavigationPage.BarTextColorProperty.PropertyName)
            {
                UpdateColors(_tabLayout);
            }

            if (!e.PropertyName.Equals(ExtendedTabbedPage.TabItemsProperty.PropertyName)) { return; }

            RemoveCollectionEvent(Element as ExtendedTabbedPage);
            AddCollectionEvent(Element as ExtendedTabbedPage);

            if (TabPage?.TabItems != null)
            {
                UpdateTabBarItems(TabPage.TabItems);
            }
        }

        private void UpdateTabBarItems(IList<ITabItem> tabItems)
        {
            if (_tabLayout == null) { return; }

            if (_tabLayout.TabCount == 0) { return; }

            var selectedTabIndex = 0;

            if (TabPage.CurrentPage != null)
            {
                selectedTabIndex = TabPage.Children.IndexOf(TabPage.CurrentPage);
            }

            for (var i = 0; i < _tabLayout.TabCount; i++)
            {
                var tab = _tabLayout.GetTabAt(i);

                var tabItem = GetTabItem(tab, tabItems);

                if (tabItem == null) { continue; }

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
