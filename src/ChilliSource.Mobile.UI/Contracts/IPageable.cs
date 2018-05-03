#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Windows.Input;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Contract for defining paging behavior of list views.
    /// </summary>
    public interface IPageable
    {
        /// <summary>
        /// Command to load additional items to the binding context
        /// </summary>
        /// <value>The load more command.</value>

        ICommand LoadMoreCommand { get; }

        /// <summary>
        /// Specifies that there are more data items that can be loaded 
        /// in addition to the currently loaded items
        /// </summary>
        /// <value><c>true</c> if has more items; otherwise, <c>false</c>.</value>
        bool HasMoreItems { get; }

        /// <summary>
        /// Specifies that list view is in the process of loading additional items
        /// </summary>
        /// <value><c>true</c> if is paging; otherwise, <c>false</c>.</value>
        bool IsPaging { get; }
    }
}
