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
            if (bladgrootte != null && string.IsNullOrEmpty(bladvorm) && string.IsNullOrEmpty(ratiobloeiblad)
                && string.IsNullOrEmpty(spruitfenologie) && string.IsNullOrEmpty(bloeiwijze) && string.IsNullOrEmpty(habitus) && string.IsNullOrEmpty(levensvorm)) 
                return null;
            Fenotype fenotype = new Fenotype();
            if (bladgrootte != null)
            {
                fenotype.Bladgrootte = bladgrootte;
            }
            if (string.IsNullOrEmpty(bladvorm))
            {
                fenotype.Bladvorm = bladvorm;
            }
            if (string.IsNullOrEmpty(ratiobloeiblad))
            {
                fenotype.RatioBloeiBlad = ratiobloeiblad;
            }
            if (string.IsNullOrEmpty(spruitfenologie))
            {
                fenotype.Spruitfenologie = spruitfenologie;
            }
            if (string.IsNullOrEmpty(bloeiwijze))
            {
                fenotype.Bloeiwijze = bloeiwijze;
            }
            if (string.IsNullOrEmpty(habitus))
            {
                fenotype.Habitus = habitus;
            }
            if (string.IsNullOrEmpty(levensvorm))
            {
                fenotype.Levensvorm = levensvorm;
            }

            context.Plants.First(p => p == plant).Fenotypes.Add(fenotype);
            context.SaveChanges();
            return fenotype; 
        }

        public static FenotypeMulti AddFenotypeMulti(Plant plant, string eigenschap, string waarde, string? month = null)
        {
            FenotypeMulti fenotype = new FenotypeMulti()
            {
                Eigenschap = eigenschap,
                Waarde = waarde,
            };
            if (month != null)
                fenotype.Maand = month;

            context.Plants.First(p => p == plant).FenotypeMultis.Add(fenotype);
            context.SaveChanges();
            return fenotype;
        }
    }
}
