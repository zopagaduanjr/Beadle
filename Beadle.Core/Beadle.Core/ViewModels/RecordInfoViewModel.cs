using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Beadle.Core.Models;
using Beadle.Core.Repository;
using Beadle.Core.Services;
using GalaSoft.MvvmLight;
using Xamarin.Forms;

namespace Beadle.Core.ViewModels
{
    public class RecordInfoViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;

        public RecordInfoViewModel(IRepository repository, MainViewModel mainViewModel, INavigationService navigationService)
        {
            mainViewModel.RecordInfoViewModel = this;
            MainViewModel = mainViewModel;
            Records = MainViewModel.Records;
            Repository = repository;
            NavigationService = navigationService;
            IsVisible = false;
            //ItemRefresherCommand = new Command(async () => await ItemRefresherProcAsync(), () => true);

        }
        private readonly INavigationService NavigationService;
        private readonly IRepository Repository;
        private List<Record> _records;
        private Record _selectedRecord;
        private List<Item> _items;
        private Item _selectedItem;
        private bool _isVisible;

        //public ICommand ItemRefresherCommand { get; private set; }


        public Record SelectedRecord
        {
            get => _selectedRecord;
            set
            {
                _selectedRecord = value;                
                RaisePropertyChanged(nameof(SelectedRecord));
                if (value != null)
                {
                    IsVisible = true;
                    RaisePropertyChanged(nameof(IsVisible));
                }
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                RaisePropertyChanged(() => IsVisible);
            }
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        public List<Item> Items
        {
            get => _items;
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
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

        public List<Record> Records
        {
            get => _records;
            set
            {
                _records = value;
                RaisePropertyChanged(() => Records);
            }
        }


        //methods
        public async Task ItemRefresherProcAsync()
        {
            if (SelectedRecord != null)
            {
                var recorddb = await Repository.Record.GetItemFromIdAsync(SelectedRecord.Id);
                Items = recorddb.Items;
                RaisePropertyChanged(nameof(Items));

            }
        }

    }
}
