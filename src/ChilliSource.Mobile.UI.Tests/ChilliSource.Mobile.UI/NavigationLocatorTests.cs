#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using ChilliSource.Mobile.UI;
using Xunit;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;

namespace Tests
{
    public class NavigationLocatorTests
    {
        private class TestViewModel : BaseViewModel { }

        [ViewModel(typeof(TestViewModel))]
        private class TestPage : Page { }

        [Fact]
        public void Register_ShouldStoreMappingBetweenViewAndViewModel()
        {
            var viewModel = new TestViewModel();
            var page = NavigationLocator.GetPage(viewModel);

            AssertPageResult(page);
        }

        [Fact]
        public void RegisterGeneric_ShouldStoreMappingBetweenViewAndViewModel()
        {
            var viewModel = new TestViewModel();
            var page = NavigationLocator.GetPage(viewModel);

            AssertPageResult(page);
        }

        [Fact]
        public void GetPage_ShouldReturnPageInstanceForViewModel_WhenGivenViewModelInstance()
        {
            var viewModel = new TestViewModel();
            var page = NavigationLocator.GetPage(viewModel);

            AssertPageResult(page);
        }

        [Fact]
        public void GetPage_ShouldReturnPageInstanceForViewModel_WhenGivenViewModelType()
        {
            var page = NavigationLocator.GetPage(typeof(TestViewModel));
            AssertPageResult(page);
        }

        [Fact]
        public void GetPageGeneric_ShouldReturnPageInstanceForViewModel()
        {
            var page = NavigationLocator.GetPage<TestViewModel>();
            AssertPageResult(page);
        }

        [Fact]
        public void GetPage_ShouldReturnSamePageInstanceForViewModelByDefault()
        {
            var viewModel = new TestViewModel();
            var page1 = NavigationLocator.GetPage(viewModel);
            var page2 = NavigationLocator.GetPage(viewModel);

            Assert.Same(page1, page2);
        }

        [Fact]
        public void GetPage_ShouldReturnNewPageInstanceForViewModel_WhenSingletonIsNotRequested()
        {
            var viewModel = new TestViewModel();
            var page1 = NavigationLocator.GetPage(viewModel, singleton: false);
            var page2 = NavigationLocator.GetPage(viewModel, singleton: false);
            Assert.NotSame(page1, page2);
        }

        private void AssertPageResult(Page page)
        {
            Assert.NotNull(page);
            Assert.IsType(typeof(TestPage), page);
        }
    }
}
