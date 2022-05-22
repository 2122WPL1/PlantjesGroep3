using Microsoft.EntityFrameworkCore;
using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    public class DaoPlant : DaoBase
    {
        public static IEnumerable<Plant> GetPlants()
        {
            return Context.Plants
                .Include(p => p.Abiotieks)
                .Include(p => p.AbiotiekMultis)
                .Include(p => p.Commensalismes)
                .Include(p => p.CommensalismeMultis)
                .Include(p => p.Fenotypes)
                .Include(p => p.FenotypeMultis)
                .Include(p => p.ExtraEigenschaps)
                .Include(p => p.BeheerMaands)
                .Include(p => p.Fotos)
                .ToList();
        }

        public static Plant AddPlant(string type, string familie, string geslacht, string soort = null, string variant = null)
        {
            Plant plant = new Plant()
            {
                Type = type,
                Familie = familie,
                Geslacht = geslacht
            };
            if (soort != null)
            {
                plant.Soort = soort;
            }
            if (variant != null)
            {
                plant.Variant = variant;
            }
            Context.Plants.Add(plant);
            Context.SaveChanges();
            return plant;
        }
        public static UpdatePlant AddUpdatePlant(Plant plant, int userid)
        {
            UpdatePlant updateplant = new UpdatePlant()
            {
                Userid = userid,
                Updatedatum = DateTime.Now
            };
            Context.UpdatePlants.Add(updateplant);
            Context.SaveChanges();
            return updateplant;
        }

        public List<string> GetAbiotiek()
        {
            return null;
        }
    }
}
