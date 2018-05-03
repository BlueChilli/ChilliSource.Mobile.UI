using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
namespace ChilliSource.Mobile.UI.Extensions
{
    /// <summary>
    /// <see cref="PropertyInfo"/> extensions for handling attributes 
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Gets the custom attribute of type T.
        /// </summary>
        /// <returns>The custom attribute of the property.</returns>
        /// <param name="property"><see cref="PropertyInfo"/> instance.</param>
        /// <typeparam name="T">The type of the attribute to return.</typeparam>
        public static T GetCustomAttribute<T>(this PropertyInfo property) where T : Attribute
        {
            return property.GetCustomAttribute(typeof(T)) as T;
        }

        /// <summary>
        /// Gets the list of custom attributes for a property.
        /// </summary>
        /// <returns>A <see cref="System.Collections.Generic.List{Type}"/> collection of custom attributes.</returns>
        /// <param name="property"><see cref="PropertyInfo"/> instance.</param>
        /// <param name="inherit"><c>true</c> to search the <see cref="PropertyInfo"/>'s inheritance chain.</param>
        /// <typeparam name="T">The type of the attribute to return.</typeparam>
        public static List<T> GetCustomAttributes<T>(this PropertyInfo property, bool inherit) where T : Attribute
        {
            return property.GetCustomAttributes(typeof(T), inherit).Cast<T>().ToList();
        }
    }
}