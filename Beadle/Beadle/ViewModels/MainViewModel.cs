﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Beadle.Models;
using Beadle.Services;
using GalaSoft.MvvmLight;

namespace Beadle.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPeopleService _peopleService;
        ObservableCollection<Person> People { get; set; }

        public MainViewModel(IPeopleService peopleService)
        {
            if (peopleService == null) throw new ArgumentNullException("peopleService");
            _peopleService = peopleService;
        }

        public async Task Init()
        {
            if (People != null) return;

            People = new ObservableCollection<Person>(await _peopleService.GetPeople());
        }
    }
}
