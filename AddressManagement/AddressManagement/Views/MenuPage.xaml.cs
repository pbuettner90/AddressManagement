using System;
using System.Collections.Generic;
using AddressManagement.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AddressManagement.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
		public MenuPage()
		{
 			BindingContext = new MenuPageViewModel();
			Title = "Menu";
			InitializeComponent();		
		}
	}
}
