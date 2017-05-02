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

			lFirstName = new Label
			{
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.Center,
				FontSize = 13
			};

			lLastName = new Label
			{
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.Center,
				FontSize = 13
			};

			lStreet = new Label
			{
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.Center,
				FontSize = 13
			};

			lCity = new Label
			{
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.Center,
				FontSize = 13
			};

			lPlz = new Label
			{
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.Center,
				FontSize = 13
			};

			btnAddressMatch = new Button
			{
				Text = "Addresse zuordnen",
				BackgroundColor = Color.Transparent,
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.Center,
				FontSize = 13
			};

			btnAddressMatch.Clicked += BtnMatchToAddress;

			Grid infoLayout = new Grid
			{
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) }

				},
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			infoLayout.Children.Add(lFirstName, 0, 0);
			infoLayout.Children.Add(lLastName, 1, 0);
			infoLayout.Children.Add(lStreet, 2, 0);
			infoLayout.Children.Add(lCity, 3, 0);
			infoLayout.Children.Add(btnAddressMatch, 4, 0);

			var cellWrapper = new Grid
			{
				Padding = 10,
				ColumnDefinitions =
				{
					new ColumnDefinition { Width = new GridLength(1,GridUnitType.Auto) },
					new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) },
				}
			};


			cellWrapper.Children.Add(infoLayout, 1, 0);

			View = cellWrapper;
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

