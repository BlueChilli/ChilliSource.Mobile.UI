#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using UIKit;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Orientation customisations for <see cref="UINavigationController"/>
    /// </summary>
    public class ExtendedUINavigationController : UINavigationController
    {
        /// <summary>
        /// Initializes a new instance of this <c>ExtendedUINavigationController</c> class.
        /// </summary>
        /// <param name="controller">Controller.</param>
        public ExtendedUINavigationController(UIViewController controller) : base(controller)
        {

        }

        /// <summary>
        /// Gets or sets the preferred orientation for this navigation controller.
        /// </summary>
        /// <value>A <see cref="UIInterfaceOrientation"/> that represents the preferred orientation.</value>
        public UIInterfaceOrientation PreferredOrientation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this navigation controller allows autorotation.
        /// </summary>
        /// <value><c>true</c> if autorotation is allowed; otherwise, <c>false</c>.</value>
        public bool AllowAutorotation { get; set; }

        /// <summary>
        /// Overrides the <see cref="UIViewController.ShouldAutorotate "/> method to turn auto-rotation on or off.
        /// </summary>
        /// <returns><c>true</c>, if autorotate should be on, <c>false</c> otherwise.</returns>
        public override bool ShouldAutorotate()
        {
            return AllowAutorotation;
        }

        /// <summary>
        /// Overrides the <see cref="UIViewController.PreferredInterfaceOrientationForPresentation"/> method 
        /// to indentify the orientation that best displays the content of this view controller.
        /// </summary>
        /// <value>A <see cref="UIInterfaceOrientation"/> that represents the preferred orientation.</value>
        public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation()
        {
            return PreferredOrientation;
        }

        /// <summary>
        /// Overrides the <see cref="UIViewController.GetSupportedInterfaceOrientations"/> method 
        /// to identify the orientations supported by this view controller.
        /// </summary>
        /// <returns>A <see cref="UIInterfaceOrientationMask"/> that represents the supported interface orientations.</returns>
        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
        {
            return UIInterfaceOrientationMask.AllButUpsideDown;
        }
    }
}
