#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Container for binding grouped data to list views, to be displayed as table section headers
    /// </summary>
    public class GroupViewModel : ObservableCollection<object>
    {
        /// <summary>
        /// Initializes a new instance with the section header name/text.
        /// </summary>
        /// <param name="name">Section header name/text.</param>
        public GroupViewModel(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance with additional button options.
        /// </summary>
        /// <param name="name">Section header name/text.</param>
        /// <param name="buttonCommand">Button command.</param>
        /// <param name="buttonText">Button text.</param>
        /// <param name="buttonImage">Button image source.</param>
        public GroupViewModel(string name, Command buttonCommand, string buttonText = "", ImageSource buttonImage = null)
        {
            Name = name;
            ButtonCommand = buttonCommand;
            ButtonText = buttonText;
            ButtonImage = buttonImage;
        }

        /// <summary>
        /// Gets or sets the section header name/text.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the command for the button.
        /// </summary>
        /// <value>A <see cref="ICommand"/> value that represents the button's command.</value>
        public ICommand ButtonCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the text for the button.
        /// </summary>
        public string ButtonText
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the image source for the button.
        /// </summary>
        /// <value>A <see cref="ImageSource"/> value that provides the source for the button's image.</value>
        public ImageSource ButtonImage
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the button is visible.
        /// </summary>
        /// <value><c>true</c> if button is visible; otherwise, <c>false</c>.</value>
        public bool ButtonVisible
        {
            get { return ButtonCommand != null; }
        }
    }
}

