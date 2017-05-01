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

namespace ChilliSource.Mobile.UI.Core
{
	/// <summary>
	/// Stores and provides access to mappings between pages and view models
	/// </summary>
	public static class NavigationLocator
	{
		private static readonly Dictionary<Type, Type> _registeredTypes = new Dictionary<Type, Type>();
		private static readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();

		public static void Register(Type viewModelType, Type pageType)
		{
			_registeredTypes[viewModelType] = pageType;
		}

		public static void Register<ViewModelType, PageType>() where PageType : Page where ViewModelType : BaseViewModel
		{
			_registeredTypes[typeof(ViewModelType)] = typeof(PageType);
		}

		public static Page GetPage(BaseViewModel model, bool singleton = true)
		{
			return GetPage(model.GetType(), singleton);
		}

		public static T GetPage<T>(BaseViewModel model, bool singleton = true) where T : Page
		{
			var page = GetPage(model, singleton);
			return (T)page;
		}

		public static Page GetPage<T>(bool singleton = true)
		{
			return GetObject<T, Page>(singleton);
		}

		public static Page GetPage(Type type, bool singleton = true)
		{
			return GetObject<Page>(type, singleton);
		}

		public static T GetObject<T>(bool singleton = true)
		{
			return GetObject<T, T>(singleton);
		}

		public static T1 GetObject<T, T1>(bool singleton = true)
		{
			return GetObject<T1>(typeof(T), singleton);
		}

		public static T GetObject<T>(Type type, bool singleton = true)
		{
			Type objectType;
			if (!_registeredTypes.TryGetValue(type, out objectType))
			{
				return default(T);
			}

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
