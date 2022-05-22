using GalaSoft.MvvmLight.Command;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Views.Home;
using System;
using System.Windows;

namespace Plantjes.ViewModels
{
    //written by kenny
    public class ViewModelRegister : ViewModelBase
    {
        private string _errorMessage;
        private ILoginUserService _loginService;

        public ViewModelRegister(ILoginUserService loginUserService)
        {
            this._loginService = loginUserService;
            RegisterCommand = new RelayCommand(RegisterButtonClick);
            BackCommand = new RelayCommand(BackButtonClick);
        }

        public RelayCommand RegisterCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public void BackButtonClick()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Application.Current.Windows[0]?.Close();
        }

        //written by Warre
        public void RegisterButtonClick()
        {
            try
            {
                _loginService.Register(VivesNrInput, LastNameInput,
                 FirstNameInput, EmailAdresInput,
                 PasswordInput, PasswordRepeatInput);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                OnPropertyChanged("ErrorMessage");
            }

        }

        #region MVVM TextFieldsBinding
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
            }
        }

        public string VivesNrInput { get; set; }

        public string FirstNameInput { get; set; }

        public string LastNameInput { get; set; }

        public string EmailAdresInput { get; set; }

        public string PasswordInput { get; set; }

        public string PasswordRepeatInput { get; set; }

        public string RolInput { get; set; }
        #endregion
    }
}

