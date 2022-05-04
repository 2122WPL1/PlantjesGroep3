using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using Plantjes.Dao;
using Plantjes.Models.Db;
using Plantjes.ViewModels.HelpClasses;
using Plantjes.Views.Home;

namespace Plantjes.ViewModels
{
    //written by Ian Dumalin on 4/5
    internal class ViewModelPasswordChange : ViewModelBase
    {
        private string _PasswordInput;
        private string _PasswordInputRepeat;
        private Gebruiker _currentGebruiker;
        public RelayCommand OkCommand { get; set; }

        public string PasswordInput
        {
            get { return _PasswordInput; }
            set { _PasswordInput = value; }
        }

        public string PasswordInputRepeat
        {
            get { return _PasswordInputRepeat; }
            set { _PasswordInputRepeat = value; }
        }

        public ViewModelPasswordChange(Gebruiker gebruiker)
        {
            _currentGebruiker = gebruiker;
            OkCommand = new RelayCommand(OkClick);
        }

        public void OkClick()
        {
            if (_PasswordInput == _PasswordInputRepeat)
            {
                MainWindow mainWindow = new MainWindow();
                DaoUser.UpdateUser(_currentGebruiker, Helper.HashString(_PasswordInput));
                mainWindow.Show();
                Application.Current.Windows[0]?.Close();
            }
        }
    }
}
