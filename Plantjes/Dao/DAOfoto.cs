using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantjes.Models.Db;

namespace Plantjes.Dao
{
    internal class DaoFoto : DaoBase
    {
        public static Foto AddFoto(Plant plant, string eigenschap, string urllocatie, byte[] thumbnail)
        {
            Foto foto = new Foto()
            {
                Eigenschap = eigenschap,
                UrlLocatie = urllocatie,
                Tumbnail = thumbnail
            };
            context.Plants.First(p => p == plant).Fotos.Add(foto);
            context.SaveChanges();
            _ = context.SaveChanges();
            return foto;
        }

        ///Owen
        public static string GetImageLocation(long plantId, string imageCategorie)
        {
            var foto = context.Fotos.Where(s => s.Eigenschap == imageCategorie).SingleOrDefault(s => s.Plant == plantId);

            if (foto != null)
            {
                return foto.UrlLocatie;
            }

            return null;
        }
    }
}
