using System.Diagnostics;
using AddressManagement.Models;
using AddressManagement.ViewModels;
using Xamarin.Forms;

namespace AddressManagement.Views
{
	public partial class NewAddressPage : ContentPage
	{
		public NewAddressPage()
		{
			InitializeComponent();
			Title = "Addresse prüfen";
			BindingContext = new NewAddressViewModel();
		}

		public NewAddressPage(Address address)
		{
			InitializeComponent();
			Title = "Datensatz hinzufügen";
			BindingContext = new NewAddressViewModel(address);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			MessagingCenter.Send(this, "GetAddress");
		}
	}
}

