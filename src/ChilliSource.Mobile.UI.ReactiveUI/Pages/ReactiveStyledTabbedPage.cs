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
using ChilliSource.Mobile.UI;
using ReactiveUI;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    /// <summary>
    /// <see cref="ExtendedTabbedPage"/> that supports reactive routing and binding
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
	public class ReactiveStyledTabbedPage<TViewModel> : ExtendedTabbedPage, IViewFor<TViewModel> where TViewModel : class
    {
        /// <summary>
        /// The ViewModel to bind to
        /// </summary>
        public TViewModel ViewModel
        {
            get { return (TViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create(nameof(ViewModel), typeof(TViewModel), typeof(ReactiveStyledTabbedPage<TViewModel>), default(TViewModel), BindingMode.OneWay);

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (TViewModel)value; }
        }
    }
}
