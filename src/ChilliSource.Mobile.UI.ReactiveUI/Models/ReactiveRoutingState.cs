#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/* based on
 * Project: ReactiveUI (https://github.com/reactiveui/ReactiveUI)
 * Author:  reactiveUI (https://github.com/reactiveui)
 * License: Ms-PL (https://github.com/reactiveui/ReactiveUI/blob/develop/LICENSE)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Text;
using ReactiveUI;
using System.Reactive.Disposables;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
	/// <summary>
	/// RoutingState manages the ViewModel Stack and allows ViewModels to
	/// navigate to other ViewModels.
	/// </summary>
	[DataContract]
	public class ReactiveRoutingState : RoutingState, IDisposable
	{
		[DataMember]
		private readonly ReactiveList<ISupportModal> _modalStack;

		[IgnoreDataMember]
		private readonly IReactiveDerivedList<ISupportModal> _readonlyModalStack;

		public ReactiveRoutingState() : base()
		{
			_modalStack = new ReactiveList<ISupportModal>();

			_readonlyModalStack = _modalStack.CreateDerivedCollection(x => x);

			SetupRx();

		}

		/// <summary>
		/// The scheduler used for commands. Defaults to <c>RxApp.MainThreadScheduler</c>.
		/// </summary>
		[IgnoreDataMember]
		public new IScheduler Scheduler
		{
			get { return base.Scheduler; }
			set
			{
				if (base.Scheduler != value)
				{
					base.Scheduler = value;
					SetupRx();
				}
			}
		}

		private void SetupRx()
		{
			var _scheduler = base.Scheduler ?? RxApp.MainThreadScheduler;

			PopModal =
				ReactiveCommand.CreateFromObservable<bool, bool>(animate =>
				{
					if (ModalStack.Any())
					{
						var viewModel = _modalStack[_modalStack.Count - 1];
						var vm = viewModel as IReactiveScreen;
						using (vm?.Router)
						{
							_modalStack.RemoveAt(ModalStack.Count - 1);
						}

					}

					return Observable.Return(animate);
				},
				outputScheduler: _scheduler);

			PushModal = ReactiveCommand.CreateFromObservable<ISupportModal, ISupportModal>(vm =>
			{
				if (vm == null)
				{
					throw new Exception("Push Modal must be called on an ISupportModal");
				}

				using (ModalStack.SuppressChangeNotifications())
				{
					_modalStack.Add(vm);
					if (vm.Router != this)
					{
						vm.WhenAnyObservable(viewModel => viewModel.Router.PopModal)
						.InvokeCommand(this.PopModal);
					}
				}

				return Observable.Return(vm);
			},
			outputScheduler: _scheduler);
		}

		public IReadOnlyReactiveList<ISupportModal> ModalStack => _readonlyModalStack;

		public ReactiveCommand<ISupportModal, ISupportModal> PushModal { get; protected set; }
		public ReactiveCommand<bool, bool> PopModal { get; protected set; }
		public void Dispose()
		{
			this.NavigationStack.Clear();
			_modalStack.Clear();
		}
	}

	public static class ReactiveRoutingStateMixins
	{
		/// <summary>
		/// Locate the first ViewModel in the stack that matches a certain Type.
		/// </summary>
		/// <returns>The matching ViewModel or null if none exists.</returns>
		public static T FindViewModelInStack<T>(this ReactiveRoutingState This)
			where T : IRoutableViewModel
		{
			return (This as RoutingState).FindViewModelInStack<T>();
		}

		/// <summary>
		/// Returns the currently visible ViewModel
		/// </summary>
		public static IRoutableViewModel GetCurrentViewModel(this ReactiveRoutingState This)
		{
			return (This as RoutingState).GetCurrentViewModel();
		}

		/// <summary>
		/// Locate the first ViewModel in the stack that matches a certain Type.
		/// </summary>
		/// <returns>The matching ViewModel or null if none exists.</returns>
		public static T FindViewModelInModalStack<T>(this ReactiveRoutingState This)
			where T : ISupportModal
		{
			return This.ModalStack.Reverse().OfType<T>().FirstOrDefault();
		}

		/// <summary>
		/// Returns the currently visible ViewModel
		/// </summary>
		public static ISupportModal GetCurrentViewModelInModalStack(this ReactiveRoutingState This)
		{
			return This.ModalStack.LastOrDefault();
		}

		public static void NavigationToRoot(this RoutingState This)
		{
			var count = This.NavigationStack.Count;
			This.NavigationStack.RemoveRange(1, count - 1);
		}

	}

}
