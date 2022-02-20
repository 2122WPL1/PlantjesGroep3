using System;
using System.Collections.Generic;

namespace Plantjes.Models.Db
{
    public partial class Plant
    {
        public Plant()
        {
            AbiotiekMultis = new HashSet<AbiotiekMulti>();
            Abiotieks = new HashSet<Abiotiek>();
            BeheerMaands = new HashSet<BeheerMaand>();
            CommensalismeMultis = new HashSet<CommensalismeMulti>();
            Commensalismes = new HashSet<Commensalisme>();
            ExtraEigenschaps = new HashSet<ExtraEigenschap>();
            Fenotypes = new HashSet<Fenotype>();
            Fotos = new HashSet<Foto>();
            UpdatePlants = new HashSet<UpdatePlant>();
        }

        public long PlantId { get; set; }
        public string Type { get; set; }
        public string Familie { get; set; }
        public string Geslacht { get; set; }
        public string Soort { get; set; }
        public string Variant { get; set; }
        public string Fgsv { get; set; }
        public string NederlandsNaam { get; set; }
        public short? PlantdichtheidMin { get; set; }
        public short? PlantdichtheidMax { get; set; }
        public short? Status { get; set; }
        public int? IdAccess { get; set; }
        public int? TypeId { get; set; }
        public int? FamilieId { get; set; }
        public int? GeslachtId { get; set; }
        public int? SoortId { get; set; }
        public int? VariantId { get; set; }

        public virtual ICollection<AbiotiekMulti> AbiotiekMultis { get; set; }
        public virtual ICollection<Abiotiek> Abiotieks { get; set; }
        public virtual ICollection<BeheerMaand> BeheerMaands { get; set; }
        public virtual ICollection<CommensalismeMulti> CommensalismeMultis { get; set; }
        public virtual ICollection<Commensalisme> Commensalismes { get; set; }
        public virtual ICollection<ExtraEigenschap> ExtraEigenschaps { get; set; }
        public virtual ICollection<Fenotype> Fenotypes { get; set; }
        public virtual ICollection<Foto> Fotos { get; set; }
        public virtual ICollection<UpdatePlant> UpdatePlants { get; set; }
    }
}
