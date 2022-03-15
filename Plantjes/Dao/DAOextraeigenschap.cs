using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantjes.Models.Db;

namespace Plantjes.Dao
{
    internal class DAOextraeigenschap:DAObase
    {
        public static ExtraEigenschap AddExtraEigenschap(long id, long plantid, string nectarwaarde, string pollenwaarde,
            bool? bijvriendelijke=null, bool? vlindervriendelijk=null, bool? eetbaar=null, bool? kruidgebruik=null, bool? geurend=null, bool? voorstgevoelig=null)
        {
            ExtraEigenschap extraEigenschap = new ExtraEigenschap()
            {
                Id = id,
                PlantId = plantid,
                Nectarwaarde = nectarwaarde,
                Pollenwaarde = pollenwaarde
            };
            if (bijvriendelijke!=null)
            {
                extraEigenschap.Bijvriendelijke = bijvriendelijke;
            }
            if (vlindervriendelijk != null)
            {
                extraEigenschap.Vlindervriendelijk = vlindervriendelijk;
            }
            if (eetbaar!=null)
            {
                extraEigenschap.Eetbaar = eetbaar;
            }
            if (kruidgebruik!=null)
            {
                extraEigenschap.Kruidgebruik = kruidgebruik;
            }
            if (geurend!=null)
            {
                extraEigenschap.Geurend = geurend;
            }
            if (voorstgevoelig!=null)
            {
                extraEigenschap.Vorstgevoelig = voorstgevoelig;
            };
            context.ExtraEigenschaps.Add(extraEigenschap);
            return extraEigenschap;
        }
    }
}
