using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.Logging;
using Unit = System.Reactive.Unit;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
   
    public sealed class NavigationService : INavigationService
    {
        private readonly ILogger _logger;
        private readonly BehaviorSubject<IImmutableList<IModalViewModel>> _modalStack;
        private readonly BehaviorSubject<IImmutableList<IPageViewModel>> _pageStack;
        private readonly BehaviorSubject<IImmutableList<IPopModalViewModel>> _popmodalStack;
        private readonly IView _view;

        public NavigationService(IView view, ILogger logger)
        {
            _logger = logger;
            this._modalStack = new BehaviorSubject<IImmutableList<IModalViewModel>>(ImmutableList<IModalViewModel>.Empty);
            this._pageStack = new BehaviorSubject<IImmutableList<IPageViewModel>>(ImmutableList<IPageViewModel>.Empty);
            this._popmodalStack = new BehaviorSubject<IImmutableList<IPopModalViewModel>>(ImmutableList<IPopModalViewModel>.Empty);
            this._view = view ?? throw new ArgumentNullException(nameof(view));

            this
                    ._view
                    .PagePopped
                    .Do(
                            poppedPage =>
                            {
                                var currentPageStack = this._pageStack.Value;

                                if (currentPageStack.Count > 0 && poppedPage == currentPageStack[currentPageStack.Count - 1])
                                {
                                    var removedPage = PopStackAndTick(this._pageStack);
                                }
                            })
                    .SubscribeSafe();
            //.Subscribe();
        }

        public IView View => this._view;

        public IObservable<IImmutableList<IModalViewModel>> ModalStack => this._modalStack;
        public IObservable<IImmutableList<IPopModalViewModel>> PopModalStack => this._popmodalStack;

        public IObservable<IImmutableList<IPageViewModel>> PageStack => this._pageStack;

        public IObservable<Unit> PushPage(IPageViewModel page, string contract = null, 
            bool resetStack = false, bool animate = true)
        {
            Ensure.ArgumentNotNull(page, nameof(page));

            return this
                ._view
                .PushPage(page, contract, resetStack, animate)
                .Do(
                    _ =>
                    {
                        AddToStackAndTick(this._pageStack, page, resetStack);
                        _logger.Debug("Added page '{0}' (contract '{1}') to stack.", page.Title, contract);
                    });
        }

        public IObservable<Unit> PopPage(bool animate = true) =>
            this
                ._view
                .PopPage(animate);

        public IObservable<Unit> PushModal(IModalViewModel modal, 
            string contract = null,
             bool animate = true)
        {
            Ensure.ArgumentNotNull(modal, nameof(modal));

            return this
                ._view
                .PushModal(modal, contract, animate)
                .Do(
                    _ =>
                    {
                        AddToStackAndTick(this._modalStack, modal, false);
                        _logger.Debug("Added modal '{0}' (contract '{1}') to stack.", modal.Title, contract);
                    });
        }

        public IObservable<Unit> PopModal(
            bool animate = true) =>
            this
                ._view
                .PopModal(animate)
                .Do(
                    _ =>
                    {
                        var removedModal = PopStackAndTick(this._modalStack, true);
                        if(removedModal != null)
                        {
                            _logger.Debug("Removed modal '{0}' from stack.", removedModal?.Title);
                        }
                    });

        public IObservable<Unit> PushPopup(IPopModalViewModel page, string contract = null,
         bool animate = true)
        {
            Ensure.ArgumentNotNull(page, nameof(page));

            return this
                    ._view
                    .PushPopup(page, contract, animate)
                    .Do(
                            _ =>
                            {
                                AddToStackAndTick(this._popmodalStack, page, false);
                                _logger.Debug("Added page '{0}' (contract '{1}') to stack.", page.Title, contract);
                            });
        }

        public IObservable<Unit> PopPopup(bool animate = true, bool resetStack = false) =>
         this
            ._view
            .PopPopup(animate, resetStack)
            .Do(
                    _ =>
                    {
                       if(resetStack) 
                       {
                         PopAllStackAndTick(this._popmodalStack);
                       }
                       else 
                       {
                          var removedModal = PopStackAndTick(this._popmodalStack, true);
                       }

                       _logger.Debug("Removed all pop modal from stack.");
                    });

        public bool HasPageInStack => _pageStack?.Value.Any() ?? false;
        public bool HasModalInStack  => _modalStack?.Value.Any() ?? false;
        public bool HasPopModalInStack  => _popmodalStack?.Value.Any() ?? false;

        public int PageStackCount => _pageStack?.Value.Count ?? 0;
        public int ModalStackCount  => _modalStack?.Value.Count ?? 0;
        public int PopModalStackCount => _popmodalStack?.Value.Count ?? 0;

        private static void AddToStackAndTick<T>(BehaviorSubject<IImmutableList<T>> stackSubject, T item, bool reset)
        {
            var stack = stackSubject.Value;

            if (reset)
            {
                stack = new[] { item }.ToImmutableList();
            }
            else
            {
                stack = stack.Add(item);
            }

            stackSubject.OnNext(stack);
        }

        private static T PopStackAndTick<T>(BehaviorSubject<IImmutableList<T>> stackSubject, bool ignoreError = false)
        {
            var stack = stackSubject.Value;

            if (stack.Count == 0)
            {
                if(!ignoreError)
                {
                    throw new InvalidOperationException("Stack is empty.");
                }

                return default(T);
            }

            var removedItem = stack[stack.Count - 1];
            stack = stack.RemoveAt(stack.Count - 1);
            stackSubject.OnNext(stack);
            return removedItem;
        }

        private static void PopAllStackAndTick<T>(BehaviorSubject<IImmutableList<T>> stackSubject) 
        {
            stackSubject.OnNext(ImmutableList<T>.Empty);
        }

        public IObservable<Unit> PopToRootPage(bool animate = false) 
            =>   this
                ._view
                .PopToRootPage(animate)
                .Do(
                    _ =>
                    {
                        PopAllStackAndTick(this._pageStack);
                    });
    }
}
