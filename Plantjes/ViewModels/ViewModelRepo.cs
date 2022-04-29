using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using Plantjes.Dao;
using Plantjes.ViewModels.Interfaces;
using Plantjes.ViewModels;

namespace Plantjes.ViewModels
{
    //geschreven door kenny adhv een voorbeeld van roy
    //herschreven door kenny voor gebruik met ioc
    public class ViewModelRepo
    {   //singleton pattern
        private static SimpleIoc iocc = SimpleIoc.Default;
        //private static ViewModelRepo instance;

        private Dictionary<string, ViewModelBase> _viewModels = new Dictionary<string, ViewModelBase>();
       
        private ViewModelAdd viewModelAdd = iocc.GetInstance<ViewModelAdd>();
        private ViewModelSearch viewModelSearch = iocc.GetInstance<ViewModelSearch>();

        public ViewModelRepo()
        {
            //hier een extra lijn code per user control
            _viewModels.Add("VIEWADD", viewModelAdd);
            _viewModels.Add("VIEWSEARCH", viewModelSearch);
        }
        
        public ViewModelBase GetViewModel(string modelName)
        {
            ViewModelBase result;
            var ok = this._viewModels.TryGetValue(modelName, out result);
            return ok ? result : null;
        }
    }

}
