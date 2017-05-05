using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ODataExample.Controls
{
	public class AddressCell : ViewCell
	{
		public static readonly BindableProperty IdProperty = BindableProperty.Create("Id", typeof(int), typeof(AddressCell), defaultValue:0);
		public static readonly BindableProperty FirstNameProperty = BindableProperty.Create("FirstName", typeof(string), typeof(AddressCell));
		public static readonly BindableProperty LastNameProperty = BindableProperty.Create("LastName", typeof(string), typeof(AddressCell));
		public static readonly BindableProperty StreetProperty = BindableProperty.Create("Street", typeof(string), typeof(AddressCell));
		public static readonly BindableProperty CityProperty = BindableProperty.Create("City", typeof(string), typeof(AddressCell));
		public static readonly BindableProperty PlzProperty = BindableProperty.Create("Plz", typeof(string), typeof(AddressCell));

		public int Id
		{
			get
			{
				return (int)GetValue(IdProperty);
			}

			set
			{
				SetValue(IdProperty, value);
			}
		}

		public string FirstName
		{
			get
			{
				return (string)GetValue(FirstNameProperty);
			}

			set
			{
				SetValue(FirstNameProperty, value);
			}
		}

		public string LastName
		{
			get
			{
				return (string)GetValue(LastNameProperty);
			}

			set
			{
				SetValue(LastNameProperty, value);
			}
		}


		public string Street
		{
			get
			{
				return (string)GetValue(StreetProperty);
			}

			set
			{
				SetValue(StreetProperty, value);
			}
		}

		public string City
		{
			get
			{
				return (string)GetValue(CityProperty);
			}

			set
			{
				SetValue(CityProperty, value);
			}
		}

		public string Plz
		{
			get
			{
				return (string)GetValue(PlzProperty);
			}

			set
			{
				SetValue(PlzProperty, value);
			}
		}


		Label lFirstName, lLastName, lStreet, lCity, lPlz;
		Button btnAddressMatch;

		public AddressCell()
		{
			lFirstName = new Label();
			lFirstName.FontSize = 13;
			lFirstName.VerticalOptions = LayoutOptions.Center;

			lStreet = new Label();
			lStreet.FontSize = 13;
			lStreet.VerticalOptions = LayoutOptions.Center;

			lCity = new Label();
			lCity.FontSize = 13;
			lCity.VerticalOptions = LayoutOptions.Center;

			btnAddressMatch = new Button();
			btnAddressMatch.BackgroundColor = Color.Transparent;
			btnAddressMatch.Text = "Addresse zuordnen";
			btnAddressMatch.Clicked += BtnMatchToAddress;


			Grid infoLayout = new Grid
			{
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
				}
			};

			infoLayout.Children.Add(lFirstName, 0, 0);
			infoLayout.Children.Add(lStreet, 1, 0);
			infoLayout.Children.Add(lCity, 2, 0);
			infoLayout.Children.Add(btnAddressMatch, 3, 0);

			View = infoLayout;
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (BindingContext != null)
			{
				lFirstName.Text = FirstName;
				lCity.Text = City;
				lStreet.Text = Street;
			}
		}


		async void BtnMatchToAddress(object sender, EventArgs e)
		{
			await Application.Current.MainPage.DisplayAlert("ID", "ID: " + Id, "OK");
		}
	}

}

