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
    /// Stacklayout extension that provides item source data binding for child views
    /// </summary>
    public class RepeaterView<T> : StackLayout where T : class
    {
        private DataTemplateSelector _currentItemSelector;
        private IDisposable _collectionChangedHandle;

        /// <summary>
        /// Initializes a new instance of this <c>RepeaterView</c> class.
        /// </summary>
        public RepeaterView()
        {
            Spacing = 0;
        }

        /// <summary>
        /// Backing store for the <c>ItemTemplate</c> bindable property.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(RepeaterView<T>), default(DataTemplate));

        /// <summary>
        /// Gets or sets the template that defines the content and layout of items within the view. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="DataTemplate"/> value that defines how to dislay the items in the repeater view.</value>
        /// <remarks>
        /// This can be used on it's own or in combination with the <c>TemplateSelector</c>.
        ///</remarks>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        /// <summary>
        /// Backing store for the <c>ItemSource</c> bindable property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<T>), typeof(RepeaterView<T>), Enumerable.Empty<T>(), BindingMode.OneWay, null, ItemsChanged);

        /// <summary>
        /// Gets or sets the data source for populating items. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="IEnumerable{T}"/> value that represents the source of the data for the repeater view.</value>
        public IEnumerable<T> ItemsSource
        {
            get { return (IEnumerable<T>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Backing store for the <c>ItemClickCommand</c> bindable property.
        /// </summary>
        public static BindableProperty ItemClickCommandProperty =
            BindableProperty.Create(nameof(ItemClickCommand), typeof(ICommand), typeof(RepeaterView<T>), null);

        /// <summary>
        /// Gets or sets the command to invoke when an item is clicked. This is a bindable proprty.
        /// </summary>
        /// <value>A <see cref="ICommand"/> value that represents the command invoked when an item is clicked.</value>
        public ICommand ItemClickCommand
        {
            get { return (ICommand)this.GetValue(ItemClickCommandProperty); }
            set { SetValue(ItemClickCommandProperty, value); }
        }

        /// <summary>
        /// Backing store for the <c>TemplateSelector</c> bindable property.
        /// </summary>
        public static BindableProperty TemplateSelectorProperty =
            BindableProperty.Create(nameof(TemplateSelector), typeof(TemplateSelector), typeof(RepeaterView<T>), default(TemplateSelector));


        /// <summary>
        /// Gets or sets the template selector for the repeater. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="TemplateSelector"/> that represnets the template selector.</value>
        public TemplateSelector TemplateSelector
        {
            get { return (TemplateSelector)GetValue(TemplateSelectorProperty); }
            set { SetValue(TemplateSelectorProperty, value); }
        }

        /// <summary>
        /// Backing store for the <c>ItemTemplateSelector</c> bindable property.
        /// </summary>
        public static BindableProperty ItemTemplateSelectorProperty =
            BindableProperty.Create(nameof(ItemTemplateSelector), typeof(DataTemplateSelector),
                                    typeof(RepeaterView<T>), default(DataTemplateSelector), propertyChanged: OnDataTemplateSelectorChanged);

        /// <summary>
        /// Gets or sets the template selector for the items of the repeater view. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="DataTemplateSelector"/> that selects the template for the items.</value>
        public DataTemplateSelector ItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
            set { SetValue(ItemTemplateSelectorProperty, value); }
        }

        private static void OnDataTemplateSelectorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((RepeaterView<T>)bindable).OnDataTemplateSelectorChanged((DataTemplateSelector)oldvalue, (DataTemplateSelector)newvalue);
        }

        /// <summary>
        /// The method to invoke when the <c>ItemTemplateSelector</c> has changed.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <exception cref="ArgumentException">Thrown when setting both ItemTemplate and ItemTemplateSelector.</exception>
        protected virtual void OnDataTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue)
        {
            // checks to see that no ItemTemplate is created.
            if (ItemTemplate != null && newValue != null)
            {
                throw new ArgumentException("Cannot set both ItemTemplate and ItemTemplateSelector", "ItemTemplateSelector");
            }

            _currentItemSelector = newValue;
        }

        /// <summary>
        /// Delegate for the <c>ItemCreated</c> event.
        /// </summary>
        /// <param name="sender">The sender(this).</param>
        /// <param name="args">An instance of the <c>RepeaterViewItemAddedEventArgs</c> containing the event data.</param>
        public delegate void RepeaterViewItemAddedEventHandler(object sender, RepeaterViewItemAddedEventArgs args);

        /// <summary>
        /// Occurs when an item has been created in the repeater view.
        ///</summary>
        public event RepeaterViewItemAddedEventHandler ItemCreated;

        /// <summary>
        /// Raises the <c>ItemCreated</c> event.
        /// </summary>
        /// <param name="view">The visual view object.</param>
        /// <param name="model">The item being added.</param>
        protected virtual void NotifyItemAdded(View view, T model)
        {
            ItemCreated?.Invoke(this, new RepeaterViewItemAddedEventArgs(view, model));
        }

        /// <summary>
        /// Selects the data template for type <paramref name="type"/> dynamically.
        /// </summary>
        /// <remarks>
        /// Initially checks the <c>TemplateSelector</c> and only if it's <c>null</c> then checks the <c>ItemTemplate</c>. 
        /// </remarks>
        /// <param name="type"></param>
        /// <returns>A <see cref="DataTemplate"/> that represents the appropriate template.</returns>
        protected virtual DataTemplate GetTemplateFor(Type type)
        {
            DataTemplate retTemplate = null;

            if (TemplateSelector != null)
            {
                retTemplate = TemplateSelector.TemplateFor(type);
            }

            return retTemplate ?? ItemTemplate;
        }

        /// <summary>
        /// Creates a view based on the <paramref name="item"/> type.
        /// </summary>
        /// <remarks>
        /// T can be a common superclass or an interface.
        /// </remarks>
        /// <param name="item">Item</param>
        /// <returns>A <see cref="View"/> as its binding context.</returns>
        /// <exception cref="InvalidVisualObjectException">
        /// Thrown when the matched data template inflates to an object not derived from either 
        /// <see cref="View"/> or <see cref="ViewCell"/>.
        /// </exception>
        protected virtual View ViewFor(T item)
        {
            // Checks the item template selector first
            View view = null;

            if (_currentItemSelector != null)
            {
                view = this.ViewFor(item, _currentItemSelector);
            }

            if (view == null)
            {
                var template = GetTemplateFor(item.GetType());
                var content = template.CreateContent();

                if (!(content is View) && !(content is ViewCell))
                {
                    throw new InvalidVisualObjectException(content.GetType());
                }

                view = (content is View) ? content as View : ((ViewCell)content).View;
            }

            view.BindingContext = item;
            view.GestureRecognizers.Add(
                new TapGestureRecognizer { Command = ItemClickCommand, CommandParameter = item });
            return view;
        }

        /// <summary>
        /// Resets the collection of bound objects by removing the old collection changed eventhandler (if any) 
        /// and creating new cells for each new item.
        /// </summary>
        /// <param name="bindable">The control.</param>
        /// <param name="oldValue">Previous bound collection.</param>
        /// <param name="newValue">New bound collection.</param>
        private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as RepeaterView<T>;

            if (control == null)
            {
                throw new Exception(
                    "Invalid bindable object passed to ReapterView::ItemsChanged expected a ReapterView<T> received a "
                    + bindable.GetType().Name);
            }

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
    /// Arguments for the <c>ItemCreated</c> event.
    /// </summary>
    public class RepeaterViewItemAddedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of this <c>RepeaterViewItemAddedEventArgs</c> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="model">The model.</param>
        public RepeaterViewItemAddedEventArgs(View view, object model)
        {
            View = view;
            Model = model;
        }

        /// <summary>
        /// Gets or sets the view for the <c>RepeaterViewItemAddedEventArgs</c>.
        /// </summary>
        /// <value>A <see cref="View"/> that represents the visual element.</value>
        public View View { get; set; }

        /// <summary>
        /// Gets or sets the original view model for the <c>RepeaterViewItemAddedEventArgs</c>.
        /// </summary>
        public object Model { get; set; }
    }
}
