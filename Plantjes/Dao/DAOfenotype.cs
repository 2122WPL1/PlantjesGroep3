using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    internal class DAOfenotype : DAObase
    {
        public static void AddFenotype(long id, long plantid, int bladgrootte, string bladvorm, string ratiobloeiblad,
            string spruitfenologie, string bloeiwijze, string habitus, string levensvorm)
        {
            Fenotype fenotype = new Fenotype()
            {
                Id = id,
                PlantId = plantid,
                Bladgrootte = bladgrootte,
                Bladvorm = bladvorm,
                Spruitfenologie = spruitfenologie,
                Bloeiwijze = bloeiwijze,
                Habitus = habitus,
                Levensvorm = levensvorm
            };
        }
    }
}
