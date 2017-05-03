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
using ChilliSource.Mobile.UI;
using System.Linq;

namespace Examples
{
	public class IndexItem : BaseCellViewModel
	{
		public IndexItem(string title, string description, Type pageType = null, SupportedPlatform platforms = SupportedPlatform.NotApplicable)
		{
			Children = new List<IndexItem>();
			Title = title;
			Description = description;
			PageType = pageType;
			SupportedPlatforms = platforms;
		}

		public string Title { get; set; }

		public string Description { get; set; }

		public string LongDescription { get; set; }

		public Type PageType { get; set; }

		public List<IndexItem> Children { get; set; }

		public SupportedPlatform SupportedPlatforms { get; set; }

		public bool ShouldBeShown()
		{
			if (Children.Count > 0)
			{
				return Children.Any((item) => CheckPlatforms(item));
			}

			return CheckPlatforms(this);
		}

		bool CheckPlatforms(IndexItem item)
		{
			switch (item.SupportedPlatforms)
			{
				case SupportedPlatform.Both:
					return true;

				case SupportedPlatform.iOS:
#if __ANDROID__
						return false;
#else
					return true;
#endif

				case SupportedPlatform.Android:
#if __IOS__
					return false;
#else
						return true;
#endif
			}

			return false;
		}
	}
}

