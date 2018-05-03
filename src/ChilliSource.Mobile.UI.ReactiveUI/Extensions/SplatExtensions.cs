#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Splat;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
    /// <summary>
    /// Splat <see cref="IMutableDependencyResolver"/> helper extensions
    /// </summary>
    public static class SplatExtensions
    {
        public static void Register<T>(this IMutableDependencyResolver resolver, Func<T> factory, string contract = null)
        {
            resolver.Register((() => factory() as object), typeof(T), contract);
        }

        public static void RegisterConstant<T, TR>(this IMutableDependencyResolver resolver, T value, string contract = null) where T : TR
        {
            resolver.RegisterConstant(value, typeof(TR), contract);
        }

        public static void RegisterLazyConstant<T, TR>(this IMutableDependencyResolver resolver, Func<T> factory, string contract = null) where T : TR
        {
            resolver.RegisterLazySingleton((() => factory() as object), typeof(TR), contract);
        }
    }
}
