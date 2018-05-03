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
// <copyright file="TemplateSelector.cs" company="XLabs Team">
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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Simplifies usage of multiple data templates
    /// </summary>
    [ContentProperty("Templates")]
    public class TemplateSelector : BindableObject
    {

        /// <summary>
        /// Initializes an instance of this <c>TemplateSelector</c> class.
        /// </summary>
        public TemplateSelector()
        {
            Templates = new DataTemplateCollection();
        }

        /// <summary>
        /// Backing store for the <c>Templates</c> Bindable Property.
        /// </summary>
        public static BindableProperty TemplatesProperty =
            BindableProperty.Create(nameof(Templates), typeof(DataTemplateCollection), typeof(TemplateSelector), default(DataTemplateCollection), BindingMode.OneWay, null, TemplatesChanged);

        /// <summary>
        /// Gets or sets a collection of data templates. This is a bindable property.
        /// </summary>
        /// <value>A <c>DataTemplateCollection</c> value that reperesents a collection of templates.</value>
        public DataTemplateCollection Templates
        {
            get { return (DataTemplateCollection)GetValue(TemplatesProperty); }
            set { SetValue(TemplatesProperty, value); }
        }

        /// <summary>
        /// Backing store for the <c>SelectorFunction</c> Bindable Property.
        /// </summary>

        public static BindableProperty SelectorFunctionProperty =
            BindableProperty.Create(nameof(SelectorFunction), typeof(Func<Type, DataTemplate>), typeof(TemplateSelector), null);

        /// <summary>
        /// Gets or sets a function of type to select data template associated with the type. This is a bindable property.
        /// </summary>
        /// <remarks>
        /// If supplied it is always called first in the matching process.
        /// </remarks>
        public Func<Type, DataTemplate> SelectorFunction
        {
            get { return (Func<Type, DataTemplate>)GetValue(SelectorFunctionProperty); }
            set { SetValue(SelectorFunctionProperty, value); }
        }


        /// <summary>
        /// Identifies the <c>ExceptionOnNoMatch</c> Bindable Property.
        /// </summary>
        public static BindableProperty ExceptionOnNoMatchProperty =
            BindableProperty.Create(nameof(ExceptionOnNoMatch), typeof(bool), typeof(TemplateSelector), true);

        /// <summary>
        /// Gets or sets a value indicating whether the <c>NoDataTemplateMatchException</c> 
        /// should be thrown when no matching template can be found. This is a bindable property.
        /// </summary>
        public bool ExceptionOnNoMatch
        {
            get { return (bool)GetValue(ExceptionOnNoMatchProperty); }
            set { SetValue(ExceptionOnNoMatchProperty, value); }
        }


        /// <summary>
        /// Clears the cache when the template is changed.
        /// </summary>
        /// <param name="bindableObject"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public static void TemplatesChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var templateSelector = bindableObject as TemplateSelector;
            if (templateSelector == null)
            {
                return;
            }

            if (oldValue != null)
            {
                (oldValue as DataTemplateCollection).CollectionChanged -= templateSelector.TemplateSetChanged;
            }

            (newValue as DataTemplateCollection).CollectionChanged += templateSelector.TemplateSetChanged;
            templateSelector.Cache = null;
        }

        /// <summary>
        /// Clears the cache when any collection of templates is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemplateSetChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Cache = null;
        }

        /// <summary>
        /// Private cache of matched data templates. The cache reset on any change to <c>Templates</c>.
        /// </summary>
        private Dictionary<Type, DataTemplate> Cache
        {
            get; set;
        }


        /// <summary>
        /// Finds the data template for the specified <paramref name="type"/>.
        /// </summary>
        /// <remarks>
        ///  Matching order: SelectorFunction, Cache, SpecificTypeMatch, InterfaceMatch, BaseTypeMatch, DefaultTempalte.
        /// </remarks>
        /// <param name="type">The type of the object that needs a data template.</param>
        /// <returns>
        /// The <see cref="DataTemplate"/> from the  wrapped template collections which is the closest match for the <paramref name="type"/>.
        /// </returns>
        /// <exception cref="NoDataTemplateMatchException">Thrown if there is no data template that matches the <paramref name="type"/>.</exception>
        public DataTemplate TemplateFor(Type type)
        {
            var typesExamined = new List<Type>();
            var template = TemplateForImpl(type, typesExamined);
            if (template == null && ExceptionOnNoMatch)
                throw new NoDataTemplateMatchException(type, typesExamined);
            return template;
        }

        /// <summary>
        /// Interal implementation of the <c>TemplateFor</c> method.
        /// </summary>
        /// <param name="type">The type to which the data template should match.</param>
        /// <param name="examined">A list of all types examined during the matching process.</param>
        /// <returns>A <see cref="DataTemplate"/> or null.</returns>
        private DataTemplate TemplateForImpl(Type type, List<Type> examined)
        {
            if (type == null)
                return null;//This can happen when we recusively check base types (object.BaseType==null)
            examined.Add(type);
            Contract.Assert(Templates != null, "Templates cannot be null");

            Cache = Cache ?? new Dictionary<Type, DataTemplate>();
            DataTemplate retTemplate = null;

            //Prefers the selector function if present
            //This has been moved before the cache check so that
            //the user supplied function has an opportunity to 
            //Make a decision with more information than simply
            //the requested type (perhaps the Ux or Network states...)
            if (SelectorFunction != null)
                retTemplate = SelectorFunction(type);

            //Happy case we already have the type in our cache
            if (Cache.ContainsKey(type))
                return Cache[type];


            //check our list
            retTemplate = Templates.Where(x => x.Type == type).Select(x => x.WrappedTemplate).FirstOrDefault();
            //Check for interfaces
            retTemplate = retTemplate ?? type.GetTypeInfo().ImplementedInterfaces.Select(x => TemplateForImpl(x, examined)).FirstOrDefault();
            //look at base types
            retTemplate = retTemplate ?? TemplateForImpl(type.GetTypeInfo().BaseType, examined);
            //If all else fails try to find a Default Template
            retTemplate = retTemplate ?? Templates.Where(x => x.IsDefault).Select(x => x.WrappedTemplate).FirstOrDefault();

            Cache[type] = retTemplate;
            return retTemplate;
        }

        /// <summary>
        /// Creates a view from the template found for the type of the passed-in <paramref name="item"/>.
        /// </summary>
        /// <remarks>
        /// The root of the template must be a <see cref="ViewCell"/>.
        /// </remarks>
        /// <param name="item">The item for which to instantiate a data template.</param>
        /// <returns>a <see cref="View"/> with its binding context set to the <paramref name="item"/>.</returns>
        /// <exception cref="InvalidVisualObjectException"></exception>Thrown when the matched data template inflates to an object not derived from either 
        /// <see cref="View"/> or <see cref="ViewCell"/>
        public View ViewFor(object item)
        {
            var template = TemplateFor(item.GetType());
            var content = template.CreateContent();
            if (!(content is View) && !(content is ViewCell))
                throw new InvalidVisualObjectException(content.GetType());

            var view = (content is View) ? content as View : ((ViewCell)content).View;
            view.BindingContext = item;
            return view;
        }


    }

    /// <summary>
    /// Provides an interface to hold type-safe instances of data template.
    /// </summary>
    public interface IDataTemplateWrapper
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        bool IsDefault
        {
            get; set;
        }
        /// <summary>
        /// Gets or sets the wrapped template.
        /// </summary>
        /// <value>The wrapped template.</value>
        DataTemplate WrappedTemplate
        {
            get; set;
        }
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        Type Type
        {
            get;
        }
    }
    /// <summary>
    /// Wrapper for a DataTemplate.
    /// Unfortunately the default constructor for DataTemplate is internal
    /// so I had to wrap the DataTemplate instead of inheriting it.
    /// </summary>
    /// <typeparam name="T">The object type that this DataTemplateWrapper matches</typeparam>
    [ContentProperty("WrappedTemplate")]
    public class DataTemplateWrapper<T> : BindableObject, IDataTemplateWrapper
    {
        /// <summary>
        /// The wrapped template property
        /// </summary>
        //public static readonly BindableProperty WrappedTemplateProperty = BindableProperty.Create<DataTemplateWrapper<T>, DataTemplate>(x => x.WrappedTemplate, null);

        public static readonly BindableProperty WrappedTemplateProperty = BindableProperty.Create(nameof(WrappedTemplate), typeof(DataTemplate), typeof(DataTemplateWrapper<T>), null);
        /// <summary>
        /// The is default property
        /// </summary>
        //public static readonly BindableProperty IsDefaultProperty = BindableProperty.Create<DataTemplateWrapper<T>, bool>(x => x.IsDefault, false);
        public static readonly BindableProperty IsDefaultProperty = BindableProperty.Create(nameof(IsDefault), typeof(bool), typeof(DataTemplateWrapper<T>), false);

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public bool IsDefault
        {
            get
            {
                return (bool)GetValue(IsDefaultProperty);
            }
            set
            {
                SetValue(IsDefaultProperty, value);
            }
        }
        /// <summary>
        /// Gets or sets the wrapped template.
        /// </summary>
        /// <value>The wrapped template.</value>
        public DataTemplate WrappedTemplate
        {
            get
            {
                return (DataTemplate)GetValue(WrappedTemplateProperty);
            }
            set
            {
                SetValue(WrappedTemplateProperty, value);
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type
        {
            get
            {
                return typeof(T);
            }
        }
    }

    /// <summary>
    /// Collection class of IDataTemplateWrapper
    /// Enables xaml definitions of collections.
    /// </summary>
    public class DataTemplateCollection : ObservableCollection<IDataTemplateWrapper>
    {
    }
    public class InvalidVisualObjectException : Exception
    {
        /// <summary>
        /// Hide any possible default constructor
        /// Redundant I know, but it costs nothing
        /// and communicates the design intent to
        /// other developers.
        /// </summary>
        private InvalidVisualObjectException() { }

        /// <summary>
        /// Constructs the exception and passes a meaningful
        /// message to the base Exception
        /// </summary>
        /// <param name="inflatedtype">The actual type the datatemplate inflated to.</param>
        /// <param name="name">The calling methods name, uses [CallerMemberName]</param>
        public InvalidVisualObjectException(Type inflatedtype, [CallerMemberName] string name = null) :
            base(string.Format("Invalid template inflated in {0}. Datatemplates must inflate to Xamarin.Forms.View(and subclasses) "
                               + "or a Xamarin.Forms.ViewCell(or subclasses).\nActual Type received: [{1}]", name, inflatedtype.Name))
        { }
        /// <summary>
        /// The actual type the datatemplate inflated to.
        /// </summary>
        public Type InflatedType { get; set; }
        /// <summary>
        /// The MemberName the exception occured in.
        /// </summary>
        public string MemberName { get; set; }
    }

    /// <summary>
    /// exception thrown when a template cannot
    /// be found for a supplied type
    /// </summary>
    public class NoDataTemplateMatchException : Exception
    {
        /// <summary>
        /// Hide any possible default constructor
        /// Redundant I know, but it costs nothing
        /// and communicates the design intent to
        /// other developers.
        /// </summary>
        private NoDataTemplateMatchException() { }

        /// <summary>
        /// Constructs the exception and passses a meaningful
        /// message to the base Exception
        /// </summary>
        /// <param name="tomatch">The type that a match was attempted for</param>
        /// <param name="candidates">All types examined during the match process</param>
        public NoDataTemplateMatchException(Type tomatch, List<Type> candidates) :
            base(string.Format("Could not find a template for type [{0}]", tomatch.Name))
        {
            AttemptedMatch = tomatch;
            TypesExamined = candidates;
            TypeNamesExamined = TypesExamined.Select(x => x.Name).ToList();
        }

        /// <summary>
        /// The type that a match was attempted for
        /// </summary>
        public Type AttemptedMatch { get; set; }
        /// <summary>
        /// A list of all types that were examined
        /// </summary>
        public List<Type> TypesExamined { get; set; }
        /// <summary>
        /// A List of the names of all examined types (Simple name only)
        /// </summary>
        public List<string> TypeNamesExamined { get; set; }
    }

}
