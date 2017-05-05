using System.Diagnostics;
using ODataExample.Models;
using ODataExample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ODataExample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddressPage : ContentPage
	{
		public AddressPage()
		{
			InitializeComponent();
		}

		public AddressPage(Address address)
		{
			InitializeComponent();
			BindingContext = new AddressesViewModel(address);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			SubscribeToMessages();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			UnsubscribeFromMessages();
		}

		void SubscribeToMessages()
		{
			MessagingCenter.Subscribe<AddressesViewModel, Address>(this, "NavigateToEdit", async (obj, address) =>
			{
				if (address != null)
				{
					await Navigation.PushAsync(new AddressEditPage(address));
				}
			});

			MessagingCenter.Subscribe<AddressesViewModel, Address>(this, "AddNewAddress", async (obj, address) =>
			{
				if (address != null)
				{
					await Navigation.PushAsync(new NewAddressPage(address));
				}
			});
		}

		void UnsubscribeFromMessages()
		{
			MessagingCenter.Unsubscribe<AddressesViewModel, Address>(this, "NavigateToEdit");
			MessagingCenter.Unsubscribe<AddressesViewModel, Address>(this, "AddNewAddress");

		}
	}
}


