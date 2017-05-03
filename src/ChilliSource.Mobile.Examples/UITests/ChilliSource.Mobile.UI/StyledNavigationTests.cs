using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace Examples.UITests
{
	//[TestFixture(Platform.Android)]
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


		[Test]
		public void StyledNavigationBarPage_ShouldChangeBasedOnSubtitleProperty()
		{
            app.Tap(x => x.Marked("Let's go!"));
			app.Tap(x => x.Id("Images/Index/Behaviours"));
            app.Tap(x => x.Marked("Style that nav bar!"));
            app.WaitForElement(x => x.Text("Hide Left Toolbar"));
            Assert.AreEqual(1, app.Query(x => x.Marked("title")).Length);
            app.Tap(x => x.Text("ChangeSubTitle"));
            Assert.AreEqual(1,app.Query(x => x.Marked("SubTitle changed")).Length);
		}

        [Test]
        public void StyledNavigationBarPage_CanChangeVisibilityOfToolbarItem()
        {
            app.Tap(x => x.Marked("Let's go!"));
            app.Tap(x => x.Id("Images/Index/Behaviours"));
            app.Tap(x => x.Marked("Style that nav bar!"));
            app.WaitForElement(x => x.Text("Hide Left Toolbar"));
            Assert.AreEqual(1, app.Query(x => x.Text("Right Item")).Length);
            app.Tap(x => x.Text("Hide Right Toolbar"));
			Assert.AreEqual(0, app.Query(x => x.Text("Right Item")).Length);
			app.Tap(x => x.Text("Show Right Toolbar"));
			Assert.AreEqual(1, app.Query(x => x.Text("Right Item")).Length);
		}
    }
}
