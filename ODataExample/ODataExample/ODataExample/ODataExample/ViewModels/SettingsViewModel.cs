using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ODataExample.ViewModels;
using Xamarin.Forms;

namespace ODataExample
{
	public class SettingsViewModel : BaseViewModel
	{
		public Command SettingsCommand { get; set; }

		public SettingsViewModel()
		{
			SettingsCommand = new Command(async () => await SaveSettings());
		}

		public string ApiKey
		{
			get { return Settings.ApiKey; }
			set
			{
				if (Settings.ApiKey == value)
					return;

				Settings.ApiKey = value;
				OnPropertyChanged();
			}
		}

	    public string Url
	    {
	        get { return Settings.Url; }
	        set
	        {
	            if (Settings.Url == value)
	                return;

	            Settings.Url = value;
	            OnPropertyChanged();
	        }
	    }

        async Task SaveSettings()
		{
			MessagingCenter.Send(this, "NavigateToNewPage", "NewAddressPage");
		}
	}
}
