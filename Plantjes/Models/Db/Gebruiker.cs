using System;
using System.Collections.Generic;

namespace Plantjes.Models.Db
{
    public partial class Gebruiker
    {
        public Gebruiker()
        {
            UpdatePlants = new HashSet<UpdatePlant>();
        }

        public int Id { get; set; }
        public string Vivesnr { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public int RolId { get; set; }
        public string Emailadres { get; set; }
        public DateTime? LastLogin { get; set; }
        public byte[] HashPaswoord { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual ICollection<UpdatePlant> UpdatePlants { get; set; }
    }
}
