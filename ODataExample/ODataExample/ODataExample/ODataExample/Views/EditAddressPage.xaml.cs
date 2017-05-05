using System.Diagnostics;
using ODataExample.Models;
using ODataExample.ViewModels;
using ODataExample.Views;
using Xamarin.Forms;

namespace ODataExample
{
	public partial class AddressEditPage : ContentPage
	{
		public AddressEditPage(Address address)
		{
			Title = "Datensatz bearbeiten";
			InitializeComponent();
			BindingContext = new EditAddressViewModel(address);
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
			MessagingCenter.Subscribe<EditAddressViewModel, string>(this, "NavigateToAddresses", async (obj, s) =>
			{
				if (s == "AddressPage")
				{
					await Navigation.PopAsync();
				}
			});

			MessagingCenter.Subscribe<EditAddressViewModel, Address>(this, "NavigateToMap", async (obj, address) =>
			{
				if (address != null)
				{
					await Navigation.PushAsync(new MapPage(address));
				}
			});
		}

		void UnsubscribeFromMessages()
		{
			MessagingCenter.Unsubscribe<EditAddressViewModel, string>(this, "NavigateToAddresses");
			MessagingCenter.Unsubscribe<EditAddressViewModel, Address>(this, "NavigateToMap");

		}
	}
}