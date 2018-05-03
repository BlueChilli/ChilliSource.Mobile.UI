#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// <see cref="INavigation"/> extension methods to add transitions to the standard page navigations.
    /// </summary>
    public static class NavigationExtensions
    {
        /// <summary>
        /// Pushes a page with the specified <paramref name="transitionEffect"/>.
        /// </summary>
        /// <param name="sender">Navigation.</param>
        /// <param name="destinationPage">Page to be pushed.</param>
        /// <param name="transitionEffect">Transition type.</param>
        /// <param name="options">Transition options.</param>
        public static Task PushAsyncWithTransition(this INavigation sender, Page destinationPage, PageTransitionEffectType transitionEffect, TransitionOptions options = null)
        {
            var originPage = sender.NavigationStack.LastOrDefault() as ContentPage;

            if (originPage == null)
            {
                return sender.PushAsync(destinationPage);
            }

            return DependencyService.Get<INavigationTransitionService>().PushAsync(originPage, destinationPage, transitionEffect, options ?? new TransitionOptions());
        }

        /// <summary>
        /// Pushes a page modally with the specified <paramref name="transitionEffect"/>.
        /// </summary>
        /// <param name="sender">Navigation.</param>
        /// <param name="destinationPage">Page to be pushed.</param>
        /// <param name="transitionEffect">Transition type.</param>
        /// <param name="options">Transition options.</param>
        public static Task PushModalAsyncWithTransition(this INavigation sender, Page destinationPage, PageTransitionEffectType transitionEffect, TransitionOptions options = null)
        {
            var originPage = sender.NavigationStack.LastOrDefault() as ContentPage;

            if (originPage == null)
            {
                return sender.PushModalAsync(destinationPage);
            }

            return DependencyService.Get<INavigationTransitionService>().PushModalAsync(originPage, destinationPage, transitionEffect, options ?? new TransitionOptions());
        }

        /// <summary>
        /// Pops a page with the specified <paramref name="transitionEffect"/>.
        /// </summary>
        /// <param name="sender">Navigation.</param>
        /// <param name="transitionEffect">Transition type.</param>
        /// <param name="options">Transition options.</param>
        public static Task PopAsyncWithTransition(this INavigation sender, PageTransitionEffectType transitionEffect, TransitionOptions options = null)
        {
            var originPage = sender.NavigationStack.LastOrDefault() as ContentPage;

            if (originPage == null)
            {
                return sender.PopAsync();
            }

            return DependencyService.Get<INavigationTransitionService>().PopAsync(originPage, transitionEffect, options ?? new TransitionOptions());
        }

        /// <summary>
        /// Pops a page modally with the specified <paramref name="transitionEffect"/>.
        /// </summary>
        /// <param name="sender">Navigation.</param>
        /// <param name="transitionEffect">Transition type.</param>
        /// <param name="options">Transition options.</param>
        public static Task PopModalAsyncWithTransition(this INavigation sender, PageTransitionEffectType transitionEffect, TransitionOptions options = null)
        {
            var originPage = sender.ModalStack?.LastOrDefault() as ContentPage;

            if (originPage == null)
            {
                return sender.PopModalAsync();
            }

            return DependencyService.Get<INavigationTransitionService>().PopModalAsync(originPage, transitionEffect, options ?? new TransitionOptions());
        }
    }
}
