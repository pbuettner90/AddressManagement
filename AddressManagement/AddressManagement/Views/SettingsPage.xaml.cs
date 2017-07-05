using System;
using System.Collections.Generic;
using System.Diagnostics;
using AddressManagement.ViewModels;
using Xamarin.Forms;

namespace AddressManagement.Views
{
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			Title = "Einstellungen";
			InitializeComponent();
			BindingContext = new SettingsViewModel();
		}

		void SetInternalWebService(object sender, ToggledEventArgs e)
		{
			if (e.Value == true)
			{
				lblWebService.Text = "Externer Webservice";
				Settings.InternalWebservice = true;
			}

			else 
			{
				lblWebService.Text = "Interner Webservice";
				Settings.InternalWebservice = false;
			}
		}
	}
}
