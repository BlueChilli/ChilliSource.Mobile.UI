#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Views.InputMethods;
using Android.Text;
using Android.Support.V4.Content.Res;
using Android.Support.V4.Content;
using Android.Graphics.Drawables;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ExtendedEntryRenderer : EntryRenderer
	{

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement == null)
			{
				return;
			}

			SetStyle();
			SetMaxLength();
			SetKeyboardReturnKey();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = Element as ExtendedEntry;

			if (view != null &&
				e.PropertyName == Entry.PlaceholderProperty.PropertyName ||
				e.PropertyName == ExtendedEntry.IsValidProperty.PropertyName ||
				e.PropertyName == ExtendedEntry.CustomFontProperty.PropertyName ||
				e.PropertyName == ExtendedEntry.CustomPlaceholderFontProperty.PropertyName ||
				e.PropertyName == ExtendedEntry.ErrorMessageProperty.PropertyName)
			{
				SetStyle();
			}
		}

		private void SetStyle()
		{
			var styledEntry = (ExtendedEntry)this.Element;

			if (styledEntry.CustomFont != null)
			{
				styledEntry.CustomFont.ApplyTo(Control);
			}

			if (styledEntry.Text != null && styledEntry.CustomFont != null)
			{
				var font = styledEntry.IsValid ? styledEntry.CustomFont : styledEntry.CustomErrorFont;
				font.ApplyTo(Control);

				if (!styledEntry.IsValid)
				{
					Control.SetError(styledEntry.ErrorMessage, ResourcesCompat.GetDrawable(Forms.Context.Resources, Resource.Drawable.ic_errorstatus, null));
				}
			}

			var backgroundDrawable = ContextCompat.GetDrawable(Context, Resource.Drawable.entry_background);

			var backgroundColor = Color.White;
			if (styledEntry.IsValid)
			{
				backgroundColor = styledEntry.NormalBackgroundColor;
			}
			else
			{
				backgroundColor = styledEntry.ErrorBackgroundColor;
			}

			var wrapper = backgroundDrawable as DrawableWrapper;
			var drawable = wrapper != null ? wrapper.Drawable : this.Control.Background;
			var d = drawable.Mutate() as GradientDrawable;

			if (d != null)
			{
				d.SetColor(backgroundColor.ToAndroid());
			}

			this.Control.Background = backgroundDrawable;
		}

		private void SetMaxLength()
		{
			var styledEntry = (ExtendedEntry)this.Element;
			Control.SetFilters(new IInputFilter[] { new InputFilterLengthFilter(styledEntry.MaxLength) });
		}

		private void SetKeyboardReturnKey()
		{
			var view = Element as ExtendedEntry;
			switch (view.KeyboardReturnType)
			{
				case KeyboardReturnKeyType.Done:
					{
						Control.ImeOptions = ImeAction.Done;
						break;
					}
				case KeyboardReturnKeyType.Go:
					{
						Control.ImeOptions = ImeAction.Go;
						break;
					}
				case KeyboardReturnKeyType.Next:
					{
						Control.ImeOptions = ImeAction.Next;
						break;
					}
			}
		}
	}
}

