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
        public static BeheerMaand AddBeheersdaden(Plant plant, string beheerdaad,
            bool? jan = null, bool? feb = null, bool? mrt = null, bool? apr = null, bool? mei = null, bool? jun = null, bool? jul = null, bool? aug = null, bool? sept = null, bool? okt = null, bool? nov = null, bool? dec = null)
        {
            BeheerMaand beheerMaand = new BeheerMaand()
            {
                Beheerdaad = beheerdaad
            };
            if (jan != null)
            {
                beheerMaand.Jan = jan;
            }
            if (feb != null)
            {
                beheerMaand.Feb = feb;
            }
            if (mrt != null)
            {
                beheerMaand.Mrt = mrt;
            }
            if (apr != null)
            {
                beheerMaand.Apr = apr;
            }
            if (mei != null)
            {
                beheerMaand.Mei = mei;
            }
            if (jun != null)
            {
                beheerMaand.Jun = jun;
            }
            if (jul != null)
            {
                beheerMaand.Jul = jul;
            }
            if (aug != null)
            {
                beheerMaand.Aug = aug;
            }
            if (sept != null)
            {
                beheerMaand.Sept = sept;
            }
            if (okt != null)
            {
                beheerMaand.Okt = okt;
            }
            if (nov != null)
            {
                beheerMaand.Nov = nov;
            }
            if (dec != null)
            {
                beheerMaand.Dec = dec;
            }
            context.Plants.First(p => p == plant).BeheerMaands.Add(beheerMaand);
            context.SaveChanges();
            return beheerMaand;

        }
    }
}
