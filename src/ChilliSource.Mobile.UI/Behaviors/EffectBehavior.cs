#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Behavior to attach an effect to a view. The effect is identified through the <see cref="Name"/> and <see cref="Group"/> properties
    /// </summary>
    public class EffectBehavior : Behavior<View>
    {
        /// <summary>
        /// Backing store for the <see cref="Group"/> bindable property.
        /// </summary>
        public static readonly BindableProperty GroupProperty =
            BindableProperty.Create(nameof(Group), typeof(string), typeof(EffectBehavior), null);

        /// <summary>
        /// Gets or sets the value of the <see cref="ResolutionGroupNameAttribute"/> for the effect class. This is a bindable property.
        /// </summary>
        public string Group
        {
            get { return (string)GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }

        /// <summary>
        /// Backing store for the <see cref="Name"/> bindable property.
        /// </summary>
        public static readonly BindableProperty NameProperty =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(EffectBehavior), null);

        /// <summary>
        /// Gets or sets the value of the <see cref="ExportEffectAttribute"/> for the effect class. This is a bindable property.
        /// </summary>
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);
            AddEffect(bindable as View);
        }

        protected override void OnDetachingFrom(BindableObject bindable)
        {
            RemoveEffect(bindable as View);
            base.OnDetachingFrom(bindable);
        }

        void AddEffect(View view)
        {
            var effect = GetEffect();
            if (effect != null)
            {
                view.Effects.Add(GetEffect());
            }
        }

        void RemoveEffect(View view)
        {
            var effect = GetEffect();
            if (effect != null)
            {
                view.Effects.Remove(GetEffect());
            }
        }

        Effect GetEffect()
        {
            if (!string.IsNullOrWhiteSpace(Group) && !string.IsNullOrWhiteSpace(Name))
            {
                return Effect.Resolve(string.Format("{0}.{1}", Group, Name));
            }
            return null;
        }
    }
}
