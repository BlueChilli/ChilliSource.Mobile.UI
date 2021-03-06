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

using System.Threading.Tasks;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Validation rule contract
    /// </summary>
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }

        bool Validate(T value);
        Task<bool> ValidateAsync(T value);
    }
}
