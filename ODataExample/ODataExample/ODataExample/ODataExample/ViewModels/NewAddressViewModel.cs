using ODataExample.Models;
using ODataExample.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Geolocator;
using ODataExample.Helper;
using ODataExample.Services;
using System.Globalization;

namespace ODataExample.ViewModels
{
	public class NewAddressViewModel : BaseViewModel
	{
		public Address Address { get; set; }

        bool _isSet;

		public Command ShowMapCommand { get; set; }
		public Command AddAddressCommand { get; set; }
		public Command GetAddressCommand { get; set; }

		public NewAddressViewModel()
		{
			if (Address == null)
			{
				Address = new Address();
			}

			ShowMapCommand = new Command(async () => await ShowMap());
			AddAddressCommand = new Command(async () => await AddAddress());
			GetAddressCommand = new Command(async () => await GetAddress());
		}

		public NewAddressViewModel(Address address)
		{
			_isSet = true;
			Address = address;
			ShowMapCommand = new Command(async () => await ShowMap());
			AddAddressCommand = new Command(async () => await AddAddress());
			GetAddressCommand = new Command(async () => await GetAddress());
		}

		async Task GetUserLocation()
		{
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

			try
			{
				var locator = CrossGeolocator.Current;
				locator.DesiredAccuracy = 50;
				var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

				if (position == null)
				{
					return;
				}

				var latitude = position.Latitude;
				var longitude = position.Longitude;
		
				var googleApi = GoogleService.GetGoogleApi(latitude, longitude);
				var googleResponseJson = await GoogleService.GetJsonResponse(googleApi);
				await GoogleService.GetAddress(Address, googleResponseJson);
            }

            catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("GPS erforderlich", "Bitte GPS aktivieren", "OK");
				Debug.WriteLine(ex.Message);
			}

			finally
			{
				IsBusy = false;
            }
		}

	    async Task AddAddress()
		{
		    try
			{
                if (_isSet)
		        {
		            MessagingCenter.Send(this, "NavigateToAddresses", Address);
					await DependencyService.Get<IDataService>().AddAddressAsync(Address);
					MessagingCenter.Send(this, "AddCustomer", Address);
		        }

		        else
		        {
					MessagingCenter.Send(this, "NavigateToAddresses", Address);
		        }
		    }

		    catch (Exception ex)
		    {
		        Debug.WriteLine(ex.Message);
		    }
		}

		async Task GetAddress()
		{
			await GetUserLocation();
		}

		public async Task ShowMap()
		{
			MessagingCenter.Send(this, "NavigateToMap", Address);
		}
	}
}