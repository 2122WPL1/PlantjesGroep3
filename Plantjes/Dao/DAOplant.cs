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
        public static Plant AddPlant(string type, string familie, string geslacht, string soort, string variant)
        {
            Plant plant = new Plant()
            {
                Type = type,
                Familie = familie,
                Geslacht = geslacht,
                Soort = soort,
                Variant = variant
            };
            context.Plants.Add(plant);
            context.SaveChanges();
            return plant;
        }
        public static UpdatePlant AddUpdatePlant(Plant plant, int userid)
        {
            UpdatePlant updateplant = new UpdatePlant()
            {
                Userid = userid,
                Updatedatum = DateTime.Now
            };
            context.UpdatePlants.Add(updateplant);
            context.SaveChanges();
            return updateplant;
        }
    }
}
