using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.ViewModels
{
    public class ViewModelPlantDetail : ViewModelBase
    {
        public ViewModelPlantDetail(Plant plant)
        {
            Plant = plant;
        }

        public Plant Plant { get; private set; }
    }
}
