#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Bindable object extensions
    /// </summary>
    public static class BindableObjectExtensions
    {
        /// <summary>
        /// Returns the template view that matches the conditions of the data template <paramref name="selector"/> for the specified <paramref name="item"/>
        /// </summary>
        /// <param name="container">The bindable object containing the template definition for the specified <paramref name="item"/>.</param>
        /// <param name="item">The object for which to return the corresponding template view.</param>
        /// <param name="selector">The <see cref="DataTemplateSelector"/> that contains the custom logic to determine which view to return for the specified <paramref name="item"/>.</param>
        /// <returns>Returns a <see cref="View"/> instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when data template associated with <paramref name="item"/> is not a <see cref="View"/>.</exception>
        public static View ViewFor(this BindableObject container, object item, DataTemplateSelector selector)
        {
            if (selector != null)
            {
                var template = selector.SelectTemplate(item, container);
                if (template != null)
                {
                    var content = template.CreateContent();

                    var view = content as View;
                    if (view == null)
                    {
                        throw new InvalidOperationException("DataTemplate must be a View.");
                    }

                    return view;
                }
            }

            return null;
        }
    }
}

