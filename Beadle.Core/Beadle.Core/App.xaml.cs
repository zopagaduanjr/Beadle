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


        //private static LocalDataService<Student> database = new 
        //    LocalDataService<Student>
        //    (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));

        //private static LocalDataService<Session> seshdatabase = new
        //    LocalDataService<Session>
        //    (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TodoSQLite.db3"));




        public static INavigationService Navigation { get; } = new NavigationService();
        //public static LocalDataService<Student> Database
        //{
        //    get
        //    {
        //        return database;
        //    }
        //}
        //ctor

        public App()
        {
            // The root page of your application
            //Navigation.Configure("MainPage", typeof(MainPage));
            //Navigation.Configure("AddEntityPage", typeof(AddEntityPage));
            //Navigation.Configure("TestFrontEndHere", typeof(TestFrontEndHere));
            //var mainPage = ((NavigationService)Navigation).SetRootPage("MainPage");

            var nav = new NavigationService();
            nav.Configure("MainPage", typeof(MainPage));
            nav.Configure("AddEntityPage", typeof(AddEntityPage));
            nav.Configure("TestFrontEndHere", typeof(TestFrontEndHere));
            SimpleIoc.Default.Register<INavigationService>(() => nav);

            var mainPage = ((NavigationService)nav).SetRootPage("MainPage");

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
