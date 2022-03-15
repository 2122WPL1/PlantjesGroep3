using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    internal class DAOabiotiek:DAObase
    {
        public static Abiotiek AddAbiotiek(long plantid, string bezonning, string grondsoort,
             string vochtbehoefte, string voedingsbehoeftes, string antagonischeomgeving)
        {
            Abiotiek abiotiek = new Abiotiek()
            {
                PlantId = plantid, Bezonning = bezonning, Grondsoort = grondsoort, Vochtbehoefte = vochtbehoefte,
                Voedingsbehoefte = voedingsbehoeftes, AntagonischeOmgeving = antagonischeomgeving
            };
            context.Abiotieks.Add(abiotiek);
            return abiotiek;

        }

        public static AbiotiekMulti AbiotiekMulti(long id, long plantid, string eigenschap, string waarde)
        {
            AbiotiekMulti abiotiekMulti = new AbiotiekMulti()
            {
                Id = id,
                PlantId = plantid,
                Eigenschap = eigenschap,
                Waarde = waarde
            };
            context.AbiotiekMultis.Add(abiotiekMulti);
            return abiotiekMulti;
        }

    }
}