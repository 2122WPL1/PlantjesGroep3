using Plantjes.Models.Db;
using Plantjes.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Plantjes.ViewModels
{
    //Written by Renzo
    public class ViewModelPlantDetail : ViewModelBase
    {
        public ViewModelPlantDetail(Plant plant)
        {
            Plant = plant;
        }

        public string PlantNaam
        {
            get => Plant.GetPlantName();
        }

        public Plant Plant { get; private set; }
        
        public BitmapImage PlantImage { get => Plant.GetPlantImage(); }
    }
}
