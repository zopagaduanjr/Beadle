using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using ReactiveUI;

namespace Beadle.ViewModels
{
    public class NumberStreamViewModel : ReactiveObject, IRoutableViewModel
    {
        public NumberStreamViewModel()
        {
            NumberStream = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Select(x => x.ToString());
        }
        public IObservable<string> NumberStream { get; }

        //need HostScreen for IRoutableViewModel
        public IScreen HostScreen { get; }

        //need UrlPathSegment for IRoutableViewModel
        public string UrlPathSegment => "Number Stream Page";
    }
}
