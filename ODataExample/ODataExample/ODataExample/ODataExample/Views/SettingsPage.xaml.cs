using System;
using System.Collections.Generic;
using System.Diagnostics;
using ODataExample.Views;
using Xamarin.Forms;

namespace ODataExample
{
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			Title = "Einstellungen";
			InitializeComponent();
			BindingContext = new SettingsViewModel();
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
			/*MessagingCenter.Subscribe<SettingsViewModel, Settings>(this, "NavigateToNewPage", async (obj, settings) =>
			{
				if (settings != null)
				{
					await Navigation.PushAsync(new NewAddressPage(settings));
				}
			});*/

			MessagingCenter.Subscribe<SettingsViewModel, string>(this, "NavigateToNewPage", async (obj, s) =>
			{
				Debug.WriteLine("Subscribe NewAddressPage NavigateToNewPage" );

				if (s == "NewAddressPage")
				{
					await Navigation.PopAsync();
				}
			});
		}

		void UnsubscribeFromMessages()
		{
			Debug.WriteLine("Unsubscribe NavigateToNewPage");
			MessagingCenter.Unsubscribe<SettingsViewModel, string>(this, "NavigateToAddresses");
		}
	}
}
