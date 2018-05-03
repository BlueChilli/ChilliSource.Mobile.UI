#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Specifies the transition type for navigation between pages. See <see cref="INavigationTransitionService"/> 
    /// </summary>
    public enum PageTransitionEffectType
    {
        None = 0,
        Checkerboard,
        ZoomFade,
        Fade,
        Swipe,
        DoorSlide,
        Fold,
        Turn

    }
}
