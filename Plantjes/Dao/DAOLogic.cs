using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using Plantjes.Models;
using Plantjes.Models.Db;
using Microsoft.EntityFrameworkCore;
using System;
using Plantjes.Models.Enums;
/*comments kenny*/

namespace Plantjes.Dao
{
    public class DAOLogic
    {
        //Robin: opzetten DAOLogic, singleton pattern

        //1.een statische private instantie instatieren die enkel kan gelezen worden.
        //deze wordt altijd teruggegeven wanneer de Instance method wordt opgeroepen
        private static readonly DAOLogic instance = new DAOLogic();

        /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
        private readonly plantenContext context;

        //2. private contructor
        private DAOLogic()
        {
            /*Niet noodzakelijk voor de singletonpattern waar wel voor de DAOLogic*/
            this.context = new plantenContext();
        }
        //3.publieke methode instance die altijd kan aangeroepen worden
        //door zijn statische eigenschappen kan hij altijd aangeroepen worden 
        //zonder er een instantie van te maken
        public static DAOLogic Instance()
        {
            return instance;
        }


        #region Get methods
        //written by Warre
        /// <summary>
        /// Gets a list of type <see cref="DbSet{TEntity}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the list.</typeparam>
        /// <param name="distinct">Boolean which decides if list must be distinct.</param>
        /// <returns>Returns of a list of type <see cref="DbSet{TEntity}"/>.</returns>
        public IEnumerable<TEntity> GetList<TEntity>(bool distinct = false) where TEntity : class
        {
            var dbset = context.Set<TEntity>();

            return distinct ? dbset.Distinct() : dbset;
        }

        //written by Warre
        /// <summary>
        /// <see cref="GetList{TEntity}(bool)"/> with a where predicate.
        /// </summary>
        /// <typeparam name="TEntity">The type of the list.</typeparam>
        /// <param name="predicate">The requirement of the where.</param>
        /// <param name="distinct"><see cref="GetList{TEntity}(bool)"/>.</param>
        /// <returns>Returns of a list of type <see cref="DbSet{TEntity}"/>.</returns>
        public IEnumerable<TEntity> GetListWhere<TEntity>(Func<TEntity, bool> predicate, bool distinct = false) where TEntity : class
        {
            return GetList<TEntity>(distinct).Where(predicate);
        }

        //written by Warre
        /// <summary>
        /// <see cref="GetListWhere{Gebruiker}(Func{Gebruiker, bool}, bool)"/>.
        /// </summary>
        /// <param name="email">The email to be parsed.</param>
        /// <returns>Retursn the user with said email.</returns>
        public Gebruiker GetGebruiker(string email)
        {
            return GetListWhere<Gebruiker>(g => g.Emailadres == email).FirstOrDefault();
        }

        ///Owen
        public string GetImages(long id , string ImageCategorie)
        {
            var foto = context.Fotos.Where(s=>s.Eigenschap == ImageCategorie).SingleOrDefault(s=> s.Plant == id);
            

            if (foto != null)
            {
                var location = foto;
                return location.UrlLocatie;
            }

            return null;
        }

        ///Robin
        //Get a list of all the Abiotiek types
        public List<Abiotiek> GetAllAbiotieks()
        {
            var abiotiek = context.Abiotieks.ToList();
            return abiotiek;
        }

        //Get a list of all the AbiotiekMulti types
        public List<AbiotiekMulti> GetAllAbiotieksMulti()
        {
            //List is unfiltered, a plantId can be present multiple times
            //The aditional filteren will take place in the ViewModel

            var abioMultiList = context.AbiotiekMultis.ToList();

            return abioMultiList;
        }

        //Get a list of all the Beheermaand types
        public List<BeheerMaand> GetBeheerMaanden()
        {
            var beheerMaanden = context.BeheerMaands.ToList();
            return beheerMaanden;
        }

        public List<Commensalisme> GetAllCommensalisme()
        {
            var commensalisme = context.Commensalismes.ToList();
            return commensalisme;
        }

        public List<CommensalismeMulti> GetAllCommensalismeMulti()
        {
            //List is unfiltered, a plantId can be present multiple times
            //The aditional filtering will take place in the ViewModel

            var commensalismeMulti = context.CommensalismeMultis.ToList();
            return commensalismeMulti;
        }

        public List<ExtraEigenschap> GetAllExtraEigenschap()
        {
            var extraEigenschap = context.ExtraEigenschaps.ToList();
            return extraEigenschap;
        }

        public List<Fenotype> GetAllFenoTypes()
        {
            var fenoTypes = context.Fenotypes
                .ToList();
            return fenoTypes;
        }

        public List<Foto> GetAllFoto()
        {
            var foto = context.Fotos.ToList();
            return foto;
        }

        public List<UpdatePlant> GetAllUpdatePlant()
        {
            var updatePlant = context.UpdatePlants.ToList();
            return updatePlant;
        }

        public List<ExtraPollenwaarde> FillExtraPollenwaardes()
        {
            var selection = context.ExtraPollenwaardes.ToList();
            return selection;
        }

        public List<ExtraNectarwaarde> FillExtraNectarwaardes()
        {
            var selection = context.ExtraNectarwaardes.ToList();
            return selection;
        }

        public List<BeheerMaand> FillBeheerdaad()
        {
            var selection = context.BeheerMaands.ToList();
            return selection;
        }
        #endregion

        #region Add/Set methods
        /// <summary>
        /// Adds a user to the db.
        /// </summary>
        /// <param name="vivesNr">The vives nr of the user.</param>
        /// <param name="firstName">The firstname of the user.</param>
        /// <param name="lastName">The lastname of the user.</param>
        /// <param name="emailadres">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        public void AddUser(string vivesNr, string firstName, string lastName, string emailadres, string password)
        {
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var md5Hasher = new MD5CryptoServiceProvider();
            var passwordHashed = md5Hasher.ComputeHash(passwordBytes);

            //written by Warre
            UserRole role = UserRole.GradStudent;
            if (emailadres.Contains("@vives.be"))
                role = UserRole.Docent;
            if (emailadres.Contains("@student.vives.be"))
                role = UserRole.Student;

            var gebruiker = new Gebruiker()
            {
                Vivesnr = vivesNr,
                Voornaam = firstName,
                Achternaam = lastName,
                Emailadres = emailadres,
                Rol = role.ToString(),
                HashPaswoord = passwordHashed
            };
            context.Gebruikers.Add(gebruiker);
            _ = context.SaveChanges();
        }
        #endregion

        #region FilterFromPlant
        ///Owen: op basis van basiscode Kenny, Christophe
        #region FilterFenoTypeFromPlant 

        public IQueryable<Fenotype> filterFenoTypeFromPlant(int selectedItem)
        {

            var selection = context.Fenotypes.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }

        public IQueryable<FenotypeMulti> FilterFenotypeMultiFromPlant(int selectedItem)
        {

            var selection = context.FenotypeMultis.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }
        #endregion

        #region FilterAbiotiekFromPlant
        public IQueryable<Abiotiek> filterAbiotiekFromPlant(int selectedItem)
        {

            var selection = context.Abiotieks.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }

        public IQueryable<AbiotiekMulti> filterAbiotiekMultiFromPlant(int selectedItem)
        {

            var selection = context.AbiotiekMultis.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }


        #endregion

        #region FilterBeheerMaandFromPlant
        public IQueryable<BeheerMaand> FilterBeheerMaandFromPlant(int selectedItem)
        {

            var selection = context.BeheerMaands.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }


        #endregion

        #region FilterCommensalismeFromPlant
        public IQueryable<Commensalisme> FilterCommensalismeFromPlant(int selectedItem)
        {

            var selection = context.Commensalismes.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }

        public IQueryable<CommensalismeMulti> FilterCommensalismeMulti(int selectedItem)
        {

            var selection = context.CommensalismeMultis.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }


        #endregion

        #region FilterExtraEigenschapFromPlant
        public IQueryable<ExtraEigenschap> FilterExtraEigenschapFromPlant(int selectedItem)
        {

            var selection = context.ExtraEigenschaps.Distinct().Where(s => s.PlantId == selectedItem);
            return selection;
        }


        #endregion

        #endregion
    }
}
