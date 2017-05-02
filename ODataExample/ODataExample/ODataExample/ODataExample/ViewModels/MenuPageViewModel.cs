using ODataExample.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace ODataExample.ViewModels
{
    public class MenuPageViewModel
    {
        public ICommand GoNewAddressCommand { get; set; }
        public ICommand GoAddressesCommand { get; set; }
        public ICommand GoAboutCommand { get; set; }

        public MenuPageViewModel()
        {
            GoNewAddressCommand = new Command(GoNewAddress);
            GoAddressesCommand = new Command(GoAddresses);
            GoAboutCommand = new Command(GoAbout);
		}

        void GoNewAddress(object obj)
        {
            App.NavigationPage.Navigation.PopToRootAsync();
            App.MenuIsPresented = false;
        }

        void GoAddresses(object obj)
        {
            App.NavigationPage.Navigation.PushAsync(new AddressPage());
            App.MenuIsPresented = false;
        }

        void GoAbout(object obj)
        {
            //App.NavigationPage.Navigation.PushAsync(new AboutPage());
            App.MenuIsPresented = false;
        }
    }
}
