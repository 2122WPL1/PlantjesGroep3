using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    internal class DAOcommensalisme:DAObase
    {
        public static void AddCommensalisme(long id, long plantid, string ontwikkelinssnelheid,
            string strategie)
        {
            Commensalisme commensalisme = new Commensalisme()
            {
                Id = id,
                PlantId = plantid,
                Ontwikkelsnelheid = ontwikkelinssnelheid,
                Strategie = strategie
            };
        }
    }
}
