using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    internal class DaoFenotype : DaoBase
    {
        public static Fenotype AddFenotype(Plant plant, int? bladgrootte = null, string bladvorm = null, string ratiobloeiblad = null,
            string spruitfenologie = null, string bloeiwijze = null, string habitus = null, string levensvorm = null)
        {
            if (new List<object>() { bladgrootte, bladvorm, ratiobloeiblad, spruitfenologie, bloeiwijze, habitus, levensvorm }.All(o => o == null)) 
            {
                return null;
            }
            Fenotype fenotype = new Fenotype();
            if (bladgrootte != null)
            {
                fenotype.Bladgrootte = bladgrootte;
            }
            if (bladvorm != null)
            {
                fenotype.Bladvorm = bladvorm;
            }
            if (ratiobloeiblad != null)
            {
                fenotype.RatioBloeiBlad = ratiobloeiblad;
            }
            if (spruitfenologie != null)
            {
                fenotype.Spruitfenologie = spruitfenologie;
            }
            if (bloeiwijze != null)
            {
                fenotype.Bloeiwijze = bloeiwijze;
            }
            if (habitus != null)
            {
                fenotype.Habitus = habitus;
            }
            if (levensvorm != null)
            {
                fenotype.Levensvorm = levensvorm;
            }

            context.Plants.First(p => p == plant).Fenotypes.Add(fenotype);
            context.SaveChanges();
            return fenotype;
        }
    }
}
