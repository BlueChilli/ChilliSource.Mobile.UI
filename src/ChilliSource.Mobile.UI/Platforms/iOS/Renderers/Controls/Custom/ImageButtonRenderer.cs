// ***********************************************************************
// Assembly         : XLabs.Forms.iOS
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="ImageButtonRenderer.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ChilliSource.Mobile.UI.ImageButton), typeof(ChilliSource.Mobile.UI.ImageButtonRenderer))]
namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Draws a button on the iOS platform with the image shown in the right 
    /// position with the right size.
    /// </summary>
    public class ImageButtonRenderer : ButtonRenderer
    {
        /// <summary>
        /// The padding to use in the control.
        /// </summary>
        private const int CONTROL_PADDING = 2;

        /// <summary>
        /// Identifies the iPad.
        /// </summary>
        private const string IPAD = "iPad";


        private const double DefaultWidth = 50;
        private const double DefaultHeight = 50;

        /// <summary>
        /// Gets the underlying element typed as an <see cref="ImageButton"/>.
        /// </summary>
        private IImageButtonController ImageButton => Element as IImageButtonController;

        /// <summary>
        /// Handles the initial drawing of the button.
        /// </summary>
        /// <param name="e">Information on the <see cref="ImageButton"/>.</param> 
        protected override async void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            var imageButton = ImageButton;
            var targetButton = Control;
            if (imageButton != null && targetButton != null && imageButton.Source != null)
            {
                // Matches Android ImageButton behavior
                targetButton.LineBreakMode = UIKit.UILineBreakMode.WordWrap;

                var width = this.GetWidth(imageButton.ImageWidthRequest);
                var height = this.GetHeight(imageButton.ImageHeightRequest);



                await SetupImages(imageButton, targetButton, width, height);

                switch (imageButton.Orientation)
                {
                    case ImageOrientation.ImageToLeft:
                        AlignToLeft(targetButton);
                        break;
                    case ImageOrientation.ImageToRight:
                        AlignToRight(imageButton.ImageWidthRequest, targetButton);
                        break;
                    case ImageOrientation.ImageOnTop:
                        AlignToTop(imageButton.ImageHeightRequest, imageButton.ImageWidthRequest, targetButton, this.Element.WidthRequest);
                        break;
                    case ImageOrientation.ImageOnBottom:
                        AlignToBottom(imageButton.ImageHeightRequest, imageButton.ImageWidthRequest, targetButton);
                        break;
                    case ImageOrientation.ImageCentered:
                        AlignToCenter(targetButton);
                        break;
                }
            }
        }

        private void AlignToCenter(UIButton targetButton)
        {
            targetButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            targetButton.TitleLabel.TextAlignment = UITextAlignment.Center;

            var titleInsets = new UIEdgeInsets(0, 0, 0, 0);

            targetButton.TitleEdgeInsets = titleInsets;
            var imageInsets = new UIEdgeInsets(CONTROL_PADDING, CONTROL_PADDING, CONTROL_PADDING, CONTROL_PADDING);
            targetButton.ImageEdgeInsets = imageInsets;
        }

        /// <summary>
        /// Called when the underlying model's properties are changed.
        /// </summary>
        /// <param name="sender">Model sending the change event.</param>
        /// <param name="e">Event arguments.</param>
        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(ImageButton.Source) ||
                e.PropertyName == nameof(ImageButton.DisabledSource) ||
                e.PropertyName == nameof(ImageButton.ImageTintColor) ||
                e.PropertyName == nameof(ImageButton.DisabledImageTintColor) ||
                e.PropertyName == "Width" ||
                e.PropertyName == "Height")
            {

                if (ImageButton?.Source != null)
                {
                    if (this.ImageButton.ImageWidthRequest > 0 && this.ImageButton.ImageHeightRequest > 0)
                    {
                        await UpdateUI(this.ImageButton.ImageWidthRequest, this.ImageButton.ImageHeightRequest);
                    }
                    else if(this.Element.Width > 0 && this.Element.Height > 0) 
                    {
                        var width = this.ImageButton.ImageWidthRequest > 0 ? this.ImageButton.ImageWidthRequest : this.Element.Width;
                        var height = this.ImageButton.ImageHeightRequest > 0 ? this.ImageButton.ImageHeightRequest : this.Element.Height;
                        await UpdateUI(width, height);
                    }
                }
            }

           
        }

        private async Task UpdateUI(double width, double height)
        {
            var targetButton = Control;
            if (targetButton != null && ImageButton?.Source != null)
            {
                await SetupImages(ImageButton, targetButton, width, height);
            }
        }

        async Task SetupImages(IImageButtonController imageButton, UIButton targetButton, double width, double height)
        {
            UIColor tintColor = imageButton.ImageTintColor == Color.Transparent ? null : imageButton.ImageTintColor.ToUIColor();
            UIColor disabledTintColor = imageButton.DisabledImageTintColor == Color.Transparent ? null : imageButton.DisabledImageTintColor.ToUIColor();

            await SetImageAsync(imageButton.Source, width, height, targetButton, UIControlState.Normal, tintColor,ImageButton.Aspect);

            if (imageButton.DisabledSource != null || disabledTintColor != null)
            {
                await SetImageAsync(imageButton.DisabledSource ?? imageButton.Source, width, height, targetButton, UIControlState.Disabled, disabledTintColor, ImageButton.Aspect);
            }
        }

        /// <summary>
        /// Properly aligns the title and image on a button to the left.
        /// </summary>
        /// <param name="targetButton">The button to align.</param>
        private static void AlignToLeft(UIButton targetButton)
        {
            targetButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            targetButton.TitleLabel.TextAlignment = UITextAlignment.Left;

            var titleInsets = new UIEdgeInsets(0, CONTROL_PADDING, 0, -1 * CONTROL_PADDING);
            targetButton.TitleEdgeInsets = titleInsets;
        }

        /// <summary>
        /// Properly aligns the title and image on a button to the right.
        /// </summary>
        /// <param name="widthRequest">The requested image width.</param>
        /// <param name="targetButton">The button to align.</param>
        private static void AlignToRight(double widthRequest, UIButton targetButton)
        {
            targetButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
            targetButton.TitleLabel.TextAlignment = UITextAlignment.Right;

            var titleInsets = new UIEdgeInsets(0, 0, 0, (nfloat) widthRequest + CONTROL_PADDING);

            targetButton.TitleEdgeInsets = titleInsets;
            var imageInsets = new UIEdgeInsets(0, (nfloat) widthRequest, 0, -1 * (nfloat) widthRequest);
            targetButton.ImageEdgeInsets = imageInsets;
        }

        /// <summary>
        /// Properly aligns the title and image on a button when the image is over the title.
        /// </summary>
        /// <param name="heightRequest">The requested image height.</param>
        /// <param name="widthRequest">The requested image width.</param>
        /// <param name="targetButton">The button to align.</param>
        /// <param name="buttonWidthRequest">The button width request.</param>
        private static void AlignToTop(double heightRequest, double widthRequest, UIButton targetButton, double buttonWidthRequest)
        {
            targetButton.VerticalAlignment = UIControlContentVerticalAlignment.Top;
            targetButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            targetButton.TitleLabel.TextAlignment = UITextAlignment.Center;
            targetButton.TitleLabel.LineBreakMode = UIKit.UILineBreakMode.WordWrap;

            targetButton.SizeToFit();

            var titleWidth = targetButton.TitleLabel.IntrinsicContentSize.Width;
            CGSize titleSize = targetButton.TitleLabel.Frame.Size;

            UIEdgeInsets titleInsets;
            UIEdgeInsets imageInsets;

            if (UIDevice.CurrentDevice.Model.Contains(IPAD))
            {
                titleInsets = new UIEdgeInsets((nfloat) heightRequest, Convert.ToInt32(-1 * (nfloat) widthRequest / 2), -1 * (nfloat) heightRequest, Convert.ToInt32((nfloat) widthRequest / 2));
                imageInsets = new UIEdgeInsets(0, Convert.ToInt32(titleWidth / 2), 0, -1 * Convert.ToInt32(titleWidth / 2));
            }
            else
            {
                titleInsets = new UIEdgeInsets((nfloat) heightRequest, Convert.ToInt32(-1 * (nfloat) widthRequest / 2), -1 * (nfloat) heightRequest, Convert.ToInt32((nfloat) widthRequest / 2));
                imageInsets = new UIEdgeInsets(0, Convert.ToInt32(targetButton.IntrinsicContentSize.Width / 2 - widthRequest / 2 - titleSize.Width / 2), 0, Convert.ToInt32(-1 * (targetButton.IntrinsicContentSize.Width / 2 - widthRequest / 2 + titleSize.Width / 2)));
            }

            targetButton.TitleEdgeInsets = titleInsets;
            targetButton.ImageEdgeInsets = imageInsets;
        }

        /// <summary>
        /// Properly aligns the title and image on a button when the title is over the image.
        /// </summary>
        /// <param name="heightRequest">The requested image height.</param>
        /// <param name="widthRequest">The requested image width.</param>
        /// <param name="targetButton">The button to align.</param>
        private static void AlignToBottom(double heightRequest, double widthRequest, UIButton targetButton)
        {
            targetButton.VerticalAlignment = UIControlContentVerticalAlignment.Bottom;
            targetButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            targetButton.TitleLabel.TextAlignment = UITextAlignment.Center;
            targetButton.SizeToFit();
            var titleWidth = targetButton.TitleLabel.IntrinsicContentSize.Width;

            UIEdgeInsets titleInsets;
            UIEdgeInsets imageInsets;

            if (UIDevice.CurrentDevice.Model.Contains(IPAD))
            {
                titleInsets = new UIEdgeInsets(-1 * (nfloat) heightRequest, Convert.ToInt32(-1 * (nfloat) widthRequest / 2), (nfloat) heightRequest, Convert.ToInt32((nfloat) widthRequest / 2));
                imageInsets = new UIEdgeInsets(0, titleWidth / 2, 0, -1 * titleWidth / 2);
            }
            else
            {
                titleInsets = new UIEdgeInsets(-1 * (nfloat) heightRequest, -1 * (nfloat) widthRequest, (nfloat) heightRequest, (nfloat) widthRequest);
                imageInsets = new UIEdgeInsets(0, 0, 0, 0);
            }

            targetButton.TitleEdgeInsets = titleInsets;
            targetButton.ImageEdgeInsets = imageInsets;
        }

        /// <summary>
        /// Loads an image from a bundle given the supplied image name, resizes it to the
        /// height and width request and sets it into a <see cref="UIButton" />.
        /// </summary>
        /// <param name="source">The <see cref="ImageSource" /> to load the image from.</param>
        /// <param name="widthRequest">The requested image width.</param>
        /// <param name="heightRequest">The requested image height.</param>
        /// <param name="targetButton">A <see cref="UIButton" /> to set the image into.</param>
        /// <param name="state">The state.</param>
        /// <param name="tintColor">Color of the tint.</param>
        /// <returns>A <see cref="Task" /> for the awaited operation.</returns>
        private static async Task SetImageAsync(ImageSource source, double widthRequest, double heightRequest, UIButton targetButton,
                                                UIControlState state = UIControlState.Normal, UIColor tintColor = null,
                                                Aspect aspect = Aspect.AspectFill)
        {
            var handler = GetHandler(source);
            using (UIImage image = await handler.LoadImageAsync(source))
            {
                UIImage scaled = image;
                var w = widthRequest != DefaultWidth ? widthRequest : image.Size.Width;
                var h = heightRequest != DefaultHeight ? heightRequest : image.Size.Height;
                scaled = scaled.ScaledImage(new CGSize(w, h), aspect);

                if (tintColor != null)
                {
                    targetButton.TintColor = tintColor;
                    targetButton.SetImage(scaled.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), state);
                }
                else
                    targetButton.SetImage(scaled.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), state);
            }
        }

        /// <summary>
        /// Layouts the subviews.
        /// </summary>
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            if (ImageButton.Orientation == ImageOrientation.ImageToRight)
            {
                var imageInsets = new UIEdgeInsets(0, Control.Frame.Size.Width - CONTROL_PADDING - (nfloat) ImageButton.ImageWidthRequest, 0, 0);
                Control.ImageEdgeInsets = imageInsets;
            }
        }

        /// <summary>
        /// Returns the proper <see cref="IImageSourceHandler"/> based on the type of <see cref="ImageSource"/> provided.
        /// </summary>
        /// <param name="source">The <see cref="ImageSource"/> to get the handler for.</param>
        /// <returns>The needed handler.</returns>
        private static IImageSourceHandler GetHandler(ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (source is FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the width based on the requested width, if request less than 0, returns 50.
        /// </summary>
        /// <param name="requestedWidth">The requested width.</param>
        /// <returns>The width to use.</returns>
        private double GetWidth(double requestedWidth)
        {
            return requestedWidth <= 0 ? DefaultWidth : requestedWidth;
        }

        /// <summary>
        /// Gets the height based on the requested height, if request less than 0, returns 50.
        /// </summary>
        /// <param name="requestedHeight">The requested height.</param>
        /// <returns>The height to use.</returns>
        private double GetHeight(double requestedHeight)
        {
           return requestedHeight <= 0 ? DefaultHeight : requestedHeight;
        }
    }


    public static class UIImageHelper
    {

        private static double GetAspectRatio(CGSize size, CGSize otherSize, Aspect aspect)
        {

            var aspectWidth = size.Width / otherSize.Width;
            var aspectHeight = size.Height / otherSize.Height;

            switch (aspect)
            {
                case Aspect.AspectFill:
                    return (double)Math.Max(aspectWidth, aspectHeight);
                case Aspect.AspectFit:
                    return (double)Math.Min(aspectWidth, aspectHeight);
            }

            return (double)Math.Max(aspectWidth, aspectHeight);
        }


        public static UIImage ScaledImage(this UIImage self, CGSize newSize, Aspect aspect = Aspect.AspectFill)
        {
            CGRect scaledImageRect = CGRect.Empty;
            double aspectRatio = GetAspectRatio(newSize, self.Size, aspect);

            scaledImageRect.Size = new CGSize(self.Size.Width * aspectRatio, self.Size.Height * aspectRatio);
            scaledImageRect.X = (newSize.Width - scaledImageRect.Size.Width) / 2.0f;
            scaledImageRect.Y = (newSize.Height - scaledImageRect.Size.Height) / 2.0f;

            UIGraphics.BeginImageContextWithOptions(newSize, false, 0);
            self.Draw(scaledImageRect);

            UIImage scaledImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return scaledImage;
        }
    }
}