using System;
using UIKit;

namespace ChilliSource.Mobile.UI
{
	public class ExtendedUITextField : UITextField
	{
		public ExtendedUITextField(CoreGraphics.CGRect rect) : base(rect)
		{

		}

        public float HorizontalContentPadding { get; set; }

		public override CoreGraphics.CGRect TextRect(CoreGraphics.CGRect forBounds)
		{
            var left = HorizontalContentPadding.Equals(0) ? forBounds.Left : HorizontalContentPadding;
            return new CoreGraphics.CGRect(left, forBounds.Top, forBounds.Width - left * 2, forBounds.Height);
		}

		public override CoreGraphics.CGRect EditingRect(CoreGraphics.CGRect forBounds)
		{
            var left = HorizontalContentPadding.Equals(0) ? forBounds.Left : HorizontalContentPadding;
			return new CoreGraphics.CGRect(left, forBounds.Top, forBounds.Width - left * 2, forBounds.Height);
		}

		public override CoreGraphics.CGRect PlaceholderRect(CoreGraphics.CGRect forBounds)
		{
            var left = HorizontalContentPadding.Equals(0) ? forBounds.Left : HorizontalContentPadding;
			return new CoreGraphics.CGRect(left, forBounds.Top, forBounds.Width - left * 2, forBounds.Height);
		}
	}
}
