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
        public static BeheerMaand AddBeheersdaden(Plant plant, string beheerdaad, List<bool> months)
        {            
            BeheerMaand beheerMaand = new BeheerMaand()
            {
                Beheerdaad = beheerdaad,
                Jan = months[0],
                Feb = months[1],
                Mrt = months[2],
                Apr = months[3],
                Mei = months[4],
                Jun = months[5],
                Jul = months[6],
                Aug = months[7],
                Sept = months[8],
                Okt = months[9],
                Nov = months[10],
                Dec = months[11]
            };
        

            context.Plants.First(p => p == plant).BeheerMaands.Add(beheerMaand);
            context.SaveChanges();
            return beheerMaand;

        }
    }
}
