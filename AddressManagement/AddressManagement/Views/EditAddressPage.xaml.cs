using System;
using System.Collections.Generic;
using AddressManagement.Models;
using AddressManagement.ViewModels;
using Xamarin.Forms;

namespace AddressManagement.Views
{
	public partial class EditAddressPage : ContentPage
	{
		public EditAddressPage(Address address)
		{
			Title = "Datensatz bearbeiten";
			InitializeComponent();
			BindingContext = new EditAddressViewModel(address);
		}
	}
}

