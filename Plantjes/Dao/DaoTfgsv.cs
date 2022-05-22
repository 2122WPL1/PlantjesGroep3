using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    // written by Warre
    public class DaoTfgsv : DaoBase
    {
        public static TfgsvFamilie AddFamilie(TfgsvType type, string familieNaam)
        {
            var familie = new TfgsvFamilie() { TypeType = type, Familienaam = familieNaam };
            Context.TfgsvFamilies.Add(familie);
            Context.SaveChanges();
            return familie;
        }

        public static TfgsvGeslacht AddGeslacht(TfgsvFamilie familie, string geslachtNaam)
        {
            var geslacht = new TfgsvGeslacht() { FamilieFamile = familie, Geslachtnaam = geslachtNaam };
            Context.TfgsvGeslachts.Add(geslacht);
            Context.SaveChanges();
            return geslacht;
        }

        public static TfgsvSoort AddSoort(TfgsvGeslacht geslacht, string soortNaam)
        {
            var soort = new TfgsvSoort() { GeslachtGeslacht = geslacht, Soortnaam = soortNaam };
            Context.TfgsvSoorts.Add(soort);
            Context.SaveChanges();
            return soort;
        }

        public static TfgsvVariant AddVariant(TfgsvSoort soort, string variantNaam)
        {
            var variant = new TfgsvVariant() { SoortSoort = soort, Variantnaam = variantNaam };
            Context.TfgsvVariants.Add(variant);
            Context.SaveChanges();
            return variant;
        }
    }
}
