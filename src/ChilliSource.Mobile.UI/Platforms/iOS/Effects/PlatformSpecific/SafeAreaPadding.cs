using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace ChilliSource.Mobile.UI.PlatformConfiguration.iOS
{
    public static class SafeAreaPadding
    {
        const string EffectName = "ChilliSource.SafeAreaPaddingEffect";

        public static readonly BindableProperty ShouldIncludeStatusBar = BindableProperty.CreateAttached("ShouldIncludeStatusBar",
            typeof(bool), typeof(SafeAreaPadding), false);

        public static readonly BindableProperty EnableSafeAreaPadding = BindableProperty.CreateAttached("EnableSafeAreaPadding",
           typeof(bool), typeof(SafeAreaPadding), false, propertyChanged: OnEnableSafeAreaPadding);

        public static bool GetShouldIncludeStatusBar(BindableObject element)
        {
            return (bool) element.GetValue(ShouldIncludeStatusBar);
        }

        public static void SetEnableSafeAreaPadding(BindableObject element, bool value)
        {
            element.SetValue(EnableSafeAreaPadding, value);
        }

        public static bool GetEnableSafeAreaPadding(BindableObject element)
        {
            return (bool)element.GetValue(EnableSafeAreaPadding);
        }

        public static void SetShouldIncludeStatusBar(BindableObject element, bool value)
        {
            element.SetValue(ShouldIncludeStatusBar, value);
        }

        private static void OnEnableSafeAreaPadding(BindableObject bindable, object oldValue, object newValue)
        {
            if(newValue is bool isEnable && isEnable)
            {
                AttachEffect(bindable);
            }
            else
            {
                DetachEffect(bindable);
            }
        }

        static void AttachEffect(BindableObject element)
        {
            if(!(element is IElementController controller) || controller == null || controller.EffectIsAttached(EffectName))
            {
                return;
            }

            if (element is Element elm)
            {
                elm.Effects.Add(new SafeAreaPaddingEffect(GetShouldIncludeStatusBar(element)));
            }

        }

        static void DetachEffect(BindableObject element)
        {
            if (!(element is IElementController controller) || controller == null || !controller.EffectIsAttached(EffectName))
            {
                return;
            }

            if (element is Element elm)
            {
                var effect = new SafeAreaPaddingEffect(GetShouldIncludeStatusBar(element));
                var toRemove = elm.Effects.FirstOrDefault(e => e.ResolveId == effect.ResolveId);
                if (toRemove != null)
                {
                    elm.Effects.Remove(toRemove);
                }
            }
        }

     
        public static IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> UseSafeAreaPadding(this IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> config, bool includeStatusBar)
        {
            SetShouldIncludeStatusBar(config.Element, includeStatusBar);
            SetEnableSafeAreaPadding(config.Element, true);
            return config;
        }
    }
}