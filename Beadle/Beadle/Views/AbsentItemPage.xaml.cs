using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beadle.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beadle.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AbsentItemPage : ContentPage
	{
		public AbsentItemPage ()
		{
			InitializeComponent ();
		}
	    async void OnSaveClicked(object sender, EventArgs e)
	    {
	        var absentItem = (AbsentItem)BindingContext;
	        await App.Database.SaveItemAsync(absentItem);
	        await Navigation.PopAsync();
	    }

	    async void OnDeleteClicked(object sender, EventArgs e)
	    {
	        var absentItem = (AbsentItem)BindingContext;
	        await App.Database.DeleteItemAsync(absentItem);
	        await Navigation.PopAsync();
	    }

	    async void OnCancelClicked(object sender, EventArgs e)
	    {
	        await Navigation.PopAsync();
	    }

    }
}