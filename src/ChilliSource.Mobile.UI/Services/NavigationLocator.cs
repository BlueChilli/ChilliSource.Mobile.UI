#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/*
 * Based on
 * Source: SimpleIoCApp (https://github.com/Clancey/SimpleIoCApp)
 * Author:  James Clancey (https://github.com/Clancey)
 * License: Apache 2.0 (https://github.com/Clancey/SimpleIoCApp/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Reflection;
using System.Linq;
using ChilliSource.Mobile.UI;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Stores and provides access to mappings between pages and view models for <see cref="NavigationService"/> 
    /// </summary>
    public static class NavigationLocator
    {
        private static readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();

        /// <summary>
        /// Returns the page instance mapped to the <paramref name="viewModel"/>.
        /// </summary>
        /// <returns>A <see cref="Page"/> instance.</returns>
        /// <param name="viewModel">The <see cref="BaseViewModel"/> instance to search for.</param>
        /// <param name="singleton"><c>true</c> if page instance to be returned should be treated as a singleton.</param>
        public static Page GetPage(BaseViewModel viewModel, bool singleton = true)
        {
            return GetPage(viewModel.GetType(), singleton);
        }

        /// <summary>
        /// Returns the page instance mapped to the <paramref name="viewModel"/>.
        /// </summary>
        /// <returns>A <see cref="Page"/> instance.</returns> 
        /// <param name="viewModel">The <see cref="BaseViewModel"/> instance to search for.</param>
        /// <param name="singleton"><c>true</c> if page instance to be returned should be treated as a singleton.</param>
        /// <typeparam name="T">The type of the view model to search for.</typeparam>
        public static T GetPage<T>(BaseViewModel viewModel, bool singleton = true) where T : Page
        {
            var page = GetPage(viewModel, singleton);
            return (T)page;
        }

        /// <summary>
        /// Returns the page instance mapped to the view model of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A <see cref="Page"/> instance.</returns>
        /// <param name="singleton"><c>true</c> if page instance to be returned should be treated as a singleton.</param>
        /// <typeparam name="T">The type of the view model to search for.</typeparam>
        public static Page GetPage<T>(bool singleton = true)
        {
            return GetObject<T, Page>(singleton);
        }

        /// <summary>
        /// Returns the page instance mapped to the view model of type <paramref name="type"/>.
        /// </summary>
        ///  <returns>A <see cref="Page"/> instance.</returns>
        /// <param name="type">The type of the view model to search for.</param>
        /// <param name="singleton"><c>true</c> if page instance to be returned should be treated as a singleton.</param>
        public static Page GetPage(Type type, bool singleton = true)
        {
            return GetObject<Page>(type, singleton);
        }

        /// <summary>
        /// Returns the instance of the object mapped to the object of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Object instance of type <typeparamref name="T"/></returns>
        /// <param name="singleton"><c>true</c> if the object to be returned should be treated as a singleton.</param>
        /// <typeparam name="T">The type of the object to search for.</typeparam>
        /// <typeparam name="T1">The type of the object to be returned.</typeparam>
        public static T1 GetObject<T, T1>(bool singleton = true)
        {
            return GetObject<T1>(typeof(T), singleton);
        }

        /// <summary>
        /// Returns the instance of the object mapped to the object of type <paramref name="type"/>.
        /// </summary>
        /// <returns>Object instance of type <typeparamref name="T"/></returns>
        /// <param name="type">The type of the object to search for.</param>
        /// <param name="singleton"><c>true</c> if the object to be returned should be treated as a singleton.</param>
        /// <typeparam name="T">The type of the object to be returned.</typeparam>
        public static T GetObject<T>(Type type, bool singleton = true)
        {
            Type objectType;

#if NETSTANDARD1_6
            objectType = type.GetTypeInfo().Assembly
                         .GetTypes()
                         .First(m =>
                          {
                              var viewModelAttribute = m.GetTypeInfo().GetCustomAttribute(typeof(ViewModelAttribute)) as ViewModelAttribute;
                              return viewModelAttribute?.ViewModelType.GetTypeInfo().IsEquivalentTo(type) ?? false;
                          });

#else
            objectType = Assembly.GetAssembly(type)
                    .GetTypes()
                    .First(m =>
                    {
                        var viewModelAttribute = m.GetCustomAttribute(typeof(ViewModelAttribute)) as ViewModelAttribute;
                        return viewModelAttribute?.ViewModelType.IsEquivalentTo(type) ?? false;
                    });
#endif

            if (!singleton)
            {
                return (T)Activator.CreateInstance(objectType);
            }

            Object item;
            if (!_singletons.TryGetValue(objectType, out item))
            {
                _singletons[objectType] = item = (T)Activator.CreateInstance(objectType);
            }

            return (T)item;
        }
    }
}
