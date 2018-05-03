#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Service contract defining methods that perform page navigation with specific <see cref="PageTransitionEffectType"/> transition effects
	/// </summary>
	public interface INavigationTransitionService
    {
        /// <summary>
        /// Navigate from <paramref name="originPage"/> to <paramref name="destinationPage"/> and perform the <paramref name="transitionEffect"/>
        /// </summary>
        /// <param name="originPage">Origin page.</param>
        /// <param name="destinationPage">Destination page.</param>
        /// <param name="transitionEffect">Transition effect.</param>
        /// <param name="options">Options.</param>
        Task PushAsync(Page originPage, Page destinationPage, PageTransitionEffectType transitionEffect, TransitionOptions options = null);


		/// <summary>
		/// Display the <paramref name="destinationPage"/> modally from <paramref name="originPage"/>  and perform the <paramref name="transitionEffect"/>
		/// </summary>
		/// <param name="originPage">Origin page.</param>
		/// <param name="destinationPage">Destination page.</param>
		/// <param name="transitionEffect">Transition effect.</param>
		/// <param name="options">Options.</param>
		Task PushModalAsync(Page originPage, Page destinationPage, PageTransitionEffectType transitionEffect, TransitionOptions options = null);


		/// <summary>
		/// Pop <paramref name="originPage"/> from the navigation stack and perform the <paramref name="transitionEffect"/>
		/// </summary>
		/// <param name="originPage">Origin page.</param>
		/// <param name="transitionEffect">Transition effect.</param>
		/// <param name="options">Options.</param>
		Task PopAsync(Page originPage, PageTransitionEffectType transitionEffect, TransitionOptions options = null);

		/// <summary>
		/// Dismiss the <paramref name="originPage"/> modal page and perform the <paramref name="transitionEffect"/>
		/// </summary>
		/// <param name="originPage">Origin page.</param>
		/// <param name="transitionEffect">Transition effect.</param>
		/// <param name="options">Options.</param>
		Task PopModalAsync(Page originPage, PageTransitionEffectType transitionEffect, TransitionOptions options = null);
    }
}
