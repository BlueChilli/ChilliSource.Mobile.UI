using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Container for <see cref="ValidatableObject{T}"/> allowing bulk validation
    /// </summary>
    public sealed class ValidatableObjects : Dictionary<string, IValidatableObject>
    {
        /// <summary>
        /// Validates all objects in the collection.
        /// </summary>
        /// <returns><c>true</c> if all objects are valid, otherwise <c>false</c>.</returns>
        public bool Validate()
        {
            var valid = new List<bool>();
            foreach (var validatableObject in this.Values)
            {
                valid.Add(validatableObject.Validate());
            }

            return valid.All(m => m);
        }

        /// <summary>
        /// Validates all objects in the collection asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task{T}"/> including the boolean outcome of the result: 
        /// <c>true</c> if all objects are valid, otherwise <c>false</c>.</returns>
        public async Task<bool> ValidateAsync()
        {
            var valid = new List<bool>();
            foreach (var validatableObject in this.Values)
            {
                valid.Add(await validatableObject.ValidateAsync());
            }

            return valid.All(m => m);
        }
    }
}
