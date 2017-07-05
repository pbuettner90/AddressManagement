using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AddressManagement.Models;
using Simple.OData.Client;
using Xamarin.Forms;

[assembly: Dependency(typeof(AddressManagement.Services.AzureDataStoreService))]
namespace AddressManagement.Services
{
	public class AzureDataStoreService : IDataService
	{
		public ODataClient Client { get; set; }
	    private dynamic Addressen;

		public AzureDataStoreService()
		{
			Client = new ODataClient(Settings.Url);
			//URL Webservice: "http://addressodata20170508023216.azurewebsites.net/odata"
			Addressen = Client.For(ODataDynamic.Expression.Addresses);
		}

		public async Task AddAddressAsync(Address address)
		{
			await Addressen.Set(new
			{
				FirstName = address.FirstName,
				LastName = address.LastName,
				Street = address.Street,
				City = address.City,
				Plz = address.Plz
			})
				.InsertEntryAsync();
		}

		public async Task<IEnumerable<Address>> GetAddressesAsync()
		{
			var customers = await GetOData();
			return customers.Select(x => new Address(x.Id, x.FirstName, x.LastName, x.Street, x.City, x.Plz, 0));
		}

		public async Task UpdateAddressAsync(Address address)
		{
			await Client.For("Addresses").Key(address.Id).Set(new
			{
					FirstName = address.FirstName,
					LastName = address.LastName,
					Street = address.Street,
					City = address.City,
					Plz = address.Plz
			
			}).UpdateEntryAsync();
		}

		private async Task<IEnumerable<dynamic>> GetOData()
		{
			AzureDataStoreService az = new AzureDataStoreService();
			var x = ODataDynamic.Expression;

			try
			{
				IEnumerable<dynamic> customers = await az.Addressen.FindEntriesAsync();
				return customers;
			}

			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}
	}
}
