using System;
using System.Collections.Generic;
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
        private string _firstName;
        public AddEntityViewModel()
        {

            AddEntityCommand = new Command(async () => await AddEntityProcAsync(), () => true);

        }
        //fields
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
        public async Task AddEntityProcAsync()
        {
            var stoods = new Student();
            stoods.FirstName = FirstName;
            await App.Database.SaveItemAsync(stoods);

            //autorefresh list
            //var list = await App.Database.GetItemsAsync();
            //Classmates = new ObservableCollection<Student>(list);
            //Classmates = new ObservableCollection<Student>(await _beadleService.GetStudent());
            ////var list = await App.Database.GetItemsAsync();
            ////Classmates = new ObservableCollection<Student>(list);
            //RaisePropertyChanged(() => Classmates);
        }

    }
}
