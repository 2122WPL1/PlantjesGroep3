using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using Plantjes.Dao;
using Plantjes.ViewModels.Interfaces;
using Plantjes.ViewModels;

namespace Plantjes.ViewModels
{
    // Written by Warre
    public class ViewModelRepo
    {
        public ViewModelBase GetViewModel(string modelName)
            => modelName switch
            {
                "VIEWADD" => SimpleIoc.Default.GetInstance<ViewModelAdd>(),
                "VIEWSEARCH" => SimpleIoc.Default.GetInstance<ViewModelSearch>(),
                "VIEWDETAIL" => SimpleIoc.Default.GetInstance<ViewModelPlantDetail>(),
                "VIEWDOCENT" => SimpleIoc.Default.GetInstance<ViewModelDocent>(),
                _ => null,
            };
    }

}
