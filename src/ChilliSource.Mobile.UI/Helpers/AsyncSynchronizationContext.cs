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

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Allows hooking into any unhandled exceptions occuring on async void calls made from the same originating thread.
    /// </summary>
    /// <remarks>
    /// see: http://www.markermetro.com/2013/01/technical/handling-unhandled-exceptions-with-asyncawait-on-windows-8-and-windows-phone-8/
    /// </remarks>
    public class AsyncSynchronizationContext : SynchronizationContext
    {
        readonly SynchronizationContext _syncContext;
        ILogger _logger;

        /// <summary>
        /// Initializes a new instance with an injected <see cref="ILogger"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public AsyncSynchronizationContext(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Initializes a new instance with an existing <see cref="SynchronizationContext"/>.
        /// </summary>
        /// <param name="syncContext">Sync context.</param>
        public AsyncSynchronizationContext(SynchronizationContext syncContext)
        {
            _syncContext = syncContext;
        }

        /// <summary>
        /// Casts the base synchronization context to a <see cref="AsyncSynchronizationContext"/>
        /// </summary>
        /// <returns>The new <see cref="AsyncSynchronizationContext"/>.</returns>
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


        /// <summary>
        /// Overrides the <see cref="SynchronizationContext.CreateCopy"/> method that
        /// creates a copy of this context.
        /// </summary>
        /// <returns>A new <see cref="SynchronizationContext"/> object.</returns>
        public override SynchronizationContext CreateCopy()
        {
            return new AsyncSynchronizationContext(_syncContext.CreateCopy());
        }

        /// <summary>
        /// Overrides the <see cref="SynchronizationContext.OperationCompleted"/> method that
        /// responds to the notification that an operation has completed.
        /// </summary>
        public override void OperationCompleted()
        {
            _syncContext.OperationCompleted();
        }

        /// <summary>
        /// Overrides the <see cref="SynchronizationContext.OperationStarted"/> method that 
        /// responds to the notification that an operation has started.
        /// </summary>
        public override void OperationStarted()
        {
            _syncContext.OperationStarted();
        }

        /// <summary>
        /// Overrides the <see cref="SynchronizationContext.Post"/> method that 
        /// dispatches an asynchronous message to this  context.
        /// </summary>
        /// <param name="d">The <see cref="SendOrPostCallback"/> delegate to call.</param>
        /// <param name="state">The object passed to the delegate.</param>
        public override void Post(SendOrPostCallback d, object state)
        {
            _syncContext.Post(WrapCallback(d, _logger), state);
        }

        /// <summary>
        /// Overrides the <see cref="SynchronizationContext.Send"/> method that
        /// dispatches a synchronous message to this context.
        /// </summary>
        /// <param name="d">The <see cref="SendOrPostCallback"/> delegate to call.</param>
        /// <param name="state">The object passed to the delegate.</param>
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
