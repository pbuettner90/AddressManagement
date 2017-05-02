using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ODataExample.Models;
using Simple.OData.Client;
using Xamarin.Forms;

[assembly: Dependency(typeof(ODataExample.Services.AzureDataStoreService))]
namespace ODataExample.Services
{
    public class AzureDataStoreService : IDataService
    {
        public ODataClient Client { get; set; }

        public AzureDataStoreService()
        {
            Client = new ODataClient("http://odatatestservice.azurewebsites.net/odata");
        }

        public async Task AddAddressAsync(Address address)
        {
            await Client.For("Tests").Set(new
                {
                    Name = address.FirstName,
                    Strasse = address.Street,
                    Ort = address.City
                })
                .InsertEntryAsync();
        }

        public async Task <IEnumerable<Address>> GetAddressesAsync()
        {
			var customers = await GetOData();
			return customers.Select(x => new Address(x.Id, x.Name, "", x.Strasse, x.Ort, "", 0));
        }

        public async Task<IEnumerable<Address>> SortAddressesAsync(Address addr)
        {
            IEnumerable<Address> sortedCustomers = null;

            try
            {
                var customers = await GetAddressesAsync();
                var unsortedCustomers = new List<Address>();

                foreach (var customer in customers)
                {
                    CalcSearchPercentage(customer,addr);
                    unsortedCustomers.Add(customer);
                }

                sortedCustomers = unsortedCustomers.OrderByDescending(address => address.SearchPercentage).ThenBy(address => address.FirstName);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return sortedCustomers;
        }

        public async Task UpdateAddressAsync(Address address)
        {

            await Client.For("Tests").Key(address.Id)
                .Set(new
                {
                    Name = address.FirstName,
                    Strasse = address.Street,
                    Ort = address.City
                })
                .UpdateEntryAsync();
        }



        private async Task<IEnumerable<dynamic>> GetOData()
        {
            var x = ODataDynamic.Expression;

            try
            {
				IEnumerable<dynamic> customers = await Client.For(x.Tests).FindEntriesAsync();
                return customers;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        void CalcSearchPercentage(Address actAddress, Address address)
        {
            if (address.FirstName.Equals(actAddress.FirstName))
            {
                actAddress.SearchPercentage = 50;

                if (address.Plz.Equals(actAddress.Plz) || address.City.Equals(actAddress.City))
                {
                    actAddress.SearchPercentage += 30;

                    if (address.Street.Equals(actAddress.Street))
                    {
                        actAddress.SearchPercentage += 20;
                    }
                }
            }
        }
    }
}
