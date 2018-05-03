using System;
using System.Linq;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Effect that adds a customizable line to a <see cref="FloatingLabelEntry"/>
    /// </summary>
    public class FloatingLabelEntryLineEffect : RoutingEffect
    {
        public FloatingLabelEntryLineEffect() : this("ChilliSource.Mobile.UI.FloatingLabelEntryLineEffect")
        {

        }

        public FloatingLabelEntryLineEffect(string effectId) : base(effectId)
        {

        }

        #region Attached Properties

        public static readonly BindableProperty HasLineProperty =
           BindableProperty.CreateAttached("HasLine", typeof(bool), typeof(FloatingLabelEntryLineEffect), false, propertyChanged: OnShowLineChanged);

        public static readonly BindableProperty LineColorProperty =
            BindableProperty.CreateAttached("LineColor", typeof(Color), typeof(FloatingLabelEntryLineEffect), Color.Default);

        public static readonly BindableProperty LineHeightProperty =
            BindableProperty.CreateAttached("LineHeight", typeof(float), typeof(FloatingLabelEntryLineEffect), 3.0f);

        public static Color GetLineColor(BindableObject view)
        {
            return (Color)view.GetValue(LineColorProperty);
        }

        public static void SetLineColor(BindableObject view, Color value)
        {
            view.SetValue(LineColorProperty, value);
        }

        public static void SetHasLine(BindableObject view, bool value)
        {
            view.SetValue(HasLineProperty, value);
        }

        public static bool GetHasLine(BindableObject view)
        {
            return (bool)view.GetValue(HasLineProperty);
        }

        public static float GetLineHeight(BindableObject view)
        {
            return (float)view.GetValue(LineHeightProperty);
        }

        public static void SetLineHeight(BindableObject view, float value)
        {
            view.SetValue(LineHeightProperty, value);
        }

        #endregion

        //Handle Show line value change
        static void OnShowLineChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;
            if (view == null)
            {
                return;
            }

            bool showLine = (bool)newValue;

            if (showLine)
            {
                view.Effects.Add(new FloatingLabelEntryLineEffect());
            }
            else
            {
                var toRemove = view.Effects.FirstOrDefault(e => e is FloatingLabelEntryLineEffect);
                if (toRemove != null)
                {
                    view.Effects.Remove(toRemove);
                }
            }
        }
    }
}
