using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    public static class ShadowEffect
    {
        public static readonly BindableProperty ShadowRadiusProperty = BindableProperty.CreateAttached("ShadowRadius",
              typeof(double), typeof(ShadowEffect), 0.0);
        public static readonly BindableProperty HasShadowProperty = BindableProperty.CreateAttached("HasShadow",
               typeof(bool), typeof(ShadowEffect), false, propertyChanged: OnHasShadowChanged);
        public static readonly BindableProperty ShadowOffsetXProperty = BindableProperty.CreateAttached("ShadowOffsetX",
               typeof(double), typeof(ShadowEffect), 0.0);
        public static readonly BindableProperty ShadowOffsetYProperty = BindableProperty.CreateAttached("ShadowOffsetY",
              typeof(double), typeof(ShadowEffect), 0.0);
        public static readonly BindableProperty ShadowColorProperty = BindableProperty.CreateAttached("ShadowColor",
              typeof(Color), typeof(ShadowEffect), Color.Transparent);
        public static readonly BindableProperty ShadowOpacityProperty = BindableProperty.CreateAttached("ShadowOpacity",
              typeof(double), typeof(ShadowEffect), 1.0);
       
        public static readonly BindableProperty RadiusProperty = BindableProperty.CreateAttached("Radius",
                typeof(double), typeof(ShadowEffect), 0.0);
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.CreateAttached("BorderWidth",
                typeof(double), typeof(ShadowEffect), 0.0);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.CreateAttached("BorderColor",
                typeof(Color), typeof(ShadowEffect), Color.Transparent);

        private static void OnHasShadowChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            if (!(bindableObject is View view)) return;
            if (!(newValue is bool hasShadow)) return;
            
            if (hasShadow)
            {
                view.Effects.Add(new SharedShadowEffect());
            }
            else
            {
                var toRemoved = view.Effects.FirstOrDefault(m => m is SharedShadowEffect);
                if (toRemoved != null)
                {
                    view.Effects.Remove(toRemoved);
                }
            }
        }

        public static bool GetHasShadow(Element bindable)
        {
            return (bool) bindable.GetValue(HasShadowProperty);
        }

        public static void SetHasShadow(Element bindable, bool value)
        {
            bindable.SetValue(HasShadowProperty, value);
        }

        public static double GetShadowRadius(Element bindable)
        {
            return (double) bindable.GetValue(ShadowRadiusProperty);
        }

        public static void SetShadowRadius(Element bindable, double value)
        {
            bindable.SetValue(ShadowRadiusProperty, value);
        }

      
        public static double GetShadowOffsetX(Element bindable)
        {
            return (double) bindable.GetValue(ShadowOffsetXProperty);}

        public static void SetShadowOffsetX(Element bindable, double value)
        {
            bindable.SetValue(ShadowOffsetXProperty, value);
        }

        public static double GetShadowOffsetY(Element bindable)
        {
            return (double) bindable.GetValue(ShadowOffsetYProperty);
        }

        public static void SetShadowOffsetY(Element bindable, double value)
        {
            bindable.SetValue(ShadowOffsetYProperty, value);
        }

        public static Color GetShadowColor(Element bindable)
        {
            return (Color) bindable.GetValue(ShadowColorProperty);
        }

        public static void SetShadowColor(Element bindable, Color value)
        {
            bindable.SetValue(ShadowColorProperty, value);
        }

        public static double GetShadowOpacity(Element bindable)
        {
            return (double)bindable.GetValue(ShadowOpacityProperty);
        }

        public static void SetShadowOpacity(Element bindable, double value)
        {
            bindable.SetValue(ShadowOpacityProperty, value);
        }

          public static double GetRadius(Element bindable)
        {
            return (double) bindable.GetValue(RadiusProperty);
        }

        public static void SetRadius(Element bindable, double value)
        {
            bindable.SetValue(RadiusProperty, value);
        }

        public static double GetBorderWidth(Element bindable)
        {
            return (double) bindable.GetValue(BorderWidthProperty);
        }

        public static void SetBorderWidth(Element bindable, double value)
        {
            bindable.SetValue(BorderWidthProperty, value);
        }

        public static Color GetBorderColor(Element bindable)
        {
            return (Color) bindable.GetValue(BorderColorProperty);
        }

        public static void SetBorderColor(Element bindable, Color value)
        {
            bindable.SetValue(BorderColorProperty, value);
        }


        public class SharedShadowEffect : RoutingEffect
        {
            public SharedShadowEffect() : base("ChilliSource.Mobile.UI.ShadowEffect")
            {

            }
        }


    }
}
