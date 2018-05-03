#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Android.Content;
using Android.Util;
using Android.Widget;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Single item of <see cref="SegmentedControlView"/>
    /// </summary>
    public class SegmentedControlButton : RadioButton
    {
        /// <summary>
        /// Initializes a new instance of this <c>SegmentedControlButton</c> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public SegmentedControlButton(Context context) : this(context, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of this <c>SegmentedControlButton</c> class.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="attrs">The attributes.</param>
        public SegmentedControlButton(Context context, IAttributeSet attrs) : this(context, attrs, Android.Graphics.Color.Red)
        {
        }

        /// <summary>
        /// Initializes a new instance of this <c>SegmentedControlButton</c> class.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="attrs">The attributes.</param>
        /// <param name="defStyle">The base definition style.</param>
        public SegmentedControlButton(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }
    }
}
