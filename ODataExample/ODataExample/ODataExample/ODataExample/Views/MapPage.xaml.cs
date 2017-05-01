using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ODataExample.Models;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace ODataExample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage(Address address)
		{
			InitializeComponent();

		    Task.Run(async () =>
		    {
		        try
		        {
		            await ShowMap(address);
		        }

		        catch (Exception ex)

		        {
		            Debug.WriteLine(ex.Message);
		        }
		    });
        }

		async Task ShowMap(Address address)
		{
			var geocoder = new Geocoder();

			try
			{
				var positions = await geocoder.GetPositionsForAddressAsync(address.Street + "" + address.City);

				if (positions.Count() > 0)
				{
					var pos = positions.First();

					MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(5000)));
				}
			}

			catch (Exception)
			{
				await DisplayAlert("Suche nach Addresse", "Adresse wurde nicht gefunden", "OK");
			}
		}
	}
}
