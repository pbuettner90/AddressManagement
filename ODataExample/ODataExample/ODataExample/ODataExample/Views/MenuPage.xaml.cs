using ODataExample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ODataExample.Views
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
