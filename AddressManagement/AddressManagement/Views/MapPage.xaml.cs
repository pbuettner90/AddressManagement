using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AddressManagement.Models;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace AddressManagement.Views
{
	public partial class MapPage : ContentPage
	{
		public MapPage(Address address)
		{
			InitializeComponent();
			ShowMap(address);
		}

		async Task ShowMap(Address address)
		{
			var geocoder = new Geocoder();

			try
			{
				var positions = await geocoder.GetPositionsForAddressAsync(address.Street + "" + address.City);

				Debug.WriteLine(positions.Count());
				if (positions.Count() > 0)
				{
					var pos = positions.First();

					MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(5000)));
				}
			}

			catch (Exception)
			{
				await DisplayAlert("Suche nach Addresse", "Adresse wurde nicht gefunden", "OK");
			}

		}
	}
}
