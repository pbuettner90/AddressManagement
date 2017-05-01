using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ODataExample.Helper;
using ODataExample.Models;
using Xamarin.Forms;
using ODataExample.Services;

namespace ODataExample.ViewModels
{
	public class AddressEditViewModel : BaseViewModel
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

		public AddressEditViewModel(Address address)
		{
			Address = address;

		    FirstName = Address.FirstName;
		    SetTextFields = false;

			AddAddressCommand = new Command(() => AddAddress());
			ShowMapCommand = new Command(async () => await MapService.ShowMap(address));
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

		void AddAddress()
		{
			MessagingCenter.Send(this, "UpdateAddress", Address);
			MessagingCenter.Send(this, "Navigate", "AddressPage");
		}
	}
}

