#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/* Based on: 
 * Class:   ExceptionHandlingSynchronizationContext (https://github.com/kiwidev/WinRTExceptions/blob/master/Source/ExceptionHandlingSynchronizationContext.cs)
 * Project: WinRTExceptions (https://github.com/kiwidev/WinRTExceptions)
 * Author:  Mark Young (https://github.com/kiwidev)
 */

using System;
using System.Threading;
using ChilliSource.Mobile.Core;

namespace ChilliSource.Mobile.UI.Core
{
    /// <summary>
    /// Allows you to hook into any unhandled exceptions occuring on async void calls made from the same originating thread.
    /// See: http://www.markermetro.com/2013/01/technical/handling-unhandled-exceptions-with-asyncawait-on-windows-8-and-windows-phone-8/
    /// </summary>
    public class AsyncSynchronizationContext : SynchronizationContext
    {
        readonly SynchronizationContext _syncContext;
        ILogger _logger;

        public AsyncSynchronizationContext(ILogger logger)
        {
            _logger = logger;
        }

        public static AsyncSynchronizationContext Register()
        {
            var syncContext = Current;
            if (syncContext == null)
            {
                throw new InvalidOperationException("Ensure a synchronization context exists before calling this method.");
            }

            var customSynchronizationContext = syncContext as AsyncSynchronizationContext;

            if (customSynchronizationContext == null)
            {
                customSynchronizationContext = new AsyncSynchronizationContext(syncContext);
                SetSynchronizationContext(customSynchronizationContext);
            }

            return customSynchronizationContext;
        }

        public AsyncSynchronizationContext(SynchronizationContext syncContext)
        {
            _syncContext = syncContext;
        }

        public override SynchronizationContext CreateCopy()
        {
            return new AsyncSynchronizationContext(_syncContext.CreateCopy());
        }

        public override void OperationCompleted()
        {
            _syncContext.OperationCompleted();
        }

        public override void OperationStarted()
        {
            _syncContext.OperationStarted();
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            _syncContext.Post(WrapCallback(d, _logger), state);
        }

        public override void Send(SendOrPostCallback d, object state)
        {
            _syncContext.Send(d, state);
        }

        private static SendOrPostCallback WrapCallback(SendOrPostCallback sendOrPostCallback, ILogger logger)
        {
            return state =>
            {
                try
                {
                    sendOrPostCallback(state);
                }
                catch (Exception ex)
                {
                    logger?.Error(ex);
                }

			};
		}
	}
}
