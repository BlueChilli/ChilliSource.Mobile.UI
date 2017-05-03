#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;

namespace Examples
{
	public static class ThemeManager
	{
		public static Color GrayColor = Color.FromRgb(197, 198, 198);
        public static Color GreyishBrownColor = Color.FromRgb(74, 74, 47);
		public static Color OrangePink = Color.FromRgb(255, 113, 87);
		public static Color LightGreyColor = Color.FromRgb(216, 216, 216);
		public static Color IntroTitleColor = Color.FromRgb(70, 209, 194);
		public static Color DarkColor = Color.FromRgb(48, 48, 48);
		public static Color TealColor = Color.FromRgb(0, 209, 194);

		public static ExtendedFont CellTitleFont = new ExtendedFont(size: 17, color: GreyishBrownColor, kerning: -0.1f);
		public static ExtendedFont CellTitleSelectedFont = new ExtendedFont(size: 17, color: Color.White, kerning: -0.1f);

		public static ExtendedFont CellSubtitleFont = new ExtendedFont(size: 12, color: GrayColor);
		public static ExtendedFont CellSubtitleFontOrange = new ExtendedFont(size: 12, color: OrangePink);

		public static ExtendedFont AdvancedActionSheetTitleFont = new ExtendedFont(size: 18, color: Color.Orange);
		public static ExtendedFont AdvancedActionSheetCancelFont = new ExtendedFont(size: 18, color: Color.Red);

		public static ExtendedFont ButtonNormalFont = new ExtendedFont(size: 12, color: Color.White);

		public static ExtendedFont IntroHeaderFont = new ExtendedFont(size: 28, color: GreyishBrownColor, fontAttributes: FontAttributes.Bold);
		public static ExtendedFont IntroContentFont = new ExtendedFont(size: 20, color: GreyishBrownColor);

		public static ExtendedFont NavigationTitleFont = new ExtendedFont(size: 17, color: GreyishBrownColor, fontAttributes: FontAttributes.Bold);

		public static ExtendedFont NavigationToolBarFont = new ExtendedFont(size: 17, color: OrangePink, fontAttributes: FontAttributes.Bold);

		public static ExtendedFont SegmentedControlFont = new ExtendedFont(size: 17, color: DarkColor, kerning: -0.1f);
		public static ExtendedFont SegmentedControlSelectedFont = new ExtendedFont(size: 17, color: TealColor, kerning: -0.1f);

#if __IOS__


		public const string AnimationImage = @"Images/Index/Animation";
		public const string AnimationPressedImage = @"Images/Index/Animation reversed";
		public const string BehaviorImage = @"Images/Index/Behaviours";
		public const string BehaviorPressedImage = @"Images/Index/Behaviours reversed";
		public const string ControlsImage = @"Images/Index/Controls";
		public const string ControlsPressedImage = @"Images/Index/Controls reversed";
		public const string LocationImage = @"Images/Index/Services";
		public const string LocationPressedImage = @"Images/Index/Services reversed";
		public const string MediaImage = @"Images/Index/Media";
		public const string MediaPressedImage = @"Images/Index/Media reversed";
		public const string DesignPatternsImage = @"Images/Index/DesPatterns";
		public const string DesignPatternsPressedImage = @"Images/Index/DesPatterns reversed";
		public const string PagesImage = @"Images/Index/Pages";
		public const string PagesPressedImage = @"Images/Index/Pages reversed";
		public const string SecurityImage = @"Images/Index/Security";
		public const string SecurityPressedImage = @"Images/Index/Security reversed";

		public const string SearchImage = @"Images/Misc/Search";
		public const string BackImage = @"Images/Navigation/Back";
		public const string HamburgerMenuImage = @"Images/Navigation/HamburgerMenu";

		public const string SplashScreenImage = @"Images/Misc/Splashscreen";

#else
		public const string AnimationImage = "animation";
		public const string AnimationPressedImage = "animation_reversed";
		public const string BehaviorImage = "behaviours";
		public const string BehaviorPressedImage = "behaviours_reversed";
		public const string ControlsImage = "controls";
		public const string ControlsPressedImage = "controls_reversed";
		public const string LocationImage = "services";
		public const string LocationPressedImage = "services_reversed";
		public const string MediaImage = "media";
		public const string MediaPressedImage = "media_reversed";
		public const string DesignPatternsImage = "despatterns";
		public const string DesignPatternsPressedImage = "despatterns_reversed";
		public const string PagesImage = "pages";
		public const string PagesPressedImage = "pages_reversed";
		public const string SecurityImage = "security";
		public const string SecurityPressedImage = "security_reversed";

		public const string SearchImage = "search_icon";
		public const string BackImage = "back_chevron";
		public const string HamburgerMenuImage = "hamburger_menu";

		public const string SplashScreenImage = "splash_screen";
#endif

	}
}
