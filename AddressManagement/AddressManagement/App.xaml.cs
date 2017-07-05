using System.Diagnostics;
using AddressManagement.Views;
using Xamarin.Forms;

namespace AddressManagement
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			var menuPage = new MenuPage();
			NavigationPage = new NavigationPage(new NewAddressPage());

			RootPage = new RootPage();
			RootPage.Master = menuPage;
			RootPage.Detail = NavigationPage;
			MainPage = RootPage;
		}

		public static NavigationPage NavigationPage { get;  set; }
		 static RootPage RootPage;
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