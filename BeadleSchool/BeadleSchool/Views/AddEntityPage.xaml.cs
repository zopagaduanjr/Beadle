﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeadleSchool.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEntityPage : ContentPage
	{
		public AddEntityPage ()
		{
			InitializeComponent ();
		    BindingContext = App.Locator.Main;

        }
    }
}