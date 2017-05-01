using System.Diagnostics;
using ODataExample.Models;
using ODataExample.ViewModels;
using Xamarin.Forms;

namespace ODataExample
{
	public partial class AddressEditPage : ContentPage
	{
		public AddressEditPage(Address address)
		{
			Title = "Datensatz bearbeiten";
			InitializeComponent();
			BindingContext = new AddressEditViewModel(address);
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
			MessagingCenter.Subscribe<AddressEditViewModel, string>(this, "Navigate", async (obj, s) =>
			{
				if (s == "AddressPage")
				{
					await Navigation.PopAsync();
				}
			});
		}

		void UnsubscribeFromMessages()
		{
			MessagingCenter.Unsubscribe<AddressEditViewModel, string>(this, "Navigate");
		}
	}
}