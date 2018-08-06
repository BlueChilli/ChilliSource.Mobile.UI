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

        public static readonly BindableProperty SafeAreaInsets = BindableProperty.CreateAttached("SafeAreaInsets",
           typeof(Thickness), typeof(SafeAreaPadding), new Thickness(0, 0, 0, 0), propertyChanged: OnSetSafeAreaPadding);

        private static void OnSetSafeAreaPadding(BindableObject bindable, object oldValue, object newValue)
        {
           if(GetEnableSafeAreaPadding(bindable))
            {
                var old = (Thickness)oldValue;
                var newV = (Thickness)newValue;
                if(old != newV)
                {
                    if (bindable is Element elm)
                    {
                       var effect = (SafeAreaPaddingEffect) elm.Effects.FirstOrDefault(m => m.ResolveId == EffectName);
                       effect?.SetSafeAreaPadding(elm, newV);
                    }
                }
            }
        }

        public static bool GetShouldIncludeStatusBar(BindableObject element)
        {
            return (bool) element.GetValue(ShouldIncludeStatusBar);
        }

        public static void SetEnableSafeAreaPadding(BindableObject element, bool value)
        {
            element.SetValue(EnableSafeAreaPadding, value);
        }

        public static Thickness GetSafeAreaInsets(BindableObject element)
        {
            return (Thickness)element.GetValue(SafeAreaInsets);
        }

        public static void SetSafeAreaInsets(BindableObject element, Thickness value)
        {
            element.SetValue(SafeAreaInsets, value);
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

        public static IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> UseSafeAreaPaddingWithInset(this IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> config, bool includeStatusBar, Thickness padding)
        {
            SetShouldIncludeStatusBar(config.Element, includeStatusBar);
            SetEnableSafeAreaPadding(config.Element, true);
            SetSafeAreaInsets(config.Element, padding);
            return config;
        }

        public static IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> SetSafeAreaPadding(this IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> config, Thickness padding)
        {
            if(GetEnableSafeAreaPadding(config.Element))
            {
                SetSafeAreaInsets(config.Element, padding);
            }

            return config;
        }

        public static Thickness GetSafeAreaPadding(this IPlatformElementConfiguration<Xamarin.Forms.PlatformConfiguration.iOS, Element> config)
        {
            if (GetEnableSafeAreaPadding(config.Element) && config.Element is Layout layout)
            {
                return layout.Padding;
            }

            return new Thickness();
        }
    }
}