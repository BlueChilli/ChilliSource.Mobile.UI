#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ChilliSource.Mobile.Core;
using ChilliSource.Mobile.Location.Google.Places;
using ChilliSource.Mobile.UI;
using ChilliSource.Mobile.UI.Core;

namespace Examples
{
	public class PlacesServiceExamplePageViewModel : BaseViewModel
	{
		bool _searchInProgress;

		public PlacesServiceExamplePageViewModel()
		{
			Cells = new ObservableRangeCollection<AddressCell>();
		}

		public ObservableRangeCollection<AddressCell> Cells { get; set; }

		public async void SearchAddress(string address)
		{
			if (!string.IsNullOrEmpty(address) && !_searchInProgress)
			{
				var filteredResults = new List<AddressCell>();

				try
				{
					_searchInProgress = true;
					var result = await Global.Instance.PlacesService.AutocompleteAsync(address, new AutocompleteRequest());

					if (result.Predictions.ToList().Count > 0)
					{
						var predictions = result.Predictions;

						foreach (var model in predictions)
						{
							if (model != null && !string.IsNullOrEmpty(model.Description))
							{
								filteredResults.Add(new AddressCell(model.Description));
							}
						}
						_searchInProgress = false;

						Cells.ReplaceRange(filteredResults);
					}
					_searchInProgress = false;
				}
				catch (Exception ex)
				{
					if (filteredResults.Count > 0)
					{
						Cells.ReplaceRange(filteredResults);
					}

					_searchInProgress = false;
					Debug.WriteLine(ex.Message.ToString());
				}
			}
			else
			{
				Cells.Clear();
			}

			OnPropertyChanged("BestMatchesViewVisible");
		}
	}
}
