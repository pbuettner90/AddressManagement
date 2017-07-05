using System.Threading.Tasks;
using Xamarin.Forms;

namespace AddressManagement.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		public Command SettingsCommand { get; set; }

		public string InternalWebServiceUrl { get; set; }
		public string ExternalWebServiceUrl { get; set; }

		public SettingsViewModel()
		{
			SettingsCommand = new Command(async () => await SaveSettings());

			InternalWebServiceUrl = "http://10.50.11.132:8082/odata";
			ExternalWebServiceUrl = "https://addressodata20170508023216.azurewebsites.net/odata";

			SetUrlWebService();
		}

		public void SetUrlWebService()
		{
			if (Settings.InternalWebservice == true)
			{
				Url = InternalWebServiceUrl;
				Settings.Url = Url;
			}

			else
			{
				Url = ExternalWebServiceUrl;
				Settings.Url = Url;
			}
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

		public bool InternalWebservice 
		{
			get { return Settings.InternalWebservice; }

			set
			{
				if (Settings.InternalWebservice == value)
					return;

				Settings.InternalWebservice = value;
				OnPropertyChanged();
			}
		}

		private async Task SaveSettings()
		{
			await App.NavigationPage.Navigation.PopAsync();
		}
	}
}

