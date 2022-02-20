using GalaSoft.MvvmLight.Ioc;
using Plantjes.ViewModels;
using System.Windows;

namespace Plantjes.Views.Home
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = SimpleIoc.Default.GetInstance<ViewModelMain>();
            InitializeComponent();
        }
    }
}
