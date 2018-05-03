#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Reactive.Linq;
using Splat;
using ChilliSource.Mobile.Logging;
using ChilliSource.Mobile.Core;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    /// <summary>
    /// <see cref="IObservable"/> logging and error handling exceptions
    /// </summary>
    public static class ObservableExtensions
    {       
        public static IObservable<T> Log<T>(this IObservable<T> observable, string message = "Action", Mobile.Core.ILogger logger = null) 
        {
			var _logger = logger ?? Locator.Current.GetService<Mobile.Core.ILogger>();
            return observable.Do((input) =>
            {
                _logger?.Information("{Message:l} - OnNext => {@Input}", message, input); 
            }, (Exception ex) =>
            {
                _logger?.Information(ex , "{Message:l} - OnError => {Exception}", message, ex.ToString());
            }, () =>
            {
                _logger?.Information($"{message} - OnCompleted");
            });
        }

        public static IObservable<IServiceResult> ToExceptionResult(this IObservable<Exception> o)
        {
            return o.Select(m => ServiceResult.AsFailure(m));
        }

        public static IObservable<IServiceResult> CatchWhen<T>(this IObservable<IServiceResult> o, Func<T, IServiceResult> exceptionHandler)
            where T : Exception
        {
            return o
                .Select(m =>
                {
                    var exception = m.Exception as T;

                    if (exception != null && !m.IsHandled)
                    {
                        return exceptionHandler((T)m.Exception);
                    }

                    return m;
                });
        }

        public static IObservable<IServiceResult> CatchAll(this IObservable<IServiceResult> o,
            string message, Func<Exception, IServiceResult> exceptionHandler = null)
        {
            return o.Select(m =>
            {

                if (!m.IsHandled)
                {
#if DEBUG
                    return exceptionHandler != null ? exceptionHandler(m.Exception) : ServiceResult.AsFailure(m.Exception);
#endif

#if __STAGING__
                    return exceptionHandler != null ? exceptionHandler(m.Exception) : ServiceResult.AsFailure(m.Exception);
#endif

#if __RELEASE__
                    return exceptionHandler != null ? exceptionHandler(m.Exception) : ServiceResult.AsFailure(message);
#endif

                }

                return m;
            });
        }
    }
}
