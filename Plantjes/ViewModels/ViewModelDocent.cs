using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Plantjes.Dao;
using Plantjes.Models.Classes;
using Plantjes.Models.Db;

namespace Plantjes.ViewModels
{
    //written by renzo
    public class ViewModelDocent:ViewModelBase
    {
        public IEnumerable<Gebruiker> Gebruikers { get => DaoUser.GetGebruikerList() ?? Enumerable.Empty<Gebruiker>(); }
    }
}
