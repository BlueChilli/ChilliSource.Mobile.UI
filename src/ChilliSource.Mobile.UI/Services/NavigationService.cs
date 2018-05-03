#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/*
 * Based on
 * Source: SimpleIoCApp (https://github.com/Clancey/SimpleIoCApp)
 * Author:  James Clancey (https://github.com/Clancey)
 * License: Apache 2.0 (https://github.com/Clancey/SimpleIoCApp/blob/master/LICENSE)
*/

using ChilliSource.Mobile.UI;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Provides viewmodel-first navigation methods in 
    /// conjunction with the <see cref="NavigationLocator"/> class.
    /// </summary>
    public static partial class NavigationService
    {
        static Page CurrentPage
        {
            get
            {
                var topPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
                var topModalPage = Application.Current.MainPage.Navigation.ModalStack.LastOrDefault();

                return topModalPage ?? topPage ?? Application.Current.MainPage;
            }
        }

        static INavigation Navigation
        {
            get
            {
                return CurrentPage.Navigation;
            }
        }

        /// <summary>
        /// Pushes the page associated with the provided <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">View model.</param>
        /// <param name="animated">Should animate.</param>
        public static async Task PushAsync(BaseViewModel viewModel, bool animated = true)
        {
            var page = BuildPage(viewModel, false);
            await Navigation.PushAsync(page, animated);
        }

        /// <summary>
        /// Pushes the page associated with the provided <paramref name="viewModel"/> and custom <paramref name="transitionEffect"/>.
        /// </summary>
        /// <param name="viewModel">View model.</param>
        /// <param name="transitionEffect">Transition effect.</param>
        /// <param name="options">Transition effect options.</param>
        public static Task PushWithTransitionAsync(BaseViewModel viewModel, PageTransitionEffectType transitionEffect, TransitionOptions options = null)
        {            
            var destinationPage = BuildPage(viewModel, false);

            return DependencyService.Get<INavigationTransitionService>().PushAsync(CurrentPage, destinationPage, transitionEffect, options);            
        }

        /// <summary>
        /// Modally pushes the page associated with the provided <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">View model.</param>
        /// <param name="animated">Should animate.</param>
        /// <param name="wrapInNavigation">If set to <c>true</c> wraps the page in a <see cref="NavigationPage"/> instance.</param>
        public static async Task PushModalAsync(BaseViewModel viewModel, bool animated = true, bool wrapInNavigation = true)
        {
            var page = BuildPage(viewModel, wrapInNavigation);
            await Navigation.PushModalAsync(page, animated);
        }

        /// <summary>
        /// Modally pushes the page associated with the provided <paramref name="viewModel"/> and custom <paramref name="transitionEffect"/>.
        /// </summary>
        /// <param name="viewModel">View model.</param>
        /// <param name="transitionEffect">Transition effect.</param>
        /// <param name="options">Transition effect options.</param>
        /// <param name="wrapInNavigation">If set to <c>true</c> wraps the page in a <see cref="NavigationPage"/> instance.</param>
        public static Task PushModalWithTransitionAsync(BaseViewModel viewModel, PageTransitionEffectType transitionEffect, TransitionOptions options = null, bool wrapInNavigation = true)
        {            
            var destinationPage = BuildPage(viewModel, wrapInNavigation);

            return DependencyService.Get<INavigationTransitionService>().PushModalAsync(CurrentPage, destinationPage, transitionEffect, options);

        }

        /// <summary>
        /// Pops the top Page from the navigation stack.
        /// </summary>
        public static async Task PopAsync(bool animated = true)
        {
            await Navigation.PopAsync(animated);
        }

        /// <summary>
        /// Pops the top Page from the navigation stack using the specified <paramref name="transitionEffect"/>.
        /// </summary>
        /// <param name="transitionEffect">Transition effect.</param>
        /// <param name="options">Transition effect options.</param>
        public static Task PopWithTransitionAsync(PageTransitionEffectType transitionEffect, TransitionOptions options = null)
        {
            return DependencyService.Get<INavigationTransitionService>().PopAsync(CurrentPage, transitionEffect, options);
        }

        /// <summary>
        /// Modally pops the top Page from the navigation stack.
        /// </summary>
        public static async Task PopModalAsync(bool animated = true)
        {
            await Navigation.PopModalAsync(animated);
        }

        /// <summary>
        /// Modally pops the top Page from the navigation stack.
        /// </summary>
        /// <param name="transitionEffect">Transition effect.</param>
        /// <param name="options">Transition effect options.</param>
        public static Task PopModalWithTransitionAsync(PageTransitionEffectType transitionEffect, TransitionOptions options = null)
        {
            return DependencyService.Get<INavigationTransitionService>().PopModalAsync(CurrentPage, transitionEffect, options);
        }
       
        /// <summary>
        /// Pops all pages from the top-most navigation stack.
        /// </summary>
        public static async Task PopToRootAsync(bool animated = true)
        {
            await Navigation.PopToRootAsync(animated);
        }
       
        /// <summary>
        /// Determine if the current page is modal or not and pops the page accordingly
        /// </summary>
        public static async Task Dismiss(bool animated = true)
        {
            var topModalPage = Application.Current.MainPage.Navigation.ModalStack.LastOrDefault();
            if (topModalPage != null)
            {
                await Navigation.PopModalAsync(animated);
            }
            else
            {
                await Navigation.PopAsync(animated);
            }
        }

        /// <summary>
        /// Sets the application's root page to the page instance associated with the provided <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">View model.</param>
        /// <param name="wrapInNavigation">If set to <c>true</c> wraps the root page in a <see cref="NavigationPage"/> instance.</param>
        public static void SetRoot(object viewModel, bool wrapInNavigation = true)
        {
            Application.Current.MainPage = BuildPage(viewModel, wrapInNavigation);
        }

        /// <summary>
        /// Returns the page instance associated with the provided <paramref name="viewModel"/>.
        /// </summary>
        /// <returns>The page.</returns>
        /// <param name="viewModel">View model.</param>
        /// <param name="wrapInNavigation">If set to <c>true</c> wraps the returned page in a <see cref="NavigationPage"/> instance.</param>
        public static Page BuildPage(object viewModel, bool wrapInNavigation = true)
        {
            //keep singleton:false while this bug exists: https://bugzilla.xamarin.com/show_bug.cgi?id=56287
            var view = NavigationLocator.GetPage(viewModel.GetType(), singleton:false);
            view.BindingContext = viewModel;
            return wrapInNavigation ? new NavigationPage(view) : view;
        }
    }
}

