#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using CoreGraphics;
using ChilliSource.Mobile.UI;
using Foundation;

[assembly: ExportRenderer(typeof(StyledNavigationBarPage), typeof(StyledNavigationBarPageRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class StyledNavigationBarPageRenderer : PageRenderer
	{
		private UINavigationItem _navbarItem;
		//this is needed until xamarin fixes this bug: https://bugzilla.xamarin.com/show_bug.cgi?id=28388

		public StyledNavigationBarPage NavigationBarPage
		{
			get { return ((StyledNavigationBarPage)Element); }
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			SetNavBarTitleView();
			if (NavigationBarPage.HideBackButton)
			{
				NavigationItem.SetHidesBackButton(true, false);
			}
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			if (NavigationBarPage.HideBackButton)
			{
				this.ParentViewController?.NavigationItem.SetHidesBackButton(true, false);
			}

			if (this.Element == null)
			{
				return;
			}

			SetNavBarTitleView();

			var toolbarItems = (this.Element as ContentPage).ToolbarItems;

			if (HasNavigationController())
			{
				if (NavigationBarPage.IsTransparentNavBar)
				{
					SetupTransparentNavigationBar(toolbarItems);
				}
				else
				{
					var navigationItem = this.NavigationController.TopViewController.NavigationItem;
					var leftNativeButtons = new List<Tuple<ToolbarItem, UIBarButtonItem>>();
					var rightNativeButtons = new List<Tuple<ToolbarItem, UIBarButtonItem>>();

					CreateUIBarButtonFromToolBarItems(leftNativeButtons, rightNativeButtons, toolbarItems);

					if (NavigationBarPage.BarTintColor != Color.Default)
					{
						NavigationController.NavigationBar.TintColor = NavigationBarPage.BarTintColor.ToUIColor();
					}

					navigationItem.RightBarButtonItems = SetCustomFontsToToolBars(rightNativeButtons,
						NavigationBarPage.RightToolbarItemFont, NavigationBarPage.RightToolbarItemVisible);

					if (navigationItem.LeftBarButtonItem == null)
					{
						navigationItem.LeftBarButtonItems = SetCustomFontsToToolBars(leftNativeButtons,
						   NavigationBarPage.LeftToolbarItemFont, NavigationBarPage.LeftToolbarItemVisible);
					}
				}
			}
		}

		private bool HasNavigationController()
		{
			return this.NavigationController != null && this.NavigationController.TopViewController != null;
		}

		private void SetupTransparentNavigationBar(IList<ToolbarItem> toolbarItems)
		{
			var navBar = new UINavigationBar(new CGRect(0, 0, this.View.Bounds.Width, 60)) { Translucent = true };
			navBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
			navBar.ShadowImage = new UIImage();

			_navbarItem = new UINavigationItem { LeftBarButtonItems = new UIBarButtonItem[] { } };
			var leftNativeButtons = new List<Tuple<ToolbarItem, UIBarButtonItem>>();
			var rightNativeButtons = new List<Tuple<ToolbarItem, UIBarButtonItem>>();

			_navbarItem.TitleView = BuildSubTitleView(NavigationBarPage);
			CreateUIBarButtonFromToolBarItems(leftNativeButtons, rightNativeButtons, toolbarItems);

			_navbarItem.RightBarButtonItems = SetCustomFontsToToolBars(rightNativeButtons,
				NavigationBarPage.RightToolbarItemFont, NavigationBarPage.RightToolbarItemVisible);

			_navbarItem.LeftBarButtonItems = SetCustomFontsToToolBars(leftNativeButtons,
				NavigationBarPage.LeftToolbarItemFont, NavigationBarPage.LeftToolbarItemVisible);

			if (NavigationBarPage.BarTintColor != Color.Default)
			{
				navBar.TintColor = NavigationBarPage.BarTintColor.ToUIColor();
			}

			navBar.Items = new[] { _navbarItem };
			this.View.AddSubview(navBar);

		}

		private void CreateUIBarButtonFromToolBarItems(List<Tuple<ToolbarItem, UIBarButtonItem>> leftNativeButtons, List<Tuple<ToolbarItem, UIBarButtonItem>> rightNativeButtons, IList<ToolbarItem> toolbarItems)
		{
			foreach (var item in toolbarItems)
			{
				if (item.Priority == 0)
				{
					leftNativeButtons.Add(new Tuple<ToolbarItem, UIBarButtonItem>(item, item.ToUIBarButtonItem(item.Order == ToolbarItemOrder.Secondary)));
				}
				else
				{
					rightNativeButtons.Add(new Tuple<ToolbarItem, UIBarButtonItem>(item, item.ToUIBarButtonItem(item.Order == ToolbarItemOrder.Secondary)));
				}
			}
		}

		private static UIBarButtonItem[] SetCustomFontsToToolBars(
			List<Tuple<ToolbarItem, UIBarButtonItem>> navigationBarItems,
			ExtendedFont toolbarFont, bool visible = true)
		{
			var barItems = new List<UIBarButtonItem>();

			navigationBarItems.ForEach(tuple =>
						{
							var nativeItem = tuple.Item2;

							if (toolbarFont != null && !string.IsNullOrEmpty(tuple.Item2.Title))
							{
								var button = new UIButton();

								button.SetAttributedTitle(toolbarFont.BuildAttributedString(tuple.Item2.Title), UIControlState.Normal);
								button.TintColor = toolbarFont.Color.ToUIColor();
								button.TitleLabel.SizeToFit();
								button.Hidden = !visible;

								if (tuple.Item1 != null)
								{
									button.TouchUpInside += (sender, e) => ((IMenuItemController)tuple.Item1).Activate();
								}

								button.SizeToFit();
								nativeItem.CustomView = button;
							}

							barItems.Add(nativeItem);

						});

			return barItems.ToArray();
		}

		//private static EventHandler itemsProcessed;

		private void SetNavBarTitleView()
		{
			var navPage = this.Element as StyledNavigationBarPage;

			if (navPage == null)
			{
				return;
			}

			var titleView = BuildSubTitleView(navPage);

			this.NavigationItem.TitleView = titleView;

			if (ParentViewController != null)
			{
				this.ParentViewController.NavigationItem.TitleView = titleView;
			}
		}

		private bool HasSubTitle
		{
			get
			{
				var navPage = this.Element as StyledNavigationBarPage;

				if (navPage == null) return false;

				return !string.IsNullOrEmpty(navPage.SubTitle);

			}
		}

		private static NSAttributedString CreateTitleString(StyledNavigationBarPage page)
		{

			if (page == null) return null;
			bool hasSubTitle = !string.IsNullOrEmpty(page.SubTitle);
			var font = hasSubTitle ? page.TitleFont : page.TitleOnlyFont;
			var titleAttributedString = font.BuildAttributedString(!String.IsNullOrWhiteSpace(page.Title) ? page.Title : "");
			return titleAttributedString;
		}

		private static UIView BuildSubTitleView(StyledNavigationBarPage page)
		{
			bool hasSubTitle = !string.IsNullOrEmpty(page.SubTitle);

			var titleLabel = new UILabel(new CoreGraphics.CGRect(0, 0, 0, 0));
			titleLabel.Tag = 100;

			var titleAttributedString = CreateTitleString(page);

			titleLabel.AdjustsFontSizeToFitWidth = true;
			titleLabel.AttributedText = titleAttributedString;
			titleLabel.SizeToFit();

			if (hasSubTitle)
			{
				var subtitleLabel = new UILabel(new CoreGraphics.CGRect(0, titleLabel.Frame.Height, 0, 0));
				subtitleLabel.Tag = 200;
				var subTitleAttributedString = page.SubTitleFont.BuildAttributedString(page.SubTitle);

				subtitleLabel.AttributedText = subTitleAttributedString;

				subtitleLabel.SizeToFit();

				var view = new UIView(new CoreGraphics.CGRect(0, 0, Math.Max(titleLabel.Frame.Width, subtitleLabel.Frame.Width), titleLabel.Frame.Height + subtitleLabel.Frame.Height));

				// Center title or subtitle on screen (depending on which is larger)
				if (titleLabel.Frame.Width >= subtitleLabel.Frame.Width)
				{
					var adjustment = subtitleLabel.Frame;
					adjustment.Location = new CoreGraphics.CGPoint(view.Frame.Left + view.Frame.Width / 2 - subtitleLabel.Frame.Width / 2, adjustment.Top);
					subtitleLabel.Frame = adjustment;
				}
				else
				{
					var adjustment = titleLabel.Frame;
					adjustment.Location = new CoreGraphics.CGPoint(view.Frame.Left + view.Frame.Width / 2 - titleLabel.Frame.Width / 2, adjustment.Top);
					titleLabel.Frame = adjustment;
				}

				view.Add(titleLabel);
				view.Add(subtitleLabel);

				return view;

			}
			else
			{
				return titleLabel;
			}
		}


		void SetVisibilityOfToolBarItems(bool visible, List<UIBarButtonItem> buttons)
		{
			if (!visible)
			{
				buttons.ForEach((button) =>
				{
					button.Enabled = false;
					button.CustomView.Hidden = true;
				});
			}
			else
			{
				buttons.ForEach((button) =>
				{
					button.Enabled = true;
					button.CustomView.Hidden = false;
				});
			}
		}

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				e.NewElement.PropertyChanged += OnElementPropertyChanged;
			}

			if (e.OldElement != null)
			{
				e.OldElement.PropertyChanged -= OnElementPropertyChanged;
			}
		}

		private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (Element == null) return;

			if (!HasNavigationController()) return;

			var properties = new List<string>()
			{
				StyledNavigationBarPage.LeftToolbarItemFontProperty.PropertyName,
				StyledNavigationBarPage.RightToolbarItemFontProperty.PropertyName,
				StyledNavigationBarPage.TitleProperty.PropertyName,
				StyledNavigationBarPage.LeftToolbarItemVisibleProperty.PropertyName,
				StyledNavigationBarPage.RightToolbarItemVisibleProperty.PropertyName,
				StyledNavigationBarPage.SubtitleProperty.PropertyName
			};

			if (!properties.Contains(e.PropertyName)) return;

			var navBarItem = this.NavigationController.TopViewController.NavigationItem;

			var toolbarItems = (this.Element as ContentPage).ToolbarItems;
			var leftNativeButtons = new List<Tuple<ToolbarItem, UIBarButtonItem>>();
			var rightNativeButtons = new List<Tuple<ToolbarItem, UIBarButtonItem>>();

			if (this.NavigationBarPage.IsTransparentNavBar)
			{
				navBarItem = _navbarItem;
			}

			if (navBarItem == null) return;


			if ((e.PropertyName.Equals(StyledNavigationBarPage.LeftToolbarItemFontProperty.PropertyName) || e.PropertyName.Equals(StyledNavigationBarPage.RightToolbarItemFontProperty.PropertyName)))
			{
				CreateUIBarButtonFromToolBarItems(leftNativeButtons, rightNativeButtons, toolbarItems);
			}

			if (e.PropertyName.Equals(StyledNavigationBarPage.LeftToolbarItemFontProperty.PropertyName))
			{
				navBarItem.LeftBarButtonItems = SetCustomFontsToToolBars(leftNativeButtons, NavigationBarPage.LeftToolbarItemFont, NavigationBarPage.LeftToolbarItemVisible);
			}

			if (e.PropertyName.Equals(StyledNavigationBarPage.RightToolbarItemFontProperty.PropertyName))
			{
				navBarItem.RightBarButtonItems = SetCustomFontsToToolBars(rightNativeButtons, NavigationBarPage.RightToolbarItemFont, NavigationBarPage.RightToolbarItemVisible);
			}

			if (e.PropertyName.Equals(StyledNavigationBarPage.TitleProperty.PropertyName) || e.PropertyName.Equals(StyledNavigationBarPage.SubtitleProperty.PropertyName))
			{
				SetNavBarTitleView();
			}

			if (e.PropertyName.Equals(StyledNavigationBarPage.LeftToolbarItemVisibleProperty.PropertyName))
			{
				SetVisibilityOfToolBarItems(NavigationBarPage.LeftToolbarItemVisible, navBarItem.LeftBarButtonItems.ToList());
			}

			if (e.PropertyName.Equals(StyledNavigationBarPage.RightToolbarItemVisibleProperty.PropertyName))
			{
				SetVisibilityOfToolBarItems(NavigationBarPage.RightToolbarItemVisible, navBarItem.RightBarButtonItems.ToList());
			}

		}
	}
}

