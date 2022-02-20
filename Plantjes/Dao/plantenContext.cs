using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Plantjes.Models.Db;

namespace Plantjes.Dao
{ 
    public partial class plantenContext : DbContext
    {
        public plantenContext()
        {
        }

        public plantenContext(DbContextOptions<plantenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AbioBezonning> AbioBezonnings { get; set; }
        public virtual DbSet<AbioGrondsoort> AbioGrondsoorts { get; set; }
        public virtual DbSet<AbioHabitat> AbioHabitats { get; set; }
        public virtual DbSet<AbioReactieAntagonischeOmg> AbioReactieAntagonischeOmgs { get; set; }
        public virtual DbSet<AbioVochtbehoefte> AbioVochtbehoeftes { get; set; }
        public virtual DbSet<AbioVoedingsbehoefte> AbioVoedingsbehoeftes { get; set; }
        public virtual DbSet<Abiotiek> Abiotieks { get; set; }
        public virtual DbSet<AbiotiekMulti> AbiotiekMultis { get; set; }
        public virtual DbSet<BeheerMaand> BeheerMaands { get; set; }
        public virtual DbSet<CommLevensvorm> CommLevensvorms { get; set; }
        public virtual DbSet<CommOntwikkelsnelheid> CommOntwikkelsnelheids { get; set; }
        public virtual DbSet<CommSocialbiliteit> CommSocialbiliteits { get; set; }
        public virtual DbSet<CommStrategie> CommStrategies { get; set; }
        public virtual DbSet<Commensalisme> Commensalismes { get; set; }
        public virtual DbSet<CommensalismeMulti> CommensalismeMultis { get; set; }
        public virtual DbSet<ExtraEigenschap> ExtraEigenschaps { get; set; }
        public virtual DbSet<ExtraNectarwaarde> ExtraNectarwaardes { get; set; }
        public virtual DbSet<ExtraPollenwaarde> ExtraPollenwaardes { get; set; }
        public virtual DbSet<FenoBladgrootte> FenoBladgroottes { get; set; }
        public virtual DbSet<FenoBladvorm> FenoBladvorms { get; set; }
        public virtual DbSet<FenoBloeiwijze> FenoBloeiwijzes { get; set; }
        public virtual DbSet<FenoHabitu> FenoHabitus { get; set; }
        public virtual DbSet<FenoKleur> FenoKleurs { get; set; }
        public virtual DbSet<FenoLevensvorm> FenoLevensvorms { get; set; }
        public virtual DbSet<FenoSpruitfenologie> FenoSpruitfenologies { get; set; }
        public virtual DbSet<Fenotype> Fenotypes { get; set; }
        public virtual DbSet<FenotypeMulti> FenotypeMultis { get; set; }
        public virtual DbSet<Foto> Fotos { get; set; }
        public virtual DbSet<Gebruiker> Gebruikers { get; set; }
        public virtual DbSet<Plant> Plants { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<TfgsvFamilie> TfgsvFamilies { get; set; }
        public virtual DbSet<TfgsvGeslacht> TfgsvGeslachts { get; set; }
        public virtual DbSet<TfgsvSoort> TfgsvSoorts { get; set; }
        public virtual DbSet<TfgsvType> TfgsvTypes { get; set; }
        public virtual DbSet<TfgsvVariant> TfgsvVariants { get; set; }
        public virtual DbSet<UpdatePlant> UpdatePlants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(SQLConnection.connectionstring);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbioBezonning>(entity =>
            {
                entity.ToTable("Abio_Bezonning");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Naam)
                    .HasMaxLength(30)
                    .HasColumnName("naam");
            });

            modelBuilder.Entity<AbioGrondsoort>(entity =>
            {
                entity.ToTable("Abio_Grondsoort");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Grondsoort)
                    .HasMaxLength(10)
                    .HasColumnName("grondsoort");
            });

            modelBuilder.Entity<AbioHabitat>(entity =>
            {
                entity.ToTable("Abio_Habitat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Afkorting)
                    .HasMaxLength(10)
                    .HasColumnName("afkorting");

                entity.Property(e => e.Waarde)
                    .HasMaxLength(100)
                    .HasColumnName("waarde");
            });

            modelBuilder.Entity<AbioReactieAntagonischeOmg>(entity =>
            {
                entity.ToTable("Abio_Reactie_Antagonische_Omg");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Antagonie)
                    .HasMaxLength(50)
                    .HasColumnName("antagonie");
            });

            modelBuilder.Entity<AbioVochtbehoefte>(entity =>
            {
                entity.ToTable("Abio_Vochtbehoefte");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Vochtbehoefte)
                    .HasMaxLength(30)
                    .HasColumnName("vochtbehoefte");
            });

            modelBuilder.Entity<AbioVoedingsbehoefte>(entity =>
            {
                entity.ToTable("Abio_Voedingsbehoefte");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Voedingsbehoefte)
                    .HasMaxLength(20)
                    .HasColumnName("voedingsbehoefte");
            });

            modelBuilder.Entity<Abiotiek>(entity =>
            {
                entity.ToTable("Abiotiek");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AntagonischeOmgeving)
                    .HasMaxLength(50)
                    .HasColumnName("antagonischeOmgeving");

                entity.Property(e => e.Bezonning)
                    .HasMaxLength(30)
                    .HasColumnName("bezonning");

                entity.Property(e => e.Grondsoort)
                    .HasMaxLength(3)
                    .HasColumnName("grondsoort");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Vochtbehoefte)
                    .HasMaxLength(30)
                    .HasColumnName("vochtbehoefte");

                entity.Property(e => e.Voedingsbehoefte)
                    .HasMaxLength(20)
                    .HasColumnName("voedingsbehoefte");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Abiotieks)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("abiotiek_plant_FK");
            });

            modelBuilder.Entity<AbiotiekMulti>(entity =>
            {
                entity.ToTable("Abiotiek_Multi");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Eigenschap)
                    .HasMaxLength(15)
                    .HasColumnName("eigenschap");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Waarde)
                    .HasMaxLength(100)
                    .HasColumnName("waarde");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.AbiotiekMultis)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commensalismev1_plant_FK");
            });

            modelBuilder.Entity<BeheerMaand>(entity =>
            {
                entity.ToTable("Beheer_Maand");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apr).HasColumnName("apr");

                entity.Property(e => e.Aug).HasColumnName("aug");

                entity.Property(e => e.Beheerdaad)
                    .HasMaxLength(150)
                    .HasColumnName("beheerdaad");

                entity.Property(e => e.Dec).HasColumnName("dec");

                entity.Property(e => e.Feb).HasColumnName("feb");

                entity.Property(e => e.Jan).HasColumnName("jan");

                entity.Property(e => e.Jul).HasColumnName("jul");

                entity.Property(e => e.Jun).HasColumnName("jun");

                entity.Property(e => e.Mei).HasColumnName("mei");

                entity.Property(e => e.Mrt).HasColumnName("mrt");

                entity.Property(e => e.Nov).HasColumnName("nov");

                entity.Property(e => e.Okt).HasColumnName("okt");

                entity.Property(e => e.Omschrijving)
                    .HasMaxLength(1000)
                    .HasColumnName("omschrijving");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Sept).HasColumnName("sept");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.BeheerMaands)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("beheer_maand_plant_FK");
            });

            modelBuilder.Entity<CommLevensvorm>(entity =>
            {
                entity.ToTable("Comm_Levensvorm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Levensvorm)
                    .HasMaxLength(100)
                    .HasColumnName("levensvorm");
            });

            modelBuilder.Entity<CommOntwikkelsnelheid>(entity =>
            {
                entity.ToTable("Comm_Ontwikkelsnelheid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Snelheid)
                    .HasMaxLength(20)
                    .HasColumnName("snelheid");
            });

            modelBuilder.Entity<CommSocialbiliteit>(entity =>
            {
                entity.ToTable("Comm_Socialbiliteit");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Sociabiliteit)
                    .HasMaxLength(50)
                    .HasColumnName("sociabiliteit");

                entity.Property(e => e.Waarde)
                    .HasMaxLength(100)
                    .HasColumnName("waarde");
            });

            modelBuilder.Entity<CommStrategie>(entity =>
            {
                entity.ToTable("Comm_Strategie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Strategie)
                    .HasMaxLength(10)
                    .HasColumnName("strategie");
            });

            modelBuilder.Entity<Commensalisme>(entity =>
            {
                entity.ToTable("Commensalisme");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ontwikkelsnelheid)
                    .HasMaxLength(10)
                    .HasColumnName("ontwikkelsnelheid");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Strategie)
                    .HasMaxLength(10)
                    .HasColumnName("strategie");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Commensalismes)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commensalisme_multiplev2_plant_FK");
            });

            modelBuilder.Entity<CommensalismeMulti>(entity =>
            {
                entity.ToTable("Commensalisme_Multi");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Eigenschap)
                    .HasMaxLength(50)
                    .HasColumnName("eigenschap");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Waarde)
                    .HasMaxLength(200)
                    .HasColumnName("waarde");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.CommensalismeMultis)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commensalisme_plant_FK");
            });

            modelBuilder.Entity<ExtraEigenschap>(entity =>
            {
                entity.ToTable("ExtraEigenschap");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bijvriendelijke).HasColumnName("bijvriendelijke");

                entity.Property(e => e.Eetbaar).HasColumnName("eetbaar");

                entity.Property(e => e.Geurend).HasColumnName("geurend");

                entity.Property(e => e.Kruidgebruik).HasColumnName("kruidgebruik");

                entity.Property(e => e.Nectarwaarde)
                    .HasMaxLength(5)
                    .HasColumnName("nectarwaarde");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Pollenwaarde)
                    .HasMaxLength(5)
                    .HasColumnName("pollenwaarde");

                entity.Property(e => e.Vlindervriendelijk).HasColumnName("vlindervriendelijk");

                entity.Property(e => e.Vorstgevoelig).HasColumnName("vorstgevoelig");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.ExtraEigenschaps)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ExtraEigenschap_plant_FK");
            });

            modelBuilder.Entity<ExtraNectarwaarde>(entity =>
            {
                entity.ToTable("Extra_Nectarwaarde");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Waarde)
                    .HasMaxLength(10)
                    .HasColumnName("waarde");
            });

            modelBuilder.Entity<ExtraPollenwaarde>(entity =>
            {
                entity.ToTable("Extra_Pollenwaarde");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Waarde)
                    .HasMaxLength(10)
                    .HasColumnName("waarde");
            });

            modelBuilder.Entity<FenoBladgrootte>(entity =>
            {
                entity.ToTable("Feno_Bladgrootte");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bladgrootte)
                    .HasMaxLength(20)
                    .HasColumnName("bladgrootte");
            });

            modelBuilder.Entity<FenoBladvorm>(entity =>
            {
                entity.ToTable("Feno_Bladvorm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Vorm)
                    .HasMaxLength(30)
                    .HasColumnName("vorm");
            });

            modelBuilder.Entity<FenoBloeiwijze>(entity =>
            {
                entity.ToTable("Feno_Bloeiwijze");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Figuur)
                    .HasColumnType("image")
                    .HasColumnName("figuur");

                entity.Property(e => e.Naam)
                    .HasMaxLength(20)
                    .HasColumnName("naam");

                entity.Property(e => e.UrlLocatie)
                    .HasMaxLength(500)
                    .HasColumnName("url/locatie");
            });

            modelBuilder.Entity<FenoHabitu>(entity =>
            {
                entity.ToTable("Feno_Habitus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Figuur)
                    .HasColumnType("image")
                    .HasColumnName("figuur");

                entity.Property(e => e.Naam)
                    .HasMaxLength(30)
                    .HasColumnName("naam");

                entity.Property(e => e.UrlLocatie)
                    .HasMaxLength(500)
                    .HasColumnName("url/locatie");
            });

            modelBuilder.Entity<FenoKleur>(entity =>
            {
                entity.ToTable("Feno_Kleur");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HexWaarde)
                    .HasMaxLength(3)
                    .HasColumnName("hex_waarde");

                entity.Property(e => e.NaamKleur)
                    .HasMaxLength(20)
                    .HasColumnName("naam kleur");
            });

            modelBuilder.Entity<FenoLevensvorm>(entity =>
            {
                entity.ToTable("Feno_Levensvorm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Figuur)
                    .HasColumnType("image")
                    .HasColumnName("figuur");

                entity.Property(e => e.Levensvorm)
                    .HasMaxLength(100)
                    .HasColumnName("levensvorm");

                entity.Property(e => e.UrlLocatie)
                    .HasMaxLength(500)
                    .HasColumnName("url/locatie");
            });

            modelBuilder.Entity<FenoSpruitfenologie>(entity =>
            {
                entity.ToTable("Feno_Spruitfenologie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fenologie)
                    .HasMaxLength(20)
                    .HasColumnName("fenologie");
            });

            modelBuilder.Entity<Fenotype>(entity =>
            {
                entity.ToTable("Fenotype");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bladgrootte).HasColumnName("bladgrootte");

                entity.Property(e => e.Bladvorm)
                    .HasMaxLength(50)
                    .HasColumnName("bladvorm");

                entity.Property(e => e.Bloeiwijze)
                    .HasMaxLength(20)
                    .HasColumnName("bloeiwijze");

                entity.Property(e => e.Habitus)
                    .HasMaxLength(20)
                    .HasColumnName("habitus");

                entity.Property(e => e.Levensvorm)
                    .HasMaxLength(50)
                    .HasColumnName("levensvorm");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.RatioBloeiBlad)
                    .HasMaxLength(50)
                    .HasColumnName("ratioBloeiBlad");

                entity.Property(e => e.Spruitfenologie)
                    .HasMaxLength(50)
                    .HasColumnName("spruitfenologie");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Fenotypes)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fenotype_plant_FK");
            });

            modelBuilder.Entity<FenotypeMulti>(entity =>
            {
                entity.ToTable("Fenotype_Multi");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Eigenschap)
                    .HasMaxLength(50)
                    .HasColumnName("eigenschap");

                entity.Property(e => e.Maand)
                    .HasMaxLength(10)
                    .HasColumnName("maand");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Waarde)
                    .HasMaxLength(50)
                    .HasColumnName("waarde");
            });

            modelBuilder.Entity<Foto>(entity =>
            {
                entity.ToTable("Foto");

                entity.Property(e => e.Fotoid).HasColumnName("fotoid");

                entity.Property(e => e.Eigenschap)
                    .HasMaxLength(20)
                    .HasColumnName("eigenschap");

                entity.Property(e => e.Plant).HasColumnName("plant");

                entity.Property(e => e.Tumbnail)
                    .HasColumnType("image")
                    .HasColumnName("tumbnail");

                entity.Property(e => e.UrlLocatie)
                    .HasMaxLength(500)
                    .HasColumnName("url/locatie");

                entity.HasOne(d => d.PlantNavigation)
                    .WithMany(p => p.Fotos)
                    .HasForeignKey(d => d.Plant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("foto_plant_FK");
            });

            modelBuilder.Entity<Gebruiker>(entity =>
            {
                entity.ToTable("Gebruiker");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Achternaam)
                    .HasMaxLength(50)
                    .HasColumnName("achternaam");

                entity.Property(e => e.Emailadres)
                    .HasMaxLength(150)
                    .HasColumnName("emailadres");

                entity.Property(e => e.HashPaswoord)
                    .HasMaxLength(64)
                    .HasColumnName("hashPaswoord");

                entity.Property(e => e.LastLogin)
                    .HasColumnType("date")
                    .HasColumnName("last_login");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("rol");

                entity.Property(e => e.Vivesnr)
                    .HasMaxLength(15)
                    .HasColumnName("vivesnr");

                entity.Property(e => e.Voornaam)
                    .HasMaxLength(50)
                    .HasColumnName("voornaam");
            });

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.ToTable("Plant");

                entity.Property(e => e.PlantId).HasColumnName("plant-id");

                entity.Property(e => e.Familie)
                    .HasMaxLength(100)
                    .HasColumnName("familie");

                entity.Property(e => e.FamilieId).HasColumnName("familieID");

                entity.Property(e => e.Fgsv)
                    .HasMaxLength(500)
                    .HasColumnName("fgsv");

                entity.Property(e => e.Geslacht)
                    .HasMaxLength(100)
                    .HasColumnName("geslacht");

                entity.Property(e => e.GeslachtId).HasColumnName("geslachtID");

                entity.Property(e => e.IdAccess).HasColumnName("idAccess");

                entity.Property(e => e.NederlandsNaam)
                    .HasMaxLength(500)
                    .HasColumnName("nederlands naam");

                entity.Property(e => e.PlantdichtheidMax).HasColumnName("plantdichtheid_max");

                entity.Property(e => e.PlantdichtheidMin).HasColumnName("plantdichtheid_min");

                entity.Property(e => e.Soort)
                    .HasMaxLength(100)
                    .HasColumnName("soort");

                entity.Property(e => e.SoortId).HasColumnName("soortID");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .HasColumnName("type");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.Variant)
                    .HasMaxLength(100)
                    .HasColumnName("variant");

                entity.Property(e => e.VariantId).HasColumnName("variantID");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Omschrijving)
                    .HasMaxLength(20)
                    .HasColumnName("omschrijving");
            });

            modelBuilder.Entity<TfgsvFamilie>(entity =>
            {
                entity.HasKey(e => e.FamileId)
                    .HasName("familie_PK");

                entity.ToTable("Tfgsv_Familie");

                entity.Property(e => e.FamileId).HasColumnName("famile_id");

                entity.Property(e => e.Familienaam)
                    .HasMaxLength(100)
                    .HasColumnName("familienaam");

                entity.Property(e => e.NlNaam)
                    .HasMaxLength(100)
                    .HasColumnName("NL naam");

                entity.Property(e => e.TypeTypeid).HasColumnName("type_typeid");

                entity.HasOne(d => d.TypeType)
                    .WithMany(p => p.TfgsvFamilies)
                    .HasForeignKey(d => d.TypeTypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("familie_type_FK");
            });

            modelBuilder.Entity<TfgsvGeslacht>(entity =>
            {
                entity.HasKey(e => e.GeslachtId)
                    .HasName("geslacht_PK");

                entity.ToTable("Tfgsv_Geslacht");

                entity.Property(e => e.GeslachtId).HasColumnName("geslacht_id");

                entity.Property(e => e.FamilieFamileId).HasColumnName("familie_famile_id");

                entity.Property(e => e.Geslachtnaam)
                    .HasMaxLength(100)
                    .HasColumnName("geslachtnaam");

                entity.Property(e => e.NlNaam)
                    .HasMaxLength(100)
                    .HasColumnName("NL naam");

                entity.HasOne(d => d.FamilieFamile)
                    .WithMany(p => p.TfgsvGeslachts)
                    .HasForeignKey(d => d.FamilieFamileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("geslacht_familie_FK");
            });

            modelBuilder.Entity<TfgsvSoort>(entity =>
            {
                entity.HasKey(e => e.Soortid)
                    .HasName("soort_PK");

                entity.ToTable("Tfgsv_Soort");

                entity.Property(e => e.Soortid).HasColumnName("soortid");

                entity.Property(e => e.GeslachtGeslachtId).HasColumnName("geslacht_geslacht_id");

                entity.Property(e => e.NlNaam)
                    .HasMaxLength(100)
                    .HasColumnName("NL naam");

                entity.Property(e => e.Soortnaam)
                    .HasMaxLength(100)
                    .HasColumnName("soortnaam");

                entity.HasOne(d => d.GeslachtGeslacht)
                    .WithMany(p => p.TfgsvSoorts)
                    .HasForeignKey(d => d.GeslachtGeslachtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("soort_geslacht_FK");
            });

            modelBuilder.Entity<TfgsvType>(entity =>
            {
                entity.HasKey(e => e.Planttypeid)
                    .HasName("type_PK");

                entity.ToTable("Tfgsv_Type");

                entity.Property(e => e.Planttypeid).HasColumnName("planttypeid");

                entity.Property(e => e.Planttypenaam)
                    .HasMaxLength(100)
                    .HasColumnName("planttypenaam");
            });

            modelBuilder.Entity<TfgsvVariant>(entity =>
            {
                entity.HasKey(e => e.VariantId)
                    .HasName("variant_PK");

                entity.ToTable("Tfgsv_Variant");

                entity.Property(e => e.VariantId).HasColumnName("variantID");

                entity.Property(e => e.NlNaam)
                    .HasMaxLength(100)
                    .HasColumnName("NL naam");

                entity.Property(e => e.SoortSoortid).HasColumnName("soort_soortid");

                entity.Property(e => e.Variantnaam)
                    .HasMaxLength(100)
                    .HasColumnName("variantnaam");
            });

            modelBuilder.Entity<UpdatePlant>(entity =>
            {
                entity.ToTable("Update_Plant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Plantid).HasColumnName("plantid");

                entity.Property(e => e.Updatedatum)
                    .HasColumnType("datetime")
                    .HasColumnName("updatedatum");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.UpdatePlants)
                    .HasForeignKey(d => d.Plantid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("update_plant_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UpdatePlants)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("update_users_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
