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
        public static Commensalisme AddCommensalisme(long id, long plantid, string ontwikkelinssnelheid,
            string strategie)
        {
            Commensalisme commensalisme = new Commensalisme()
            {
                Id = id,
                PlantId = plantid,
                Ontwikkelsnelheid = ontwikkelinssnelheid,
                Strategie = strategie
            };
            context.Commensalismes.Add(commensalisme);
            return commensalisme;
        }

        public static CommensalismeMulti AddCommensalismeMulti(long id, long plantid, string eigenschap, string waarde)
        {
            CommensalismeMulti commensalismemulti = new CommensalismeMulti()
            {
                Id = id,
                PlantId = plantid,
                Eigenschap = eigenschap,
                Waarde = waarde
            };
            context.CommensalismeMultis.Add(commensalismemulti);
            return commensalismemulti;
        }
    }
}
