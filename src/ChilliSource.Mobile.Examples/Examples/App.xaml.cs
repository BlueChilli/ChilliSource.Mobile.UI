#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Examples
{
	public partial class App : Application
	{
		public static App CurrentApp;

		public App()
		{
			InitializeComponent();

			CurrentApp = this;

			Global.Instance.Initialize();

			MainPage = new NavigationPage(new IntroPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
