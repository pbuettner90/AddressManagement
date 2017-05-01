using System.ComponentModel;
using System.Runtime.CompilerServices;
using ODataExample.ViewModels;
using Xamarin.Forms;

namespace ODataExample.Models
{
    public class Photo : BaseViewModel
    {
        ImageSource _photoUrl;
        double _photoGpsLatitude;
        double _photoGpsLongitude;

        public Photo()
        {

        }

        public Photo(ImageSource photoUrl, double photoGpsLatitude, double photoGpsLongitude)
        {
            _photoUrl = photoUrl;
            _photoGpsLatitude = photoGpsLatitude;
            _photoGpsLongitude = photoGpsLongitude;
        }

        public ImageSource PhotoUrl
        {
            get {return _photoUrl; }
            set { SetProperty(ref _photoUrl, value); }
        }

        public double PhotoGpsLatitude
        {
            get {return _photoGpsLatitude; }
            set { SetProperty(ref _photoGpsLatitude, value); }
        }

        public double PhotoGpsLongitude
        {
            get { return _photoGpsLongitude; }
            set { SetProperty(ref _photoGpsLongitude, value); }
        }
    }
}