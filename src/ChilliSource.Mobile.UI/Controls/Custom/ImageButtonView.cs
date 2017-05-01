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
	public class ImageButtonView : ContentView
	{
		private Image _image;

		public ImageButtonView()
		{
			_image = new Image();
			_image.HorizontalOptions = LayoutOptions.FillAndExpand;
			_image.VerticalOptions = LayoutOptions.FillAndExpand;
			_image.SetBinding(Image.AspectProperty, "Aspect");
			Content = _image;
		}

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
				if (OnButtonTapped != null)
				{
					OnButtonTapped(this, new EventArgs());
				}
			}
		}

		public event EventHandler OnButtonTapped;

		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ImageButtonView), default(ICommand));

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public static readonly BindableProperty CommandParameterProperty =
			BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ImageButtonView), default(object));

		public object CommandParameter
		{
			get { return (object)GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public static readonly BindableProperty DefaultSourceProperty =
			BindableProperty.Create(nameof(DefaultSource), typeof(string), typeof(ImageButtonView), null);

		public string DefaultSource
		{
			get { return (string)GetValue(DefaultSourceProperty); }
			set { SetValue(DefaultSourceProperty, value); }
		}

		public static readonly BindableProperty PressedSourceProperty =
			BindableProperty.Create(nameof(PressedSource), typeof(string), typeof(ImageButtonView), null);

		public string PressedSource
		{
			get { return (string)GetValue(PressedSourceProperty); }
			set { SetValue(PressedSourceProperty, value); }
		}

		public static readonly BindableProperty DisabledSourceProperty =
			BindableProperty.Create(nameof(DisabledSource), typeof(string), typeof(ImageButtonView), null);

		public string DisabledSource
		{
			get { return (string)GetValue(DisabledSourceProperty); }
			set { SetValue(DisabledSourceProperty, value); }
		}

		public static readonly BindableProperty EnabledProperty =
			BindableProperty.Create(nameof(Enabled), typeof(bool), typeof(ImageButtonView), true);

		public bool Enabled
		{
			get { return (bool)GetValue(EnabledProperty); }
			set { SetValue(EnabledProperty, value); }
		}

		public static readonly BindableProperty AspectProperty =
			BindableProperty.Create(nameof(Aspect), typeof(Aspect), typeof(ImageButtonView), Aspect.AspectFit);

		public Aspect Aspect
		{
			get { return (Aspect)GetValue(AspectProperty); }
			set { SetValue(Image.AspectProperty, value); }
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

