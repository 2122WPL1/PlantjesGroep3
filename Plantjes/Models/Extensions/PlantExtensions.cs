using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantjes.Models.Db;

namespace Plantjes.Models.Extensions
{
    internal static class PlantExtensions
    {
        public static string PlantToString(this Plant plant)
        {
            return $"{plant.Familie};{plant.Geslacht}";
        }
    }
}
