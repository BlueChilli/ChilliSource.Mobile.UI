#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;

namespace ChilliSource.Mobile.UI.Core
{
	/// <summary>
	/// Provides methods that allow access to the native clipboard
	/// </summary>
	public interface IClipboardService
	{
		/// <summary>
		/// Copies the specified <paramref name="text"/> to the native clipboard
		/// </summary>
		/// <param name="text">Text.</param>
		void CopyToClipboard(String text);
	}
}

