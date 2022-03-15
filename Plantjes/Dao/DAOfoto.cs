using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantjes.Models.Db;

namespace Plantjes.Dao
{
    internal class DAOfoto:DAObase
    {
        public static Foto AddFoto(long fotoid, long plant, string eigenschap, string urllocatie, byte[] thumbnail)
        {
            Foto foto = new Foto()
            {
                Fotoid = fotoid,
                Plant = plant,
                Eigenschap = eigenschap,
                UrlLocatie = urllocatie,
                Tumbnail = thumbnail
            };
            context.Fotos.Add(foto);
            return foto;
        }
    }
}
