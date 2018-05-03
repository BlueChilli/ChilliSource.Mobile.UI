using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    [Flags]
    public enum RoundedCornerPosition
    {
        None = 0,
        AllCorners = 2,
        TopLeft = 4,
        TopRight = 8,
        BottomLeft = 16,
        BottomRight = 32
    }

    public static class RoundedCornerEffect
    {
        public static readonly BindableProperty HasRoundedCornerProperty = BindableProperty.CreateAttached("HasRoundedCorner",
                typeof(bool), typeof(RoundedCornerEffect), false, propertyChanged: OnHasRoundedCornerChanged );

       
        private static void OnHasRoundedCornerChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            if (!(bindableObject is View view)) return;
            if (!(newValue is bool hasRoundedCorner)) return;
            
            if (hasRoundedCorner)
            {
                view.Effects.Add(new SharedRoundedCornerEffect());
            }
            else
            {
                var toRemoved = view.Effects.FirstOrDefault(m => m is SharedRoundedCornerEffect);
                if (toRemoved != null)
                {
                    view.Effects.Remove(toRemoved);
                }
            }
        }

        public static readonly BindableProperty RadiusProperty = BindableProperty.CreateAttached("Radius",
                typeof(double), typeof(RoundedCornerEffect), 0.0);
        public static readonly BindableProperty RoundedCornerPositionProperty = BindableProperty.CreateAttached("Position",
                typeof(RoundedCornerPosition), typeof(RoundedCornerEffect), RoundedCornerPosition.None);
        public static readonly BindableProperty BorderWidthProperty = BindableProperty.CreateAttached("BorderWidth",
                typeof(double), typeof(RoundedCornerEffect), 0.0);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.CreateAttached("BorderColor",
                typeof(Color), typeof(RoundedCornerEffect), Color.Transparent);
      

        public static bool GetHasRoundedCorner(Element bindable)
        {
            return (bool) bindable.GetValue(HasRoundedCornerProperty);
        }

        public static void SetHasRoundedCorner(Element bindable, bool value)
        {
            bindable.SetValue(HasRoundedCornerProperty, value);
        }

        public static double GetRadius(Element bindable)
        {
            return (double) bindable.GetValue(RadiusProperty);
        }

        public static void SetRadius(Element bindable, double value)
        {
            bindable.SetValue(RadiusProperty, value);
        }

        public static RoundedCornerPosition GetRoundedCornerPosition(Element bindable)
        {
            return (RoundedCornerPosition) bindable.GetValue(RoundedCornerPositionProperty);
        }

        public static void SetRoundedCornerPosition(Element bindable, RoundedCornerPosition value)
        {
            bindable.SetValue(RoundedCornerPositionProperty, value);
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


        #region shadowProperties
      

        #endregion
       
        

        public static bool HasTopLeft(Element bindable) => GetRoundedCornerPosition(bindable).HasFlag(RoundedCornerPosition.TopLeft);
        public static bool HasTopRight(Element bindable) => GetRoundedCornerPosition(bindable).HasFlag(RoundedCornerPosition.TopRight);
        public static bool HasBottomRight(Element bindable) => GetRoundedCornerPosition(bindable).HasFlag(RoundedCornerPosition.BottomLeft);
        public static bool HasBottomLeft(Element bindable) => GetRoundedCornerPosition(bindable).HasFlag(RoundedCornerPosition.BottomRight);


        public class SharedRoundedCornerEffect : RoutingEffect
        {
            public SharedRoundedCornerEffect() : base("ChilliSource.Mobile.UI.RoundedCornerEffect")
            {

            }
        }
    }

   
}
