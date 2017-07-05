using System;
using System.Diagnostics;
using AddressManagement.ViewModels;

namespace AddressManagement.Models
{
	public class Address : BaseViewModel
	{
		private string _firstName;
	    private string _lastName;
	    private string _street;
	    private string _city;
	    private string _plz;
	    private int _id;
	    private int _searchPercentage;

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
			get { return _firstName; }
			set { SetProperty(ref _firstName, value); }
		}

		public string LastName
		{
			get { return _lastName; }
			set { SetProperty(ref _lastName, value); }
		}

		public string Plz
		{
			get { return _plz; }
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

		public bool Compare(Address addr, string search)
		{
			return addr.FirstName.Contains(search)
					   || addr.LastName.Contains(search)
					   || addr.City.Contains(search)
					   || addr.Street.Contains(search)
					   || addr.Plz.Contains(search);
		}
	}
}

