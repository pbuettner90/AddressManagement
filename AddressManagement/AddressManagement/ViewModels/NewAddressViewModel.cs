using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AddressManagement.Models;
using AddressManagement.Services;
using AddressManagement.Views;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace AddressManagement.ViewModels
{
	public class NewAddressViewModel : BaseViewModel
	{
		public Address Address { get; set; }
	    public bool IsSet { get; set; }
	    public Address ResponseAddress { get; set; }

        public Command AddAddressCommand { get; set; }
		public Command GetAddressCommand { get; set; }

		public NewAddressViewModel()
		{
			AddAddressCommand = new Command(async () => await AddAddress());
			GetAddressCommand = new Command(async () => await GetAddress());

			Address = new Address();
			SubscribeToMessages();
		}

		public NewAddressViewModel(Address address)
		{
			IsSet = true;
			Address = address;
			AddAddressCommand = new Command(async () => await AddAddress());
			GetAddressCommand = new Command(async () => await GetAddress());
		}

	    void SubscribeToMessages()
	    {
	        MessagingCenter.Subscribe<NewAddressPage>(this, "GetAddress", (sender) =>
	        {
	            MessagingCenter.Unsubscribe<NewAddressPage>(this, "GetAddress");
	            Device.BeginInvokeOnMainThread(async () =>
	            {
	                try
	                {
	                    await InitialLocation();
	                }

	                catch (Exception ex)
	                {
	                    Debug.WriteLine(ex.Message);
	                }
	            });
	        });
	    }

        private async Task AddAddress()
		{
		    try
		    {
		        if (IsSet)
		        {
		            await DependencyService.Get<IDataService>().AddAddressAsync(Address);
		        }
		    }

		    catch (Exception ex)
		    {
		        Debug.WriteLine(ex.Message);
		    }

		    finally
		    {
		        await App.NavigationPage.Navigation.PushAsync(new AddressesPage(Address));
            }
        }

		private async Task InitialLocation()
		{
			try
			{
				IGeolocator loc = CrossGeolocator.Current;
				loc.DesiredAccuracy = 50;
				var position = await loc.GetPositionAsync(timeoutMilliseconds: 10000);
				await PerformReverseGeocode(position);
			}

			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		private async Task PerformReverseGeocode(Position p)
		{
			var lat = p.Latitude;
			var lon = p.Longitude;
			var ds = DependencyService.Get<IReverseGeocode>();
			var address = await ds.ReverseGeoCodeLatLonAsync(lat, lon);

		    ResponseAddress = new Address
		    {
		        Plz = address.Plz,
		        City = address.City,
		        Street = address.Street
		    };
		}

		private async Task GetAddress()
		{
			if (ResponseAddress == null)
			{
				await Application.Current.MainPage.DisplayAlert( "Addresse ermitteln", "Fehler", "OK");
			}
			
			if (ResponseAddress.Street == null)
			{
				await Application.Current.MainPage.DisplayAlert( "Addresse ermitteln", 
				                                                 " Addresse konnte nicht ermittelt werden", 
				                                                 "OK");
			}

			else
			{
			    Address.Plz = ResponseAddress.Plz;
			    Address.City = ResponseAddress.City;
                Address.Street = ResponseAddress.Street;
			}
		}
	}
}