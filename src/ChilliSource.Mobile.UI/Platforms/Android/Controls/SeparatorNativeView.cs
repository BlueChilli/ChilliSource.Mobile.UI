#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;

namespace ChilliSource.Mobile.UI
{    
    public class SeparatorNativeView : View
    {

        private SeparatorOrientation _orientation;
        private float _densityMeasure;

        /// <summary>
        /// Initializes a new instance of this <c>SeparatorNativeView</c> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SeparatorNativeView(Context context)
            : base(context)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of this <c>SeparatorNativeView</c> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="attrs">The attributes.</param>
        public SeparatorNativeView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of this <c>SeparatorNativeView</c> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="attrs">The attributes.</param>
        /// <param name="defStyle">The base definition style.</param>
        public SeparatorNativeView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Initialize();
        }

        /// <summary>
        /// Gets or sets the thickness of the separator.
        /// </summary>
        public double Thickness { set; get; }

        /// <summary>
        /// Gets or sets the padding before the separator.
        /// </summary>
        public double SpacingBefore { set; get; }

        /// <summary>
        /// Gets or sets the amount of space after the separator.
        /// </summary>
        public double SpacingAfter { set; get; }

        /// <summary>
        /// Gets or sets the color of the stroke.
        /// </summary>
        /// <value>A <see cref="Color"/> that represents the color of the stroke.</value>
        public Color StrokeColor { set; get; }

        /// <summary>
        /// Gets or sets the type of the stroke.
        /// </summary>
        /// <value>A <see cref="StrokeType"/>that represents the type of the stroke.</value>
        public StrokeType StrokeType { set; get; }

        /// <summary>
        /// Gets or sets the orientation of the separator.
        /// </summary>
        /// <value>A <see cref="SeparatorOrientation"/> that represents the orientation.</value>
        public SeparatorOrientation Orientation
        {
            set
            {
                _orientation = value;
                Invalidate();
            }
            get
            {
                return _orientation;
            }
        }

        /// <summary>
        /// Overrides the <see cref="View.OnDraw"/> method to draw the separator view.
        /// </summary>
        /// <param name="canvas">The canvas on which the background will be drawn.</param>
        /// <since version="Added in API level 1" />
        /// <remarks>
        ///     <para tool="javadoc-to-mdoc">Implement this to do your drawing.</para>
        ///     <para tool="javadoc-to-mdoc">
        ///         <format type="text/html">
        ///             <a href="http://developer.android.com/reference/android/view/View.html#onDraw(android.graphics.Canvas)"
        ///                 target="_blank">
        ///                 [Android Documentation]
        ///             </a>
        ///         </format>
        ///     </para>
        /// </remarks>
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            var r = new Rect(0, 0, canvas.Width, canvas.Height);
            var dAdjustedThicnkess = (float)Thickness * _densityMeasure;

            var paint = new Paint { Color = StrokeColor, StrokeWidth = dAdjustedThicnkess, AntiAlias = true };
            paint.SetStyle(Paint.Style.Stroke);
            switch (StrokeType)
            {
                case StrokeType.Dashed:
                    paint.SetPathEffect(new DashPathEffect(new[] { 6 * _densityMeasure, 2 * _densityMeasure }, 0));
                    break;
                case StrokeType.Dotted:
                    paint.SetPathEffect(new DashPathEffect(new[] { dAdjustedThicnkess, dAdjustedThicnkess }, 0));
                    break;
                default:

                    break;
            }

            var desiredTotalSpacing = (SpacingAfter + SpacingBefore) * _densityMeasure;
            float leftForSpacing = 0;
            float actualSpacingBefore = 0;

            if (Orientation == SeparatorOrientation.Horizontal)
            {
                leftForSpacing = r.Height() - dAdjustedThicnkess;
            }
            else
            {
                leftForSpacing = r.Width() - dAdjustedThicnkess;
            }
            if (desiredTotalSpacing > 0)
            {
                var spacingCompressionRatio = (float)(leftForSpacing / desiredTotalSpacing);
                actualSpacingBefore = (float)SpacingBefore * _densityMeasure * spacingCompressionRatio;
            }
            else
            {
                actualSpacingBefore = 0;
            }
            var thicknessOffset = (dAdjustedThicnkess) / 2.0f;

            var p = new Path();
            if (Orientation == SeparatorOrientation.Horizontal)
            {
                p.MoveTo(0, actualSpacingBefore + thicknessOffset);
                p.LineTo(r.Width(), actualSpacingBefore + thicknessOffset);
            }
            else
            {
                p.MoveTo(actualSpacingBefore + thicknessOffset, 0);
                p.LineTo(actualSpacingBefore + thicknessOffset, r.Height());
            }
            canvas.DrawPath(p, paint);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            _densityMeasure = Application.Context.Resources.DisplayMetrics.Density;
        }
    }
}
