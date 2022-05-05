using GalaSoft.MvvmLight.Ioc;
using System;
using System.Windows.Input;

namespace Plantjes.ViewModels.HelpClasses
{
    public class SwitchTabCommand : ICommand
    {
        private readonly Action<string> _action;

        public SwitchTabCommand(Action<string> action)
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
