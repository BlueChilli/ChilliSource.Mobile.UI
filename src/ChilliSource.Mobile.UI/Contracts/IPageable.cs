#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Windows.Input;

namespace ChilliSource.Mobile.UI
{
	public interface IPageable
	{
		ICommand LoadMoreCommand { get; }
		bool HasMoreItems { get; }
		bool IsPaging { get; }
	}
}
