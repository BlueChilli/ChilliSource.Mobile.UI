#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	/// <summary>
	/// Individual Action/Button to display in the Advanced Action Sheet.
	/// </summary>
	public class AdvancedActionSheetAction
	{
		public static AdvancedActionSheetAction CreateAction(string title, ExtendedFont font, Command command, ActionType type = ActionType.Default)
		{
			return new AdvancedActionSheetAction()
			{
				Title = title,
				Font = font,
				Command = command,
				ActionType = type
			};
		}

		public string Title
		{
			get; set;
		}

		public ExtendedFont Font
		{
			get; set;
		}

		public ICommand Command
		{
			get; set;
		}

		public ActionType ActionType
		{
			get; set;
		}
	}

	public enum ActionType
	{
		Default,
		Cancel
	}
}
