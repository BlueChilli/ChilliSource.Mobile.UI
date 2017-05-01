#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Extends INavigation to provide tansition options between pages
	/// </summary>
	public static class NavigationExtensions
	{
		/// <summary>
		/// Push with transition
		/// </summary>
		/// <param name="sender">Navigation</param>
		/// <param name="destinationPage">Page to be pushed</param>
		/// <param name="transition">Transition type</param>
		/// <param name="options">Transition options</param>
		public static Task PushAsyncWithTransition(this INavigation sender, Page destinationPage, PageTransitionType transition, TransitionOptions options = null)
		{
			var originPage = sender.NavigationStack.LastOrDefault() as ContentPage;

			if (originPage == null)
			{
				return sender.PushAsync(destinationPage);
			}

			return DependencyService.Get<INavigationTransitionService>().PushAsync(originPage, destinationPage, transition, options ?? new TransitionOptions());
		}

		/// <summary>
		/// Push modally with transition
		/// </summary>
		/// <param name="sender">Navigation</param>
		/// <param name="destinationPage">Page to be pushed</param>
		/// <param name="transition">Transition type</param>
		/// <param name="options">Transition options</param>
		public static Task PushModalAsyncWithTransition(this INavigation sender, Page destinationPage, PageTransitionType transition, TransitionOptions options = null)
		{
			var originPage = sender.NavigationStack.LastOrDefault() as ContentPage;

			if (originPage == null)
			{
				return sender.PushModalAsync(destinationPage);
			}

			return DependencyService.Get<INavigationTransitionService>().PushModalAsync(originPage, destinationPage, transition, options ?? new TransitionOptions());
		}

		/// <summary>
		/// Pop with transition
		/// </summary>
		/// <param name="sender">Navigation</param>
		/// <param name="transition">Transition type</param>
		/// <param name="options">Transition options</param>
		public static Task PopAsyncWithTransition(this INavigation sender, PageTransitionType transition, TransitionOptions options = null)
		{
			var originPage = sender.NavigationStack.LastOrDefault() as ContentPage;

			if (originPage == null)
			{
				return sender.PopAsync();
			}

			return DependencyService.Get<INavigationTransitionService>().PopAsync(originPage, transition, options ?? new TransitionOptions());
		}

		/// <summary>
		/// Pop modally with transition
		/// </summary>
		/// <param name="sender">Navigation</param>
		/// <param name="transition">Transition type</param>
		/// <param name="options">Transition options</param>
		public static Task PopModalAsyncWithTransition(this INavigation sender, PageTransitionType transition, TransitionOptions options = null)
		{
			var originPage = sender.ModalStack?.LastOrDefault() as ContentPage;

			if (originPage == null)
			{
				return sender.PopModalAsync();
			}

			return DependencyService.Get<INavigationTransitionService>().PopModalAsync(originPage, transition, options ?? new TransitionOptions());
		}
	}
}
