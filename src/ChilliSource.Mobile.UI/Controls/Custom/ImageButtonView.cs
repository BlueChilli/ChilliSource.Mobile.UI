#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms;
using System.Windows.Input;
using System;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Control that creates an image that acts like a button
    /// </summary>
    public class ImageButtonView : ContentView
    {
        private Image _image;

        /// <summary>
        /// Initializes a new instance of the <c>ImageButtonView</c> class.
        /// </summary>
        public ImageButtonView()
        {
            _image = new Image();
            _image.HorizontalOptions = LayoutOptions.FillAndExpand;
            _image.VerticalOptions = LayoutOptions.FillAndExpand;
            _image.SetBinding(Image.AspectProperty, "Aspect");
            Content = _image;
        }


        /// <summary>
        /// Backing store for the <c>Command</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ImageButtonView), default(ICommand));

        /// <summary>
        /// Gets or sets the command to invoke when the image button is activated. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ICommand"/> value that represents the command invoked whenever the image button is tapped.</value>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>CommandParameter</c> bindable property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ImageButtonView), default(object));

        /// <summary>
        /// Gets or sets the parameter of the command. This is a bindable property.
        /// </summary>
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>DefaultSource</c> bindable property.
        /// </summary>
        public static readonly BindableProperty DefaultSourceProperty =
            BindableProperty.Create(nameof(DefaultSource), typeof(string), typeof(ImageButtonView), null);

        /// <summary>
        /// Gets or sets the default source of the image for the image button. This is a bindable property.
        /// </summary>
        public string DefaultSource
        {
            get { return (string)GetValue(DefaultSourceProperty); }
            set { SetValue(DefaultSourceProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>PressedSource</c> bindable property.
        /// </summary>
        public static readonly BindableProperty PressedSourceProperty =
            BindableProperty.Create(nameof(PressedSource), typeof(string), typeof(ImageButtonView), null);

        /// <summary>
        /// Gets or sets the image source of the button when it is pressed. This is a bindable property.
        /// </summary>
        public string PressedSource
        {
            get { return (string)GetValue(PressedSourceProperty); }
            set { SetValue(PressedSourceProperty, value); }
        }


        /// <summary>
        ///  Backing store for the <c>DisabledSource</c> bindable property.
        /// </summary>
        public static readonly BindableProperty DisabledSourceProperty =
            BindableProperty.Create(nameof(DisabledSource), typeof(string), typeof(ImageButtonView), null);

        /// <summary>
        ///  Gets or sets the image source of the button when it is disabled. This is a bindable property.
        /// </summary>
        public string DisabledSource
        {
            get { return (string)GetValue(DisabledSourceProperty); }
            set { SetValue(DisabledSourceProperty, value); }
        }


        /// <summary>
        /// Identifies the <c>Enabled</c> bindable property.
        /// </summary>
        public static readonly BindableProperty EnabledProperty =
            BindableProperty.Create(nameof(Enabled), typeof(bool), typeof(ImageButtonView), true);

        /// <summary>
        /// Gets or sets a value indicating whether the image button is enabled.
        /// </summary>
        /// <value><c>true</c> if is enabled; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool Enabled
        {
            get { return (bool)GetValue(EnabledProperty); }
            set { SetValue(EnabledProperty, value); }
        }


        /// <summary>
        /// Identifies the <c>Aspect</c> bindable property.
        /// </summary>
        public static readonly BindableProperty AspectProperty =
            BindableProperty.Create(nameof(Aspect), typeof(Aspect), typeof(ImageButtonView), Aspect.AspectFit);

        /// <summary>
        /// Gets or sets the scaling mode for the image. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Xamarin.Forms.Aspect"/> value that represents the scale mode of the image.
        /// The default value is <see cref="Xamarin.Forms.Aspect.AspectFit"/>.</value>
        public Aspect Aspect
        {
            get { return (Aspect)GetValue(AspectProperty); }
            set { SetValue(Image.AspectProperty, value); }
        }



        /// <summary>
        /// Occurs when the image button is tapped.
        /// </summary>
        public event EventHandler OnButtonTapped;

        /// <summary>
        /// A method that raises the <c>OnButtonTapped</c> event when the image button is pressed.
        /// </summary>
        /// <param name="pressed">If set to <c>true</c> the image button is pressed.</param>
        public void OnPressed(bool pressed)
        {
            if (!Enabled)
            {
                return;
            }

            if (pressed)
            {
                if (PressedSource == null)
                {
                    Opacity = 0.5;
                    return;
                }
                _image.Source = PressedSource;
            }
            else
            {
                if (PressedSource == null)
                {
                    this.FadeTo(1);
                }
                _image.Source = DefaultSource;

                if (this.Command != null)
                {
                    this.Command.Execute(CommandParameter);


                }
                OnButtonTapped?.Invoke(this, new EventArgs());
            }
        }


        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Enabled))
            {
                if (Enabled)
                {
                    _image.Source = DefaultSource;
                }
                else
                {
                    _image.Source = DisabledSource;
                }
            }

            if (propertyName == nameof(DefaultSource) && Enabled)
            {
                _image.Source = DefaultSource;
            }

            if (propertyName == nameof(DisabledSource) && !Enabled)
            {
                _image.Source = DisabledSource;
            }

            if (propertyName == nameof(Aspect))
            {
                _image.Aspect = Aspect;
            }
        }
    }
}

