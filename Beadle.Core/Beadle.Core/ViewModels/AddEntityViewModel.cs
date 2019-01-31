using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Beadle.Core.Models;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace Beadle.Core.ViewModels
{
    public class AddEntityViewModel : ViewModelBase
    {

        //constructor


        private string _firstName;
        private MainViewModel _mainViewModel;

        public AddEntityViewModel(MainViewModel mainViewModel)
        {
            //AddEntityCommand = new Command(async () => await AddEntityProcAsync(), () => true);
            _mainViewModel = mainViewModel;
        }

        //fields
        public MainViewModel MainViewModel
        {
            get => _mainViewModel;
            set => _mainViewModel = value;
        }

        public ICommand AddEntityCommand { get; private set; }
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                RaisePropertyChanged(nameof(FirstName));

            }
        }

        //methods
        //public async Task AddEntityProcAsync()
        //{
        //    var stoods = new Student();
        //    stoods.FirstName = FirstName;
        //    await App.Database.SaveItemAsync(stoods);
        //    //autorefresh list
        //    var list = await App.Database.GetItemsAsync();
        //    MainViewModel.Classmates = new ObservableCollection<Student>(list);
        //    //Classmates = new ObservableCollection<Student>(await _beadleService.GetStudent());
        //    ////var list = await App.Database.GetItemsAsync();
        //    ////Classmates = new ObservableCollection<Student>(list);
        //    //RaisePropertyChanged(() => Classmates);
        //}

    }
}
