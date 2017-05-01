#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Examples
{
	public partial class PickersExamplePage : BaseContentPage
	{
		public PickersExamplePage()
		{
			BindingContext = this;
			InitializeComponent();
		}

		public DateTime MinDate { get; set; } = DateTime.Now;
		public DateTime MaxDate { get; set; } = DateTime.Now.AddMonths(5);

		public IList<IList<string>> ItemSource
		{
			get
			{
				return
					new List<IList<string>>()
				{new ObservableCollection<string>()
					{
					"Item 1",
											"Item 2",
											"Item 3",
											"Item 4",

					}
				};
			}
		}

		public ObservableCollection<string> SelectedItems
		{
			get
			{
				return new ObservableCollection<string>()
				{
					"Item2"
				};
			}
		}

		public List<string> FixedTextItems
		{
			get; set;
		}
	}
}
