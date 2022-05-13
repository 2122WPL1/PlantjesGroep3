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
            context.TfgsvFamilies.Add(familie);
            context.SaveChanges();
            return familie;
        }

        public static TfgsvGeslacht AddGeslacht(TfgsvFamilie familie, string geslachtNaam)
        {
            var geslacht = new TfgsvGeslacht() { FamilieFamile = familie, Geslachtnaam = geslachtNaam };
            context.TfgsvGeslachts.Add(geslacht);
            context.SaveChanges();
            return geslacht;
        }

        public static TfgsvSoort AddSoort(TfgsvGeslacht geslacht, string soortNaam)
        {
            var soort = new TfgsvSoort() { GeslachtGeslacht = geslacht, Soortnaam = soortNaam };
            context.TfgsvSoorts.Add(soort);
            context.SaveChanges();
            return soort;
        }

        public static TfgsvVariant AddVariant(TfgsvSoort soort, string variantNaam)
        {
            var variant = new TfgsvVariant() { SoortSoort = soort, Variantnaam = variantNaam };
            context.TfgsvVariants.Add(variant);
            context.SaveChanges();
            return variant;
        }
    }
}
