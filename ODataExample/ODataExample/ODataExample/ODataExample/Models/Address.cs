using System.Diagnostics;
using ODataExample.ViewModels;

namespace ODataExample.Models
{
	public class Address : BaseViewModel
    {
        string _firstName;
        string _lastName;
        string _street;
        string _city;
		string _plz;
		int _id;
        int _searchPercentage;

        public Address()
        {
           
        }

        public Address(int Id, string firstName, string lastName, string street, string city, string plz, int searchPercentage)
        {
			_id = Id;
            _firstName = firstName;
            _lastName = lastName;
            _street = street;
            _city = city;
            _plz = plz;
            _searchPercentage = searchPercentage;
        }

        public int Id
		{
			get { return _id; }
			set { SetProperty(ref _id, value); }
		}

        public int SearchPercentage
        {
            get { return _searchPercentage; }
            set { SetProperty(ref _searchPercentage, value); }
        }

        public string FirstName
        {
            get {return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        public string Plz
        {
            get {return _plz; }
            set { SetProperty(ref _plz, value); }
        }

        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        public string Street
        {
            get { return _street; }
            set { SetProperty(ref _street, value); }
        }
    }
}

