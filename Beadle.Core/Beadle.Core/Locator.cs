using System;
using System.Collections.Generic;
using System.Text;
using Beadle.Core.Services;
using Beadle.Core.ViewModels;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Beadle.Core
{
    public class Locator
    {
        public Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // ViewModels
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddEntityViewModel>();

            // Services
            SimpleIoc.Default.Register<IBeadleService, BeadleService>();

        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public AddEntityViewModel AddEntity
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddEntityViewModel>();
            }
        }
    }
}
