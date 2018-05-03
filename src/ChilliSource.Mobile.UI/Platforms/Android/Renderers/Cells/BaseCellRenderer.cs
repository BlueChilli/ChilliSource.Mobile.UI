#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System.Linq;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using ChilliSource.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(BaseCell), typeof(BaseCellRenderer))]
namespace ChilliSource.Mobile.UI
{
	public class BaseCellRenderer : ViewCellRenderer
	{
		protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
		{
			var view = convertView ?? base.GetCellCore(item, convertView, parent, context);

			var baseViewCell = (BaseCell)item;

			var drawable = new StateListDrawable();
			drawable.AddState(new int[] { Android.Resource.Attribute.StateSelected }, new ColorDrawable(baseViewCell.SelectionColor.ToAndroid()));
			drawable.AddState(new int[] { Android.Resource.Attribute.StatePressed }, new ColorDrawable(baseViewCell.SelectionColor.ToAndroid()));
			drawable.AddState(StateSet.WildCard.ToArray(), new ColorDrawable(baseViewCell.BackgroundColor.ToAndroid()));


			view.Background = drawable;


			return view;
		}

	}
}
