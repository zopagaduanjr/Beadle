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
    public class SessionInfoViewModel : ViewModelBase
    {
            

        //constructor
        public SessionInfoViewModel(IRepository repository, MainViewModel mainViewModel, INavigationService navigationService)
        {
            MainViewModel = mainViewModel;
            Repository = repository;
            NavigationService = navigationService;
            Population = MainViewModel.SelectedSession.Persons.Count;
        }

        //private fields
        private readonly INavigationService NavigationService;
        private MainViewModel _mainViewModel;
        private readonly IRepository Repository;
        private Person _selectedPerson;
        private int _population;

        //properties
        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                RaisePropertyChanged(nameof(SelectedPerson));

            }
        }

        public int Population
        {
            get => _population;
            set
            {
                _population = value;
                RaisePropertyChanged(nameof(Population));

            }
        }

        public MainViewModel MainViewModel
        {
            get => _mainViewModel;
            set
            {
                _mainViewModel = value;
                RaisePropertyChanged(nameof(MainViewModel));
            }
        }
        //methods
        //canclick
    }

}

