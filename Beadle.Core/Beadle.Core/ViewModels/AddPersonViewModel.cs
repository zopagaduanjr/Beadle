using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
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
    public class AddPersonViewModel : ViewModelBase
    {

        //constructor
        public AddPersonViewModel(IRepository repository, MainViewModel mainViewModel, INavigationService navigationService)
        {
            SelectedFieldsIsTrue = false;
            mainViewModel.AddPersonViewModel = this;
            AddPersonCommand = new Command(async () => await AddPersonProcAsync(), () => SelectedFieldsIsTrue);
            MainViewModel = mainViewModel;
            Repository = repository;
            NavigationService = navigationService;
        }

        //private fields
        private string _firstName;
        private readonly INavigationService NavigationService;
        private MainViewModel _mainViewModel;
        private readonly IRepository Repository;
        private string _lastName;
        private bool _selectedFieldsIsTrue;

        //properties
        public ICommand AddPersonCommand { get; private set; }
        public MainViewModel MainViewModel
        {
            get => _mainViewModel;
            set => _mainViewModel = value;
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                        _firstName = value;
                        RaisePropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                    if (value != null)
                        SelectedFieldsIsTrue = true;
                    RaisePropertyChanged(nameof(LastName));
                    RaisePropertyChanged(nameof(SelectedFieldsIsTrue));
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
        public async Task AddPersonProcAsync()
        {
            //to avoid multipressing the button
            SelectedFieldsIsTrue = false;
            await NavigationService.GoBack();
            var person = new Person();
            Regex rgx = new Regex("[^a-zA-Z]");
            person.LastName = rgx.Replace(LastName,"");
            person.FirstName = rgx.Replace(FirstName,"");
            MainViewModel.SelectedSession.Persons.Add(person);
            await Repository.Person.SaveItemAsync(person);
            await Repository.Session.UpdateWithChildrenAsync(MainViewModel.SelectedSession);
            await Task.Run(() => Init());
            FirstName = null;
            LastName = null;
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
