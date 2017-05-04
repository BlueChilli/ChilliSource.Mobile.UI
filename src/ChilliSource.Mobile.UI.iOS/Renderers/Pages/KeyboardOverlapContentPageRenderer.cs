#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;
using System.Drawing;
using System.Linq;
using System;

[assembly: ExportRenderer(typeof(KeyboardOverlapContentPage), typeof(KeyboardOverlapContentPageRenderer))]
namespace ChilliSource.Mobile.UI
{
	public class KeyboardOverlapContentPageRenderer : PageRenderer
	{
		public UIView ContentView;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			RegisterForKeyboardNotifications();
			AutomaticallyAdjustsScrollViewInsets = false;
			ContentView = View;
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			NSNotificationCenter.DefaultCenter.RemoveObserver(UIKeyboard.WillHideNotification);
			NSNotificationCenter.DefaultCenter.RemoveObserver(UIKeyboard.WillShowNotification);
		}

		public virtual bool HandlesKeyboardNotifications()
		{
			return false;
		}

		protected virtual void RegisterForKeyboardNotifications()
		{
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
		}

		protected virtual UIView KeyboardGetActiveView()
		{
			return ContentView.FindFirstResponder();
		}

		private void OnKeyboardNotification(NSNotification notification)
		{
			//Check if the keyboard is becoming visible
			var visible = notification.Name == UIKeyboard.WillShowNotification;

			//Start an animation, using values from the keyboard
			UIView.BeginAnimations("AnimateForKeyboard");
			UIView.SetAnimationBeginsFromCurrentState(true);
			UIView.SetAnimationDuration(UIKeyboard.AnimationDurationFromNotification(notification));
			UIView.SetAnimationCurve((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification(notification));

			//Pass the notification, calculating keyboard height, etc.
			bool landscape = UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight;
			var keyboardFrame = visible
									? UIKeyboard.FrameEndFromNotification(notification)
									: UIKeyboard.FrameBeginFromNotification(notification);

			OnKeyboardChanged(visible, (float)(landscape ? keyboardFrame.Width : keyboardFrame.Height));

			//Commit the animation
			UIView.CommitAnimations();
		}

		void OnKeyboardChanged(bool visible, float keyboardHeight)
		{
			var activeView = KeyboardGetActiveView();
			if (activeView == null)
				return;

			var scrollView = activeView.FindSuperviewOfType(ContentView, typeof(UIScrollView)) as UIScrollView;
			if (scrollView == null)
				return;

			if (!visible)
				RestoreScrollPosition(scrollView);
			else
				CenterViewInScroll(activeView, scrollView, keyboardHeight);
		}

		protected virtual void CenterViewInScroll(UIView viewToCenter, UIScrollView scrollView, float keyboardHeight)
		{
			var contentInsets = new UIEdgeInsets(0.0f, 0.0f, keyboardHeight, 0.0f);
			scrollView.ContentInset = contentInsets;
			scrollView.ScrollIndicatorInsets = contentInsets;

			// Position of the active field relative isnside the scroll view
			RectangleF relativeFrame = (System.Drawing.RectangleF)viewToCenter.Superview.ConvertRectToView(viewToCenter.Frame, scrollView);

			bool landscape = UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft || UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight;
			var spaceAboveKeyboard = (landscape ? scrollView.Frame.Width : scrollView.Frame.Height) - keyboardHeight;

			// Move the active field to the center of the available space
			var offset = relativeFrame.Y - (spaceAboveKeyboard - viewToCenter.Frame.Height) / 2;
			scrollView.ContentOffset = new PointF(0, (float)offset);
		}

		protected virtual void RestoreScrollPosition(UIScrollView scrollView)
		{
			scrollView.ContentInset = UIEdgeInsets.Zero;
			scrollView.ScrollIndicatorInsets = UIEdgeInsets.Zero;
		}

		UINavigationController GetUINavigationController(UIViewController controller)
		{
			if (controller != null)
			{
				Console.WriteLine("controller is not null");
				if (controller is UINavigationController)
				{
					Console.WriteLine("Found uinavigationcontroller");
					return (controller as UINavigationController);
				}

				if (controller.ChildViewControllers.Count() != 0)
				{
					var count = controller.ChildViewControllers.Count();

					for (int c = 0; c < count; c++)
					{
						Console.WriteLine(
							"local iteration {0}: current controller has {1} children", c, count);
						var child = GetUINavigationController(controller.ChildViewControllers[c]);
						if (child == null)
						{
							Console.WriteLine("No children left on current controller. Moving back up");
						}
						else if (controller is UINavigationController)
						{
							Console.WriteLine("returning customnavigationrenderer");
							return (child as UINavigationController);
						}
					}
				}
			}

			return null;
		}
	}
}

