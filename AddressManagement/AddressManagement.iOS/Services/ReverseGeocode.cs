using System.Threading.Tasks;
using Xamarin.Forms;
using CoreLocation;
using AddressManagement.Models;
using AddressManagement.Services;

[assembly: Dependency(typeof(AddressManagement.iOS.ReverseGeocode))]

namespace AddressManagement.iOS
{
	public class ReverseGeocode : IReverseGeocode
	{
		public async Task<Address> ReverseGeoCodeLatLonAsync(double lat, double lon)
		{
			var geocoder = new CLGeocoder();
			var loc = new CLLocation(lat, lon);
			var addresses = await geocoder.ReverseGeocodeLocationAsync(loc);
            
			if (addresses.Length > 0)
            {
				var place = new Address();
				var add = addresses[0];
				place.City = add.Locality;
				place.Plz = add.PostalCode;
				place.Street = add.Name;

				return place;
            }

            return null;
		}
	}
}
