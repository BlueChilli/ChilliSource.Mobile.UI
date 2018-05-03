#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using ChilliSource.Mobile.UI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;

[assembly: ExportRenderer(typeof(StyledNavigationBarPage), typeof(StyledNavigationBarPageRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class StyledNavigationBarPageRenderer : PageRenderer
	{

	    public StyledNavigationBarPageRenderer(Context context) : base(context)
	    {

	    }

		protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged(e);

			SetStyle();
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			//SetStyle ();
		}

		private void SetStyle()
		{
			//var actionBar = (Context as FormsAppCompatActivity).SupportActionBar;
			//var styledPage = Element as StyledNavigationBarPage;
			/*
			actionBar.Title = "BlaBla";
			actionBar.Subtitle = "Bla";
			actionBar.SetDisplayShowCustomEnabled (true);
			actionBar.CustomView = new TextView (Context) {
				Text = styledPage.Title + "BlaBla",
				TextSize = styledPage.TitleFont.Size,
				Typeface = UIHelper.GetTypeface (styledPage.TitleFont.Family),
				Gravity = Android.Views.GravityFlags.Right
			};
            */
		}
	}
}

