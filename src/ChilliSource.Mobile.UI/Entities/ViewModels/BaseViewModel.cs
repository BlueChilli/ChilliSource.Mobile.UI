#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/*
Source: SimpleIoCApp (https://github.com/Clancey/SimpleIoCApp)
Author:  James Clancey (https://github.com/Clancey)
License: Apache 2.0 (https://github.com/Clancey/SimpleIoCApp/blob/master/LICENSE)
*/

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// View model base class to simplify the <see cref="INotifyPropertyChanged"/> implementation.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Detaches the <c>PropertyChanged</c> event handlers
        /// </summary>
        /// <remarks>
        /// Call <c>Dispose</c> when you are finished using this <c>BaseViewModel</c> object. This method leaves the
        /// <c>BaseViewModel</c> object in an unusable state. After calling this method, you must release all references to the
        /// <c>BaseViewModel</c> object so the garbage collector can reclaim the memory that the object was occupying.
        /// </remarks>
        public void Dispose()
        {
            ClearEvents();
        }

        /// <summary>
        /// Occurs when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Detaches the <c>PropertyChanged</c> event handlers
        /// </summary>
        public virtual void ClearEvents()
        {
            var invocation = PropertyChanged?.GetInvocationList() ?? new Delegate[0];
            foreach (var property in invocation)
            {
                PropertyChanged -= (PropertyChangedEventHandler)property;
            }
        }

        protected bool OnPropertyChanged<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
            {
                return false;
            }

            currentValue = newValue;

            if (PropertyChanged == null)
            {
                return true;
            }

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }

        protected void OnPropertyChanged([CallerMemberName]string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }


    }
}

