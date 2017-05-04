using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ODataExample.Models;
using ODataExample.Models.JSON;
using Xamarin.Forms;

namespace ODataExample.Helper
{
	public class JsonParser
	{
		public static async Task<Address> AddressJsonParser(Address address, string json)
		{
			if (json != null)
			{
				try
				{
					AddressJSON.RootObject r = JsonConvert.DeserializeObject<AddressJSON.RootObject>(json);

					address.City = r.results[0].address_components[2].short_name;
					address.Street = r.results[0].address_components[1].short_name + " " + r.results[0].address_components[0].short_name;
					address.Plz = r.results[0].address_components[7].short_name;

					return address;

				}

				catch (Exception ex)
				{
					await Application.Current.MainPage.DisplayAlert("JSON-Fehler", "Fehler beim Laden der JSON-Daten", "OK");

					Debug.WriteLine(ex.Message);
				}
			}

			return null;
		}
	}
}
