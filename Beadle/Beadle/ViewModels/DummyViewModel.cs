using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using ReactiveUI;
using Splat;

namespace Beadle.ViewModels
{
    public class DummyViewModel : ReactiveObject, IRoutableViewModel
    {
        public DummyViewModel(IScreen hostScreen = null)
        {
            HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

            NavigateToDummyPage = ReactiveCommand.CreateFromObservable(
                () => HostScreen.Router.Navigate.Execute(new DummyViewModel()).Select(_ => Unit.Default));
        }
        public ReactiveCommand<Unit, Unit> NavigateToDummyPage { get; }

        public ReactiveCommand<Unit, Unit> NavigateBack => HostScreen.Router.NavigateBack;
        //need HostScreen for IRoutableViewModel
        public IScreen HostScreen { get; }
        //need UrlPathSegment for IRoutableViewModel
        public string UrlPathSegment => "Dummy Page";
    }
}
