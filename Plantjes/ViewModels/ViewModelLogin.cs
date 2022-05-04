using GalaSoft.MvvmLight.Command;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Models.Classes;
using Plantjes.Models.Enums;
using Plantjes.Views.Home;
using System;
using System.Windows;
using Plantjes.Models.Db;
//written by kenny
namespace Plantjes.ViewModels
{
    public class ViewModelLogin : ViewModelBase
    {
        private string userNameInput;
        private string passwordInput;
        private string errorMessage;

        private ILoginUserService _loginService { get; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }

        public ViewModelLogin(ILoginUserService loginUserService)
        {
            this._loginService = loginUserService;
            LoginCommand = new RelayCommand(LoginButtonClick);
            CancelCommand = new RelayCommand(CancelButton);
            RegisterCommand = new RelayCommand(RegisterButtonView);
        }

        public void RegisterButtonView()
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Application.Current.Windows[0]?.Close();
        }

        public void CancelButton()
        {
            Application.Current.Shutdown();
        }

        //written by Warre
        private void LoginButtonClick()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(UserNameInput))
                {
                    if (_loginService.IsLogin(UserNameInput, PasswordInput))
                    {
                        //  loggedInMessage = _loginService.LoggedInMessage(userNameInput);

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Application.Current.Windows[0]?.Close();
                    }
                }
                else
                {
                    throw new Exception("Gebruikersnaam invullen!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //errorMessage = ex.Message;
                //OnPropertyChanged("ErrorMessage");
            }
        }
        //public string ErrorMessage
        //{
        //    get
        //    {
        //        return errorMessage;
        //    }
        //}
     
        public string UserNameInput
        {
            get
            {
                return userNameInput;
            }
            set
            {
                userNameInput = value;
            }
        }

        public string PasswordInput
        {
            get
            {
                return passwordInput;
            }
            set
            {
                passwordInput = value;
            }
        }
    }
}
