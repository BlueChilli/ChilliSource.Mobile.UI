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
using Xamarin.Forms;
using Splat;
using ReactiveUI;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Diagnostics;
using System.Reactive;
using ReactiveUI.XamForms;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    /// <summary>
    /// Proxy page that control navigation
    /// </summary>
	public class ReactiveRoutedViewHost : NavigationPage, IActivatable
	{
		public static readonly BindableProperty RouterProperty = BindableProperty.Create(nameof(Router),
																						 typeof(RoutingState),
																						 typeof(ReactiveRoutedViewHost));



		public ReactiveRoutingState Router
		{
			get { return (ReactiveRoutingState)GetValue(RouterProperty); }
			set { SetValue(RouterProperty, value); }
		}

		public ReactiveRoutedViewHost()
		{
			Initialize(null);

		}

		public ReactiveRoutedViewHost(Page root, ReactiveRoutingState routingState) : base(root)
		{
			Initialize(routingState);
		}


		public ReactiveRoutedViewHost(ReactiveRoutingState routingState)
		{
			Initialize(routingState);
		}

		private void Initialize(ReactiveRoutingState routingState)
		{
			this.WhenActivated(d =>
			{
				bool currentlyPopping = false;
				bool popToRootPending = false;
				bool userInstigated = false;


				d(this.WhenAnyObservable(x => x.Router.NavigationStack.Changed)
					.Where(_ => Router.NavigationStack.IsEmpty)
					.Select(x =>
					{
						// Xamarin Forms does not let us completely clear down the navigation stack
						// instead, we have to delay this request momentarily until we receive the new root view
						// then, we can insert the new root view first, and then pop to it
						popToRootPending = true;
						return x;
					})
					.Subscribe());

				var previousCount = this.WhenAnyObservable(x => x.Router.NavigationStack.CountChanged).StartWith(this.Router.NavigationStack.Count);
				var currentCount = previousCount.Skip(1);

				d(Observable.Zip(previousCount, currentCount, (previous, current) => new { Delta = previous - current, Current = current })
					.Where(_ => !userInstigated)
					.Where(x => x.Delta > 0)
					.SelectMany(
						async x =>
						{
							// XF doesn't provide a means of navigating back more than one screen at a time apart from navigating right back to the root page
							// since we want as sensible an animation as possible, we pop to root if that makes sense. Otherwise, we pop each individual
							// screen until the delta is made up, animating only the last one
							var popToRoot = x.Current == 1;
							currentlyPopping = true;

							try
							{
								if (popToRoot)
								{
									await this.PopToRootAsync(true);
								}
								else
								{
									for (var i = 0; i < x.Delta; ++i)
									{
										await this.PopAsync(i == x.Delta - 1);
									}
								}
							}
							finally
							{
								currentlyPopping = false;
								((IViewFor)this.CurrentPage).ViewModel = Router.GetCurrentViewModel();
							}

							return Unit.Default;
						})
					.Subscribe());

				d(this.WhenAnyObservable(x => x.Router.Navigate)
					.SelectMany(_ => PageForViewModel(Router.GetCurrentViewModel()))
					.SelectMany(async x =>
					{
						if (popToRootPending && this.Navigation.NavigationStack.Count > 0)
						{
							this.Navigation.InsertPageBefore(x, this.Navigation.NavigationStack[0]);
							await this.PopToRootAsync();
						}
						else
						{
							await this.PushAsync(x);
						}

						popToRootPending = false;
						return x;
					})
					.Subscribe());

				var poppingEvent = Observable.FromEventPattern<NavigationEventArgs>(x => this.Popped += x, x => this.Popped -= x);

				// NB: Catch when the user hit back as opposed to the application
				// requesting Back via NavigateBack
				d(poppingEvent
					.Where(_ => !currentlyPopping && Router != null)
					.Subscribe(_ =>
					{
						userInstigated = true;

						try
						{
							Router.NavigationStack.RemoveAt(Router.NavigationStack.Count - 1);
						}
						finally
						{
							userInstigated = false;
						}

						((IViewFor)this.CurrentPage).ViewModel = Router.GetCurrentViewModel();
					}));


				// modal navigation

				d(this.WhenAnyObservable(x => x.Router.PushModal)
					  .SelectMany(vm => PageForViewModel(Router.GetCurrentViewModelInModalStack()))
					  .SelectMany(async x =>
					  {
						  await this.Navigation.PushModalAsync(x.Item1, x.Item2);
						  return x.Item1;
					  })
					  .Subscribe());


				d(this.WhenAnyObservable(x => x.Router.PopModal)
				  .Where(animated => this.Navigation.ModalStack.Count > 0)
				  .SelectMany(async animated => await this.Navigation.PopModalAsync(animated))
				  .Subscribe());
			});

			Func<IReactiveScreen> getScreen = () =>
			{
				var screen = Locator.Current.GetService<IReactiveScreen>();
				if (screen == null)
					throw new Exception("You *must* register an IReactiveScreen class representing your App's main Screen");

				return screen;
			};

			Router = routingState ?? getScreen().Router;

			if (Router == null)
				throw new Exception(
					"You *must* register an IReactiveScreen class representing your App's main Screen r pass the router");


			this.WhenAnyValue(x => x.Router)
				.Do(router =>
				{
					if (this.CurrentPage != null)
					{
						var vm = ((IViewFor)this.CurrentPage).ViewModel as IRoutableViewModel;
						SetViewModelToPage(this.CurrentPage, vm);
					}
				})
				.Where(m => this.CurrentPage == null)
				.SelectMany(router =>
				{
					return router.NavigationStack.ToObservable()
								 .Select(x => (Page)ViewLocator.Current.ResolveView(x))
								 .SelectMany(x => this.PushAsync(x).ToObservable())
								 .Finally(() =>
								 {
									 var vm = router.GetCurrentViewModel();
									 if (vm == null) return;

									 SetViewModelToPage(this.CurrentPage, vm);
								 });
				})
				.Subscribe();
		}

		protected void SetViewModelToPage(Page page, IRoutableViewModel vm)
		{
			((IViewFor)page).ViewModel = vm;
			if (vm != null) page.Title = vm.UrlPathSegment;
		}
		protected IObservable<Page> PageForViewModel(IRoutableViewModel vm)
		{
			if (vm == null) return Observable.Empty<Page>();

			var ret = ViewLocator.Current.ResolveView(vm);
			if (ret == null)
			{
				var msg = String.Format(
										"Couldn't find a View for ViewModel. You probably need to register an IViewFor<{0}>",
										vm.GetType().Name);

				return Observable.Throw<Page>(new Exception(msg));
			}

			ret.ViewModel = vm;

			var pg = (Page)ret;
			pg.BindingContext = vm;
			pg.Title = vm.UrlPathSegment;
			return Observable.Return(pg);
		}

		protected IObservable<Tuple<Page, bool>> PageForViewModel(ISupportModal vm)
		{
			if (vm == null) return Observable.Empty<Tuple<Page, bool>>();

			var ret = ViewLocator.Current.ResolveView(vm);
			if (ret == null)
			{
				var msg = String.Format(
										"Couldn't find a View for ViewModel. You probably need to register an IViewFor<{0}>",
										vm.GetType().Name);

				return Observable.Throw<Tuple<Page, bool>>(new Exception(msg));
			}

			ret.ViewModel = vm;

			var pg = (Page)ret;
			pg.BindingContext = vm;
			pg.Title = vm.UrlPathSegment;

			var barTextColor = Color.Default;
			var barColor = Color.Default;

			if (vm.NavBarColor.HasValue)
			{
				barTextColor = vm.NavBarColor.Value;
			}

			if (vm.NavBarTextColor.HasValue)
			{
				barColor = vm.NavBarTextColor.Value;
			}


			NavigationPage.SetHasNavigationBar(pg, vm.WithNavigationBar);

			var page = new ReactiveRoutedViewHost(pg, vm.Router)
			{
				BarBackgroundColor = barColor,
				BarTextColor = barTextColor
			};


			return Observable.Return(new Tuple<Page, bool>(page, vm.Animated));
		}

	}
}



