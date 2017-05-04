#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using ChilliSource.Mobile.UI.Core;
using Xunit;
using ReactiveUI.Testing;
using Microsoft.Reactive.Testing;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;
using ChilliSource.Mobile.Tests;
using ReactiveUI.XamForms;
using ReactiveUI;
using ChilliSource.Mobile.Core;
using Xamarin.Forms;
using System.Reactive.Concurrency;
using ChilliSource.Mobile.UI.ReactiveUI;

namespace ChilliSource.ReactiveUI
{
    public class TestViewModel : ReactiveObject, ISupportModal, IRoutableViewModel
    {
        private readonly ReactiveRoutingState _router;
        public TestViewModel(IScheduler scheduler)
        {
            _router = new ReactiveRoutingState();
            _router.Scheduler = scheduler;
        }

        public bool Animated { get; set;}

        public string UrlPathSegment => "Test Page";

        public bool WithNavigationBar => true;


        public Color? NavBarColor { get; }
       

        public Color? NavBarTextColor { get; }

        public ReactiveRoutingState Router => _router;


        RoutingState IScreen.Router => _router;

        public IScreen HostScreen => this;
       

    }

    public class TestViewModel2 : ReactiveObject, ISupportModal
    {
        private readonly ReactiveRoutingState _router;
        readonly IReactiveScreen _screen;

        public TestViewModel2(IScheduler scheduler, IReactiveScreen screen)
        {
            this._screen = screen;
            _router = new ReactiveRoutingState();
            _router.Scheduler = scheduler;
        }

        public bool Animated { get; set; }

        public string UrlPathSegment => "Test Page";

        public bool WithNavigationBar => true;


        public Color? NavBarColor { get; }


        public Color? NavBarTextColor { get; }

        public ReactiveRoutingState Router => _screen.Router;

        RoutingState IScreen.Router => _screen.Router;
       
    }

    public class ParentViewModel : ReactiveObject, IReactiveScreen
    {
        private readonly ReactiveRoutingState _router;
        public ParentViewModel(IScheduler scheduler)
        {
            _router = new ReactiveRoutingState();
            _router.Scheduler = scheduler;
          
        }

        public ReactiveRoutingState Router => _router;


        RoutingState IScreen.Router => _router;
       
    }

    public class ReactiveRoutingStateTests
	{

		[Fact]
		public void Scheduler_ShouldCallSetupRx_WhenSchedulerChanged()
		{
           var routing = new ReactiveRoutingState();
           routing.Scheduler = RxApp.TaskpoolScheduler;
           Assert.Equal(RxApp.TaskpoolScheduler, routing.Scheduler);
		}

        [Fact]
        public void PushModal_ShouldThrow_IfEmptyObjectIsPassed()
        {
            var testScheduler = new TestScheduler();

            testScheduler.With(scheduler =>
            {
                var parent = new ParentViewModel(scheduler);
             
                parent.Router.PushModal.Execute(null)
                      .Subscribe(onNext:_ => {}, onError: ex =>
                      {
                          Assert.NotNull(ex);
                      });

            });
        }

        [Fact]
        public void PushModal_ShouldAddViewModelToModalStack()
        {
            var testScheduler = new TestScheduler();

            testScheduler.With(scheduler =>
            {
                var parent = new ParentViewModel(scheduler);
                var vm = new TestViewModel(scheduler);

                Assert.Equal(0, vm.Router.ModalStack.Count);

                parent.Router.PushModal.Execute(vm).Subscribe();
                scheduler.AdvanceByMs(10);
                Assert.Equal(1, parent.Router.ModalStack.Count);
            });
        }

        [Fact]
        public void PopModal_ShouldCleanUpTheNavigationStacks()
        {
            var testScheduler = new TestScheduler();

            testScheduler.With(scheduler =>
            {
                var parent = new ParentViewModel(scheduler);
                var vm = new TestViewModel(scheduler);
                vm.Router.NavigationStack.Add(new TestViewModel(scheduler));
                vm.Router.NavigationStack.Add(new TestViewModel(scheduler));

                parent.Router.PushModal.Execute(vm)
                      .Subscribe();

                vm.Router.PushModal.Execute(vm).Subscribe();
                Assert.Equal(2, vm.Router.NavigationStack.Count);

                vm.Router.PopModal.Execute(true).Subscribe();

                scheduler.AdvanceByMs(10);

                Assert.Equal(0, vm.Router.NavigationStack.Count);
                Assert.Equal(0, vm.Router.ModalStack.Count);
                Assert.Equal(0, parent.Router.ModalStack.Count);

            });
        }


        [Fact]
        public void PopModal_ShouldDoNothing_WhenThereAreNoItemInModalStack()
        {
            var testScheduler = new TestScheduler();

            testScheduler.With(scheduler =>
            {
                var vm = new TestViewModel(scheduler);
                vm.Router.NavigationStack.Add(new TestViewModel(scheduler));
                vm.Router.NavigationStack.Add(new TestViewModel(scheduler));
                vm.Router.PopModal.Execute(true).Subscribe();

                scheduler.AdvanceByMs(10);

                Assert.Equal(2, vm.Router.NavigationStack.Count);
                Assert.Equal(0, vm.Router.ModalStack.Count);
            });
        }
		
        [Fact]
        public void PopModal_ShouldPopItemsFromModalStack()
        {
            var testScheduler = new TestScheduler();

            testScheduler.With(scheduler =>
            {
                var parent = new ParentViewModel(scheduler);
                var vm = new TestViewModel2(scheduler, parent);
                var isExecuted = false;
                parent.Router.PushModal.Execute(vm)
                      .Subscribe();

                parent.Router.PopModal.Subscribe(m =>
                {
                    isExecuted = true;
                });

                vm.Router.PopModal.Execute(true).Subscribe();

                scheduler.AdvanceByMs(10);

                Assert.Equal(0, vm.Router.ModalStack.Count);
                Assert.Equal(0, parent.Router.ModalStack.Count);
                Assert.True(isExecuted);

            });
        }

	}
}
