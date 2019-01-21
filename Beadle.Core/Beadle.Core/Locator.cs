using System;
using System.Collections.Generic;
using System.Text;
using Beadle.Core.Models;
using Beadle.Core.Repository;
using Beadle.Core.Repository.LocalRepository;
using Beadle.Core.Services;
using Beadle.Core.ViewModels;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Beadle.Core
{
    public class Locator
    {
        /// <summary>
        /// Register all the used ViewModels, Services et. al. with the IoC Container
        /// </summary>
        public Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // ViewModels
            SimpleIoc.Default.Register<MainViewModel>();

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

    }
}
