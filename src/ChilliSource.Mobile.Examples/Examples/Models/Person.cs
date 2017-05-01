#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
namespace Examples
{
	public class Person
	{
		public string Name { get; private set; }

		public int Age { get; private set; }

		public Person(string name, int age)
		{
			Name = name;
			Age = age;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
