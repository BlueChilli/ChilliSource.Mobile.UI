#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using UIKit;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Use this class to customize the behavior of a navigation controller by setting the relevant properties
	/// </summary>
	public class ExtendedUINavigationController : UINavigationController
	{
		public ExtendedUINavigationController(UIViewController controller) : base(controller)
		{

		}

		public UIInterfaceOrientation PreferredOrientation { get; set; }
		public bool AllowAutorotation { get; set; }

		public override bool ShouldAutorotate()
		{
			return AllowAutorotation;
		}

		public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation()
		{
			return PreferredOrientation;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations()
		{
			return UIInterfaceOrientationMask.AllButUpsideDown;
		}
	}
}
