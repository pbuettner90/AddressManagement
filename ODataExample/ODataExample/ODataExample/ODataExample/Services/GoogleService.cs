using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ODataExample.Services
{
    public class GoogleService
    {
        public static async Task<string> RequestGoogle(string url)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }

            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Internetverbindung erforderlich", "Internetverbindung konnte nicht hergestellt werden", "OK");

                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}