#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

/* inspired from
 * Source:     BikeSharing360 (https://github.com/Microsoft/BikeSharing360_MobileApps)
 * Author:     Microsoft (https://github.com/Microsoft)
 * License:    MIT https://opensource.org/licenses/MIT
*/

using System;


namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Password repeat comparison validation rule. 
    /// </summary>
    public class RepeatPasswordRule : ComparisonRule<string>
    {
        /// <summary>
        /// Initializes a new instance with the password to compare to.
        /// </summary>
        /// <param name="otherPassword">Other password.</param>
        /// <param name="validationMessage">Validation message.</param>
        public RepeatPasswordRule(Func<string> otherPassword, string validationMessage) : base(otherPassword, validationMessage)
        {

        }

        /// <summary>
        /// <see cref="Comparison"/> type       
        /// </summary>
        public override Comparison Comparison => Comparison.Equals;
    }
}
