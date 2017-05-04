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

		//protected AddressesViewModel AddressViewModel => (AddressesViewModel)BindingContext;

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

		/*private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var param = e.SelectedItem as Address;
			var command = ((AddressesViewModel)BindingContext).SelectedItemCommand;

			if (command.CanExecute(param))
			{
				Debug.WriteLine(param.FirstName);
				command.Execute(param);

 			}
		}*/
	}
}


