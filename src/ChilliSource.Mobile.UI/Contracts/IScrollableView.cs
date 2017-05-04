#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public interface IScrollableView
	{

		void BeginUpdate();
		void EndUpdate();


	}

}
