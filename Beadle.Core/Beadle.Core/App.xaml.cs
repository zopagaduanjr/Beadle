using System;
using System.IO;
using Beadle.Core.Models;
using Beadle.Core.Repository.LocalRepository;
using Beadle.Core.Services;
using Beadle.Core.Views;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Beadle.Core
{
    public partial class App : Application
    {
        //fields
        private static readonly Locator _locator = new Locator();
        public static Locator Locator => _locator;

        //ctor
        public App()
        {
            //MainPage = new MasterPage();
            ////WORKING MVVM NAVIGATION USING IOC, BUT LIMITED TO PUSH PAGES, NOT YET MASTER DETAIL
            var nav = new NavigationService();
            nav.Configure("MasterPage", typeof(MasterPage));
            nav.Configure("DashboardPage", typeof(DashboardPage));
            nav.Configure("AddPersonPage", typeof(AddPersonPage));
            nav.Configure("AddSessionPage", typeof(AddSessionPage));
            nav.Configure("SessionInfoPage", typeof(SessionInfoPage));
            nav.Configure("RecordInfoPage", typeof(RecordInfoPage));
            SimpleIoc.Default.Register<INavigationService>(() => nav);
            var mainPage = ((NavigationService)nav).SetRootPage("MasterPage");
            MainPage = mainPage;

        }
        //methods
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

}
