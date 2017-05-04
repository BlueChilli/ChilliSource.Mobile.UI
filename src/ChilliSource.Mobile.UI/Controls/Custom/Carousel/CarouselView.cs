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
using System.Linq;
using System.Timers;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class CarouselView : ScrollView
	{
		private readonly StackLayout _stack;
		private Timer _selectedItemTimer;
		private bool _layingOutChildren;
		private int _selectedIndex;

		public CarouselView()
		{
			Orientation = ScrollOrientation.Horizontal;

			_stack = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Spacing = 0
			};

			Content = _stack;

			_selectedItemTimer = new Timer
			{
				AutoReset = false,
				Interval = 300
			};

			_selectedItemTimer.Elapsed += SelectedItemTimerElapsed;
		}

		#region Properties

		public IList<View> Children
		{
			get
			{
				return _stack.Children;
			}
		}

		public static readonly BindableProperty SelectedIndexProperty =
			BindableProperty.Create(nameof(SelectedIndex),
				typeof(int), typeof(CarouselView), 0, BindingMode.TwoWay,
				propertyChanged: (bindable, oldValue, newValue) =>
				{
					((CarouselView)bindable).UpdateSelectedItem();
				});

		public int SelectedIndex
		{
			get
			{
				return (int)GetValue(SelectedIndexProperty);
			}
			set
			{
				SetValue(SelectedIndexProperty, value);
			}
		}

		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(CarouselView), null,
			  		propertyChanging: (bindableObject, oldValue, newValue) =>
			  		{
			  			((CarouselView)bindableObject).ItemsSourceChanging();
			  		},
			  		propertyChanged: (bindableObject, oldValue, newValue) =>
			  		{
			  			((CarouselView)bindableObject).ItemsSourceChanged();
					  });

		public IList ItemsSource
		{
			get
			{
				return (IList)GetValue(ItemsSourceProperty);
			}
			set
			{
				SetValue(ItemsSourceProperty, value);
			}
		}

		public DataTemplate ItemTemplate
		{
			get;
			set;
		}

		public static readonly BindableProperty SelectedItemProperty =
			BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(CarouselView), null, BindingMode.TwoWay,
				propertyChanged: (bindable, oldValue, newValue) =>
				{
					((CarouselView)bindable).UpdateSelectedIndex();
				});

		public object SelectedItem
		{
			get
			{
				return GetValue(SelectedItemProperty);
			}
			set
			{
				SetValue(SelectedItemProperty, value);
			}
		}

		#endregion


		#region Overrides

		protected override void LayoutChildren(double x, double y, double width, double height)
		{
			base.LayoutChildren(x, y, width, height);
			if (_layingOutChildren)
			{
				return;
			}

			_layingOutChildren = true;
			foreach (var child in Children)
			{
				child.WidthRequest = width;
			}
			_layingOutChildren = false;
		}

		#endregion

		#region Private Methods

		private void UpdateSelectedItem()
		{
			_selectedItemTimer.Stop();
			_selectedItemTimer.Start();
		}

		private void SelectedItemTimerElapsed(object sender, ElapsedEventArgs e)
		{
			Device.BeginInvokeOnMainThread(() => {

				SelectedItem = SelectedIndex > -1 ? Children[SelectedIndex].BindingContext : null;

			});
		}

		private void ItemsSourceChanging()
		{
			if (ItemsSource == null) return;
			_selectedIndex = ItemsSource.IndexOf(SelectedItem);
		}

		private void ItemsSourceChanged()
		{
			_stack.Children.Clear();

			foreach (var item in ItemsSource)
			{
				View view = null;

				if (ItemTemplate is DataTemplateSelector)
				{
					var selector = ItemTemplate as DataTemplateSelector;
					var template = selector?.SelectTemplate(item, this);
					view = template?.CreateContent() as View;
				}
				else
				{
					view = ItemTemplate.CreateContent() as View;
				}

				var bindableObject = view as BindableObject;
				if (bindableObject != null)
				{
					bindableObject.BindingContext = item;
				}
				_stack.Children.Add(view);
			}

			if (_selectedIndex >= 0)
			{
				SelectedIndex = _selectedIndex;
			}
		}

		private void UpdateSelectedIndex()
		{
			if (SelectedItem == BindingContext)
			{
				return;
			}

			SelectedIndex = Children
				.Select(c => c.BindingContext)
				.ToList()
				.IndexOf(SelectedItem);
		}



		#endregion
	}
}

