#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Reactive.Disposables;
using ReactiveUI;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    /// <summary>
    /// <see cref="ReactiveObject"/> that maintaines a list of <see cref="CompositeDisposable"/> to be disposed
    /// </summary>
    public class DisposableReactiveObject : ReactiveObject, IDisposable
	{
		private readonly CompositeDisposable _disposables;

		public DisposableReactiveObject()
		{
			_disposables = new CompositeDisposable();
		}

		public CompositeDisposable Disposables => _disposables;

		public void Dispose()
		{
			_disposables.Clear();
		}	
	}
}
