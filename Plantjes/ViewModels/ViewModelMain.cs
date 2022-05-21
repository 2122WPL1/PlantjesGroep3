using GalaSoft.MvvmLight.Ioc;
using MvvmHelpers.Commands;
using Plantjes.Models.Db;
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

        public TabCommand mainNavigationCommand { get; set; }
        public ViewModelBase currentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public ViewModelMain(ILoginUserService loginUserService)
        {
            _viewModelRepo = iocc.GetInstance<ViewModelRepo>();
            _currentViewModel = iocc.GetInstance<ViewModelSearch>();

            mainNavigationCommand = new TabCommand(OnNavigationChanged, loginUserService.Gebruiker);
            //  dialogService.ShowMessageBox(this, "", "");
        }

        public Gebruiker Gebruiker { get; set; } = null;

        public void OnNavigationChanged(string userControlName)
        {
            currentViewModel = _viewModelRepo.GetViewModel(userControlName);
        }
    }
}
