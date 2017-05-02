using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace Examples.UITests
{
	[TestFixture(Platform.Android)]
	[TestFixture(Platform.iOS)]
    public class StyledNavigationBarPageTests
    {
		IApp app;
		Platform platform;

		public StyledNavigationBarPageTests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

	
    }
}
