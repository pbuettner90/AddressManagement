using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AddressManagement;
using AddressManagement.Services;
using AddressManagement.Models;

[assembly: Dependency(typeof(AddressManagement.Droid.ReverseGeocode))]

namespace AddressManagement.Droid
{
	public class ReverseGeocode : IReverseGeocode
	{
		public async Task<Models.Address> ReverseGeoCodeLatLonAsync(double lat, double lon)
		{
			var geo = new Geocoder(Forms.Context);
			var addresses = await geo.GetFromLocationAsync(lat, lon, 1);

			if (addresses.Any())
			{
				var place = new Models.Address();
				var addr = addresses[0];

				place.City = addr.Locality;
				place.Plz = addr.PostalCode;
				place.Street = addr.Thoroughfare + " " + addr.SubThoroughfare;

				return place;
			}

			return null;

		}
	}
}
