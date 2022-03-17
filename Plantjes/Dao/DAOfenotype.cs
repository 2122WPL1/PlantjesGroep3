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
        public static Fenotype AddFenotype(Plant plant, int bladgrootte, string bladvorm, string ratiobloeiblad,
            string spruitfenologie, string bloeiwijze, string habitus, string levensvorm)
        {
            Fenotype fenotype = new Fenotype()
            {
                Bladgrootte = bladgrootte,
                Bladvorm = bladvorm,
                Spruitfenologie = spruitfenologie,
                Bloeiwijze = bloeiwijze,
                Habitus = habitus,
                Levensvorm = levensvorm
            };
            context.Plants.First(p => p == plant).Fenotypes.Add(fenotype);
            context.SaveChanges();
            return fenotype;
        }
    }
}
