#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Windows.Input;
using System.Threading.Tasks;
using ChilliSource.Mobile.UI;

[assembly: ExportRenderer(typeof(BaseCell), typeof(BaseCellRenderer))]

namespace ChilliSource.Mobile.UI
{
	public class BaseCellRenderer : ViewCellRenderer
	{
		TouchGestureRecognizer _touchGestureRecognizer;
		bool _selected;

		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);

			if (cell == null)
			{
				return null;
			}

			var baseCell = (BaseCell)item;

			if (baseCell.ShouldMonitorTouchEvents)
			{
				if (cell.GestureRecognizers != null)
				{
					foreach (var recognizer in cell.GestureRecognizers)
					{
						cell.RemoveGestureRecognizer(recognizer);
					}
				}

				_touchGestureRecognizer = new TouchGestureRecognizer((gesture) =>
			   {
				   if (gesture.State == UIGestureRecognizerState.Began)
				   {
					   if (baseCell.TouchDownOccurred != null)
					   {
						   ExecuteCommand(baseCell.TouchDownOccurred);
						   _selected = true;
					   }
				   }
				   else if (gesture.State == UIGestureRecognizerState.Ended || gesture.State == UIGestureRecognizerState.Cancelled
							 || gesture.State == UIGestureRecognizerState.Failed)
				   {
					   if (baseCell.TouchUpOccurred != null)
					   {
						   _selected = false;
						   baseCell.TouchUpOccurred.Execute(null);
					   }
				   }
			   });

				_touchGestureRecognizer.CancelsTouchesInView = false;
				cell.AddGestureRecognizer(_touchGestureRecognizer);
			}


			if (baseCell.HasDisclosureIndicator)
			{
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}
			else
			{
				cell.Accessory = UITableViewCellAccessory.None;
			}

			if (!baseCell.IsSelectable)
			{
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			}

			cell.UserInteractionEnabled = baseCell.IsUserInteractionEnabled;

			if (baseCell.RemoveEdgeInsets)
			{
				cell.PreservesSuperviewLayoutMargins = false;
				cell.SeparatorInset = UIEdgeInsets.Zero;
				cell.LayoutMargins = UIEdgeInsets.Zero;
			}

			if (!baseCell.ShowSeparator)
			{
				tv.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			}

			cell.SelectedBackgroundView = new UIView()
			{
				BackgroundColor = baseCell.SelectionColor.ToUIColor()
			};


			cell.BackgroundView = new UIView()
			{
				BackgroundColor = baseCell.BackgroundColor.ToUIColor()
			};

			return cell;
		}

		private async void ExecuteCommand(ICommand command)
		{
			await Task.Delay(50);
			if (_selected)
			{
				command.Execute(null);
			}
		}
	}
}

