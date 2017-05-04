#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Examples
{
	public class EventToCommandPageViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Person> People { get; private set; }

		public ICommand HandleItemSelectedCommand { get; private set; }

		public string SelectedItemText { get; private set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public EventToCommandPageViewModel()
		{
			People = new ObservableCollection<Person> {
				new Person ("Steve", 21),
				new Person ("John", 37),
				new Person ("Mary", 42),
				new Person ("Lucas", 29),
				new Person ("Bob", 39),
				new Person ("Jane", 30)
			};
			HandleItemSelectedCommand = new Command<Person>(OutputAge);
		}

		void OutputAge(Person person)
		{
			SelectedItemText = string.Format("{0} is {1} years old.", person.Name, person.Age);
			OnPropertyChanged(nameof(SelectedItemText));
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var changed = PropertyChanged;
			if (changed != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
