#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Collections.Generic;
using Splat;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    /// <summary>
    /// Dependency injection helpers
    /// </summary>
    public static class PageExtensions
    {
        public static T GetService<T>(this Page page, string contract = null)
        {
            return Locator.Current.GetService<T>(contract);
        }

        public static IEnumerable<T> GetServices<T>(this Page page, string contract = null)
        {
            return Locator.Current.GetServices<T>(contract);
        }
    }
}
