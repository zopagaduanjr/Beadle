using System;
using System.IO;
using BeadleSchool.Models;
using BeadleSchool.Repository.LocalRepository;
using BeadleSchool.Services;
using BeadleSchool.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BeadleSchool
{
    public partial class App : Application
    {
        //fields
        private static readonly Locator _locator = new Locator();
        public static Locator Locator => _locator;
        private static LocalDataService<Student> database = new LocalDataService<Student>(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));
        public static INavigationService Navigation { get; } = new NavigationService();
        public static LocalDataService<Student> Database
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
            Navigation.Configure("MainPage", typeof(MainPage));
            Navigation.Configure("AddEntityPage", typeof(AddEntityPage));
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
