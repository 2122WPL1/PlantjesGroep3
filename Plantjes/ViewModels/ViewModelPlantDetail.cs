using Plantjes.Models.Db;
using Plantjes.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.ViewModels
{
    //Written by Renzo
    public class ViewModelPlantDetail : ViewModelBase
    {
        private string _plantNaam;

        public ViewModelPlantDetail(Plant plant)
        {
            Plant = plant;
        }
        public string PlantNaam
        {
            get { return _plantNaam = Plant.Variant.RemoveQuotes() ?? $"{Plant.Geslacht.FirstToUpper()} {Plant.Soort.FirstToUpper()}"; }
        }
        public Plant Plant { get; private set; }

    }
}
