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
        /// <see cref="GetList{Gebruiker}(bool)"/>.
        /// </summary>
        /// <param name="email">The email to be parsed.</param>
        /// <returns>Retursn the user with said email.</returns>
        public Gebruiker GetGebruiker(string email)
        {
            return GetList<Gebruiker>().FirstOrDefault(g => g.Emailadres == email);
        }

        ///Owen
        public string GetImageLocation(long plantId , string imageCategorie)
        {
            var foto = context.Fotos.Where(s=>s.Eigenschap == imageCategorie).SingleOrDefault(s=> s.Plant == plantId);

            if (foto != null)
            {
                var location = foto;
                return location.UrlLocatie;
            }

            return null;
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
    }
}
