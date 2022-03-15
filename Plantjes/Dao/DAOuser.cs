using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantjes.Models.Db;
using System.Security.Cryptography;

namespace Plantjes.Dao
{
    internal class DAOuser:DAObase
    {
        //written by Warre
        /// <summary>
        /// <see cref="GetList{Gebruiker}(bool)"/>.
        /// </summary>
        /// <param name="email">The email to be parsed.</param>
        /// <returns>Retursn the user with said email.</returns>
        public static Gebruiker GetGebruiker(string email)
        {
            return GetList<Gebruiker>().FirstOrDefault(g => g.Emailadres == email);
        }

        //written by Renzo
        /// <summary>
        /// Adds a user to the db.
        /// </summary>
        /// <param name="vivesNr">The vives nr of the user.</param>
        /// <param name="firstName">The firstname of the user.</param>
        /// <param name="lastName">The lastname of the user.</param>
        /// <param name="emailadres">The email of the user.</param>
        /// <param name="password">The password of the user.</param>
        public static void AddUser(string vivesNr, string firstName, string lastName, string emailadres, string password)
        {
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var md5Hasher = new MD5CryptoServiceProvider();
            var passwordHashed = md5Hasher.ComputeHash(passwordBytes);

            //written by Warre

            var gebruiker = new Gebruiker()
            {
                Vivesnr = vivesNr,
                Voornaam = firstName,
                Achternaam = lastName,
                Emailadres = emailadres,
                HashPaswoord = passwordHashed
            };
            context.Gebruikers.Add(gebruiker);
            _ = context.SaveChanges();
        }

    }
}
