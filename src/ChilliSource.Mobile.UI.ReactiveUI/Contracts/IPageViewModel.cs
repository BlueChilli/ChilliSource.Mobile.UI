using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reactive;
using System.Text;

namespace ChilliSource.Mobile.UI.ReactiveUI
{
 
   

    public interface IModalViewModel
    {
        string Title
        {
            get;
        }

    }

    public interface IPageViewModel
    {
        string Title
        {
            get;
        }

    }

    public interface IPopModalViewModel
    {
        string Title
        {
            get;
        }
    }
    

    public interface IView
    {
        IObservable<IPageViewModel> PagePopped
        {
            get;
        }

        IObservable<Unit> PushPage(
            IPageViewModel pageViewModel,
            string contract,
            bool resetStack,
            bool animate);

          IObservable<Unit> PopToRootPage(
                bool animate);

        IObservable<Unit> PopPage(
            bool animate);

        IObservable<Unit> PushModal(
            IModalViewModel modalViewModel,
            string contract,
            bool animate);

        IObservable<Unit> PopModal(
              bool animate);

        IObservable<Unit> PushPopup(
                IPopModalViewModel page,
                string contract,
                bool animate);

        IObservable<Unit> PopPopup(
                bool animate,
                bool resetStac);

    }

    public interface INavigationService
    {
        IView View
        {
            get;
        }

        IObservable<IImmutableList<IPageViewModel>> PageStack
        {
            get;
        }

        IObservable<IImmutableList<IModalViewModel>> ModalStack
        {
            get;
        }

        IObservable<IImmutableList<IPopModalViewModel>> PopModalStack
        {
            get;
        }

        IObservable<Unit> PushPage(
                IPageViewModel page,
                string contract = null,
                bool resetStack = false,
                bool animate = true);

        IObservable<Unit> PopPage(
                bool animate = true);

         IObservable<Unit> PopToRootPage(
                bool animate = false);

        IObservable<Unit> PushModal(
                IModalViewModel modal,
                string contract = null,
                bool animate = true);

        IObservable<Unit> PopModal(
                bool animate = true);

       
        IObservable<Unit> PushPopup(
                IPopModalViewModel page,
                string contract = null,
                bool animate = true);

        IObservable<Unit> PopPopup(bool animate = true,
          bool resetStack = false);
      
        bool HasPageInStack { get; }
        bool HasModalInStack { get; }
        bool HasPopModalInStack { get; }
        int  PageStackCount {get;}
        int  ModalStackCount {get;}
        int  PopModalStackCount {get;}
    }

}
