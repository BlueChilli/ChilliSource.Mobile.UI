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

namespace Examples
{
	public partial class TransitionPage : BaseContentPage
	{
		public TransitionPage()
		{
			ToolbarItems.Remove(ToolbarItems[1]);
			InitializeComponent();
		}

		public PageTransitionType OriginTransitionType
		{
			set
			{
				CreateBackCommand(value);
			}
		}

		void CreateBackCommand(PageTransitionType type)
		{
			TransitionOptions options = new TransitionOptions()
			{
				ApplyPopSwipeTransitionToOrigin = true,
				PopSwipeTransitionDirection = SwipeDirection.BottomToTop
			};

			switch (type)
			{
				case PageTransitionType.DoorSlide:
				case PageTransitionType.Checkerboard:
				case PageTransitionType.Fade:
				case PageTransitionType.ZoomFade:

					BackCommand = new Command(async () =>
					{
						await Navigation.PopAsyncWithTransition(type, options);
					});
					break;

				case PageTransitionType.Swipe:

					BackCommand = new Command(async () =>
					{
						await Navigation.PopModalAsyncWithTransition(type, options);
					});
					break;
			}
		}

		void BackButtonClicked(object sender, System.EventArgs e)
		{
			BackCommand.Execute(null);
		}
	}
}
