#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion


namespace Examples
{
	public partial class ShapeExamplePage : BaseContentPage
	{

		int _borderWidth;

		public ShapeExamplePage()
		{
			_borderWidth = 5;
			BindingContext = this;
			InitializeComponent();
		}


		public int BorderWidth
		{
			get
			{
				return _borderWidth;
			}

			set
			{
				_borderWidth = value;
				OnPropertyChanged();
			}
		}

		void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
		{
			BorderWidth = (int)(e.NewValue * 10);
		}
	}
}
