using GalaSoft.MvvmLight.Ioc;
using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Plantjes.ViewModels.HelpClasses
{
    public class TabCommand : ICommand
    {
        private readonly Action<string> _action;

        public TabCommand(Action<string> action)
        {
            _action = action;
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
                if (parameter as string == "VIEWDOCENT" && SimpleIoc.Default.IsRegistered<ViewModelMain>() && SimpleIoc.Default.GetInstance<ViewModelMain>().Gebruiker?.RolId > 1)
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
