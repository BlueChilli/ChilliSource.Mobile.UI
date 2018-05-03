using System;
namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Attribute to associate a view model with a page, for the purpose of providing view model first navigation
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewModelAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the type of the view model.
        /// </summary>
        /// <value>A <see cref="Type"/> value that represents the type of the view model.</value>
        public Type ViewModelType { get; protected set; }

        /// <summary>
        /// Initializes a new instance of this <c>ViewModelAttribute</c> class.
        /// </summary>
        /// <param name="viewModelType">View model type.</param>
        public ViewModelAttribute(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }
    }
}
