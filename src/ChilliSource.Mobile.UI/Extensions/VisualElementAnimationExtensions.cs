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
	/// Animates a color change with a visual element
	/// </summary>
	public static class VisualElementAnimationExtensions
	{
		public static Task<bool> ColorTo(this VisualElement self, Color fromColor, Color toColor, Action<Color> callback, uint length = 250, Easing easing = null)
		{
			Func<double, Color> transform = (t) =>
			  Color.FromRgba(fromColor.R + t * (toColor.R - fromColor.R),
							 fromColor.G + t * (toColor.G - fromColor.G),
							 fromColor.B + t * (toColor.B - fromColor.B),
							 fromColor.A + t * (toColor.A - fromColor.A));

			return ColorAnimation(self, "ColorTo", transform, callback, length, easing);
		}

		public static void CancelAnimation(this VisualElement self)
		{
			self.AbortAnimation("ColorTo");
		}

		static Task<bool> ColorAnimation(VisualElement element, string name, Func<double, Color> transform, Action<Color> callback, uint length, Easing easing)
		{
			easing = easing ?? Easing.Linear;
			var taskCompletionSource = new TaskCompletionSource<bool>();

			element.Animate<Color>(name, transform, callback, 16, length, easing, (v, c) => taskCompletionSource.SetResult(c));

			return taskCompletionSource.Task;
		}
	}
}

