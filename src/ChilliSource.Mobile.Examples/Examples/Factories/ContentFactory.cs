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

			//Animatios:
			var Animation = new RootItem("Animations", ThemeManager.AnimationImage, ThemeManager.AnimationPressedImage);
			var AnimationsItem1 = new IndexItem("Custom Animations", "Custom Bounce, Fade, Quick Entrance , Rotation, Scale, Shake and Vibrate Animation", typeof(AnimationsExamplePage));
			var AnimationsItem2 = new IndexItem("Lottie View Animations", "Animations using JSON");
			var LottieViewAnimationsItem1 = new IndexItem("Playback Controls", "Animation with playback controls", typeof(LottieAnimationsExamplePage));
			var LottieViewAnimationItem2 = new IndexItem("User Interactivity", "Animation with user interactivity", typeof(LottieAnimationsExamplePage2));

			AnimationsItem1.LongDescription = "Long Desc1";
			LottieViewAnimationsItem1.LongDescription = "Long Desc2";
			LottieViewAnimationItem2.LongDescription = "Long Desc3";

			items.Add(Animation);
			Animation.Children.Add(AnimationsItem1);
			Animation.Children.Add(AnimationsItem2);
			AnimationsItem2.Children.Add(LottieViewAnimationsItem1);
			AnimationsItem2.Children.Add(LottieViewAnimationItem2);

			//Navigation:
			var Navigation = new RootItem("Navigation", ThemeManager.PagesImage, ThemeManager.PagesPressedImage);
			var NavigationItem1 = new IndexItem("Carousel View", "Navigate between pages using a swipe gesture", typeof(CarouselViewExamplePage), SupportedPlatform.iOS);
			var NavigationItem2 = new IndexItem("Transitions", "Page transition with animation", typeof(TransitionsExamplePage), SupportedPlatform.iOS);

			NavigationItem1.LongDescription = "Long Desc4";
			NavigationItem2.LongDescription = "Long Desc5";

			items.Add(Navigation);
			Navigation.Children.Add(NavigationItem1);
			Navigation.Children.Add(NavigationItem2);

			//Interaction:
			var Interaction = new RootItem("Interaction", ThemeManager.ControlsImage, ThemeManager.ControlsPressedImage);
			var InteractionItem1 = new IndexItem("Pickers", "Pick wisely", typeof(PickersExamplePage));
			var InteractionItem2 = new IndexItem("Buttons", "Buttons. Buttons everywhere.", typeof(ExtendedButtonExamplePage), SupportedPlatform.iOS);
			var InteractionItem3 = new IndexItem("Inputs", "Fields to input text", typeof(InputsExamplePage));
			var InteractionItem4 = new IndexItem("Keyboard Overlap", "Fix that pesky keyboard!", typeof(KeyboardOverlapExamplePage));
			var InteractionItem5 = new IndexItem("Miscellaneous", "Random things go here", typeof(MiscellaneousExamplePage));

			InteractionItem1.LongDescription = "Long Desc6";
			InteractionItem2.LongDescription = "Long Desc7";
			InteractionItem3.LongDescription = "Long Desc8";
			InteractionItem4.LongDescription = "Long Desc9";
			InteractionItem5.LongDescription = "Long Desc10";

			items.Add(Interaction);
			Interaction.Children.Add(InteractionItem1);
			Interaction.Children.Add(InteractionItem2);
			Interaction.Children.Add(InteractionItem3);
			Interaction.Children.Add(InteractionItem4);
			Interaction.Children.Add(InteractionItem5);

			//Presentation:
			var Presentation = new RootItem("Presentation", ThemeManager.BehaviorImage, ThemeManager.BehaviorPressedImage);
			var PresentationItem1 = new IndexItem("Shape", "Time to shape up, LOL", typeof(ShapeExamplePage));
			var PresentationItem2 = new IndexItem("Images", "Blurry or Circled...", typeof(ImagesExamplePage));
			var PresentationItem3 = new IndexItem("Advanced Action Sheet", "Fancy hey?", typeof(AdvancedActionSheetExamplePage));
			var PresentationItem4 = new IndexItem("Advanced Alert View", "Fancy hey X 2?", typeof(AdvancedAlertViewExamplePage));
			var PresentationItem5 = new IndexItem("Indicators", "Hey!", typeof(IndicatorsExamplePage));
			var PresentationItem6 = new IndexItem("Paging Behavior", "Infinite scrolling listview", typeof(PagingBehaviorExamplePage));
			var PresentationItem7 = new IndexItem("Styled Navigation Bar", "Style that nav bar!", typeof(StyledNavigationBarExamplePage));
			var PresentationItem8 = new IndexItem("Styled Tab Page", "Style that tab page!", typeof(StyledTabExamplePage));

			PresentationItem1.LongDescription = "Long Desc11";
			PresentationItem2.LongDescription = "Long Desc12";
			PresentationItem3.LongDescription = "Long Desc13";
			PresentationItem4.LongDescription = "Long Desc14";
			PresentationItem5.LongDescription = "Long Desc15";
			PresentationItem6.LongDescription = "Long Desc16";
			PresentationItem7.LongDescription = "Long Desc17";
			PresentationItem8.LongDescription = "Long Desc18";

			items.Add(Presentation);
			Presentation.Children.Add(PresentationItem1);
			Presentation.Children.Add(PresentationItem2);
			Presentation.Children.Add(PresentationItem3);
			Presentation.Children.Add(PresentationItem4);
			Presentation.Children.Add(PresentationItem5);
			Presentation.Children.Add(PresentationItem6);
			Presentation.Children.Add(PresentationItem7);
			Presentation.Children.Add(PresentationItem8);

			//Security:
			var SecurityItem = new RootItem("Security", ThemeManager.SecurityImage, ThemeManager.SecurityPressedImage);
			var SecurityItem1 = new IndexItem("Hashing", "Hash away with Sha1!", typeof(HashingExamplePage));

			SecurityItem1.LongDescription = "Long Desc19";

			items.Add(SecurityItem);
			SecurityItem.Children.Add(SecurityItem1);

			//Design Patterns:
			var DesignPatterns = new RootItem("Design Patterns", ThemeManager.DesignPatternsImage, ThemeManager.DesignPatternsPressedImage);
			var DesignPatternsItem1 = new IndexItem("MVVM", "a pattern and a palindrome!");
			var MVVMItem1 = new IndexItem("EventToCommand", "a way to bind any time of event to a command", typeof(EventToCommandPage));

			MVVMItem1.LongDescription = "Long Desc20";

			items.Add(DesignPatterns);
			DesignPatterns.Children.Add(DesignPatternsItem1);
			DesignPatternsItem1.Children.Add(MVVMItem1);

			//Media:
			var Media = new RootItem("Media", ThemeManager.MediaImage, ThemeManager.MediaPressedImage);
			var MediaItem1 = new IndexItem("Video", "Radio star watch out!", typeof(VideoServiceExamplePage));
			var MediaItem2 = new IndexItem("Audio", "recording and playback", typeof(AudioRecordingExamplePage));
			var MediaItem3 = new IndexItem("Media Library", "", typeof(MediaLibraryExamplePage), SupportedPlatform.iOS);

			MediaItem1.LongDescription = "Long Desc21";
			MediaItem2.LongDescription = "Long Desc22";
			MediaItem3.LongDescription = "Long Desc23";

			items.Add(Media);
			Media.Children.Add(MediaItem1);
			Media.Children.Add(MediaItem2);
			Media.Children.Add(MediaItem3);

			//Location:
			var Location = new RootItem("Location", ThemeManager.LocationImage, ThemeManager.LocationPressedImage);
			var LocationItem1 = new IndexItem("Location Service", "", typeof(LocationServiceExamplePage));
			var LocationItem2 = new IndexItem("Google", "'_'");
			var GoogleItem1 = new IndexItem("Places", "Find any address...", typeof(PlacesServiceExamplePage));
			var GoogleItem2 = new IndexItem("Directions", "Lost?", typeof(DirectionsExamplePage));

			LocationItem1.LongDescription = "Long Desc24";
			GoogleItem1.LongDescription = "Long Desc25";
			GoogleItem2.LongDescription = "long Desc26";

			items.Add(Location);
			Location.Children.Add(LocationItem1);
			Location.Children.Add(LocationItem2);
			LocationItem2.Children.Add(GoogleItem1);
			LocationItem2.Children.Add(GoogleItem2);


			return items;
		}
	}
}
