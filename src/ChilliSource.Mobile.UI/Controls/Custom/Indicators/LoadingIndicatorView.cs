#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class LoadingIndicatorView : StackLayout
	{
		private ActivityIndicator _activityIndicator;
		private ExtendedLabel _ExtendedLabel;

		public LoadingIndicatorView()
		{

			_activityIndicator = new ActivityIndicator
			{
				IsRunning = true,
			};

			_ExtendedLabel = new ExtendedLabel
			{
				VerticalTextAlignment = TextAlignment.Center,
				Text = "Loading..."
			};

			HorizontalOptions = LayoutOptions.Center;
			VerticalOptions = LayoutOptions.Center;

#if __IOS__

			var layout = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 10,
				Children = { _activityIndicator, _ExtendedLabel }
			};

			Children.Add(layout);

#elif __ANDROID__

			Children.Add(_activityIndicator);
			Children.Add(_ExtendedLabel);

#endif
		}

		public static readonly BindableProperty CustomFontProperty =
			BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(LoadingIndicatorView), null);


		public ExtendedFont CustomFont
		{
			get { return (ExtendedFont)GetValue(CustomFontProperty); }
			set { SetValue(CustomFontProperty, value); }
		}

		protected override void OnPropertyChanged(string propertyName)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == nameof(CustomFont))
			{
				_ExtendedLabel.CustomFont = CustomFont;
				_activityIndicator.Color = CustomFont.Color;
			}
		}
	}
}

