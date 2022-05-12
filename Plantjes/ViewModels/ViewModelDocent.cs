using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Plantjes.Dao;
using Plantjes.Models.Classes;
using Plantjes.Models.Db;
using Plantjes.ViewModels.HelpClasses;

namespace Plantjes.ViewModels
{
    //written by Renzo
    // Export made by Ian
    public class ViewModelDocent : ViewModelBase
    {
        private IEnumerable<Gebruiker> _gebruikers;
        public RelayCommand ExportCommand { get; set; }
        public RelayCommand ImportCommand { get; set; }

        public ViewModelDocent()
        {
            ExportCommand = new RelayCommand(ExportClick);
            ImportCommand = new RelayCommand(ImportClick);
            _gebruikers = DaoUser.GetGebruikerList() ?? Enumerable.Empty<Gebruiker>();
        }
        public IEnumerable<Gebruiker> Gebruikers
        {
            get => _gebruikers;
        }

        private void ExportClick()
        {
            CSVHelper.ExportUsersToCSV(Gebruikers);
        }

        private void ImportClick()
        {
            _gebruikers = Helper.PopulateDB(_gebruikers).ToList();
            OnPropertyChanged("Gebruikers");
        }
    }
}
