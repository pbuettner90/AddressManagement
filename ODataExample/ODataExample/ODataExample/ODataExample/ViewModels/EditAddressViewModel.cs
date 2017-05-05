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
	    private bool _setTextFields;
	    public bool SetTextFields { get { return _setTextFields; } set { SetProperty(ref _setTextFields, value); } }

        public Command ShowMapCommand { get; set; }
		public Command AddAddressCommand { get; set; }

		public EditAddressViewModel(Address address)
		{
			Address = address;
		    SetTextFields = false;

			AddAddressCommand = new Command(async () => await AddAddress());
			ShowMapCommand = new Command(async () => await ShowMap());
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
            MessagingCenter.Send(this, "UpdateAddress", Address);
            MessagingCenter.Send(this, "NavigateToAddresses", "AddressPage");
		}

		async Task ShowMap()
		{
			MessagingCenter.Send(this, "NavigateToMap", Address);
		}
	}
}

