#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;


namespace ChilliSource.Mobile.UI
{
	public interface IHudService
	{
		void Show();
		void Show(string message);
		void Show(string message, ExtendedFont font);
		void Dismiss();
	}
}

