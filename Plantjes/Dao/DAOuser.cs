using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plantjes.Models.Db;
using System.Security.Cryptography;
using Plantjes.ViewModels.HelpClasses;

namespace Plantjes.Dao
{
    internal class DaoUser : DaoBase
    {
        //written by Warre
        /// <summary>
        /// <see cref="GetList{Gebruiker}(bool)"/>.
        /// </summary>
        /// <param name="email">The email to be parsed.</param>
        /// <returns>Returns the user with said email.</returns>
        public static Gebruiker GetGebruiker(string email)
        {
            return GetList<Gebruiker>().FirstOrDefault(g => g.Emailadres == email);
        }

        public static List<Gebruiker> GetGebruikerList()
        {
            return GetList<Gebruiker>().ToList();
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
        public static Gebruiker AddUser(string vivesNr, string firstName, string lastName, string emailadres, string password)
        {
            var passwordHashed = Helper.HashString(password);
            return UserToDb(vivesNr, firstName, lastName, emailadres, passwordHashed);
        }

        public static Gebruiker UserToDb(string vivesNr, string firstName, string lastName, string emailadres, byte[] password)
        {
            //written by Warre
            List<Rol> roles = GetList<Rol>().OrderBy(r => r.Id).ToList();
            int role = roles[2].Id;
            if (emailadres.ToLower().Contains("@vives.be"))
                role = roles[0].Id;
            if (emailadres.ToLower().Contains("@student.vives.be"))
                role = roles[1].Id;

            var gebruiker = new Gebruiker()
            {
                Vivesnr = vivesNr,
                Voornaam = firstName,
                Achternaam = lastName,
                Emailadres = emailadres,
                RolId = role,
                HashPaswoord = password
            };
            Context.Gebruikers.Add(gebruiker);
            Context.SaveChanges();
            return gebruiker;
        }

        public static void UpdateUserLogin(Gebruiker gebruiker)
        {
            Context.Gebruikers.Update(gebruiker);
            Context.SaveChanges();
        }

        public static void UpdateUser(Gebruiker gebruiker, byte[] password)
        {
            gebruiker.LastLogin = DateTime.Now;
            if (password != gebruiker.HashPaswoord)
            {
                gebruiker.HashPaswoord = password;
            }
            UpdateUserLogin(gebruiker);
            Context.SaveChanges();
        }
    }
}
