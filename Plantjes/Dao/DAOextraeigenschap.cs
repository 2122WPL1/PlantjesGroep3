using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantjes.Models.Db;

namespace Plantjes.Dao
{
    internal class DaoExtraeigenschap : DaoBase
    {
        public static ExtraEigenschap AddExtraEigenschap(string nectarwaarde = null, string pollenwaarde = null, bool? bijvriendelijke = null,
            bool? vlindervriendelijk = null, bool? eetbaar = null, bool? kruidgebruik = null, bool? geurend = null, bool? vorstgevoelig = null)
        {
            if (new List<object>() { nectarwaarde, pollenwaarde, bijvriendelijke, vlindervriendelijk, eetbaar, kruidgebruik, geurend, vorstgevoelig }.All(o => o == null))
            {
                return null;
            }
            ExtraEigenschap extraEigenschap = new ExtraEigenschap();
            if (nectarwaarde != null)
            {
                extraEigenschap.Nectarwaarde = nectarwaarde;
            }
            if (pollenwaarde != null)
            {
                extraEigenschap.Pollenwaarde = pollenwaarde;
            }
            if (bijvriendelijke != null)
            {
                extraEigenschap.Bijvriendelijke = bijvriendelijke;
            }
            if (vlindervriendelijk != null)
            {
                extraEigenschap.Vlindervriendelijk = vlindervriendelijk;
            }
            if (eetbaar != null)
            {
                extraEigenschap.Eetbaar = eetbaar;
            }
            if (kruidgebruik != null)
            {
                extraEigenschap.Kruidgebruik = kruidgebruik;
            }
            if (geurend != null)
            {
                extraEigenschap.Geurend = geurend;
            }
            if (vorstgevoelig != null)
            {
                extraEigenschap.Vorstgevoelig = vorstgevoelig;
            };
            context.ExtraEigenschaps.Add(extraEigenschap);
            context.SaveChanges();
            return extraEigenschap;
        }
    }
}
