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
			BindingContext = new AddressDetailViewModel();
		}

		public NewAddressPage(Address address)
		{
			InitializeComponent();
			Title = "Datensatz hinzufügen";
			BindingContext = new AddressDetailViewModel(address);
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
			MessagingCenter.Subscribe<AddressDetailViewModel, Address>(this, "NavigateToDetail", async (obj, address) =>
			{
				if (address != null)
				{
					await Navigation.PushAsync(new AddressPage(address));
				}
			});
		}

		void UnsubscribeFromMessages()
		{
			MessagingCenter.Unsubscribe<AddressDetailViewModel, Address>(this, "NavigateToDetail");
		}
	}
}
