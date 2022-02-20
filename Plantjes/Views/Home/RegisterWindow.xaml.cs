using Plantjes.ViewModels;
using System.Windows;

namespace Plantjes.Views.Home
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            this.DataContext = GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance<ViewModelRegister>();
            InitializeComponent();
        }
    }
}
