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
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public enum Mode
	{
		Fixed,
		Scrollable
	}

	public enum Gravity
	{
		Fill,
		Center
	}

	public interface ITabItem
	{
		string Icon { get; set; }
		string SelectedIcon { get; set; }
		string Title { get; set; }
		string BadgeCount { get; set; }
		bool IsEnabled { get; set; }
	}
	public class TabItem : ITabItem
	{
		public TabItem()
		{
			IsEnabled = true;
		}

		public TabItem(string title, string icon, string selectedIcon)
		{
			IsEnabled = true;
			Title = title;
			Icon = icon;
			SelectedIcon = selectedIcon;
		}

		public string Icon { get; set; }
		public string SelectedIcon { get; set; }
		public string Title { get; set; }
		public string BadgeCount { get; set; }
		public bool IsEnabled { get; set; }
	}

	public abstract class StyledTabbedPage : TabbedPage
	{
		public static BindableProperty TabItemsProperty = BindableProperty.Create(nameof(TabItems), typeof(IList<ITabItem>), typeof(StyledTabbedPage), new ObservableCollection<ITabItem>());

		public IList<ITabItem> TabItems
		{
			get { return (IList<ITabItem>)this.GetValue(TabItemsProperty); }
			set { this.SetValue(TabItemsProperty, value); }
		}

		public bool IsOpaque { get; set; }

		public Color SelectedTintColor { get; set; }

		public Color TintColor { get; set; }

		public Color? SelectedBarTextColor { get; set; }
		public Mode TabMode { get; set; }

		public Gravity Gravity { get; set; }

	}
}
