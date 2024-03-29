﻿using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    internal class DaoAbiotiek : DaoBase
    {
        public static Abiotiek AddAbiotiek(Plant plant, string bezonning = null, string grondsoort=null,
             string vochtbehoefte = null, string voedingsbehoeftes = null, string antagonischeomgeving = null)
        {
            if (string.IsNullOrEmpty(bezonning) && string.IsNullOrEmpty(bezonning) && string.IsNullOrEmpty(bezonning) && string.IsNullOrEmpty(bezonning))
                return null;
            Abiotiek abiotiek = new Abiotiek();
            if (!string.IsNullOrEmpty(bezonning))
            {
                abiotiek.Bezonning = bezonning;
            }
            if (!string.IsNullOrEmpty(grondsoort))
            {
                abiotiek.Grondsoort = grondsoort;
            }
            if (!string.IsNullOrEmpty(voedingsbehoeftes))
            {
                abiotiek.Voedingsbehoefte = voedingsbehoeftes;
            }
            if (!string.IsNullOrEmpty(antagonischeomgeving))
            {
                abiotiek.AntagonischeOmgeving = antagonischeomgeving;
            }
            Context.Plants.First(p => p == plant).Abiotieks.Add(abiotiek);
            Context.SaveChanges();
            return abiotiek;
        }

        public static AbiotiekMulti AddAbiotiekMulti(Plant plant, string eigenschap, string waarde)
        {
            AbiotiekMulti abiotiekMulti = new AbiotiekMulti()
            {
                Eigenschap = eigenschap,
                Waarde = waarde
            };
            Context.Plants.First(p => p == plant).AbiotiekMultis.Add(abiotiekMulti);
            Context.SaveChanges();
            return abiotiekMulti;
        }
    }
}