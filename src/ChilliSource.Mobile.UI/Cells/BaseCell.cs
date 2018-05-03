#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms;
using System.Windows.Input;

namespace ChilliSource.Mobile.UI
{
    /// <summary>
    /// Base cell with additional properties to control the cell's display and user interaction
    /// </summary>
    public class BaseCell : ViewCell
    {
        /// <summary>
        /// Identifies the <see cref="HasDisclosureIndicator"/> bindable property.
        /// </summary>
        public static readonly BindableProperty HasDisclosureIndicatorProperty =
            BindableProperty.Create(nameof(HasDisclosureIndicator), typeof(bool), typeof(BaseCell), false);

        /// <summary>
        /// Determines whether the cell has a disclosure indicator. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if has disclosure indicator; otherwise, <c>false</c>. The default value is <c>false</c>.</value>
        public bool HasDisclosureIndicator
        {
            get { return (bool)GetValue(HasDisclosureIndicatorProperty); }
            set { SetValue(HasDisclosureIndicatorProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="IsSelectable"/> bindable property.
        /// </summary>
        public static readonly BindableProperty IsSelectableProperty =
            BindableProperty.Create(nameof(IsSelectable), typeof(bool), typeof(BaseCell), true);

        /// <summary>
        /// Determines whether the cell is selectable. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if the cell is selectable; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool IsSelectable
        {
            get { return (bool)GetValue(IsSelectableProperty); }
            set { SetValue(IsSelectableProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="IsUserInteractionEnabled"/> bindable property.
        /// </summary>
        public static readonly BindableProperty IsUserInteractionEnabledProperty =
            BindableProperty.Create(nameof(IsUserInteractionEnabled), typeof(bool), typeof(BaseCell), true);

        /// <summary>
        /// Determines whether user interaction is enabled for this cell. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if user interaction is enabled; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool IsUserInteractionEnabled
        {
            get { return (bool)GetValue(IsUserInteractionEnabledProperty); }
            set { SetValue(IsUserInteractionEnabledProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="RemoveEdgeInsets"/> bindable property.
        /// </summary>
        public static readonly BindableProperty RemoveEdgeInsetsProperty =
            BindableProperty.Create(nameof(RemoveEdgeInsets), typeof(bool), typeof(BaseCell), true);

        /// <summary>
        /// Determines whether edge insets of the cell should be removed. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if edge insets should be removed; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool RemoveEdgeInsets
        {
            get { return (bool)GetValue(RemoveEdgeInsetsProperty); }
            set { SetValue(RemoveEdgeInsetsProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="ShowSeparator"/> bindable property.
        /// </summary>
        public static readonly BindableProperty ShowSeparatorProperty =
            BindableProperty.Create(nameof(ShowSeparator), typeof(bool), typeof(BaseCell), true);

        /// <summary>
        /// Determines whether the cell separator should be shown. This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if the separator should be visible; otherwise, <c>false</c>. The default value is <c>true</c>.</value>
        public bool ShowSeparator
        {
            get { return (bool)GetValue(ShowSeparatorProperty); }
            set { SetValue(ShowSeparatorProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="ShouldMonitorTouchEvents"/>  bindable property.
        /// </summary>
        public static readonly BindableProperty ShouldMonitorTouchEventsProperty =
            BindableProperty.Create(nameof(ShouldMonitorTouchEvents), typeof(bool), typeof(BaseCell), false);

        /// <summary>
        /// Determines whether the touch up and touch down events of this cell should be monitored. 
        /// If enabled, the <see cref="TouchUpOccurred"/> and <see cref="TouchDownOccurred"/> commands will be executed when the equivalent events are raised.
        /// This is a bindable property.
        /// </summary>
        /// <value><c>true</c> if touch events should be monitored; otherwise, <c>false</c>. The default value is <c>false</c>.</value>
        public bool ShouldMonitorTouchEvents
        {
            get { return (bool)GetValue(ShouldMonitorTouchEventsProperty); }
            set { SetValue(ShouldMonitorTouchEventsProperty, value); }
        }

        /// <summary>
        /// Backing store for the <see cref="TouchDownOccurred"/> property.
        /// </summary>
        public static readonly BindableProperty TouchDownOccurredProperty =
            BindableProperty.Create(nameof(TouchDownOccurred), typeof(ICommand), typeof(BaseCell), default(ICommand));

        /// <summary>
        /// Command to invoke in response to the iOS <c>TouchesBegan</c> event. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ICommand"/> value that represents 
        /// the command invoked when the cell is first touched.</value>
        public ICommand TouchDownOccurred
        {
            get { return (ICommand)GetValue(TouchDownOccurredProperty); }
            set { SetValue(TouchDownOccurredProperty, value); }
        }

        /// <summary>
        /// Backing store for the <see cref="TouchUpOccurred"/> property.
        /// </summary>
        public static readonly BindableProperty TouchUpOccurredProperty =
            BindableProperty.Create(nameof(TouchUpOccurred), typeof(ICommand), typeof(BaseCell), default(ICommand));

        /// <summary>
        /// Command to invoke in response to the iOS <c>TouchesEnded</c> event. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="ICommand"/> value that represents the command invoked 
        /// whenever user's fingers are lifted off the cell.</value>
        public ICommand TouchUpOccurred
        {
            get { return (ICommand)GetValue(TouchUpOccurredProperty); }
            set { SetValue(TouchUpOccurredProperty, value); }
        }

        /// <summary>
        /// Backing store for the <see cref="SelectionColor"/> property.
        /// </summary>
        public static readonly BindableProperty SelectionColorProperty =
            BindableProperty.Create(nameof(SelectionColor), typeof(Color), typeof(BaseCell), Color.FromRgb(217, 217, 217));

        /// <summary>
        /// Gets or sets the color of the cell when it is selected. This is a bindable property.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the color of the cell whenever is selected. 
        /// The default is R=217, G=217, B=217
        /// </value>
        public Color SelectionColor
        {
            get { return (Color)GetValue(SelectionColorProperty); }
            set { SetValue(SelectionColorProperty, value); }
        }

        /// <summary>
        /// Backing store for the <see cref="BackgroundColor"/> property.
        /// </summary>
        public static readonly BindableProperty BackgroundColorProperty =
         BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(BaseCell), Color.Default);

        /// <summary>
        /// Gets or sets the color of the cell's background.
        /// </summary>
        /// <value>A <see cref="Color"/> value that represents the color of the cell's background. 
        /// The default value is <see cref="Color.Default"/>.</value>
        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }
    }
}

