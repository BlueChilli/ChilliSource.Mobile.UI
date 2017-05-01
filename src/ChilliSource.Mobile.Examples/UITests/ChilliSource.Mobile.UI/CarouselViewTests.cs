#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Examples.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class CarouselViewTests
    {
        IApp app;
        Platform platform;

        public CarouselViewTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void CarouselView_ShouldAllowSwipingBetweenItems()
        {
            //navigate to carousel example
            app.WaitForElement(x => x.Marked("Index"));
            app.Tap(x => x.Marked("Controls"));

            app.WaitForElement(x => x.Marked("Controls Index"));
            app.Tap(x => x.Marked("CarouselView"));

            app.WaitForElement(x => x.Marked("Carousel View"));
            app.Screenshot("Carousel view screen");

            Assert.IsTrue(app.Query("Item 1").Any());

            app.SwipeRightToLeft();
            Assert.IsTrue(app.Query("Item 2").Any());

            app.SwipeRightToLeft();
            Assert.IsTrue(app.Query("Item 3").Any());

            app.SwipeLeftToRight();
            Assert.IsTrue(app.Query("Item 2").Any());

            app.SwipeLeftToRight();
            Assert.IsTrue(app.Query("Item 1").Any());
        }

        [Test]
        public void CarouselView_ShouldDisplayPageIndicators()
        {
            //navigate to carousel example
            app.WaitForElement(x => x.Marked("Index"));
            app.Tap(x => x.Marked("Controls"));

            app.WaitForElement(x => x.Marked("Controls Index"));
            app.Tap(x => x.Marked("CarouselView"));

            app.WaitForElement(x => x.Marked("Carousel View"));
            app.Screenshot("Carousel view screen");

            var pageIndicatorView = app.Query(c => c.Marked("PageIndicatorViewId")).FirstOrDefault();
            Assert.IsNotNull(pageIndicatorView);

            var childViews = app.Query(c => c.Marked("PageIndicatorViewId").Child());
            Assert.AreEqual(3, childViews.Count());
        }
    }
}
