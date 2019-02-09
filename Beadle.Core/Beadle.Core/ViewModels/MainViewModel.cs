using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            NavigationService = navigationService;
            Repository = repository;
            SelectedSession = null;
            SelectedStudent = null;
            Task.Run(() => Init());
            AddRandomStudentCommand = new Command(async () => await AddRandomStudentProcAsync(), () => true);
            AddRandomSessionCommand = new Command(async () => await AddRandomSessionProcAsync(), () => true);
            ShowSelectedSessionCommand = new Command(async () => await ShowSelectedSessionProcAsync(), () => true);
            AddLateCommand = new Command(async () => await AddLateProcAsync(), () => true);
            AddAbsenceCommand = new Command(async () => await AddAbsenceProcAsync(), () => true);
            //ShowAddPageCommand = new Command(async () => await ShowAddPageProcAsync(), () => canShow);
        }

        //fields
        private static Random rand = new Random(DateTime.Now.Second);
        private readonly INavigationService NavigationService;
        private readonly IRepository Repository;
        private ObservableCollection<Student> _classmates;
        private List<Session> _sessions;
        private Session _selectedSession;
        private Student _selectedStudent;


        public ObservableCollection<Student> Classmates
        {
            get => _classmates;
            set
            {
                _classmates = value;
                RaisePropertyChanged(() => Classmates);

            }
        }
        public ICommand ShowAddPageCommand { get; private set; }
        public ICommand ShowSelectedSessionCommand { get; private set; }
        public ICommand AddRandomStudentCommand { get; private set; }
        public ICommand DeleteStudentCommand { get; private set; }
        public ICommand AddRandomSessionCommand { get; private set; }
        public ICommand AddLateCommand { get; private set; }
        public ICommand AddAbsenceCommand { get; private set; }
        public Session SelectedSession
        {
            get => _selectedSession;
            set
            {
                _selectedSession = value;
                RaisePropertyChanged(() => SelectedSession);

            }
        }
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                RaisePropertyChanged(() => SelectedStudent);

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


        //methods
        public async Task Init()
        {
            //updaters
            Sessions = await Repository.Session.GetItemsAsync();
            RaisePropertyChanged(() => Sessions);
            var holdsession = SelectedSession;
            var holdstudent = SelectedStudent;
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
            if (holdstudent != null)
            {
                var a = SelectedSession.Students;
                foreach (var item in a)
                {
                    if (item.Id == holdstudent.Id)
                        SelectedStudent = item;
                }
            }
            RaisePropertyChanged(() => SelectedSession);
            RaisePropertyChanged(() => SelectedStudent);
        }
        //public async Task ShowAddPageProcAsync()
        //{
        //    _navigationService.Configure("TestFrontEndHere", typeof(TestFrontEndHere));
        //    await _navigationService.NavigateAsync(nameof(TestFrontEndHere));
        //    //var stoods = new Student();
        //    //stoods.FirstName = "zal";
        //    //await App.Database.SaveItemAsync(stoods);
        //    ////await App.Database.DeleteItemAsync(SelectedStudent);
        //    ////RaisePropertyChanged(() => Classmates);
        //    //var list = await App.Database.GetItemsAsync();
        //    //Classmates = new ObservableCollection<Student>(list);
        //    RaisePropertyChanged(() => Classmates);


        //}
        public async Task AddRandomStudentProcAsync()
        {
            var stoods = new Student();
            stoods.FirstName = FirstNameGenerator();
            stoods.LastName = LastNameGenerator();
            SelectedSession.Students.Add(stoods);
            await Repository.Student.SaveItemAsync(stoods);
            await Repository.Session.UpdateWithChildrenAsync(SelectedSession);
            Task.Run(() => Init());
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
            var sesh = new Session();
            sesh.Name = SessionGenerator();
            sesh.Day = DayGenerator();
            sesh.Time = TimeGenerator();
            sesh.Students = new List<Student>();
            await Repository.Session.SaveItemAsync(sesh);
            Task.Run(() => Init());
            RaisePropertyChanged(() => SelectedSession);

        }
        public async Task AddLateProcAsync()
        {
            var holdselestude = SelectedStudent;
            SelectedStudent.Late++;
            await Repository.Student.UpdateItemAsync(SelectedStudent);    
            Task.Run(() => Init());
        }
        public async Task AddAbsenceProcAsync()
        {
            SelectedStudent.Absence++;
            await Repository.Student.UpdateItemAsync(SelectedStudent);
            Task.Run(() => Init());
        }

        public async Task ShowSelectedSessionProcAsync()
        {
            await NavigationService.NavigateAsync(nameof(TestFrontEndHere));

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
