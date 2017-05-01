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
	public static class ContentFactory
	{
		public static List<RootItem> BuildIndexHierarchy()
		{
			List<RootItem> items = new List<RootItem>();

			items.Add(new RootItem("Animations", ThemeManager.AnimationImage, ThemeManager.AnimationPressedImage)
			{
				Children =
				{
					new IndexItem("Custom Animations", "Custom", typeof(AnimationsExamplePage)),
					new IndexItem("Lottie View Animations", "Animations using JSON")
					{
						Children =
						{
							new IndexItem("Example 1", "Playback controls", typeof(LottieAnimationsExamplePage)),
							new IndexItem("Example 2", "Interactive", typeof(LottieAnimationsExamplePage2))
						}
					}
				}
			});

			items.Add(new RootItem("Navigation", ThemeManager.PagesImage, ThemeManager.PagesPressedImage)
			{
				Children =
				{
					new IndexItem("Carousel", "Great for onboarding and fun", typeof(CarouselViewExamplePage), SupportedPlatform.iOS),
					new IndexItem("Transitions", "They look awesome!", typeof(TransitionsExamplePage), SupportedPlatform.iOS)
				}
			});

			items.Add(new RootItem("Interaction", ThemeManager.ControlsImage, ThemeManager.ControlsPressedImage)
			{
				Children =
				{
					new IndexItem("Pickers", "Pick wisely", typeof(PickersExamplePage)),
					new IndexItem("Buttons", "Buttons. Buttons everywhere.", typeof(ExtendedButtonExamplePage), SupportedPlatform.iOS),
					new IndexItem("Inputs", "Fields to input text", typeof(InputsExamplePage)),
					new IndexItem("Keyboard Overlap", "Fix that pesky keyboard!", typeof(KeyboardOverlapExamplePage)),
					new IndexItem("Miscellaneous", "Random things go here", typeof(MiscellaneousExamplePage))
				}
			});

			items.Add(new RootItem("Presentation", ThemeManager.BehaviorImage, ThemeManager.BehaviorPressedImage)
			{
				Children =
				{
					new IndexItem("Shape", "Time to shape up, LOL", typeof(ShapeExamplePage)),
					new IndexItem("Images", "Blurry or Circled...", typeof(ImagesExamplePage)),
					new IndexItem("Advanced Action Sheet", "Fancy hey?", typeof(AdvancedActionSheetExamplePage)),
					new IndexItem("Advanced Alert View", "Fancy hey X 2?", typeof(AdvancedAlertViewExamplePage)),
					new IndexItem("Indicators", "Hey!", typeof(IndicatorsExamplePage)),
					new IndexItem("Paging Behavior", "Infinite scrolling listview", typeof(PagingBehaviorExamplePage)),
					new IndexItem("Styled Navigation Bar", "Style that nav bar!", typeof(StyledNavigationBarExamplePage)),
					new IndexItem("Styled Tab Page", "Style that tab page!", typeof(StyledTabExamplePage)),
				}
			});

			items.Add(new RootItem("Security", ThemeManager.SecurityImage, ThemeManager.SecurityPressedImage)
			{
				Children =
				{
					new IndexItem("Hashing", "Hash away with Sha1!", typeof(HashingExamplePage))
				}
			});

			items.Add(new RootItem("Design Patterns", ThemeManager.DesignPatternsImage, ThemeManager.DesignPatternsPressedImage)
			{
				Children =
				{
					new IndexItem("MVVM", "a pattern and a palindrome!")
					{
						Children =
						{
							new IndexItem("EventToCommand", "a way to bind any time of event to a command", typeof(EventToCommandPage))
						}
					}
				}
			});

			items.Add(new RootItem("Media", ThemeManager.MediaImage, ThemeManager.MediaPressedImage)
			{
				Children =
				{
					new IndexItem("Video", "Radio star watch out!", typeof(VideoServiceExamplePage)),
					new IndexItem("Audio", "recording and playback", typeof(AudioRecordingExamplePage)),
					new IndexItem("Media Library", "", typeof(MediaLibraryExamplePage), SupportedPlatform.iOS)
				}
			});

			items.Add(new RootItem("Location", ThemeManager.LocationImage, ThemeManager.LocationPressedImage)
			{
				Children =
				{
					new IndexItem("Location Service", "", typeof(LocationServiceExamplePage)),
					new IndexItem("Google", "'_'")
					{
						Children =
						{
							new IndexItem("Places", "Find any address...", typeof(PlacesServiceExamplePage)),
							new IndexItem("Directions", "Lost?", typeof(DirectionsExamplePage))
						}
					}
				}
			});

			return items;
		}
	}
}
