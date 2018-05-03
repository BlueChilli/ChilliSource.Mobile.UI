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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Basic validation contract
    /// </summary>
    public interface IValidatableObject
    {
        bool Validate();
        Task<bool> ValidateAsync();
    }

    /// <summary>
    /// Validation contract for <see cref="ValidatableObject{T}"/>
    /// </summary>
    public interface IValidatableObject<T> : IValidatableObject, IValidity
    {

        /// <summary>
        /// <see cref="IValidationRule{T}"/> calidation rules
        /// </summary>		
        List<IValidationRule<T>> Validations { get; }

        /// <summary>
        /// Validation errors.
        /// </summary>
        ObservableCollection<string> Errors { get; }

        /// <summary>
        /// Determines if the validation rules specify the object to be required (non-empty)
        /// </summary>
        bool IsRequired { get; }

        /// <summary>
        /// Underlying object being validated.
        /// </summary>
        T Value { get; set; }

        /// <summary>
        /// Forces validation to be passed
        /// </summary>
        void MakeValid();

        /// <summary>
        /// Adds a new error message and sets the validation status to invalid
        /// </summary>
        void AddError(string message);
    }
}
