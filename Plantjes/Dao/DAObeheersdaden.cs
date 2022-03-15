using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    public class DAObeheersdaden:DAObase
    {
        public static void AddBeheersdaden(long id, long plantid, string beheerdaad, string omschrijving, 
            bool jan, bool feb, bool mrt, bool apr, bool mei, bool jun, bool jul, bool aug, bool sept, bool okt, bool nov, bool dec)
        {
            BeheerMaand beheerMaand = new BeheerMaand()
            {
                Id = id,
                PlantId = plantid,
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
        }
    }
}
