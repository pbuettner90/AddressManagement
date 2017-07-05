using System.Threading.Tasks;
using AddressManagement.Helper;
using AddressManagement.Views;

namespace AddressManagement.ViewModels
{
	public class BaseViewModel : ObservableObject
	{
		private bool _isBusy;

		public bool IsBusy
		{
			get { return _isBusy; }
			set { SetProperty(ref _isBusy, value); }
		}
	}
}
