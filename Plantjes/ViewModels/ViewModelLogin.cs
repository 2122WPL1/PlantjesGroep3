﻿using GalaSoft.MvvmLight.Command;
using Plantjes.ViewModels.Interfaces;
using Plantjes.Models.Classes;
using Plantjes.Models.Enums;
using Plantjes.Views.Home;
using System;
using System.Windows;
using Plantjes.Dao;
using Plantjes.Models.Db;
using Plantjes.ViewModels.HelpClasses;
using GalaSoft.MvvmLight.Ioc;

//written by kenny
namespace Plantjes.ViewModels
{
    public class ViewModelLogin : ViewModelBase
    {
        private string _userNameInput;
        private string _passwordInput;
        //private string errorMessage;

        private ILoginUserService LoginService { get; }
        public RelayCommand LoginCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }

        public ViewModelLogin(ILoginUserService loginUserService)
        {
            this.LoginService = loginUserService;
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
                    if (LoginService.IsLogin(UserNameInput, PasswordInput))
                    {
                        //  loggedInMessage = _loginService.LoggedInMessage(userNameInput);
                        var currentGebruiker = DaoUser.GetGebruiker(_userNameInput);
                        if ((currentGebruiker.HashPaswoord == Helper.HashString(currentGebruiker.Vivesnr) || currentGebruiker.LastLogin == null) && currentGebruiker.Emailadres != "admin")
                        {
                            //ViewModelPasswordChange vmpc = new ViewModelPasswordChange(currentGebruiker);
                            var window = new PasswordChangeWindow(currentGebruiker);
                            window.Show();
                            Application.Current.Windows[0]?.Close();
                        }
                        else
                        {
                            DaoUser.UpdateUserLogin(currentGebruiker);
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            Application.Current.Windows[0]?.Close();
                        }
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
                return _userNameInput;
            }
            set
            {
                _userNameInput = value;
            }
        }

        public string PasswordInput
        {
            get
            {
                return _passwordInput;
            }
            set
            {
                _passwordInput = value;
            }
        }
    }
}
