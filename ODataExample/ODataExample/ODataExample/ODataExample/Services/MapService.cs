using System;
using System.Threading.Tasks;
using ODataExample.Models;
using ODataExample.Views;

namespace ODataExample.Services
{
	public class MapService
	{
		public static async Task ShowMap(Address address)
		{
			await App.NavigationPage.Navigation.PushAsync(new MapPage(address));
		}
	}
}
