#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms.Platform.Android;
namespace ChilliSource.Mobile.UI
{
	public class BaseActivity : FormsAppCompatActivity
	{
		public BaseActivity()
		{
		}

		public int TabLayoutResourceId { get; set; }
		public int ToolbarResourceId { get; set; }

	}
}
