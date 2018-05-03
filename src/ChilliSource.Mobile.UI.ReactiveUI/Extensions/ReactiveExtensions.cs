using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.Logging;
using Splat;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    public static class ReactiveExtensions
    {
        public static IDisposable SubscribeSafe<T>(
            this IObservable<T> @this,
            [CallerMemberName]string callerMemberName = null,
            [CallerFilePath]string callerFilePath = null,
            [CallerLineNumber]int callerLineNumber = 0)
        {
            Ensure.ArgumentNotNull(@this, nameof(@this));

            return @this
                    .Subscribe(
                        _ => { },
                        ex =>
                        {
                            var logger = Locator.Current.GetService<ChilliSource.Mobile.Core.ILogger>();
                            logger.Error(ex, "An exception went unhandled. Caller member name: '{@callerMemberName}', caller file path: '{@callerFilePath}', caller line number: {@callerLineNumber}.", callerMemberName, callerFilePath, callerLineNumber);
                        });
        }

        public static IDisposable SubscribeSafe<T>(this IObservable<T> @this,
            Action<T> onNext,
            [CallerMemberName] string callerMemberName = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
            Ensure.ArgumentNotNull(@this, nameof(@this));
            return @this
                .Subscribe(onNext, ex =>
                {
                    var logger = Locator.Current.GetService<ChilliSource.Mobile.Core.ILogger>();
                    logger.Error(ex, "An exception went unhandled. Caller member name: '{@callerMemberName}', caller file path: '{@callerFilePath}', caller line number: {@callerLineNumber}.", callerMemberName, callerFilePath, callerLineNumber);
                });
        }
        
        public static IDisposable SubscribeAndLogException<T>(this IObservable<T> @this,
                [CallerMemberName] string callerMemberName = null,
                [CallerFilePath] string callerFilePath = null,
                [CallerLineNumber] int callerLineNumber = 0) where T : Exception
        {
            Ensure.ArgumentNotNull(@this, nameof(@this));
            return @this
                    .Subscribe(ex =>
                            {
                                var logger = Locator.Current.GetService<ChilliSource.Mobile.Core.ILogger>();
                                logger.Error(ex, "An exception went unhandled. Caller member name: '{@callerMemberName}', caller file path: '{@callerFilePath}', caller line number: {@callerLineNumber}.", callerMemberName, callerFilePath, callerLineNumber);
                            });
        }

        public static IObservable<ServiceResult<T>> LogResult<T>(this IObservable<ServiceResult<T>> @this)
        {
            Ensure.ArgumentNotNull(@this, nameof(@this));
            return @this
                    .Do(m =>
                    {
                        var logger = Locator.Current.GetService<ChilliSource.Mobile.Core.ILogger>();

                        if (m.IsFailure || m.IsCancelled)
                        {
                            logger.Error(m.Exception, "An exception has occurred with {@statusCode}", m.StatusCode);
                        }
                        else if (m.IsCancelled)
                        {
                            logger.Information(m.Message);
                        }
                    });
        }

      
    }
}
