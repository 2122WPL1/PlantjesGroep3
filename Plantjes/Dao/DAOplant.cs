using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plantjes.Dao
{
    public class DAOPlant : DAObase
    {
        public static void AddPlant(string type, string familie, string geslacht, string soort, string variant, Abiotiek abiotiek,AbiotiekMulti abiotiekmulti, BeheerMaand beheermaand, Commensalisme commensalisme,CommensalismeMulti commensalismemulti, Fenotype fenotype,
            string fgsv, string nederlandsnaam, short plantdichtheidmin, short plantdichtheidmax, short status, int idaccess, int typeid,
            int familielid, int geslachtid, int soortidd, int variantid, string bezonning, string antagonischeOmgeving, string grondsoort, string vochtbehoefte,
            string voedingsboehoefte, ICollection<AbiotiekMulti> abiotiekMultis, ICollection<Abiotiek> abiotieks,
            ICollection<BeheerMaand> beheerMaands, ICollection<CommensalismeMulti> commensalismemultis, ICollection<Commensalisme> commensalismes,
            ICollection<ExtraEigenschap> extraEigenschaps, ICollection<Fenotype> fenotypes, ICollection<Foto> fotos,
            ICollection<UpdatePlant> updatePlants)
        {
            Plant plant = new Plant();
            context.Plants.Add(plant);
            long plantId = GetListWhere<Plant>(p => p == plant).First().PlantId;

            List<AbiotiekMulti> AbiotiekMultis = new List<AbiotiekMulti>();
            //string var =
            context.Plants.Add(new Plant()
            {
                Abiotieks = new List<Abiotiek>()
                {
                    abiotiek
                },
                AbiotiekMultis = new List<AbiotiekMulti>()
                {
                    abiotiekmulti
                },
                BeheerMaands = new List<BeheerMaand>()
                {
                    beheermaand
                },
                Commensalismes = new List<Commensalisme>()
                {
                    commensalisme
                },
                CommensalismeMultis = new List<CommensalismeMulti>()
                {
                    commensalismemulti
                },
                Fenotypes = new List<Fenotype>()
                {
                    fenotype
                }

            }); 
            _ = context.SaveChanges();
        }
    }
}
