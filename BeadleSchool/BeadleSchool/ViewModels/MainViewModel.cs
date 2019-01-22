using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BeadleSchool.Models;
using BeadleSchool.Services;
using BeadleSchool.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

namespace BeadleSchool.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private static Random rand = new Random(DateTime.Now.Second);
        //ctor
        public MainViewModel(IBeadleService beadleService)
        {
            if (beadleService == null) throw new ArgumentNullException("beadleService");
            _beadleService = beadleService;
            _navigationService = App.Navigation;
            SelectedStudent = null;
            Task.Run(() => Init());
            AddRandomStudentCommand = new Command(async () => await AddRandomStudentProcAsync(), () => canShow);
            ShowAddPageCommand = new Command(async () => await ShowAddPageProcAsync(), () => canShow1);
            DeleteStudentCommand = new Command(async () => await DeleteStudentProcAsync(), () => canShow2);
        }

        //fields
        private IBeadleService _beadleService;
        private INavigationService _navigationService;
        private Student _selectedStudent;
        private RelayCommand _navigateCommand;
        private string _selectedFullName;

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
        public ICommand AddRandomStudentCommand { get; private set; }
        public ICommand DeleteStudentCommand { get; private set; }
        bool canShow = true;
        bool canShow1 = true;
        bool canShow2 = true;
        private ObservableCollection<Student> _classmates;


        public string SelectedFullName
        {
            get => _selectedFullName;
            set
            {
                _selectedFullName = value;
                RaisePropertyChanged(nameof(SelectedFullName));
            }
        }
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                if (value != null)
                {
                    _selectedFullName = value.FullName;
                }
                RaisePropertyChanged(nameof(SelectedStudent));
                RaisePropertyChanged(nameof(Classmates));
                RaisePropertyChanged(nameof(SelectedFullName));
            }
        }

        //methods
        public async Task Init()
        {
            if (Classmates != null) return;
            var list = await App.Database.GetItemsAsync();
            Classmates = new ObservableCollection<Student>(list);
            //Classmates = new ObservableCollection<Student>(await _beadleService.GetStudent());
            RaisePropertyChanged(() => Classmates);
            RaisePropertyChanged(() => SelectedStudent);



        }
        public async Task ShowAddPageProcAsync()
        {
            await _navigationService.NavigateAsync(nameof(AddEntityPage));
            //var stoods = new Student();
            //stoods.FirstName = "zal";
            //await App.Database.SaveItemAsync(stoods);
            ////await App.Database.DeleteItemAsync(SelectedStudent);
            ////RaisePropertyChanged(() => Classmates);
            //var list = await App.Database.GetItemsAsync();
            //Classmates = new ObservableCollection<Student>(list);
            //RaisePropertyChanged(() => Classmates);


        }
        public async Task AddRandomStudentProcAsync()
        {
            var stoods = new Student();
            stoods.FirstName = FirstNameGenerator();
            stoods.LastName = LastNameGenerator();
            await App.Database.SaveItemAsync(stoods);
            //autorefresh list
            //var list = await App.Database.GetItemsAsync();
            //Classmates = new ObservableCollection<Student>(list);
            //Classmates = new ObservableCollection<Student>(await _beadleService.GetStudent());
            var list = await App.Database.GetItemsAsync();
            Classmates = new ObservableCollection<Student>(list);
            RaisePropertyChanged(() => Classmates);
        }

        public async Task DeleteStudentProcAsync()
        {
            await App.Database.DeleteItemAsync(SelectedStudent);
            //autorefresh list
            //var list = await App.Database.GetItemsAsync();
            //Classmates = new ObservableCollection<Student>(list);
            //Classmates = new ObservableCollection<Student>(await _beadleService.GetStudent());
            var list = await App.Database.GetItemsAsync();
            Classmates = new ObservableCollection<Student>(list);
            RaisePropertyChanged(() => Classmates);
            RaisePropertyChanged(() => SelectedStudent);


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

    }
}
