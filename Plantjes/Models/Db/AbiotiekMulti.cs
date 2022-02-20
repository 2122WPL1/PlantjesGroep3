using System;
using System.Collections.Generic;

namespace Plantjes.Models.Db
{
    public partial class AbiotiekMulti
    {
        public long Id { get; set; }
        public long PlantId { get; set; }
        public string Eigenschap { get; set; }
        public string Waarde { get; set; }

        public virtual Plant Plant { get; set; }
    }
}
