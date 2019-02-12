using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Beadle.Core.Models;
using Beadle.Core.Repository;
using Beadle.Core.Services;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace Beadle.Core.ViewModels
{
    public class AddSessionViewModel : ViewModelBase
    {

        //constructor
        public AddSessionViewModel(IRepository repository, MainViewModel mainViewModel, INavigationService navigationService)
        {
            SelectedFieldsIsTrue = false;
            mainViewModel.AddSessionViewModel = this;
            AddSessionCommand = new Command(async () => await AddSessionProcAsync(), () => SelectedFieldsIsTrue);
            MainViewModel = mainViewModel;
            Repository = repository;
            NavigationService = navigationService;
        }

        //private fields
        private readonly INavigationService NavigationService;
        private MainViewModel _mainViewModel;
        private readonly IRepository Repository;
        private bool _selectedFieldsIsTrue;
        private string _name;
        private string _day;
        private string _time;

        //properties
        public ICommand AddSessionCommand { get; private set; }
        public MainViewModel MainViewModel
        {
            get => _mainViewModel;
            set
            {
                _mainViewModel = value;
                RaisePropertyChanged(nameof(MainViewModel));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (value != null)
                    SelectedFieldsIsTrue = true;
                _name = value;
                RaisePropertyChanged(nameof(Name));

            }
        }
        public string Day
        {
            get => _day;
            set
            {
                _day = value;
                RaisePropertyChanged(nameof(Day));
            }
        }
        public string Time
        {
            get => _time;
            set
            {
                _time = value;
                RaisePropertyChanged(nameof(Time));
            }
        }

        //methods
        public async Task Init()
        {
            //updaters
            MainViewModel.Sessions = await Repository.Session.GetAllItemsAsync();
            RaisePropertyChanged(() => MainViewModel.Sessions);
            var holdsession = MainViewModel.SelectedSession;
            var holdperson = MainViewModel.SelectedPerson;
            //highlighters
            if (holdsession != null)
            {
                foreach (var session in MainViewModel.Sessions)
                {
                    if (session.Id == holdsession.Id)
                        MainViewModel.SelectedSession = session;
                }
            }
            //highlighters
            if (holdperson != null)
            {
                var a = MainViewModel.SelectedSession.Persons;
                foreach (var item in a)
                {
                    if (item.Id == holdperson.Id)
                        MainViewModel.SelectedPerson = item;
                }
            }
            RaisePropertyChanged(() => MainViewModel.SelectedSession);
            RaisePropertyChanged(() => MainViewModel.SelectedPerson);
        }
        public async Task AddSessionProcAsync()
        {
            //to avoid multipressing the button
            SelectedFieldsIsTrue = false;
            await NavigationService.GoBack();
            var session = new Session();
            Regex rgx = new Regex("[^a-zA-Z]");
            session.Name = Name;
            //only adding Session Name, tbd sched
            if (Day == null || Time == null)
            {
                session.Day = "tbd";
                session.Time = "tbd";
            }
            else
            {
                session.Day = rgx.Replace(Day, "");
                session.Time = Time;
            }
            await Repository.Session.SaveItemAsync(session);
            await Task.Run(() => Init());
            RaisePropertyChanged(() => MainViewModel.SelectedSession);
        }
        //canclick
        public bool SelectedFieldsIsTrue
        {
            get => _selectedFieldsIsTrue;
            set
            {
                _selectedFieldsIsTrue = value;
                RaisePropertyChanged(nameof(SelectedFieldsIsTrue));
            }
        }
    }
}
