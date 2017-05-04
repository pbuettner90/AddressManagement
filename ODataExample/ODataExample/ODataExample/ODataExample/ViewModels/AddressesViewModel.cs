using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using ODataExample.Models;
using ODataExample.Services;
using System.Windows.Input;

namespace ODataExample.ViewModels
{
	public class AddressesViewModel : BaseViewModel
	{
		public Address Address { get; set; }
		ObservableCollection<Address> _addresses;
		public string Search { get; set; }

		public Command CustomerSearchCommand { get; set; }
		public ICommand SelectedItemCommand { get; private set; }
		public ObservableCollection<Address> AddressList
		{
			get { return _addresses; }
			set { SetProperty(ref _addresses, value); }
		}

		public string Header
		{
			get => $"Ihre Addresse: {Address.FirstName} {Address.Street} {Address.City}";
			}

		public AddressesViewModel() : this(new Address())
		{
			Address = new Address();
			//GetData();
		}

		public AddressesViewModel(Address address)
		{
			AddressList = new ObservableCollection<Address>();
			Address = address;
			CustomerSearchCommand = new Command(async () => await CustomerSearch());

			SelectedItemCommand = new Command<Address>(SelectedItem);

			MessagingCenter.Subscribe<EditAddressViewModel, Address>(this, "UpdateAddress", async (obj, updateAddress) =>
			{
				await DependencyService.Get<IDataService>().UpdateAddressAsync(updateAddress);

				var found = _addresses.FirstOrDefault(x => x.Id == updateAddress.Id);
				int i = _addresses.IndexOf(found);
				_addresses[i] = updateAddress;
			});

			MessagingCenter.Subscribe<NewAddressViewModel, Address>(this, "AddCustomer", (obj, insertCustomer) =>
		   {
			   AddressList.Add(insertCustomer);
		   });

			CustomerSort();
		}

		void SelectedItem(Address customer)
		{
			MessagingCenter.Send(this, "NavigateToEdit", customer);
		}

		async Task CustomerSort()
		{
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

			try
			{
				var addresses = await DependencyService.Get<IDataService>().SortAddressesAsync(Address);

				foreach (var customer in addresses)
				{
					AddressList.Add(customer);
				}
			}

			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}

			finally
			{
				IsBusy = false;
			}
		}

		async Task CustomerSearch()
		{
			try
			{
				if (Search != null)
				{
					var searchResults = new ObservableCollection<Address>();
					foreach (Address address in AddressList)
					{
						var firstName = address.FirstName + "";
						var city = address.City + "";
						var street = address.Street + "";

						if (firstName.Contains(Search)
							|| street.Contains(Search)
							|| city.Contains(Search))
						{
							searchResults.Add(address);
						}
					}

					AddressList.Clear();
					AddressList = searchResults;
				}
			}

			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		public Command NewCustomerCommand
		{
			get
			{
				return new Command(() =>
			   {
				   MessagingCenter.Send(this, "AddNewAddress", Address);
			   });
			}
		}

 	}
}









