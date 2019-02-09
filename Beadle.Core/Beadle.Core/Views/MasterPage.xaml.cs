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
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.Main;
            NavigationPage.SetHasNavigationBar(this, false);
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(DashboardPage)));

        }

    }
}