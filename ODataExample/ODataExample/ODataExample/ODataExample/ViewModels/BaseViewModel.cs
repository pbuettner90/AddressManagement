using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ODataExample.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		static bool _isBusy;

		public bool IsBusy
		{
			get { return _isBusy; }
			set { SetProperty(ref _isBusy, value); }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string name = null) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

	    protected bool SetProperty<T>(ref T storage, T value,
	        [CallerMemberName] String propertyName = null)
	    {
	        if (Equals(storage, value)) return false;
	        storage = value;
	        OnPropertyChanged(propertyName);
	        return true;
	    }
    }
}
