﻿using System;
using System.Collections.Generic;
using System.Text;
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
        public Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // ViewModels           
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddPersonViewModel>();
            SimpleIoc.Default.Register<AddSessionViewModel>();
            SimpleIoc.Default.Register<SessionInfoViewModel>();
            SimpleIoc.Default.Register<RecordInfoViewModel>();

            // Services
            SimpleIoc.Default.Register<IRepository, LocalRepository>();
            //SimpleIoc.Default.Register<INavigationService, NavigationService>();

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
        public AddPersonViewModel AddPerson
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddPersonViewModel>();
            }
        }

        public AddSessionViewModel AddSession
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AddSessionViewModel>();
            }
        }
        public SessionInfoViewModel SessionInfo
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SessionInfoViewModel>();
            }
        }
        public RecordInfoViewModel RecordInfo
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RecordInfoViewModel>();
            }
        }
    }
}
