#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Stores arguments for keyboard visibility changed events.
    /// </summary>
    public class KeyboardVisibilityEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="isVisible">If set to <c>true</c> the keyboard is visible.</param>
        /// <param name="height">Height of the keyboard.</param>
        /// <param name="animationDuration">Slideout animation duration.</param>
        public KeyboardVisibilityEventArgs(bool isVisible, float height, int animationDuration)
        {
            Height = height;
            AnimationDuration = animationDuration;
            IsVisible = isVisible;
        }

        /// <summary>
        /// Gets the height of the keyboard.
        /// </summary>
        public float Height
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the duration of the keyboard slideout animation.
        /// </summary>
        public int AnimationDuration
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the keyboard is visible.
        /// </summary>
        /// <value><c>true</c> if it is visible; otherwise, <c>false</c>.</value>
        public bool IsVisible
        {
            get;
            private set;
        }
    }
}

