using Beadle.Core.Models;
using Beadle.Core.Repository;
using Beadle.Core.Services;
using Beadle.Core.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Beadle.Core.Repository.LocalRepository;
using Xamarin.Forms;

namespace Beadle.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //ctor
        public MainViewModel(IBeadleService beadleService)
        {
            if (beadleService == null) throw new ArgumentNullException("beadleService");
            _beadleService = beadleService;
            _navigationService = App.Navigation;
            SelectedStudent = null;
            Task.Run(() => Init());
            ShowAddPageCommand = new Command(async () => await ShowAddPageProcAsync(), () => canShow);
        }

        //fields
        private  IBeadleService _beadleService;
        private  INavigationService _navigationService;
        private Student _selectedStudent;
        private RelayCommand _navigateCommand;
        private string _listTitle;
        public List<Student> Classmates { get; set; }
        public List<Student> NewClassmates { get; set; }
        public ICommand ShowAddPageCommand { get; private set; }
        bool canShow = true;
        public string ListTitle
        {
            get => _listTitle;
            set
            {
                _listTitle = value;
                RaisePropertyChanged(nameof(ListTitle));
            }
        }
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                if (value != null) _listTitle = value.FullName;
                RaisePropertyChanged(nameof(SelectedStudent));
                RaisePropertyChanged(nameof(ListTitle));
            }
        }

        //methods
        public async Task Init()
        {
            if (Classmates != null) return;
            Classmates = new List<Student>(await App.Database.GetItemsAsync());
            //Classmates = new ObservableCollection<Student>(await _beadleService.GetStudent());       
            var asdf = Classmates;
            RaisePropertyChanged(() => Classmates);
            var asdflf = Classmates;


        }
        public async Task ShowAddPageProcAsync()
        {
            //await _navigationService.NavigateAsync(nameof(AddEntityPage));
            //await App.Database.SaveItemAsync(new Student("flame", "guard"));
            //RaisePropertyChanged(() => Classmates);
            Classmates = new List<Student>(await App.Database.GetItemsAsync());
            //Classmates = new ObservableCollection<Student>(await _beadleService.GetStudent());       
            var asdf = Classmates;
            RaisePropertyChanged(() => Classmates);
            var asdflf = Classmates;


        }



        //dirtyworks

    }
}
