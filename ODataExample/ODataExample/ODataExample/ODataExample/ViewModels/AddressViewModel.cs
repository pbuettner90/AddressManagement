using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Threading.Tasks;
using ODataExample.Views;
using ODataExample.Helper;
using ODataExample.Models;

namespace ODataExample.ViewModels
{
	public class AddressViewModel : BaseViewModel
	{
		public Address Address { get; set; }
		ObservableCollection<Address> _addresses;

		OData data = new OData();
		public string Search { get; set; }

		public Command CustomerSearchCommand { get; set; }


		public ObservableCollection<Address> AddressList
		{
			get { return _addresses; }
			set { SetProperty(ref _addresses, value); }
		}

		public string Header
		{
			
			get => $"Ihre Addresse: {Address.FirstName} {Address.Street} {Address.City}";
		}

		public AddressViewModel()
		{
			Address = new Address();
			//GetData();
		}

		public AddressViewModel(Address address)
		{
			AddressList = new ObservableCollection<Address>();
			Address = address;
			CustomerSearchCommand = new Command(() => CustomerSearch());

			MessagingCenter.Subscribe<AddressEditViewModel, Address>(this, "UpdateAddress", async (obj, updateAddress) =>
			{
				await data.UpdateData(updateAddress);

				var found = _addresses.FirstOrDefault(x => x.Id == updateAddress.Id);
				int i = _addresses.IndexOf(found);
				_addresses[i] = updateAddress;
			});

			/*MessagingCenter.Subscribe<AddressDetailViewModel, Address>(this, "AddCustomer", (obj, insertCustomer) =>
			{
				var list = new List<Address>();
				list.Add(insertCustomer);


				//AddressList = new ObservableCollection<SelectableItemWrapper<Address>>
				//		(list.Select(pk => new SelectableItemWrapper<Address> { Item = pk }));
			});*/

			Task.Run(async() =>
			{
				try
				{
                    Debug.WriteLine("IsBusy: AddressViewModel - Task.Run " + IsBusy);
					//await GetData();
					await CustomerSort();
				}

				catch (Exception ex)

				{
					Debug.WriteLine(ex.Message);
				}
			});
		}

	    void CalcSearchPercentage(Address actAddress)
	    {
	        if (Address.FirstName.Equals(actAddress.FirstName))
	        {
	            actAddress.SearchPercentage = 50;

	            if (Address.Plz.Equals(actAddress.Plz) || Address.City.Equals(actAddress.City))
	            {
	                actAddress.SearchPercentage += 30;

	                if (Address.Street.Equals(actAddress.Street))
	                {
	                    actAddress.SearchPercentage += 20;
	                }
	            }
	        }
        }

        async Task CustomerSort()
	    {
	        IEnumerable<Address> sortedCustomers = null;

            if (IsBusy)
	        {
	            return;
	        }

	        IsBusy = true;

	        try
	        {
	            var customers = data.GetData();
	            var unsortedCustomers = new List<Address>();

	            foreach (var customer in customers)
	            {
	                CalcSearchPercentage(customer);
	                unsortedCustomers.Add(customer);
                }

                sortedCustomers = unsortedCustomers.OrderByDescending(address => address.SearchPercentage).ThenBy(address => address.FirstName);

	            foreach (var address in (sortedCustomers))
	            {
	                AddressList.Add(address);
                }
            }

            catch (Exception ex)
	        {
	            Debug.WriteLine(ex.Message);
	        }

	        finally
	        {
	            IsBusy = false;
	            Debug.WriteLine("IsBusy AddressDetail -- CustomerSort(): " + IsBusy);
            }
	    }

	    void GetData()
	    {
			if (IsBusy)
			{
				return;
			}

			IsBusy = true;

			try
			{
				var customers = data.GetData();

				foreach (var customer in customers)
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

            Debug.WriteLine("IsBusy: AddressViewModel - GetData() " + IsBusy);
		}

		void CustomerSearch()
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

		Address _selectedAddressItem;
		public Address SelectedAddressItem
        {
		    get
		    {
		        return _selectedAddressItem;
		    }

            set
            {
                SetProperty(ref _selectedAddressItem, value);

				if (SelectedAddressItem == null)
				{
			        return;
				}

                MessagingCenter.Send(this, "NavigateToDetail", SelectedAddressItem);

				_selectedAddressItem = null;
			}
		}

		public Command NewCustomerCommand
		{
			get
			{
				return new Command(async () =>
			   {
					await App.NavigationPage.Navigation.PushAsync(new NewAddressPage(Address));
			   });
			}
		} 	}
}









