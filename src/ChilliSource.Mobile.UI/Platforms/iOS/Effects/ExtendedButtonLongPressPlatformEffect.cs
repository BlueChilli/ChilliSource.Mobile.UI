#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Threading.Tasks;
using CoreGraphics;
using ChilliSource.Mobile.UI;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreAnimation;

[assembly: ExportEffect(typeof(ExtendedButtonLongPressPlatformEffect), nameof(ExtendedButtonLongPressEffect))]
namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Platform effect to add the long press effect on the extended button control
    /// </summary>
    public class ExtendedButtonLongPressPlatformEffect : PlatformEffect
    {
        ExtendedButton _ExtendedButton;
        UIView _fillView;
        bool _longPressAnimationEnded;
        bool _longPressCommandExecuted;

        protected override void OnAttached()
        {
            _ExtendedButton = Element as ExtendedButton;

            _ExtendedButton.OnTouchesMoved += HandleTouchesMoved;
            _ExtendedButton.OnTouchesBegan += HandleTouchesBegan;
            _ExtendedButton.OnTouchesEnded += HandleTouchesEnded;
            _ExtendedButton.OnDidLayoutSubviews += ExtendedButton_OnDidLayoutSubviews;

            CreateFillView();
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            _fillView.Frame = GetResetFillFrame();
            base.OnElementPropertyChanged(args);
        }

        protected override void OnDetached()
        {
            if (_ExtendedButton != null)
            {
                _ExtendedButton.OnTouchesMoved -= HandleTouchesMoved;
                _ExtendedButton.OnTouchesBegan -= HandleTouchesBegan;
                _ExtendedButton.OnTouchesEnded -= HandleTouchesEnded;
                _ExtendedButton.OnDidLayoutSubviews -= ExtendedButton_OnDidLayoutSubviews;

            }
        }

        #region Setup

        void CreateFillView()
        {
            Control.ClipsToBounds = true;

            _fillView = new UIView();
            _fillView.BackgroundColor = _ExtendedButton.PressedBackgroundColor.ToUIColor();

            _fillView.UserInteractionEnabled = false;
            _fillView.ExclusiveTouch = false;

            Control.AddSubview(_fillView);
            Control.SendSubviewToBack(_fillView);
        }

        #endregion

        #region TouchHandlers

        /// <summary>
        /// Change the fillView frame using the forceTouchPercentage value and fill direction
        /// </summary>
        /// <param name="forceTouchPercentage">Force touch percentage.</param>
        void HandleTouchesMoved(float forceTouchPercentage)
        {
            switch (_ExtendedButton.LongPressFillDirection)
            {
                case LongPressDirection.BottomToTop:
                    _fillView.Frame = new CGRect(0, Control.Frame.Size.Height - (Control.Frame.Size.Height * (forceTouchPercentage)), Control.Frame.Size.Width, Control.Frame.Size.Height);
                    break;

                case LongPressDirection.LeftToRight:
                    _fillView.Frame = new CGRect(0, 0, (Control.Frame.Size.Width * (forceTouchPercentage)), Control.Frame.Size.Height);
                    break;

                case LongPressDirection.RightToLeft:
                    _fillView.Frame = new CGRect(Control.Frame.Size.Width - (Control.Frame.Size.Width * (forceTouchPercentage)), 0, Control.Frame.Size.Width, Control.Frame.Size.Height);
                    break;

                case LongPressDirection.TopToBotton:
                    _fillView.Frame = new CGRect(0, 0, Control.Frame.Size.Width, (Control.Frame.Size.Height * (forceTouchPercentage)));
                    break;
            }

            if (forceTouchPercentage >= 1 && !_longPressAnimationEnded && _ExtendedButton.TouchUpAfterLongPressCommand != null)
            {
                _fillView.Frame = CGRect.Empty;
                _longPressAnimationEnded = true;
                _ExtendedButton.TouchUpAfterLongPressCommand.Execute(null);
                _longPressCommandExecuted = true;
            }
        }

        void HandleTouchesBegan()
        {
            AnimateView(true);
        }

        void HandleTouchesEnded(bool invokedFromForceTouch)
        {
            if (invokedFromForceTouch)
            {
                if (!_longPressAnimationEnded || _longPressCommandExecuted)
                {
                    _fillView.Frame = GetResetFillFrame();

                    if (_longPressCommandExecuted)
                    {
                        _longPressAnimationEnded = false;
                    }
                }
            }
            else
            {
                if (_longPressAnimationEnded && _ExtendedButton.TouchUpAfterLongPressCommand != null)
                {
                    _ExtendedButton.TouchUpAfterLongPressCommand.Execute(null);
                }

                AnimateView(false);
            }
        }

        void HandleTouchesCancelled()
        {
            _longPressAnimationEnded = false;
            AnimateView(false);
        }

        void ExtendedButton_OnDidLayoutSubviews()
        {
            _fillView.Frame = GetResetFillFrame();
        }

        #endregion

        #region ViewAnimation

        void AnimateView(bool up)
        {
            UIView.AnimateNotify(up ? _ExtendedButton.LongPressDuration : 0.5, 0, UIViewAnimationOptions.BeginFromCurrentState, delegate ()
           {
               _longPressAnimationEnded = false;

               if (up)
               {
                   _fillView.Frame = Control.Frame;
               }
               else
               {
                   _fillView.Frame = GetResetFillFrame();
                   Control.LayoutSubviews();
               }

           }, (finished) =>
           {
               if (up)
               {
                   _longPressAnimationEnded = true;
               }
               else
               {
                   _longPressAnimationEnded = false;
               }
           });
        }

        #endregion

        #region ResetFillFrame

        CGRect GetResetFillFrame()
        {
            switch (_ExtendedButton.LongPressFillDirection)
            {
                case LongPressDirection.BottomToTop:
                    return new CGRect(0, Control.Frame.Size.Height, Control.Frame.Size.Width, Control.Frame.Size.Height);

                case LongPressDirection.LeftToRight:
                    return new CGRect(0, 0, 0, Control.Frame.Size.Height);

                case LongPressDirection.RightToLeft:
                    return new CGRect(Control.Frame.Size.Width, 0, Control.Frame.Size.Width, Control.Frame.Size.Height);

                case LongPressDirection.TopToBotton:
                    return new CGRect(0, 0, Control.Frame.Size.Width, 0);
            }

            return CGRect.Empty;
        }
        #endregion
    }
}

