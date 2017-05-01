#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Examples
{
	public partial class LottieAnimationsExamplePage2 : BaseContentPage
	{
		public LottieAnimationsExamplePage2()
		{
			BindingContext = this;
			InitializeComponent();
		}

		public float SliderValue
		{
			get
			{
				return (anim != null) ? anim.Animation.Progress : 0;
			}

			set
			{
				if (anim != null)
				{
					anim.Animation.Progress = value;
				}
			}
		}

		void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
		{
			OnPropertyChanged(nameof(SliderValue));
		}
	}
}
