using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AddressManagement.Helper;
using AddressManagement.Models;
using AddressManagement.Services;
using AddressManagement.Views;
using Xamarin.Forms;

namespace AddressManagement.ViewModels
{
	public class AddressesViewModel : BaseViewModel
	{
		public Address HeaderAddress { get; set; }
	    public IEnumerable<Address> AllAddresses { get; set; } = null;

        private ObservableRangeCollection<Address> _addresses;
	    public ObservableRangeCollection<Address> AddressList
	    {
	        get { return _addresses; }
	        set { SetProperty(ref _addresses, value); }
	    }

        public string Search { get; set; }

		public Command CustomerSearchCommand { get; set; }
		public ICommand SelectedItemCommand { get;  set; }

		public string Header
		{
			get
			{
				return "Ihre Addresse: " + " " + HeaderAddress.FirstName + " " + HeaderAddress.LastName + " " + HeaderAddress.Street + " " + HeaderAddress.Plz + " " + HeaderAddress.City;
			}

		}

		public AddressesViewModel() : this(new Address())
		{
		}

		public AddressesViewModel(Address address)
		{
            AddressList = new ObservableRangeCollection<Address>();
			HeaderAddress = address;
			CustomerSearchCommand = new Command(async () => await CustomerSearch());

			SelectedItemCommand = new Command<Address>(SelectedItem);

			MessagingCenter.Subscribe<EditAddressViewModel, Address>(this, "UpdateAddress", async (obj, updateAddress) =>
			{
			    await GetSortedAddressesFromWebservice();
			});

		   Task.Run(async () => { await GetSortedAddressesFromWebservice(); });
        }

	    private void SelectedItem(Address customer)
		{
			App.NavigationPage.Navigation.PushAsync(new EditAddressPage(customer));
		}

		private void CalcSearchPercentage(Address actAddress, Address address)
		{
			if (address.FirstName.Equals(actAddress.FirstName) && address.LastName.Equals(actAddress.LastName))
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

		private async Task GetSortedAddressesFromWebservice()
		{
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

            try
            {
                if (AllAddresses == null)
                {
                    AllAddresses = await DependencyService.Get<IDataService>().GetAddressesAsync();
                }

                IEnumerable<Address> sortedCustomers = null;
				var unsortedCustomers = new List<Address>();

				foreach (var customer in AllAddresses)
				{
					CalcSearchPercentage(customer, HeaderAddress);
					unsortedCustomers.Add(customer);
				}

				sortedCustomers = unsortedCustomers.OrderByDescending(address => address.SearchPercentage).ThenBy(address => address.FirstName);
			    AddressList.Clear();
                AddressList.AddRange(sortedCustomers);
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

		private async Task CustomerSearch()
		{
			Address addr = new Address();

			if (Search != null)
			{
				var searchResults = AllAddresses.Where(address => addr.Compare(address, Search));
				AddressList.Clear();
				AddressList.AddRange(searchResults);
			}
		}

		public Command NewCustomerCommand
		{
			get
			{
				return new Command(() =>
			   {
				   App.NavigationPage.Navigation.PushAsync(new NewAddressPage(HeaderAddress));
			   });
			}
		}	
	}
}


