using Beadle.Core.Repository.LocalRepository;
using Beadle.Core.Models;
using Beadle.Core.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Beadle.Core.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Beadle.Core
{
    public partial class App : Application
    {
        //fields
        private static readonly  Locator _locator = new Locator();
        public static Locator Locator => _locator;
        private static LocalDataService database = new LocalDataService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));
        public static INavigationService Navigation { get; } = new NavigationService();
        public static LocalDataService Database
        {
            get
            {
                
                return database;
            }
        }
        //ctor

        public App()
        {
            // The root page of your application
            Navigation.Configure("MainPage", typeof(Views.MainPage));
            Navigation.Configure("AddEntityPage", typeof(Views.AddEntityPage));
            var mainPage = ((NavigationService)Navigation).SetRootPage("MainPage");

            MainPage = mainPage;
        }

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
