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
        public static void AddPlant(string type, string familie, string geslacht, string soort, string variant, Abiotiek abiotiek,AbiotiekMulti abiotiekmulti,
            BeheerMaand beheermaand, Commensalisme commensalisme,CommensalismeMulti commensalismemulti, Fenotype fenotype, ExtraEigenschap extraEigenschap,
            UpdatePlant updateplant, Foto foto,string fgsv, string nederlandsnaam, short plantdichtheidmin, short plantdichtheidmax, short status, int idaccess, int typeid,
            int familielid, int geslachtid, int soortidd, int variantid, string bezonning, string antagonischeOmgeving, string grondsoort,
            string vochtbehoefte,string voedingsboehoefte, ICollection<AbiotiekMulti> abiotiekMultis, ICollection<Abiotiek> abiotieks,
            ICollection<BeheerMaand> beheerMaands, ICollection<CommensalismeMulti> commensalismemultis, ICollection<Commensalisme> commensalismes,
            ICollection<ExtraEigenschap> extraEigenschaps, ICollection<Fenotype> fenotypes, ICollection<Foto> fotos,ICollection<UpdatePlant> updatePlants)
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
                },
                ExtraEigenschaps = new List<ExtraEigenschap>()
                {
                    extraEigenschap
                },
                UpdatePlants = new List<UpdatePlant>()
                {
                    updateplant
                },
                Fotos = new List<Foto>()
                {
                    foto
                }

            }); 
            _ = context.SaveChanges();
        }
        public static UpdatePlant AddUpdatePlant(int id, long plantid, int userid, DateTime? updatedatum = null)
        {
            UpdatePlant updateplant = new UpdatePlant()
            {
                Id = id,
                Plantid = plantid,
                Userid = userid
            };
            if (updatedatum!=null)
            {
                updateplant.Updatedatum = updatedatum;
            }
            context.UpdatePlants.Add(updateplant);
            return updateplant;
        }
    }
}
