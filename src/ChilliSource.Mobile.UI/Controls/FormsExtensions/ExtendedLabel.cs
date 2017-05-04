#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	public class ExtendedLabel : Label, IViewContainer<StyledTextPart>
	{

		public ExtendedLabel()
		{
			Children = new List<StyledTextPart>();
		}

		public static readonly BindableProperty CustomFontProperty =
			BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(ExtendedLabel), null);


		public ExtendedFont CustomFont
		{
			get { return (ExtendedFont)GetValue(CustomFontProperty); }
			set { SetValue(CustomFontProperty, value); }
		}

		public static readonly BindableProperty ChildrenProperty =
			BindableProperty.Create(nameof(Children), typeof(List<StyledTextPart>), typeof(ExtendedLabel), null);

		public IList<StyledTextPart> Children
		{
			get { return (IList<StyledTextPart>)GetValue(ChildrenProperty); }
			set
			{
				SetValue(ChildrenProperty, value);
			}
		}

		public static readonly BindableProperty AdjustFontSizeToFitProperty =
			BindableProperty.Create(nameof(AdjustFontSizeToFit), typeof(bool), typeof(ExtendedLabel), false);

		public bool AdjustFontSizeToFit
		{
			get { return (bool)GetValue(AdjustFontSizeToFitProperty); }
			set { SetValue(AdjustFontSizeToFitProperty, value); }
		}

		public static readonly BindableProperty NumberOfLinesProperty =
			BindableProperty.Create(nameof(NumberOfLines), typeof(int), typeof(ExtendedLabel), 0);

		public int NumberOfLines
		{
			get { return (int)GetValue(NumberOfLinesProperty); }
			set { SetValue(NumberOfLinesProperty, value); }
		}

		//we have our own LineBreak property because the Xamarin one forces specific number of lines, which does not work well for all scenarios
		public static readonly BindableProperty SimpleLineBreakModeProperty =
			BindableProperty.Create(nameof(SimpleLineBreakMode), typeof(LineBreakMode), typeof(ExtendedLabel), LineBreakMode.NoWrap);

		public LineBreakMode SimpleLineBreakMode
		{
			get { return (LineBreakMode)GetValue(SimpleLineBreakModeProperty); }
			set { SetValue(SimpleLineBreakModeProperty, value); }
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			if (Children != null && Children.Count > 0)
			{
				Children.ToList().ForEach(child => child.BindingContext = BindingContext);
			}
		}
	}

	public class StyledTextPart : View
	{
		public StyledTextPart()
		{
			this.SetBinding(TextProperty, new Binding(nameof(Text)));
			this.SetBinding(CustomFontProperty, new Binding(nameof(CustomFont)));
			this.SetBinding(CommandProperty, new Binding(nameof(Command)));
			this.SetBinding(CommandParameterProperty, new Binding(nameof(CommandParameter)));
		}

		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(ExtendedLabel), string.Empty);

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static readonly BindableProperty CustomFontProperty =
			BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(ExtendedLabel), null);


		public ExtendedFont CustomFont
		{
			get { return (ExtendedFont)GetValue(CustomFontProperty); }
			set { SetValue(CustomFontProperty, value); }
		}

		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ExtendedLabel), default(ICommand));

		public static readonly BindableProperty CommandParameterProperty =
			BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ExtendedLabel));

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public object CommandParameter
		{
			get { return (object)GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}
	}
}

