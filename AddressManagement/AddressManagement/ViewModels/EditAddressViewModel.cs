using System.Threading.Tasks;
using AddressManagement.Models;
using AddressManagement.Services;
using Xamarin.Forms;

namespace AddressManagement.ViewModels
{
	public class EditAddressViewModel : BaseViewModel
	{
		public Address Address { get; set; }
		private bool _setTextFields;

	    public bool SetTextFields
	    {
	        get { return _setTextFields; }
            set { SetProperty(ref _setTextFields, value); }
	    }

		public Command AddAddressCommand { get; set; }

		public EditAddressViewModel(Address address)
		{
			Address = address;
			SetTextFields = false;

			AddAddressCommand = new Command(async () => await SaveAddress());
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

		private async Task SaveAddress()
		{
			await App.NavigationPage.Navigation.PopAsync();
		    await DependencyService.Get<IDataService>().UpdateAddressAsync(Address);
            MessagingCenter.Send(this, "UpdateAddress", Address);
		}
	}
}

