﻿using GalaSoft.MvvmLight.Ioc;
using Plantjes.ViewModels.Interfaces;
using Plantjes.ViewModels.Services;

namespace Plantjes.ViewModels.HelpClasses
{
    /*written by kenny*/
    public class ServiceProvider
    {
        public static void RegisterServices()
        {
            // basisstructuur kenny, mede gebruikt door Robin

            // de Default instantie (singleton) van de class SimpleIOC container
            // gebruiken als container voor de services.
            SimpleIoc iocc = SimpleIoc.Default;

            // registreren van utility services
            iocc.Register<ILoginUserService, LoginUserService>();
            iocc.Register<ISearchService, SearchService>();
        }
    }
}