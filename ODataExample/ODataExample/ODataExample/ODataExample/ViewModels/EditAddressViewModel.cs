using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ODataExample.Helper;
using ODataExample.Models;
using Xamarin.Forms;
using ODataExample.Services;

namespace ODataExample.ViewModels
{
	public class EditAddressViewModel : BaseViewModel
	{
		public Address Address { get; set; }

	    private string _firstName;
	    private string _lastName;
	    private string _street;
	    private string _plz;
	    private string _city;

	    private bool _setTextFields;

        public string FirstName { get { return _firstName; } set { SetProperty(ref _firstName, value); } }
	    public string LastName { get { return _lastName; } set { SetProperty(ref _lastName, value); } }
	    public string Street { get { return _street; } set { SetProperty(ref _street, value); } }
	    public string City { get { return _city; } set { SetProperty(ref _city, value); } }
	    public string Plz { get { return _plz; } set { SetProperty(ref _plz, value); } }

	    public bool SetTextFields { get { return _setTextFields; } set { SetProperty(ref _setTextFields, value); } }

        public Command ShowMapCommand { get; set; }
		public Command AddAddressCommand { get; set; }

		public EditAddressViewModel(Address address)
		{
			Address = address;

		    CopyDataToTextField();

		    SetTextFields = false;

			AddAddressCommand = new Command(async () => await AddAddress());
			ShowMapCommand = new Command(async () => await ShowMap(address));
		}

	    private void CopyDataToTextField()
	    {
	        FirstName = Address.FirstName;
	        LastName = Address.LastName;
	        Street = Address.Street;
	        City = Address.City;
	        Plz = Address.Plz;
	    }

	    private void CopyTextFieldToData()
	    {
	        Address.FirstName = FirstName;
	        Address.LastName = LastName;
	        Address.Street = Street;
	        Address.City = City;
	        Address.Plz = Plz;
	    }

        public Command BtnEditCommand
		{
			get
			{
				return new Command(() =>
				{
				    SetTextFields = true;
				});
			}
		}

		async Task AddAddress()
		{
		    CopyTextFieldToData();
            MessagingCenter.Send(this, "UpdateAddress", Address);
            MessagingCenter.Send(this, "NavigateToAddresses", "AddressPage");
		}

		public async Task ShowMap(Address address)
		{
			Address = address;
			MessagingCenter.Send(this, "NavigateToMap", address);
		}
	}
}

