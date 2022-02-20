using System;
using System.Collections.Generic;

namespace Plantjes.Models.Db
{
    public partial class TfgsvType
    {
        public TfgsvType()
        {
            TfgsvFamilies = new HashSet<TfgsvFamilie>();
        }

        public long Planttypeid { get; set; }
        public string Planttypenaam { get; set; }

        public virtual ICollection<TfgsvFamilie> TfgsvFamilies { get; set; }
    }
}
