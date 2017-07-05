using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AddressManagement.Models;

namespace AddressManagement.Services
{
	public interface IDataService
	{
		Task<IEnumerable<Address>> GetAddressesAsync();
		Task AddAddressAsync(Address address);
		Task UpdateAddressAsync(Address address);
	}
}
