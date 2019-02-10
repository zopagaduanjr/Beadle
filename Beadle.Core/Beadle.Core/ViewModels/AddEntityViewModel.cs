using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Beadle.Core.Models;
using Beadle.Core.Repository;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace Beadle.Core.ViewModels
{
    public class AddEntityViewModel : ViewModelBase
    {

        //constructor
        public AddEntityViewModel(IRepository repository, MainViewModel mainViewModel)
        {
            AddEntityCommand = new Command(async () => await AddEntityProcAsync(), () => true);
            MainViewModel = mainViewModel;
            Repository = repository;
        }

        //fields
        private string _firstName;
        private MainViewModel _mainViewModel;
        private readonly IRepository Repository;
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
        public async Task AddEntityProcAsync()
        {
            var person = new Person();
            person.FirstName = FirstName;
            MainViewModel.SelectedSession.Persons.Add(person);
            await Repository.Person.SaveItemAsync(person);
            await Repository.Session.UpdateWithChildrenAsync(MainViewModel.SelectedSession);
            await Task.Run(() => Init());
        }
        public async Task Init()
        {
            //updaters
            MainViewModel.Sessions = await Repository.Session.GetItemsAsync();
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


    }
}
