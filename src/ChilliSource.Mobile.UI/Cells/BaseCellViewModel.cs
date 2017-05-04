#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using ChilliSource.Mobile.UI.Core;

namespace ChilliSource.Mobile.UI
{
	public interface ICellViewModel
	{
		void ForceUpdateSize();
		event EventHandler SizeUpdateRequested;
	}

	public class BaseCellViewModel : BaseViewModel, ICellViewModel
	{
		public void ForceUpdateSize()
		{
			if (SizeUpdateRequested != null)
			{
				SizeUpdateRequested(this, new EventArgs());
			}
		}

		public event EventHandler SizeUpdateRequested;
	}
}

