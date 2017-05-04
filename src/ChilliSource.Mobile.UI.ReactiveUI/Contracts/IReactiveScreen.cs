#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Runtime.Serialization;
using System.Text;
using ChilliSource.Mobile.Core;
using ReactiveUI;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    public interface IReactiveScreen : IScreen
    {
        new ReactiveRoutingState Router { get; }
    }
 
}
