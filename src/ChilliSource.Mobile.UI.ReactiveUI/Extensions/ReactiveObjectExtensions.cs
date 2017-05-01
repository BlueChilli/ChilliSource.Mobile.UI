#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using ChilliSource.Mobile.Core;
using ReactiveUI;
using Splat;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
	public static class ReactiveObjectExtensions
	{
		public static T GetService<T>(this IReactiveObject o, string contract = null)
		{
			return Locator.Current.GetService<T>(contract);
		}

		public static IEnumerable<T> GetServices<T>(this IReactiveObject o, string contract = null)
		{
			return Locator.Current.GetServices<T>(contract);
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
