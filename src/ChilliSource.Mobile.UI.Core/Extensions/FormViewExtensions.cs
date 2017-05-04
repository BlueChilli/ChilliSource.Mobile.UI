#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/*
Source: Xamarin Forms - Relative Layout (https://github.com/xamarin/Xamarin.Forms/blob/master/Xamarin.Forms.Core/RelativeLayout.cs)
Author: Xamarin (https://github.com/xamarin)
License: MIT (https://github.com/xamarin/Xamarin.Forms/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using System.Linq;
using ChilliSource.Mobile.UI.Core;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public static class FormViewExtensions
	{
		/// <summary>
		/// Sets the constraints of a view in its relative layout.
		/// </summary>
		/// <param name="relativeLayout">Relative layout.</param>
		/// <param name="xConstraint">X constraint.</param>
		/// <param name="yConstraint">Y constraint.</param>
		/// <param name="widthConstraint">Width constraint.</param>
		/// <param name="heightConstraint">Height constraint.</param>
		public static void SetConstraints(this View view, RelativeLayout relativeLayout, ExposedConstraint xConstraint = null, ExposedConstraint yConstraint = null, ExposedConstraint widthConstraint = null, ExposedConstraint heightConstraint = null)
		{
			var parents = new List<View>();

			Func<double> x;
			if (xConstraint != null)
			{
				x = () => xConstraint.Compute(relativeLayout);
				if (xConstraint.RelativeTo != null)
					parents.AddRange(xConstraint.RelativeTo);
			}
			else
				x = () => 0;

			Func<double> y;
			if (yConstraint != null)
			{
				y = () => yConstraint.Compute(relativeLayout);
				if (yConstraint.RelativeTo != null)
					parents.AddRange(yConstraint.RelativeTo);
			}
			else
				y = () => 0;

			Func<double> width;
			if (widthConstraint != null)
			{
				width = () => widthConstraint.Compute(relativeLayout);
				if (widthConstraint.RelativeTo != null)
					parents.AddRange(widthConstraint.RelativeTo);
			}
			else
				width = () => view.Measure(relativeLayout.Width, relativeLayout.Height, MeasureFlags.IncludeMargins).Request.Width;

			Func<double> height;
			if (heightConstraint != null)
			{
				height = () => heightConstraint.Compute(relativeLayout);
				if (heightConstraint.RelativeTo != null)
					parents.AddRange(heightConstraint.RelativeTo);
			}
			else
				height = () => view.Measure(relativeLayout.Width, relativeLayout.Height, MeasureFlags.IncludeMargins).Request.Height;

			BoundsConstraint bounds = BoundsConstraint.FromExpression(() => new Rectangle(x(), y(), width(), height()), parents.Distinct().ToArray());
			RelativeLayout.SetBoundsConstraint(view, bounds);
		}
	}
}
