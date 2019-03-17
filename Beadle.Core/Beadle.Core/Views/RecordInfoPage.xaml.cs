using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Beadle.Core.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecordInfoPage : ContentPage
	{
		public RecordInfoPage ()
		{
			InitializeComponent ();
            BindingContext = App.Locator.RecordInfo;

        }
    }
}