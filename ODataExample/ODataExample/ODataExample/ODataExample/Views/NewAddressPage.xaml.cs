using System;
using ODataExample.Models;
using ODataExample.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ODataExample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewAddressPage : ContentPage
	{
		public NewAddressPage()
		{
			InitializeComponent();
			Title = "Addresse prüfen";
			BindingContext = new NewAddressViewModel();
		}

		/*public NewAddressPage(Settings settings)
		{
			InitializeComponent();
			BindingContext = new NewAddressViewModel(settings);
		}*/

		public NewAddressPage(Address address)
		{
			InitializeComponent();
			Title = "Datensatz hinzufügen";
			BindingContext = new NewAddressViewModel(address);
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
			MessagingCenter.Subscribe<NewAddressViewModel, Address>(this, "NavigateToAddresses", async (obj, address) =>
			{
				Debug.WriteLine("Subscribe NewAddressPage NavigateToNewAddresses" );
				if (address != null)
				{
					await Navigation.PushAsync(new AddressPage(address));
				}
			});

			MessagingCenter.Subscribe<NewAddressViewModel, Address>(this, "NavigateToMap", async (obj, address) =>
			{
				Debug.WriteLine("Subscribe NewAddressPage NavigateToMap" );

				if (address != null)
				{
					await Navigation.PushAsync(new MapPage(address));
				}
			});
		}

		void UnsubscribeFromMessages()
		{
			MessagingCenter.Unsubscribe<NewAddressViewModel, Address>(this, "NavigateToAddresses");
			MessagingCenter.Unsubscribe<NewAddressViewModel, Address>(this, "NavigateToMap");

		}
	}
}
