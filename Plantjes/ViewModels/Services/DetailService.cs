using GalaSoft.MvvmLight.Ioc;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Dao;
using System.ComponentModel;

namespace Plantjes.ViewModels.Services
{
    public class DetailService : IDetailService, INotifyPropertyChanged
    {
        //Robin
        //Op dit moment wordt de service niet gebruikt, deze is opgezet om later de plantdetails te kunnen weergeven en te kunnen toevoegen in alle usercontrols

        private DAOLogic _dao;
        private static DetailService _detailService;
        private static SimpleIoc iocc = SimpleIoc.Default;
        private ISearchService _searchService = iocc.GetInstance<ISearchService>();

        public event PropertyChangedEventHandler PropertyChanged;
        public DetailService(ISearchService searchService)
        {
            this._dao = DAOLogic.Instance();
            _searchService = searchService;
        }
        
    }
}
