#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using CoreGraphics;
using UIKit;

namespace ChilliSource.Mobile.UI
{
	public class InfiniteScrollState
	{
		public InfiniteScrollState()
		{

#if TARGET_OS_TV
     		IndicatorStyle = UIActivityIndicatorViewStyle.White;
#else
			IndicatorStyle = UIActivityIndicatorViewStyle.Gray;
#endif

			IndicatorMargin = 11;
		}

		//Indicates whether scroll is initialized 
		public bool Initialized { get; set; }


		//Indicates whether loading is in progress.         
		public bool Loading { get; set; }

		public UIView IndicatorView { get; set; }

		//Indicator style when UIActivityIndicatorView used.         
		public UIActivityIndicatorViewStyle IndicatorStyle { get; set; }

		//Extra padding to push indicator view below view bounds.
		//Used in case when content size is smaller than view bounds        
		public nfloat ExtraBottomInset { get; set; }

		//Indicator view inset.
		//Essentially is equal to indicator view height.         
		public nfloat IndicatorInset { get; set; }

		//Indicator view margin (top and bottom)         
		public nfloat IndicatorMargin { get; set; }

		//Infinite scroll handler block        
		public Action<IScrollableView> InfiniteScrollViewHandler { get; set; }

		//Infinite scroll allowed block
		//Return NO to block the infinite scroll. Useful to stop requests when you have shown all results, etc.         
		public Func<IScrollableView, bool> ShouldShowInfiniteScrollHandler { get; set; }
	}
}
