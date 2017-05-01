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

namespace ChilliSource.Mobile.UI.ReactiveUI.Extensions
{
    public static class ObservableExtensions
    {
        public static IObservable<T> Log<T>(IObservable<T> observable, string message = "Action", Mobile.Core.ILogger logger = null) 
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
    }
}
