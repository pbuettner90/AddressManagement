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
		public Address Address { get; set; }

		public AddressPage()
		{
			InitializeComponent();
		}

		public AddressPage(Address address)
		{
			InitializeComponent();
			BindingContext = new AddressViewModel(address);
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
			MessagingCenter.Subscribe<AddressViewModel, Address>(this, "NavigateToDetail", async (obj, address) =>
			{
				if (address != null)
				{
					await Navigation.PushAsync(new AddressEditPage(address));
				}
			});
		}

		void UnsubscribeFromMessages()
		{
			MessagingCenter.Unsubscribe<AddressViewModel, Address>(this, "NavigateToDetail");
		}
	}
}


