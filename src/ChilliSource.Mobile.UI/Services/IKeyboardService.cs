#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Provides monitoring of keyboard events.
    /// </summary>
    public interface IKeyboardService
    {
        /// <summary>
        /// Occurs when the native keyboard is about to be shown or about to be hidden.
        /// </summary>
        event EventHandler<KeyboardVisibilityEventArgs> KeyboardVisibilityChanged;
    }
}

