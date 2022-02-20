using Plantjes.ViewModels;
using System.Windows;

namespace Plantjes.Views.Home
{/*written by kenny*/
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            DataContext = GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.GetInstance<ViewModelLogin>();
            InitializeComponent();
        }
    }
}
