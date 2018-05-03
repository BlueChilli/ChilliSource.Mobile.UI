using System;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Attribute representing data template association between a view and its view model
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = true)]
    public class DataTemplateAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of this <c>DataTemplateAttribute</c> class.
        /// </summary>
        /// <param name="template">Template.</param>
        /// <param name="viewModel">View model.</param>
        public DataTemplateAttribute(Type template, Type viewModel)
        {
            Template = template;
            ViewModel = viewModel;
        }

        /// <summary>
        /// Gets or sets the data template.
        /// </summary>
        /// <value>A <see cref="Type"/> that represents the template.</value>
        public Type Template { get; protected set; }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>A <see cref="Type"/> that represents the view model.</value>
        public Type ViewModel { get; protected set; }
    }
}
