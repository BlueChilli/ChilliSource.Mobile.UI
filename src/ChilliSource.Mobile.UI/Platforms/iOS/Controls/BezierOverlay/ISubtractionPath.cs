#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using CoreGraphics;
using UIKit;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Provides an interface for the 
    /// </summary>
    public interface ISubtractionPath
    {
        CGRect Frame { get; }
        UIBezierPath BezierPath { get; }
    }
}
