using System;
using System.Threading.Tasks;
using AddressManagement.Models;

namespace AddressManagement.Services
{
	public interface IReverseGeocode
	{
		Task<Address> ReverseGeoCodeLatLonAsync(double lat, double lon);
	}
}
