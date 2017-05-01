using ODataExample.Models;
using ODataExample.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Geolocator;
using ODataExample.Helper;
using ODataExample.Services;

namespace ODataExample.ViewModels
{
	public class AddressDetailViewModel : BaseViewModel
	{
		public Address Address { get; set; }

	    private string _firstName;
	    private string _lastName;
	    private string _street;
	    private string _plz;
	    private string _city;

	    public string FirstName { get { return _firstName; } set { SetProperty(ref _firstName, value); } }
	    public string LastName { get { return _lastName; } set { SetProperty(ref _lastName, value); } }
	    public string Street { get { return _street; } set { SetProperty(ref _street, value); } }
	    public string City { get { return _city; } set { SetProperty(ref _city, value); } }
	    public string Plz { get { return _plz; } set { SetProperty(ref _plz, value); } }

        bool _isSet;

		public Command ShowMapCommand { get; set; }
		public Command AddAddressCommand { get; set; }
		public Command GetAddressCommand { get; set; }

		public AddressDetailViewModel()
		{
			
			if (Address == null)
			{
				Address = new Address();
			}

			ShowMapCommand = new Command(async () => await MapService.ShowMap(Address));
			AddAddressCommand = new Command(() => AddAddress());
			GetAddressCommand = new Command(async () => await GetAddress());
		}

		public AddressDetailViewModel(Address address)
		{
			_isSet = true;

			Address = address;

			ShowMapCommand = new Command(async () => await MapService.ShowMap(Address));
			AddAddressCommand = new Command(() => AddAddress());
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

				var apiKey = "AIzaSyACqhxmHfAZXslRcDf2uVZ6UTW1jPhP2KA";
				var googleApi = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={latitude.ToString(Separator.changeSeparator())},{longitude.ToString(Separator.changeSeparator())}&key={apiKey}";
				var googleResponseJson = await GoogleService.RequestGoogle(googleApi);

				await JsonParser.AddressJsonParser(Address, googleResponseJson);

			    City = Address.City;
			    Plz = Address.Plz;
			    Street = Address.Street;
            }

            catch (Exception ex)
			{
				await Application.Current.MainPage.DisplayAlert("GPS erforderlich", "Bitte GPS aktivieren", "OK");
				Debug.WriteLine(ex.Message);
			}

			finally
			{
				IsBusy = false;
			    Debug.WriteLine("IsBusy AddressDetail -- GetUserLocation(): " + IsBusy);
            }
		}

		void AddAddress()
		{
		    try
		    {
		        Address = new Address
		        {
		            FirstName = FirstName,
		            LastName = LastName
		        };

		        if (_isSet)
		        {
		            MessagingCenter.Send(this, "AddCustomer", Address);
		            MessagingCenter.Send(this, "NavigateToDetail", Address);
		        }

		        else
		        {
		            MessagingCenter.Send(this, "NavigateToDetail", Address);
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
	}
}