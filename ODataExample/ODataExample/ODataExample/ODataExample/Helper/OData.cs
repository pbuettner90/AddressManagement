using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ODataExample.Models;

namespace ODataExample.Helper
{
	public class OData
	{
		ODataClient client;

		public OData()
		{
			InitODataClient();
		}

		public void InitODataClient()
		{
			client = new ODataClient("http://odatatestservice.azurewebsites.net/odata");
		}

		public async Task UpdateData(Address address)
		{
			await client.For("Tests").Key(address.Id)
			            .Set(new { Name = address.FirstName, 
								   Strasse = address.Street, 
								   Ort = address.City })
			            .UpdateEntryAsync();
		}

		public async Task InsertData(Address address)
		{

			await client.For("Tests").Set(new { Name = address.FirstName, 
												Strasse = address.Street, 
												Ort = address.City  })
						.InsertEntryAsync();
		}

		public IEnumerable<dynamic>  GetOData()
		{
			var x = ODataDynamic.Expression;

			try
			{
				IEnumerable<dynamic> customers = client.For(x.Tests).FindEntriesAsync().Result;
				return customers ;
			}

			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public IEnumerable<Address> GetData()
		{
			var customers = GetOData();
			return customers.Select(x => new Address(x.Id, x.Name, "", x.Strasse, x.Ort, "", 0));
		}
	}
}
