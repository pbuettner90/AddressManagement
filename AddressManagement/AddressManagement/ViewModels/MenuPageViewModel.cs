using System.Windows.Input;
using AddressManagement.Views;
using Xamarin.Forms;

namespace AddressManagement.ViewModels
{
	public class MenuPageViewModel
	{
		public ICommand GoNewAddressCommand { get; set; }
		public ICommand GoAddressesCommand { get; set; }
		public ICommand GoAboutCommand { get; set; }
		public ICommand GoSettingsCommand { get; set; }

		public MenuPageViewModel()
		{
			GoNewAddressCommand = new Command(GoNewAddress);
			GoAddressesCommand = new Command(GoAddresses);
			GoSettingsCommand = new Command(GoSettings);
			GoAboutCommand = new Command(GoAbout);
		}

		private void GoNewAddress(object obj)
		{
			App.NavigationPage.Navigation.PopToRootAsync();
			App.MenuIsPresented = false;
		}

		private void GoAddresses(object obj)
		{
			App.NavigationPage.Navigation.PushAsync(new AddressesPage());
			App.MenuIsPresented = false;
		}

		private void GoSettings(object obj)
		{
			App.NavigationPage.Navigation.PushAsync(new SettingsPage());
			App.MenuIsPresented = false;
		}

		private void GoAbout(object obj)
		{
			//App.NavigationPage.Navigation.PushAsync(new AboutPage());
			App.MenuIsPresented = false;
		}
	}
}

