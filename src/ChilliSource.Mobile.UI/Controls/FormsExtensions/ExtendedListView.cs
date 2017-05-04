#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class ExtendedListView : ListView
	{
		public ExtendedListView() : this(ListViewCachingStrategy.RetainElement)
		{

		}

		public ExtendedListView(ListViewCachingStrategy cacheStrategy) : base(cacheStrategy)
		{
		}

		//Note: instead of calling RefrehUI, a better approach is to call ForceUpdateSize on the cell that needs to have its height recalculated.
		//If the ListView is jumping to the top make sure to give the EstimatedRowHeight property a value in the ListView renderer.
		public void RefreshUI()
		{
			UIRefreshRequested?.Invoke(this, new EventArgs());
		}

		public event EventHandler UIRefreshRequested;

	}
}

