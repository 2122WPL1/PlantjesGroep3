#nullable enable
using Plantjes.Models.Db;
using System.Linq;

namespace Plantjes.Dao
{
    internal class DaoFenotype : DaoBase
    {
        public static Fenotype? AddFenotype(Plant plant, int? bladgrootte = null, string? bladvorm = null, string? ratiobloeiblad = null,
            string? spruitfenologie = null, string? bloeiwijze = null, string? habitus = null, string? levensvorm = null)
        {
            if (bladgrootte == null && string.IsNullOrEmpty(bladvorm) && string.IsNullOrEmpty(ratiobloeiblad)
                && string.IsNullOrEmpty(spruitfenologie) && string.IsNullOrEmpty(bloeiwijze) && string.IsNullOrEmpty(habitus) && string.IsNullOrEmpty(levensvorm)) 
                return null;
            Fenotype fenotype = new();
            if (bladgrootte != null)
            {
                fenotype.Bladgrootte = bladgrootte;
            }
            if (!string.IsNullOrEmpty(bladvorm))
            {
                fenotype.Bladvorm = bladvorm;
            }
            if (!string.IsNullOrEmpty(ratiobloeiblad))
            {
                fenotype.RatioBloeiBlad = ratiobloeiblad;
            }
            if (!string.IsNullOrEmpty(spruitfenologie))
            {
                fenotype.Spruitfenologie = spruitfenologie;
            }
            if (!string.IsNullOrEmpty(bloeiwijze))
            {
                fenotype.Bloeiwijze = bloeiwijze;
            }
            if (!string.IsNullOrEmpty(habitus))
            {
                fenotype.Habitus = habitus;
            }
            if (!string.IsNullOrEmpty(levensvorm))
            {
                fenotype.Levensvorm = levensvorm;
            }

            Context.Plants.First(p => p == plant).Fenotypes.Add(fenotype);
            Context.SaveChanges();
            return fenotype; 
        }

        public static FenotypeMulti AddFenotypeMulti(Plant plant, string eigenschap, string waarde, string? month = null)
        {
            FenotypeMulti fenotype = new()
            {
                Eigenschap = eigenschap,
                Waarde = waarde,
            };
            if (month != null)
                fenotype.Maand = month;

            Context.Plants.First(p => p == plant).FenotypeMultis.Add(fenotype);
            Context.SaveChanges();
            return fenotype;
        }
    }
}
