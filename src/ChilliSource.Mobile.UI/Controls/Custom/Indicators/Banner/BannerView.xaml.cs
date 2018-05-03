#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Specifies the position of the banner.
    /// </summary>
    public enum BannerPosition
    {
        Top,
        Bottom
    }

    /// <summary>
    /// Android-style toast view
    /// </summary>
    public partial class BannerView : PopupPage
    {
        /// <summary>
        /// Displays a toast-style banner view.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/> that displays the toast view.</returns>
        /// <param name="title">Title.</param>
        /// <param name="titlefont">Titlefont.</param>
        /// <param name="image">Image.</param>
        /// <param name="backgroundColor">Background color.</param>
        /// <param name="navigation">Navigation.</param>
        /// <param name="position">Position.</param>
        public static async Task DisplayToast(string title, ExtendedFont titlefont, ImageSource image, Color backgroundColor, INavigation navigation, BannerPosition position = BannerPosition.Top)
        {
            var view = new BannerView()
            {
                Text = title,
                TitleFont = titlefont,
                Image = image,
                Color = backgroundColor,
                Position = position
            };
            await navigation.PushPopupAsync(view);
            await Task.Delay(2000);
            await navigation.PopPopupAsync();
        }

        /// <summary>
        /// Initializes a new instance of the <c>BannerView</c> class.
        /// </summary>
        public BannerView()
        {
            BindingContext = this;
            Animation = new BannerViewAnimation();
            HasSystemPadding = false;
            CloseWhenBackgroundIsClicked = false;

            //as of version 1.0.4 the property below does not seem to exist anymore
            //IsBackgroundAnimating = true;

            BackgroundColor = Color.Transparent;

            InitializeComponent();
        }


        private ImageSource ImageProperty { get; set; }

        /// <summary>
        /// Gets or sets the source of the banner's image.
        /// </summary>
        /// <value>A <see cref="ImageSource"/> value that represnets the source of the banner's image.</value>
        public ImageSource Image
        {
            get
            {
                return ImageProperty;
            }
            set
            {
                ImageProperty = value;
                OnPropertyChanged("Image");
                OnPropertyChanged("ImageVisible");
            }
        }


        /// <summary>
        /// Gets a value indicating whether the banner's image is visible.
        /// </summary>
        /// <value><c>true</c> if the image is visible; otherwise, <c>false</c>.</value>
        public bool ImageVisible { get { return Image != null; } }


        private Color ColorProperty { get; set; }

        /// <summary>
        /// Gets or sets the background color for the banner.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the background color of the banner view.</value>
        public Color Color
        {
            get
            {
                return ColorProperty;
            }
            set
            {
                ColorProperty = value;
                OnPropertyChanged("Color");
            }
        }


        private string TextProperty { get; set; }

        /// <summary>
        /// Gets or sets the text for the banner.
        /// </summary>
        public string Text
        {
            get
            {
                return TextProperty;
            }
            set
            {
                TextProperty = value;
                OnPropertyChanged("Text");
            }
        }


        private ExtendedFont TitleFontProperty { get; set; }

        /// <summary>
        /// Gets or sets the font for the banner's title.
        /// </summary>
        /// <value>a <see cref="ExtendedFont"/> value that represents the font of the title.</value>
        public ExtendedFont TitleFont
        {
            get
            {
                return TitleFontProperty;
            }
            set
            {
                TitleFontProperty = value;
                OnPropertyChanged("TitleFont");
            }
        }


        private BannerPosition PositionProperty { get; set; }

        /// <summary>
        /// Gets or sets the position of the banner.
        /// </summary>
        /// <value>A <c>BannerPosition</c> value that represents the vertical position of the banner view.</value>
        public BannerPosition Position
        {
            get
            {
                return PositionProperty;
            }
            set
            {
                PositionProperty = value;
                OnPropertyChanged("Position");
                OnPropertyChanged("MainVerticalOption");
            }
        }

        /// <summary>
        /// Gets the vertical layout option for the banner.
        /// </summary>
        /// <value>A <see cref="LayoutOptions"/> value that represents the layout options for the view.</value>
        public LayoutOptions MainVerticalOption
        {
            get { return Position == BannerPosition.Top ? LayoutOptions.Start : LayoutOptions.End; }
        }
    }
}
