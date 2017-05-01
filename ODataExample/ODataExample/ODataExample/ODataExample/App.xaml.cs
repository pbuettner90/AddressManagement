using ODataExample.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ODataExample.ViewModels;
using Xamarin.Forms;

namespace ODataExample
{
    public partial class App : Application
    {
        public AddressViewModel AddressViewModel { get; private set; }

        public App()
        {
            //AddressViewModel = new AddressViewModel();
            InitializeComponent();
            var menuPage = new MenuPage();
            NavigationPage = new NavigationPage(new NewAddressPage());
            //NavigationPage = new NavigationPage(new CameraPage());
            //NavigationPage = new NavigationPage(new Page1());

			RootPage = new RootPage();
            RootPage.Master = menuPage;
            RootPage.Detail = NavigationPage;
            MainPage = RootPage;
        }

        public static NavigationPage NavigationPage { get; private set; }
        private static RootPage RootPage;
        public static bool MenuIsPresented
        {
            get
            {
                return RootPage.IsPresented;
            }
            set
            {
                RootPage.IsPresented = value;
            }
        }
    }
}
