using GalaSoft.MvvmLight.Ioc;
using MvvmHelpers.Commands;
using Plantjes.ViewModels.HelpClasses;
using Plantjes.ViewModels.Interfaces;
using System;

namespace Plantjes.ViewModels
{
    public class ViewModelMain :ViewModelBase
    {
        //geschreven door kenny, adhv een voorbeeld van roy

        private SimpleIoc iocc = SimpleIoc.Default;
        private ViewModelRepo _viewModelRepo;

        private ViewModelBase _currentViewModel;

        public SwitchTabCommand mainNavigationCommand { get; set; }
        public ViewModelBase currentViewModel
        {
            get { return _currentViewModel; }
            set 
            { 
                SetProperty(ref _currentViewModel, value);
            }
        }

        public ILoginUserService loginUserService;
        private ISearchService _searchService;
        public ViewModelMain(ILoginUserService loginUserService, ISearchService searchService)
        {
            loggedInMessage = loginUserService.LoggedInMessage();
            this._viewModelRepo = iocc.GetInstance<ViewModelRepo>();
            this._searchService = searchService;
            this.loginUserService = loginUserService;
            _currentViewModel = iocc.GetInstance<ViewModelSearch>();

            mainNavigationCommand = new SwitchTabCommand(new Action<string>(OnNavigationChanged));
            //  dialogService.ShowMessageBox(this, "", "");
        }

        private string _loggedInMessage { get; set; }
        public string loggedInMessage
        {
            get
            {
                return _loggedInMessage;
            }
            set
            {
                _loggedInMessage = value;

                RaisePropertyChanged("loggedInMessage");
            }
        }

        public void OnNavigationChanged(string userControlName)
        {
            this.currentViewModel = this._viewModelRepo.GetViewModel(userControlName);
        }
    }
}
