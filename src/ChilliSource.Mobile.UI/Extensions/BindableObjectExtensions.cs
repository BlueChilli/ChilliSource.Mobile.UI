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
	public static class BindableObjectExtensions
	{
		/// <summary>
		/// Views for.
		/// </summary>
		/// <param name="This">The this.</param>
		/// <param name="item">The item.</param>
		/// <param name="selector">The selector.</param>
		/// <returns>View.</returns>
		/// <exception cref="System.InvalidOperationException">DataTemplate must be a View</exception>
		public static View ViewFor(this BindableObject This, object item, DataTemplateSelector selector)
		{
			if (selector != null)
			{
				var template = selector.SelectTemplate(item, This);
				if (template != null)
				{
					var templateInstance = template.CreateContent();
					// see if it's a view or a cell
					var templateView = templateInstance as View;

					if (templateView == null)
						throw new InvalidOperationException("DataTemplate must be a View");

					return templateView;
				}
			}

			return null;
		}
	}
}

