using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Beadle.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beadle.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AbsentListPage : ContentPage
	{
		public AbsentListPage ()
		{
			InitializeComponent ();
		}
	    protected override async void OnAppearing()
	    {
	        base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
	        //((App)App.Current).ResumeAtTodoId = -1;
            listView.ItemsSource = await App.Database.GetItemsAsync();
	    }

	    async void OnItemAdded(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new AbsentItemPage
	        {
	            BindingContext = new AbsentItem()
	        });
	    }

	    async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        //((App)App.Current).ResumeAtTodoId = (e.SelectedItem as TodoItem).ID;
	        //Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as TodoItem).ID);
	        if (e.SelectedItem != null)
	        {
	            await Navigation.PushAsync(new AbsentItemPage
                {
	                BindingContext = e.SelectedItem as AbsentItem
                });
	        }
	    }
    }
}