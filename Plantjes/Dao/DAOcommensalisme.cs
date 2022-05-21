using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    //written by Renzo
    internal class DaoCommensalisme : DaoBase
    {
        public static Commensalisme AddCommensalisme(Plant plant, string ontwikkelinssnelheid = null, string strategie = null)
        {
            if (string.IsNullOrEmpty(ontwikkelinssnelheid) && string.IsNullOrEmpty(strategie)) 
                return null;
            Commensalisme commensalisme = new Commensalisme();
            if (!string.IsNullOrEmpty(ontwikkelinssnelheid))
            {
                commensalisme.Ontwikkelsnelheid = ontwikkelinssnelheid;
            }
            if (!string.IsNullOrEmpty(strategie))
            {
                commensalisme.Strategie = strategie;
            }
            Context.Plants.First(p => p == plant).Commensalismes.Add(commensalisme);
            Context.SaveChanges();
            return commensalisme;
        }

        public static CommensalismeMulti AddCommensalismeMulti(Plant plant, string eigenschap, string waarde)
        {
            CommensalismeMulti commensalismemulti = new CommensalismeMulti()
            {
                Eigenschap = eigenschap,
                Waarde = waarde
            };
            Context.Plants.First(p => p == plant).CommensalismeMultis.Add(commensalismemulti);
            Context.SaveChanges();
            return commensalismemulti;
        }
    }
}
