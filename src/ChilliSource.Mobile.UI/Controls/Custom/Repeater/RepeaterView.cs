#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

// ***********************************************************************
// Assembly         : XLabs.Forms
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="RepeaterView.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Low cost control to display a set of clickable items
	/// </summary>
	/// <typeparam name="T">The Type of viewmodel</typeparam>
	public class RepeaterView<T> : StackLayout
		where T : class
	{
		/// <summary>
		/// Definition for <see cref="ItemTemplate"/>
		/// </summary>
		/// Element created at 15/11/2014,3:11 PM by Charles
		//public static readonly BindableProperty ItemTemplateProperty =
		//	BindableProperty.Create<RepeaterView<T>, DataTemplate>(
		//		p => p.ItemTemplate,
		//		default(DataTemplate));
		public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate),
																							   typeof(DataTemplate),
																							   typeof(RepeaterView<T>),
																							   default(DataTemplate));

		/// <summary>
		/// Definition for <see cref="ItemsSource"/>
		/// </summary>
		/// Element created at 15/11/2014,3:11 PM by Charles
		//public static readonly BindableProperty ItemsSourceProperty =
		//	BindableProperty.Create<RepeaterView<T>, IEnumerable<T>>(
		//		p => p.ItemsSource,
		//		Enumerable.Empty<T>(),
		//		BindingMode.OneWay,
		//		null,
		//		ItemsChanged);
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource),
																							  typeof(IEnumerable<T>),
																							  typeof(RepeaterView<T>),
																							  Enumerable.Empty<T>(),
																							  BindingMode.OneWay,
																							  null, ItemsChanged);

		/// <summary>
		/// Definition for <see cref="ItemClickCommand"/>
		/// </summary>
		/// Element created at 15/11/2014,3:11 PM by Charles
		//public static BindableProperty ItemClickCommandProperty =
		//	BindableProperty.Create<RepeaterView<T>, ICommand>(x => x.ItemClickCommand, null);

		public static BindableProperty ItemClickCommandProperty = BindableProperty.Create(nameof(ItemClickCommand),
																						  typeof(ICommand),
																						  typeof(RepeaterView<T>),
																						  null);

		/// <summary>
		/// Definition for <see cref="TemplateSelector"/>
		/// </summary>
		/// Element created at 15/11/2014,3:12 PM by Charles
		//public static readonly BindableProperty TemplateSelectorProperty =
		//	BindableProperty.Create<RepeaterView<T>, TemplateSelector>(
		//		x => x.TemplateSelector,
		//		default(TemplateSelector));

		public static BindableProperty TemplateSelectorProperty = BindableProperty.Create(nameof(TemplateSelector),
																						  typeof(TemplateSelector),
																						  typeof(RepeaterView<T>),
																						  default(TemplateSelector));

		/// <summary>
		/// The item template selector property
		/// </summary>
		//public static readonly BindableProperty ItemTemplateSelectorProperty = BindableProperty.Create<RepeaterView<T>, DataTemplateSelector>(x => x.ItemTemplateSelector, default(DataTemplateSelector), propertyChanged: OnDataTemplateSelectorChanged);
		public static BindableProperty ItemTemplateSelectorProperty = BindableProperty.Create(nameof(ItemTemplateSelector),
																							  typeof(DataTemplateSelector),
																							  typeof(RepeaterView<T>),
																							  default(DataTemplateSelector),
																							  propertyChanged: OnDataTemplateSelectorChanged);

		private DataTemplateSelector currentItemSelector;
		/// <summary>
		/// Gets or sets the item template selector.
		/// </summary>
		/// <value>The item template selector.</value>
		public DataTemplateSelector ItemTemplateSelector
		{
			get
			{
				return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty);
			}
			set
			{
				SetValue(ItemTemplateSelectorProperty, value);
			}
		}

		private static void OnDataTemplateSelectorChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			((RepeaterView<T>)bindable).OnDataTemplateSelectorChanged((DataTemplateSelector)oldvalue, (DataTemplateSelector)newvalue);
		}

		/// <summary>
		/// Called when [data template selector changed].
		/// </summary>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		/// <exception cref="System.ArgumentException">Cannot set both ItemTemplate and ItemTemplateSelector;ItemTemplateSelector</exception>
		protected virtual void OnDataTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue)
		{
			// check to see we don't have an ItemTemplate set
			if (ItemTemplate != null && newValue != null)
				throw new ArgumentException("Cannot set both ItemTemplate and ItemTemplateSelector", "ItemTemplateSelector");

			currentItemSelector = newValue;
		}

		/// <summary>
		/// Event delegate definition fo the <see cref="ItemCreated"/> event
		/// </summary>
		/// <param name="sender">The sender(this).</param>
		/// <param name="args">The <see cref="RepeaterViewItemAddedEventArgs"/> instance containing the event data.</param>
		/// Element created at 15/11/2014,3:12 PM by Charles
		public delegate void RepeaterViewItemAddedEventHandler(
			object sender,
			RepeaterViewItemAddedEventArgs args);

		/// <summary>Occurs when a view has been created.</summary>
		/// Element created at 15/11/2014,3:13 PM by Charles
		public event RepeaterViewItemAddedEventHandler ItemCreated;

		/// <summary>
		/// The Collection changed handler
		/// </summary>
		/// Element created at 15/11/2014,3:13 PM by Charles
		private IDisposable _collectionChangedHandle;

		/// <summary>
		/// Initializes a new instance of the <see cref="RepeaterView{T}"/> class.
		/// </summary>
		/// Element created at 15/11/2014,3:13 PM by Charles
		public RepeaterView()
		{
			Spacing = 0;
		}

		/// <summary>Gets or sets the items source.</summary>
		/// <value>The items source.</value>
		/// Element created at 15/11/2014,3:13 PM by Charles
		public IEnumerable<T> ItemsSource
		{
			get { return (IEnumerable<T>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		/// <summary>Gets or sets the template selector.</summary>
		/// <value>The template selector.</value>
		/// Element created at 15/11/2014,3:13 PM by Charles
		public TemplateSelector TemplateSelector
		{
			get { return (TemplateSelector)GetValue(TemplateSelectorProperty); }
			set { SetValue(TemplateSelectorProperty, value); }
		}

		/// <summary>Gets or sets the item click command.</summary>
		/// <value>The item click command.</value>
		/// Element created at 15/11/2014,3:13 PM by Charles
		public ICommand ItemClickCommand
		{
			get { return (ICommand)this.GetValue(ItemClickCommandProperty); }
			set { SetValue(ItemClickCommandProperty, value); }
		}

		/// <summary>
		/// The item template property
		/// This can be used on it's own or in combination with 
		/// the <see cref="TemplateSelector"/>
		/// </summary>
		/// Element created at 15/11/2014,3:10 PM by Charles
		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}

		/// <summary>
		/// Gives codebehind a chance to play with the
		/// newly created view object :D
		/// </summary>
		/// <param name="view">The visual view object</param>
		/// <param name="model">The item being added</param>
		protected virtual void NotifyItemAdded(View view, T model)
		{
			if (ItemCreated != null)
			{
				ItemCreated(this, new RepeaterViewItemAddedEventArgs(view, model));
			}
		}

		/// <summary>
		/// Select a datatemplate dynamically
		/// Prefer the TemplateSelector then the DataTemplate
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		protected virtual DataTemplate GetTemplateFor(Type type)
		{
			DataTemplate retTemplate = null;
			if (TemplateSelector != null) retTemplate = TemplateSelector.TemplateFor(type);
			return retTemplate ?? ItemTemplate;
		}

		/// <summary>
		/// Creates a view based on the items type
		/// While we do have T, T could very well be
		/// a common superclass or an interface by
		/// using the items actual type we support
		/// both inheritance based polymorphism
		/// and shape based polymorphism
		///
		/// </summary>
		/// <param name="item"></param>
		/// <returns>A <see cref="View"/> item as it's BindingContext</returns>
		/// <exception cref="InvalidVisualObjectException"></exception>Thrown when the matched datatemplate inflates to an object not derived from either
		/// <see cref="Xamarin.Forms.View"/> or <see cref="Xamarin.Forms.ViewCell"/>
		protected virtual View ViewFor(T item)
		{
			// Check the item template selector first
			View view = null;
			if (currentItemSelector != null)
			{
				view = this.ViewFor(item, currentItemSelector);
			}

			if (view == null)
			{
				var template = GetTemplateFor(item.GetType());
				var content = template.CreateContent();

				if (!(content is View) && !(content is ViewCell)) throw new InvalidVisualObjectException(content.GetType());
				view = (content is View) ? content as View : ((ViewCell)content).View;
			}

			view.BindingContext = item;
			view.GestureRecognizers.Add(
				new TapGestureRecognizer { Command = ItemClickCommand, CommandParameter = item });
			return view;
		}



		/// <summary>
		/// Reset the collection of bound objects
		/// Remove the old collection changed eventhandler (if any)
		/// Create new cells for each new item
		/// </summary>
		/// <param name="bindable">The control</param>
		/// <param name="oldValue">Previous bound collection</param>
		/// <param name="newValue">New bound collection</param>
		private static void ItemsChanged(
			BindableObject bindable,
			object oldValue,
			object newValue)
		{
			var control = bindable as RepeaterView<T>;
			if (control == null)
				throw new Exception(
					"Invalid bindable object passed to ReapterView::ItemsChanged expected a ReapterView<T> received a "
					+ bindable.GetType().Name);

			if (control._collectionChangedHandle != null)
			{
				control._collectionChangedHandle.Dispose();
			}

			control._collectionChangedHandle = new CollectionChangedHandle<View, T>(
				control.Children,
				(IEnumerable<T>)newValue,
				control.ViewFor,
				(v, m, i) => control.NotifyItemAdded(v, m));
		}
	}

	/// <summary>
	/// Argument for the <see cref="RepeaterView{T}.ItemCreated"/> event
	/// </summary>
	/// Element created at 15/11/2014,3:13 PM by Charles
	public class RepeaterViewItemAddedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RepeaterViewItemAddedEventArgs"/> class.
		/// </summary>
		/// <param name="view">The view.</param>
		/// <param name="model">The model.</param>
		/// Element created at 15/11/2014,3:14 PM by Charles
		public RepeaterViewItemAddedEventArgs(View view, object model)
		{
			View = view;
			Model = model;
		}

		/// <summary>Gets or sets the view.</summary>
		/// <value>The visual element.</value>
		/// Element created at 15/11/2014,3:14 PM by Charles
		public View View { get; set; }

		/// <summary>Gets or sets the model.</summary>
		/// <value>The original viewmodel.</value>
		/// Element created at 15/11/2014,3:14 PM by Charles
		public object Model { get; set; }
	}


}
