using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;
using Splat;

namespace Beadle.ViewModels
{
    public class NavigableViewModel : ReactiveObject, IRoutableViewModel
    {
        public NavigableViewModel(IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        }
        public ReactiveCommand<Unit, Unit> NavigateToDummyPage { get; }

        //need HostScreen for IRoutableViewModel
        public IScreen HostScreen { get; }

        //need UrlPathSegment for IRoutableViewModel
        public string UrlPathSegment => "Navigable Page";
    }
}
