using AddressManagement.Models;
using AddressManagement.ViewModels;
using Xamarin.Forms;

namespace AddressManagement.Views
{
	public partial class AddressesPage : ContentPage
	{
		public AddressesPage()
		{
			InitializeComponent();
            BindingContext = new AddressesViewModel();
		}

		public AddressesPage(Address address)
		{
			InitializeComponent();
			BindingContext = new AddressesViewModel(address);
		}
	}
}
