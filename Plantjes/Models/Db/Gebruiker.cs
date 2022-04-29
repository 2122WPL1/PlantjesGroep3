using System;
using System.Collections.Generic;
using Plantjes.ViewModels.HelpClasses;

namespace Plantjes.Models.Db
{
    public /*partial*/ class Gebruiker
    {
        public Gebruiker()
        {
            UpdatePlants = new HashSet<UpdatePlant>();
        }

        // Not-default constructor written by Ian Dumalin on 27/04
        public Gebruiker(string rNumber, string voornaam, string achternaam, string emailadres, byte[] paswoord)
        {
            this.Vivesnr = rNumber;
            this.Voornaam = voornaam;
            this.Achternaam = achternaam;
            this.Emailadres = emailadres;
            this.HashPaswoord = paswoord;

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
