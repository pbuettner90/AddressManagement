﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AddressManagement.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RootPage : MasterDetailPage
	{
		public RootPage()
		{
			InitializeComponent();
			MasterBehavior = MasterBehavior.Popover;
		}
	}
}
