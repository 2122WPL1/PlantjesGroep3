using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Plantjes.ViewModels;
using System.Windows;
using Plantjes.Dao;
using Plantjes.Models.Db;
using Plantjes.ViewModels.HelpClasses;

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
            Helper.PopulateDB();
            InitializeComponent();
        }
    }
}
