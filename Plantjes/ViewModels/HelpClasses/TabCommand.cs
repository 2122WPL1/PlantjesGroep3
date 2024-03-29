﻿using GalaSoft.MvvmLight.Ioc;
using Plantjes.Dao;
using Plantjes.Models.Db;
using System;
using System.Linq;
using System.Windows.Input;

namespace Plantjes.ViewModels.HelpClasses
{
    public class TabCommand : ICommand
    {
        private readonly Action<string> _action;
        private readonly Gebruiker _gebruiker;

        public TabCommand(Action<string> action, Gebruiker gebruiker)
        {
            _action = action;
            _gebruiker = gebruiker;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                if (parameter as string == "VIEWDOCENT" && _gebruiker?.RolId > DaoUser.GetList<Rol>().OrderBy(r => r.Id).ToList()[0].Id)
                    return false;
                if (parameter as string == "VIEWADD" && _gebruiker?.RolId > DaoUser.GetList<Rol>().OrderBy(r => r.Id).ToList()[1].Id)
                    return false;
                SimpleIoc.Default.GetInstance<ViewModelRepo>().GetViewModel(parameter as string);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            _action(parameter as string);
        }
    }
}
