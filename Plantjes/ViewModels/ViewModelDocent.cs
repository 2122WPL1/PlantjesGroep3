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
using Plantjes.Models.Classes;

namespace Plantjes.ViewModels
{
    internal class ViewModelDocent:ViewModelBase
    {
        private ObservableCollection<GroupBox> Notificaties;

        public ViewModelDocent()
        {
            Notificaties = new ObservableCollection<GroupBox>() { new PlantNotification(" ") };//TODO add parameter for object
        }

        private void AddNotificatie()
        {
            Notificaties.Add(new PlantNotification(""));
        }
    }
}
