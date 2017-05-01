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
using Android.Support.V4.Content;
using Android.Graphics.Drawables;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(ExtendedEditor), typeof(ExtendedEditorRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class ExtendedEditorRenderer : EditorRenderer
	{

		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged(e);

			if (Element == null)
			{
				return;
			}

			var styledEditor = (ExtendedEditor)this.Element;

			if (string.IsNullOrEmpty(styledEditor.Text))
			{
				SetPlaceholder();
			}

			SetStyle();
			SetKeyboardReturnKey();

			styledEditor.Focused += (object sender, FocusEventArgs fea) =>
			{
				if (Control.Text == styledEditor.Placeholder)
				{
					Control.Text = string.Empty;
				}
			};

			styledEditor.Unfocused += (object sender, FocusEventArgs ufea) =>
			{
				if (string.IsNullOrEmpty(Control.Text.Trim()))
				{
					SetPlaceholder();
				}
			};
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = Element as ExtendedEditor;

			if (view != null &&
				e.PropertyName == Entry.TextProperty.PropertyName ||
				e.PropertyName == Entry.PlaceholderProperty.PropertyName)
			{
				SetStyle();
			}
		}

		private void SetPlaceholder()
		{
			var styledEditor = (ExtendedEditor)this.Element;
			styledEditor.CustomPlaceholderFont.ApplyTo(Control);
			Control.Text = styledEditor.Placeholder;
		}

		private void SetStyle()
		{
			var styledEditor = (ExtendedEditor)this.Element;

			if (styledEditor.Text != null && styledEditor.Text != string.Empty)
			{
				styledEditor.CustomFont.ApplyTo(Control);
			}

			var backgroundDrawable = ContextCompat.GetDrawable(Context, Resource.Drawable.entry_background);

			var backgroundColor = styledEditor.BackgroundColor;
			var wrapper = backgroundDrawable as DrawableWrapper;
			var drawable = wrapper != null ? wrapper.Drawable : this.Control.Background;
			var d = drawable.Mutate() as GradientDrawable;

			if (d != null)
			{
				d.SetColor(backgroundColor.ToAndroid());
			}

			this.Control.Background = backgroundDrawable;
		}

		private void SetKeyboardReturnKey()
		{
			var view = Element as ExtendedEditor;
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

