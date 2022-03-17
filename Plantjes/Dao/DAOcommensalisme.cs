using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    internal class DaoCommensalisme : DaoBase
    {
        public static Commensalisme AddCommensalisme(Plant plant, string ontwikkelinssnelheid, string strategie)
        {
            Commensalisme commensalisme = new Commensalisme()
            {
                Ontwikkelsnelheid = ontwikkelinssnelheid,
                Strategie = strategie
            };
            context.Plants.First(p => p == plant).Commensalismes.Add(commensalisme);
            context.SaveChanges();
            return commensalisme;
        }

        public static CommensalismeMulti AddCommensalismeMulti(Plant plant, string eigenschap, string waarde)
        {
            CommensalismeMulti commensalismemulti = new CommensalismeMulti()
            {
                Eigenschap = eigenschap,
                Waarde = waarde
            };
            context.Plants.First(p => p == plant).CommensalismeMultis.Add(commensalismemulti);
            context.SaveChanges();
            return commensalismemulti;
        }
    }
}
