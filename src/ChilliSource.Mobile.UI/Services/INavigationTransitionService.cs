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

namespace ChilliSource.Mobile.UI
{
	public interface INavigationTransitionService
	{
		Task PushAsync(Page originPage, Page destinationPage, PageTransitionType transition, TransitionOptions options = null);
		Task PushModalAsync(Page originPage, Page destinationPage, PageTransitionType transition, TransitionOptions options = null);
		Task PopAsync(Page originPage, PageTransitionType transition, TransitionOptions options = null);
		Task PopModalAsync(Page originPage, PageTransitionType transition, TransitionOptions options = null);
	}
}
