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
            Task.Run(() => Init());
            SelectedSession = null;
            SelectedPerson = null;
            ShowNoobPage = true;

            //Command Initializers
            AddRandomPersonCommand = new Command(async () => await AddRandomPersonProcAsync(), () => true);
            AddRandomSessionCommand = new Command(async () => await AddRandomSessionProcAsync(), () => true);
            AddLateCommand = new Command(async () => await AddLateProcAsync(), () => SelectedPersonIsTrue);
            AddAbsenceCommand = new Command(async () => await AddAbsenceProcAsync(), () => SelectedPersonIsTrue);
            ShowAddPersonWindowCommand = new Command(async () => await ShowAddPersonWindowProcAsync(), () => true);
            ShowAddSessionWindowCommand = new Command(async () => await ShowAddSessionWindowProcAsync(), () => true);
            ShowSessionInfoCommand = new Command(async () => await ShowSessionInfoProcAsync(), () => true);
            ShowDbPopulationCommand = new Command(async () => await ShowDbPopulationProcAsync(), () => true);
            GoBackCommand = new Command(async () => await GoBackProcAsync(), () => true);
            NextSelectedSessionCommand = new Command(async () => await NextSelectedSessionProcAsync(), () => true);


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
        private SessionInfoViewModel _sessionInfoViewModel;
        private int _population;
        private bool _showNoobPage;

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
        public ICommand ShowSessionInfoCommand { get; set; }
        public ICommand ShowDbPopulationCommand { get; set; }
        public ICommand NextSelectedSessionCommand { get; set; }
        public Session SelectedSession
        {
            get => _selectedSession;
            set
            {
                _selectedSession = value;
                if (value != null)
                {
                    SelectedSessionIsTrue = true;
                    Population = value.Persons.Count;
                    ShowNoobPage = false;

                }
                else
                {
                    SelectedSessionIsTrue = false;
                    ShowNoobPage = true;
                }
                RaisePropertyChanged(() => SelectedSession);
                RaisePropertyChanged(() => ShowNoobPage);
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
        public int Population
        {
            get => _population;
            set
            {
                _population = value;
                RaisePropertyChanged(() => Population);

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
                RaisePropertyChanged(() => AddSessionViewModel);
            }
        }
        public SessionInfoViewModel SessionInfoViewModel
        {
            get => _sessionInfoViewModel;
            set
            {
                _sessionInfoViewModel = value;
                RaisePropertyChanged(() => SessionInfoViewModel);
            }
        }


        //methods
        public async Task Init()
        {
            //updaters
            Sessions = await Repository.Session.GetAllItemsAsync();
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

            Population = Sessions.Count;
            RaisePropertyChanged(() => SelectedSession);
            RaisePropertyChanged(() => SelectedPerson);
        }

        public async Task DeleteRefresher()
        {
            Sessions = await Repository.Session.GetAllItemsAsync();
            RaisePropertyChanged(() => Sessions);
            SelectedSession = null;
            SelectedPerson = null;
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
            await Task.Run(Init);
            RaisePropertyChanged(() => SelectedSession);
            await Task.Delay(1000);
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
            await Task.Delay(1000);

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
        public async Task ShowSessionInfoProcAsync()
        {
            if (SessionInfoViewModel != null)
            {

                //SessionInfoViewModel.IsSelectedPersonTrue = false;
                SessionInfoViewModel.SelectedPerson = SelectedSession.Persons.FirstOrDefault();
            }
            await NavigationService.NavigateAsync(nameof(SessionInfoPage), true);

        }
        public async Task ShowDbPopulationProcAsync()
        {
            var persontable = await Repository.Person.GetAllItemsAsync();
            var sessiontable = await Repository.Session.GetAllItemsAsync();
            var personpopulation = "Person Table Population: " + persontable.Count.ToString();
            var sessionpopulation = "Session Table Population: " + sessiontable.Count.ToString();
            var lastperson = 0;
            var lastsession = 0;
            if (persontable.LastOrDefault() != null)
            {
                lastperson = persontable.LastOrDefault().Id;
            }

            if (sessiontable.LastOrDefault() != null)
            {
                lastsession = sessiontable.LastOrDefault().Id;

            }
            var personlastkey = "Person Table Last Id: " + lastperson;
            var sessionlastkey = "Session Table Last Id: " + lastsession;


            await Application.Current.MainPage.DisplayActionSheet("Database Info", "Cancel", null, personpopulation, personlastkey, sessionpopulation, sessionlastkey);

        }

        public async Task NextSelectedSessionProcAsync()
        {
            var b  = await Repository.Session.GetItemAsync(c => c.Id == 5);
            foreach (var session in Sessions)
            {
                if (b.Id == session.Id)
                    SelectedSession = session;
            }
            RaisePropertyChanged(() => SelectedSession);
            await Task.Run(Init);

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
        public bool ShowNoobPage
        {
            get => _showNoobPage;
            set
            {
                _showNoobPage = value;
                RaisePropertyChanged(() => ShowNoobPage);
            }
        }
        

        //dirtyworks
        public string FirstNameGenerator()
        {
            string[] FirstNames = {
                "Adam",
                "Chase",
                "Jace",
                "Ian",
                "Cooper",
                "Easton",
                "Kevin",
                "Jose",
                "Tyler",
                "Brandon",
                "Asher",
                "Jaxson",
                "Mateo",
                "Jason",
                "Ayden",
                "Zachary",
                "Carson",
                "Xavier",
                "Leo",
                "Ezra",
                "Bentley",
                "Sawyer",
                "Kayden",
                "Blake",
                "Nathaniel",
                "Ryder",
                "Theodore",
                "Elias",
                "Tristan",
                "Roman",
                "Leonardo",
                "Emma",
                "Olivia",
                "Sophia",
                "Ava",
                "Isabella",
                "Mia",
                "Abigail",
                "Emily",
                "Charlotte",
                "Harper",
                "Madison",
                "Amelia",
                "Elizabeth",
                "Sofia",
                "Evelyn",
                "Avery",
                "Chloe",
                "Ella",
                "Grace",
                "Victoria",
                "Aubrey",
                "Scarlett",
                "Zoey",
                "Addison",
                "Lily",
                "Lillian",
                "Natalie",
                "Hannah",
                "Aria",
                "Layla",
                "Brooklyn",
            };
            var random = rand.Next(0, 60);
            return FirstNames[random];
        }
        public string LastNameGenerator()
        {
            string[] lastNames = {
                "Smith",
                "Johnson",
                "Williams",
                "Brown",
                "Jones",
                "Miller",
                "Davis",
                "Garcia",
                "Rodriguez",
                "Wilson",
                "Martinez",
                "Anderson",
                "Taylor",
                "Thomas",
                "Hernandez",
                "Moore",
                "Martin",
                "Jackson",
                "Thompson",
                "White",
                "Lopez",
                "Lee",
                "Gonzalez",
                "Harris",
                "Clark",
                "Lewis",
                "Robinson",
                "Walker",
                "Perez",
                "Hall",
                "Young",
                "Allen",
                "Sanchez",
                "Wright",
                "King",
                "Scott",
                "Green",
                "Baker",
                "Adams",
                "Nelson",
                "Hill",
                "Ramirez",
                "Campbell",
                "Mitchell",
                "Roberts",
                "Carter",
                "Phillips",
                "Evans",
                "Turner",
                "Torres",
                "Parker",
                "Collins",
                "Edwards",
                "Stewart",
                "Flores",
                "Morris",
                "Nguyen",
                "Murphy",
                "Rivera",
                "Cook",
                "Rogers"};
            var random = rand.Next(0, 60);
            return lastNames[random];
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
