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

namespace ChilliSource.Mobile.UI.Core
{
	/// <summary>
	/// Provides container for binding grouped data to ListViews
	/// </summary>
	public class GroupViewModel : ObservableCollection<object>
	{
		public GroupViewModel(string name)
		{
			Name = name;
		}

		public GroupViewModel(string labelText, Command buttonCommand, string buttonText = "", ImageSource buttonImage = null)
		{
			Name = labelText;
			ButtonCommand = buttonCommand;
			ButtonText = buttonText;
			ButtonImage = buttonImage;
		}

		public string Name
		{
			get;
			set;
		}

		public ICommand ButtonCommand
		{
			get;
			private set;
		}

		public string ButtonText
		{
			get;
			private set;
		}

		public ImageSource ButtonImage
		{
			get;
			private set;
		}


		public bool ButtonVisible
		{
			get { return ButtonCommand != null; }
		}
	}
}

