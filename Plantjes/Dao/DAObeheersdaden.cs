using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    public class DaoBeheersdaden : DaoBase
    {
        public static BeheerMaand AddBeheersdaden(Plant plant, string beheerdaad, string omschrijving, 
            bool jan, bool feb, bool mrt, bool apr, bool mei, bool jun, bool jul, bool aug, bool sept, bool okt, bool nov, bool dec)
        {
            BeheerMaand beheerMaand = new BeheerMaand()
            {
                Beheerdaad = beheerdaad,
                Omschrijving = omschrijving,
                Jan = jan,
                Feb = feb,
                Mrt = mrt,
                Apr = apr,
                Mei = mei,
                Jun = jun,
                Jul = jul,
                Aug = aug,
                Sept = sept,
                Okt = okt,
                Nov = nov,
                Dec = dec
            };
            context.Plants.First(p => p == plant).BeheerMaands.Add(beheerMaand);
            context.SaveChanges();
            return beheerMaand;
        }
    }
}
