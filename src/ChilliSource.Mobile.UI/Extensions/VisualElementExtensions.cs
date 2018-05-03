#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Visual element extensions
    /// </summary>
    public static class VisualElementExtensions
    {
        /// <summary>
        /// Animates color change of the visual <paramref name="element"/> starting with <paramref name="fromColor"/> and ending with <paramref name="toColor"/>.
        /// </summary>
        /// <param name="element">The visual element for which to change the color.</param>
        /// <param name="fromColor">Starting color.</param>
        /// <param name="toColor">Ending color.</param>
        /// <param name="callback">Callback method returning the current color for each color change.</param>
        /// <param name="length">Duration of animation.</param>
        /// <param name="easing">Optional easing function.</param>
        public static Task<bool> ChangeColorAnimated(this VisualElement element, Color fromColor, Color toColor, Action<Color> callback, uint length = 250, Easing easing = null)
        {
            Func<double, Color> transform = (t) =>
              Color.FromRgba(fromColor.R + t * (toColor.R - fromColor.R),
                             fromColor.G + t * (toColor.G - fromColor.G),
                             fromColor.B + t * (toColor.B - fromColor.B),
                             fromColor.A + t * (toColor.A - fromColor.A));

            return AnimateColor(element, "ColorTo", transform, callback, length, easing);
        }

        /// <summary>
        /// Cancels the animation.
        /// </summary>
        /// <param name="element">The visual element.</param>
        public static void CancelAnimation(this VisualElement element)
        {
            element.AbortAnimation("ColorTo");
        }

        /// <summary>
        /// Returns width of control as positioned in the specified <paramref name="layout"/>
        /// </summary>
        /// <param name="control"></param>
        /// <param name="layout">the parent layout</param>
        /// <returns></returns>
        public static double GetWidth(this VisualElement control, Layout layout)
        {
            return control.Measure(layout.Width, layout.Height).Request.Width;
        }

        /// <summary>
        /// Returns height of control as positioned in the specified <paramref name="layout"/>
        /// </summary>
        /// <param name="control"></param>
        /// <param name="layout">the parent layout</param>
        /// <returns></returns>
        public static double GetHeight(this VisualElement control, Layout layout)
        {
            return control.Measure(layout.Width, layout.Height).Request.Height;
        }

        /// <summary>
        /// Returns the position of the specified control on the screen
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static Point GetScreenLocation(this VisualElement control)
        {
            double screenCoordinateX = control.X;
            double screenCoordinateY = control.Y;

            if (control.Parent.GetType() != typeof(Application))
            {
                VisualElement parent = (VisualElement)control.Parent;

                while (parent != null)
                {
                    screenCoordinateX += parent.X;
                    screenCoordinateY += parent.Y;

                    if (parent.Parent.GetType() == typeof(Application))
                    {
                        parent = null;
                    }
                    else
                    {
                        parent = (VisualElement)parent.Parent;
                    }
                }
            }

            return new Point(screenCoordinateX, screenCoordinateY);
        }

        static Task<bool> AnimateColor(VisualElement element, string name, Func<double, Color> transform, Action<Color> callback, uint length, Easing easing)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate<Color>(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }
    }
}

