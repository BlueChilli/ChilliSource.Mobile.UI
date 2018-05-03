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
    /// Stores the various parameters for configuring navigation transitions via <see cref="INavigationTransitionService"/>
    /// </summary>
	public class TransitionOptions
    {
        /// <summary>
        /// Gets or sets the duration of the transition in seconds. Default is 1.
        /// </summary>
		public float TransitionDuration { get; set; } = 1.0f;

        /// <summary>
        /// Gets or sets the transition direction when a page is being pushed and the swipe transition animation is being used.
        /// </summary>
        public TransitionDirection PushSwipeTransitionDirection { get; set; }

        /// <summary>
        /// Gets or sets the transition direction when a page is being popped and the swipe transition animation is being used.
        /// </summary>		
        public TransitionDirection PopSwipeTransitionDirection { get; set; }

        /// <summary>
        /// Determines whether the swipe transition should be enabled for pages that are being popped
        /// </summary>
        public bool EnablePopSwipeTransition { get; set; }

        /// <summary>
        /// Determines whether the source page of the transition has a navigation bar.
        /// </summary>
        public bool SourcePageHasNavigationBar { get; set; } = true;

        /// <summary>
        /// Determines if the destination page of the transition has a navigation bar.
        /// </summary>
        public bool DestinationPageHasNavigationBar { get; set; } = true;
    }
}

