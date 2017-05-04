#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms;
using System.Windows.Input;

namespace ChilliSource.Mobile.UI
{
	public class BaseCell : ViewCell
	{
		public BaseCell()
		{

		}

		public static readonly BindableProperty HasDisclosureIndicatorProperty =
			BindableProperty.Create(nameof(HasDisclosureIndicator), typeof(bool), typeof(BaseCell), false);


		public bool HasDisclosureIndicator
		{
			get { return (bool)GetValue(HasDisclosureIndicatorProperty); }
			set { SetValue(HasDisclosureIndicatorProperty, value); }
		}

		public static readonly BindableProperty IsSelectableProperty =
			BindableProperty.Create(nameof(IsSelectable), typeof(bool), typeof(BaseCell), true);


		public bool IsSelectable
		{
			get { return (bool)GetValue(IsSelectableProperty); }
			set { SetValue(IsSelectableProperty, value); }
		}

		public static readonly BindableProperty IsUserInteractionEnabledProperty =
			BindableProperty.Create(nameof(IsUserInteractionEnabled), typeof(bool), typeof(BaseCell), true);


		public bool IsUserInteractionEnabled
		{
			get { return (bool)GetValue(IsUserInteractionEnabledProperty); }
			set { SetValue(IsUserInteractionEnabledProperty, value); }
		}

		public static readonly BindableProperty RemoveEdgeInsetsProperty =
			BindableProperty.Create(nameof(RemoveEdgeInsets), typeof(bool), typeof(BaseCell), true);


		public bool RemoveEdgeInsets
		{
			get { return (bool)GetValue(RemoveEdgeInsetsProperty); }
			set { SetValue(RemoveEdgeInsetsProperty, value); }
		}

		public static readonly BindableProperty ShowSeparatorProperty =
			BindableProperty.Create(nameof(ShowSeparator), typeof(bool), typeof(BaseCell), true);


		public bool ShowSeparator
		{
			get { return (bool)GetValue(ShowSeparatorProperty); }
			set { SetValue(ShowSeparatorProperty, value); }
		}

		public static readonly BindableProperty ShouldMonitorTouchEventsProperty =
			BindableProperty.Create(nameof(ShouldMonitorTouchEvents), typeof(bool), typeof(BaseCell), false);


		public bool ShouldMonitorTouchEvents
		{
			get { return (bool)GetValue(ShouldMonitorTouchEventsProperty); }
			set { SetValue(ShouldMonitorTouchEventsProperty, value); }
		}

		public static readonly BindableProperty TouchDownOccurredProperty =
			BindableProperty.Create(nameof(TouchDownOccurred), typeof(ICommand), typeof(BaseCell), default(ICommand));

		public ICommand TouchDownOccurred
		{
			get { return (ICommand)GetValue(TouchDownOccurredProperty); }
			set { SetValue(TouchDownOccurredProperty, value); }
		}

		public static readonly BindableProperty TouchUpOccurredProperty =
			BindableProperty.Create(nameof(TouchUpOccurred), typeof(ICommand), typeof(BaseCell), default(ICommand));

		public ICommand TouchUpOccurred
		{
			get { return (ICommand)GetValue(TouchUpOccurredProperty); }
			set { SetValue(TouchUpOccurredProperty, value); }
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var context = this.BindingContext as ICellViewModel;

			if (context != null)
			{
				context.SizeUpdateRequested += (sender, e) =>
				{
					this.ForceUpdateSize();
				};
			}
		}

		public static readonly BindableProperty SelectionColorProperty =
			BindableProperty.Create(nameof(SelectionColor), typeof(Color), typeof(BaseCell), Color.FromRgb(217, 217, 217));

		public Color SelectionColor
		{
			get { return (Color)this.GetValue(SelectionColorProperty); }
			set { this.SetValue(SelectionColorProperty, value); }
		}

		public static readonly BindableProperty BackgroundColorProperty =
		 BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(BaseCell), Color.Default);

		public Color BackgroundColor
		{
			get { return (Color)this.GetValue(BackgroundColorProperty); }
			set { this.SetValue(BackgroundColorProperty, value); }
		}
	}
}

