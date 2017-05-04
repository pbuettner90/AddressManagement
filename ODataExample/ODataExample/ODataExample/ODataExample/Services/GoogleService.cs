using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using ODataExample.Helper;
using ODataExample.Models;
using Xamarin.Forms;

namespace ODataExample.Services
{
	public class GoogleService
	{
		public static string GetApiKey()
		{
			return "AIzaSyACqhxmHfAZXslRcDf2uVZ6UTW1jPhP2KA";
		}

		public static string GetGoogleApi(double latitude, double longitude)
		{
			CultureInfo InvC = new CultureInfo("");
			var googleApi = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude.ToString(InvC)},{longitude.ToString(InvC)}&key={GetApiKey()}";
			return googleApi;
		}

		public static async Task<string> GetJsonResponse(string url)
		{
			try
			{
				HttpClient httpClient = new HttpClient();
				var response = await httpClient.GetAsync(url);
				return await response.Content.ReadAsStringAsync();
			}

			catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("Internetverbindung erforderlich", "Internetverbindung konnte nicht hergestellt werden", "OK");

				Debug.WriteLine(ex.Message);
				return null;

			}
		}

		public static async Task<Address> GetAddress(Address address, string googleResponseJson)
		{
				var responseAddress = await JsonParser.AddressJsonParser(address, googleResponseJson);
				return responseAddress;	
		}
	}
}
