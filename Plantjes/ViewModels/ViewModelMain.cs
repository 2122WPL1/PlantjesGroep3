using GalaSoft.MvvmLight.Ioc;
using Plantjes.Models.Db;
using Plantjes.ViewModels.HelpClasses;
using Plantjes.ViewModels.Interfaces;

namespace Plantjes.ViewModels
{
    public class ViewModelMain :ViewModelBase
    {
        //geschreven door kenny, adhv een voorbeeld van roy

        private readonly SimpleIoc _iocc = SimpleIoc.Default;
        private readonly ViewModelRepo _viewModelRepo;

        private ViewModelBase _currentViewModel;

        public TabCommand MainNavigationCommand { get; set; }
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public ViewModelMain(ILoginUserService loginUserService)
        {
            _viewModelRepo = _iocc.GetInstance<ViewModelRepo>();
            _currentViewModel = _iocc.GetInstance<ViewModelSearch>();

            MainNavigationCommand = new TabCommand(OnNavigationChanged, loginUserService.Gebruiker);
            //  dialogService.ShowMessageBox(this, "", "");
        }

        public Gebruiker Gebruiker { get; set; } = null;

        public void OnNavigationChanged(string userControlName)
        {
            CurrentViewModel = _viewModelRepo.GetViewModel(userControlName);
        }
    }
}
