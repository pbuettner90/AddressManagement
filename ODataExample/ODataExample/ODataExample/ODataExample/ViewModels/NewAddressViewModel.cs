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
		public SelectableItemWrapper<Address> WrapperAddress { get; set; }

		INavigation _navigation;
		bool _isEdit;
		bool _setTextFields;
		bool _isSet = true;
		OData data = new OData();

		public Command ShowMapCommand { get; set; }
		public Command AddAddressCommand { get; set; }
		public Command GetAddressCommand { get; set; }

		public bool IsEdit
		{
			get { return _isEdit; }
			set { SetProperty(ref _isEdit, value); }
		}

		public bool SetTextFields
		{
			get
			{
				return _setTextFields;
			}

			set { SetProperty(ref _setTextFields, value); }
		}

		public AddressDetailViewModel(Photo photo, INavigation navigation)
		{
		}

		public AddressDetailViewModel(INavigation navigation)
		{
			_navigation = navigation;

			if (Address == null)
			{
				Address = new Address();
				_isEdit = false;
				_setTextFields = true;
			}

			ShowMapCommand = new Command(async () => await ShowMap());
			AddAddressCommand = new Command(async () => await AddAddress());
			GetAddressCommand = new Command(async () => await GetAddress());
		}

		public AddressDetailViewModel(SelectableItemWrapper<Address> address)
		{
			WrapperAddress = address;
			Debug.WriteLine("ListView Selected: " + address.Item.FirstName);
			//_isEdit = true;
			//_setTextFields = false;
			//WrapperAddress = address;

		}

		public AddressDetailViewModel(Address address)
		{
			_isEdit = true;
			_setTextFields = false;
			Address = address;
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
				await JSONParser.AddressJsonParser(Address, googleResponseJson);
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
			//await SearchDatabase(Address);

			if (_isSet)
			{
				await App.NavigationPage.Navigation.PushAsync(new AddressPage(_navigation, Address));

			}

			else
			{
				Debug.WriteLine("Update");
				//data.UpdateData(Address.Id, Address.FirstName, Address.Street, Address.City);
				await App.NavigationPage.Navigation.PopAsync();
			}
		}

		async Task ShowMap()
		{
			await App.NavigationPage.Navigation.PushAsync(new MapPage(Address));
		}

		async Task GetAddress()
		{
			await GetUserLocation();
		}

		public Command BtnEditCommand
		{
			get
			{
				return new Command(() =>
				{
					Debug.WriteLine("BtnEdit");
					_setTextFields = true;
				});
			}
		}
	}
}

	



