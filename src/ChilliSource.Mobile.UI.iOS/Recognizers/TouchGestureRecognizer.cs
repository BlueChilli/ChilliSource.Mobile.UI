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
	public class TouchGestureRecognizer : UIGestureRecognizer
	{
		private CoreGraphics.CGPoint _firstPoint;

		private const double Tolerance = 1;

		public TouchGestureRecognizer(EventReport ev)
		{
			Event += ev;
		}

		public delegate void EventReport(UIGestureRecognizer gesture);

		public EventReport Event;

		public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesBegan(touches, evt);

			if (evt.AllTouches.Count == 1)
			{
				_firstPoint = base.View.ConvertPointToView(base.View.Frame.Location, null);
				base.State = UIGestureRecognizerState.Began;
				Event.Invoke(this);
			}
			else
			{
				base.State = UIGestureRecognizerState.Failed;
				Event.Invoke(this);
			}

		}

		public override void TouchesCancelled(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled(touches, evt);
			base.State = UIGestureRecognizerState.Cancelled;
			Event.Invoke(this);
		}

		public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
		{
			base.TouchesEnded(touches, evt);
			base.State = UIGestureRecognizerState.Ended;
			Event.Invoke(this);
		}

		public override void TouchesMoved(Foundation.NSSet touches, UIEvent evt)
		{
			var point = base.View.ConvertPointToView(base.View.Frame.Location, null);

			if (Math.Abs(point.X - _firstPoint.X) > Tolerance || Math.Abs(point.Y - _firstPoint.Y) > Tolerance)
			{
				base.State = UIGestureRecognizerState.Cancelled;
				Event.Invoke(this);
			}
			else
			{
				var location = LocationInView(base.View);

				if (location.X < 0 || location.X > base.View.Frame.Size.Width || location.Y < 0 || location.Y > base.View.Frame.Size.Height)
				{
					base.State = UIGestureRecognizerState.Cancelled;
					Event.Invoke(this);
				}
			}
		}

		public override bool CanBePreventedByGestureRecognizer(UIGestureRecognizer preventingGestureRecognizer)
		{
			return false;
		}

		public override bool CanPreventGestureRecognizer(UIGestureRecognizer preventedGestureRecognizer)
		{
			return false;
		}
	}
}

