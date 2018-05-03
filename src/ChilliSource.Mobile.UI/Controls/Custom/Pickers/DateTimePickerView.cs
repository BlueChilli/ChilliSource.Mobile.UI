#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Specifies the type of the picker.
    /// </summary>
    public enum PickerMode
    {
        DateTime,
        Date,
    }

    /// <summary>
    /// iOS-style date and timer picker view
    /// </summary>
    public class DateTimePickerView : View
    {
        /// <summary>
        /// Backing store for the <c>MinimumDateTime</c> bindable property.
        /// </summary>
        public static readonly BindableProperty MinimumDateTimeProperty =
            BindableProperty.Create(nameof(MinimumDateTime), typeof(DateTime), typeof(DateTimePickerView), DateTime.MinValue);

        /// <summary>
        /// Gets or sets the minimum value for date and time. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="DateTime"/> value that represents the minimum date and time of the picker.</value>
        public DateTime MinimumDateTime
        {
            get { return (DateTime)GetValue(MinimumDateTimeProperty); }
            set { SetValue(MinimumDateTimeProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>MaximumDateTime</c> bindable property.
        /// </summary>
        public static readonly BindableProperty MaximumDateTimeProperty =
            BindableProperty.Create(nameof(MaximumDateTime), typeof(DateTime), typeof(DateTimePickerView), DateTime.MaxValue);

        /// <summary>
        /// Gets or sets the maximum value for date  and time. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="DateTime"/> value that represents the maximum date and time of the picker.
        /// The default value is <see cref="DateTime.MaxValue"/>.</value>
        public DateTime MaximumDateTime
        {
            get { return (DateTime)GetValue(MaximumDateTimeProperty); }
            set { SetValue(MaximumDateTimeProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>SelectedDateTime</c> bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedDateTimeProperty =
            BindableProperty.Create(nameof(SelectedDateTime), typeof(DateTime), typeof(DateTimePickerView), DateTime.Now);

        /// <summary>
        /// Gets or sets the selected date and time. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="DateTime"/> value that represents the selected date and time of the picker. 
        /// The default value is <see cref="DateTime.Now"/>.</value>
        public DateTime SelectedDateTime
        {
            get { return (DateTime)GetValue(SelectedDateTimeProperty); }
            set { SetValue(SelectedDateTimeProperty, value); }
        }


        /// <summary>
        /// Backing store for the <c>PickerMode</c> bindable property.
        /// </summary>
        public static readonly BindableProperty PickerModeProperty =
            BindableProperty.Create(nameof(PickerMode), typeof(PickerMode), typeof(DateTimePickerView), PickerMode.DateTime);

        /// <summary>
        /// Gets or sets the type of the picker. This is a bindable property.
        /// </summary>
        /// <value>A <c>PickerMode</c> value that represents the type of the picker view.
        /// The default value is <c>PickerMode.DateTime</c>.</value>
        public PickerMode PickerMode
        {
            get { return (PickerMode)GetValue(PickerModeProperty); }
            set { SetValue(PickerModeProperty, value); }
        }
    }
}

