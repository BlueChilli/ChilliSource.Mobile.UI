#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/*
Source: Xamarin Forms - Constraint (https://github.com/xamarin/Xamarin.Forms/blob/master/Xamarin.Forms.Core/Constraint.cs)
Author: Xamarin (https://github.com/xamarin)
License: MIT (https://github.com/xamarin/Xamarin.Forms/blob/master/LICENSE)
*/

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// This is a copy of the sealed Xamarin Forms <see cref="Constraint"/> class 
    /// with the difference that the <see cref="Compute"/> method has been made public, 
    /// thereby making it easier to refresh the constraint 
    /// </summary>
    public class ExposedConstraint
    {
        Func<RelativeLayout, double> _measureFunc;

        public IEnumerable<View> RelativeTo { get; set; }

        public static ExposedConstraint Constant(double size)
        {
            return new ExposedConstraint { _measureFunc = parent => size };
        }

        public static ExposedConstraint RelativeToParent(Func<RelativeLayout, double> measure)
        {
            return new ExposedConstraint { _measureFunc = measure };
        }

        public static ExposedConstraint RelativeToView(View view, Func<RelativeLayout, View, double> measure)
        {
            return new ExposedConstraint { _measureFunc = layout => measure(layout, view), RelativeTo = new[] { view } };
        }

        public double Compute(RelativeLayout parent)
        {
            return _measureFunc(parent);
        }
    }
}
