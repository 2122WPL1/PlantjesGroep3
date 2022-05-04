using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Plantjes.Models.Db;
using Plantjes.ViewModels;

namespace Plantjes.Views.Home
{
    /// <summary>
    /// Interaction logic for PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {
        public PasswordChangeWindow(Gebruiker gebuiker)
        {
            DataContext = new ViewModelPasswordChange(gebuiker);
            InitializeComponent();
        }
    }
}
