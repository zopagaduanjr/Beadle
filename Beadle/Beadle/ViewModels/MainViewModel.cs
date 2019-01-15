using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;
using Splat;

namespace Beadle.ViewModels
{
    public class MainViewModel : ReactiveObject, IScreen
    {
        private MasterCellViewModel _selected;

        public MainViewModel()
        {
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

        }
        public RoutingState Router { get; } //needed for the interface IScreen
        public ReactiveCommand<IRoutableViewModel, Unit> NavigateToMenuItem { get; }
        public MasterCellViewModel Selected
        {
            get => _selected;
            set => this.RaiseAndSetIfChanged(ref _selected, value);
        }

        public IEnumerable<MasterCellViewModel> MenuItems { get;  }


        //DetailsPageFirst then this
        private IEnumerable<MasterCellViewModel> GetMenuItems()
        {
            return new[]
            {
                new MasterCellViewModel { Title = "Navigable Page", IconSource = "contacts.png", TargetType = typeof(NavigableViewModel) },
            };
        }
    }
}
