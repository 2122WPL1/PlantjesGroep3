using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    internal class DaoAbiotiek : DaoBase
    {
        public static Abiotiek AddAbiotiek(Plant plant, string bezonning, string grondsoort,
             string vochtbehoefte, string voedingsbehoeftes, string antagonischeomgeving)
        {
            Abiotiek abiotiek = new Abiotiek()
            {
                Bezonning = bezonning,
                Grondsoort = grondsoort,
                Vochtbehoefte = vochtbehoefte,
                Voedingsbehoefte = voedingsbehoeftes,
                AntagonischeOmgeving = antagonischeomgeving
            };
            context.Plants.First(p => p == plant).Abiotieks.Add(abiotiek);
            context.SaveChanges();
            return abiotiek;

        }

        public static AbiotiekMulti AbiotiekMulti(Plant plant, string eigenschap, string waarde)
        {
            AbiotiekMulti abiotiekMulti = new AbiotiekMulti()
            {
                Eigenschap = eigenschap,
                Waarde = waarde
            };
            context.Plants.First(p => p == plant).AbiotiekMultis.Add(abiotiekMulti);
            context.SaveChanges();
            return abiotiekMulti;
        }

    }
}