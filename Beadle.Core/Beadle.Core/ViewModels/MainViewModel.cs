using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Beadle.Core.Models;
using Beadle.Core.Repository;
using Beadle.Core.Repository.LocalRepository;
using Beadle.Core.Services;
using Beadle.Core.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace Beadle.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //ctor
        public MainViewModel(IRepository repository, INavigationService navigationService)
        {
            //IOC getters
            NavigationService = navigationService;
            Repository = repository;

            //initializers
            SelectedSession = null;
            SelectedPerson = null;
            Task.Run(() => Init());


            //Command Initializers
            AddRandomPersonCommand = new Command(async () => await AddRandomPersonProcAsync(), () => true);
            AddRandomSessionCommand = new Command(async () => await AddRandomSessionProcAsync(), () => true);
            AddLateCommand = new Command(async () => await AddLateProcAsync(), () => SelectedPersonIsTrue);
            AddAbsenceCommand = new Command(async () => await AddAbsenceProcAsync(), () => SelectedPersonIsTrue);
            ShowAddPersonWindowCommand = new Command(async () => await ShowAddPersonWindowProcAsync(), () => true);
            ShowAddSessionWindowCommand = new Command(async () => await ShowAddSessionWindowProcAsync(), () => true);
            ShowSettingsCommand = new Command(async () => await ShowSettingsProcAsync(), () => true);
            GoBackCommand = new Command(async () => await GoBackProcAsync(), () => true);


            //codebehind transfer here

        }

        //backing fields
        private static Random rand = new Random(DateTime.Now.Second);
        private readonly INavigationService NavigationService;
        private readonly IRepository Repository;
        private ObservableCollection<Student> _classmates;
        private List<Session> _sessions;
        private Session _selectedSession;
        private bool _selectedSessionIsTrue;
        private Person _selectedPerson;
        private bool _selectedPersonIsTrue;
        private AddPersonViewModel _addPersonViewModel;
        private AddSessionViewModel _addSessionViewModel;

        //properties
        public ObservableCollection<Student> Classmates
        {
            get => _classmates;
            set
            {
                _classmates = value;
                RaisePropertyChanged(() => Classmates);

            }
        }
        public ICommand AddRandomPersonCommand { get; private set; }
        public ICommand DeleteStudentCommand { get; private set; }
        public ICommand AddRandomSessionCommand { get; private set; }
        public ICommand AddLateCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }
        public ICommand AddAbsenceCommand { get; private set; }
        public ICommand ShowAddPersonWindowCommand { get; set; }
        public ICommand ShowAddSessionWindowCommand { get; set; }
        public ICommand ShowSettingsCommand { get; set; }
        public Session SelectedSession
        {
            get => _selectedSession;
            set
            {
                _selectedSession = value;
                if (value != null)
                    SelectedSessionIsTrue = true;
                RaisePropertyChanged(() => SelectedSession);
                RaisePropertyChanged(() => SelectedSessionIsTrue);
            }
        }
        public List<Session> Sessions
        {
            get => _sessions;
            set
            {
                _sessions = value;
                RaisePropertyChanged(() => Sessions);
            }
        }
        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                if (value != null)
                    SelectedPersonIsTrue = true;

                RaisePropertyChanged(() => SelectedPerson);
                RaisePropertyChanged(() => SelectedPersonIsTrue);


            }
        }

        public AddPersonViewModel AddPersonViewModel
        {
            get => _addPersonViewModel;
            set
            {
                _addPersonViewModel = value;
                RaisePropertyChanged(() => AddPersonViewModel);
            }
        }

        public AddSessionViewModel AddSessionViewModel
        {
            get => _addSessionViewModel;
            set
            {
                _addSessionViewModel = value;
                RaisePropertyChanged(() => AddPersonViewModel);
            }
        }

        //methods
        public async Task Init()
        {
            //updaters
            Sessions = await Repository.Session.GetItemsAsync();
            RaisePropertyChanged(() => Sessions);
            var holdsession = SelectedSession;
            var holdperson = SelectedPerson;
            //highlighters
            if (holdsession != null)
            {
                foreach (var session in Sessions)
                {
                    if (session.Id == holdsession.Id)
                        SelectedSession = session;                        
                }
            }
            //highlighters
            if (holdperson != null)
            {
                var a = SelectedSession.Persons;
                foreach (var item in a)
                {
                    if (item.Id == holdperson.Id)
                        SelectedPerson = item;
                }
            }
            RaisePropertyChanged(() => SelectedSession);
            RaisePropertyChanged(() => SelectedPerson);
        }
        //public async Task DeleteStudentProcAsync()
        //{
        //    await App.Database.DeleteItemAsync(SelectedStudent);
        //    //autorefresh list
        //    var list = await App.Database.GetItemsAsync();
        //    Classmates = new ObservableCollection<Student>(list);
        //    //Classmates = new ObservableCollection<Student>(await _beadleService.GetStudent());
        //    //var list = await App.Database.GetItemsAsync();
        //    //Classmates = new ObservableCollection<Student>(list);
        //    RaisePropertyChanged(() => Classmates);
        //    RaisePropertyChanged(() => SelectedStudent);
        //    Task.Run(() => Init());
        //}

        public async Task AddRandomSessionProcAsync()
        {
            var session = new Session();
            session.Name = SessionGenerator();
            session.Day = DayGenerator();
            session.Time = TimeGenerator();
            session.Persons = new List<Person>();
            await Repository.Session.SaveItemAsync(session);
            await Task.Run(() => Init());
            RaisePropertyChanged(() => SelectedSession);
        }
        public async Task AddRandomPersonProcAsync()
        {
            var person = new Person();
            person.FirstName = FirstNameGenerator();
            person.LastName = LastNameGenerator();
            SelectedSession.Persons.Add(person);
            await Repository.Person.SaveItemAsync(person);
            await Repository.Session.UpdateWithChildrenAsync(SelectedSession);
            await Task.Run(() => Init());
        }
        public async Task AddLateProcAsync()
        {
            SelectedPerson.Late++;
            await Repository.Person.UpdateItemAsync(SelectedPerson);
            await Task.Run(() => Init());
        }
        public async Task AddAbsenceProcAsync()
        {
            SelectedPerson.Absence++;
            await Repository.Person.UpdateItemAsync(SelectedPerson);
            await Task.Run(() => Init());
        }
        public async Task ShowAddPersonWindowProcAsync()
        {
            if (AddPersonViewModel != null)
            {
                AddPersonViewModel.LastName = null;
                AddPersonViewModel.FirstName = null;
            }
            await NavigationService.NavigateAsync(nameof(AddPersonPage),true);
        }
        public async Task GoBackProcAsync()
        {
            await NavigationService.NavigateAsync(nameof(MasterPage));
        }

        public async Task ShowAddSessionWindowProcAsync()
        {
            if (AddSessionViewModel != null)
            {
                AddSessionViewModel.Name = null;
                AddSessionViewModel.Day = null;
                AddSessionViewModel.Time = null;
            }
            await NavigationService.NavigateAsync(nameof(AddSessionPage), true);
        }

        public async Task ShowSettingsProcAsync()
        {
            await Application.Current.MainPage.DisplayActionSheet("Sort Options", "Cancel", null, "By Approval Due Date", "Meeting Date", "Meeting Type");
        }

        //canclicks
        public bool SelectedSessionIsTrue
        {
            get => _selectedSessionIsTrue;
            set
            {
                _selectedSessionIsTrue = value;
                RaisePropertyChanged(() => SelectedSessionIsTrue);
            }
        }
        public bool SelectedPersonIsTrue
        {
            get => _selectedPersonIsTrue;
            set
            {
                _selectedPersonIsTrue = value;
                RaisePropertyChanged(() => SelectedPersonIsTrue);

            }
        }



        //dirtyworks
        public string FirstNameGenerator()
        {
            string[] maleNames = { "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolfo", "adolph", "adrian" };
            var random = rand.Next(0, 10);
            return maleNames[random];
        }
        public string LastNameGenerator()
        {
            string[] maleNames = { "Dimitri", "Cousins", "Humera", "Workman","Pooja", "Wainwright",
                "Will", "Regan"};
            var random = rand.Next(0, 8);
            return maleNames[random];
        }
        public string SessionGenerator()
        {
            string[] maleNames = { "Math", "History", "Filipino", "MAPEH","HEKASI", "Work Education",
                "Chinese", "Nihongo"};
            var random = rand.Next(0, 8);
            return maleNames[random];
        }
        public string DayGenerator()
        {
            string[] maleNames = { "Monday", "Tuesday", "Wednesday", "Thursday","Friday", "Saturday",
                "Sunday"};
            var random = rand.Next(0, 7);
            return maleNames[random];
        }
        public string TimeGenerator()
        {
            string[] maleNames = { "7:00 am - 8:00 am", "8:00 am - 9:00 am", "9:00 am - 10:00 am", "11:00 am - 12:00 pm","1:00 pm - 2:00 pm", "2:00 pm - 3:00 pm",
                "3:00 pm - 4:00 pm"};
            var random = rand.Next(0, 7);
            return maleNames[random];
        }

    }
}
