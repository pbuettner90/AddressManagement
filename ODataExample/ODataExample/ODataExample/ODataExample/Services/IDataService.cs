using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODataExample.Models;

namespace ODataExample.Services
{
    public interface IDataService
    {
        Task <IEnumerable<Address>> GetAddressesAsync();
        Task <IEnumerable<Address>> SortAddressesAsync(Address address);
        Task AddAddressAsync(Address address);
        Task UpdateAddressAsync(Address address);

    }
}
