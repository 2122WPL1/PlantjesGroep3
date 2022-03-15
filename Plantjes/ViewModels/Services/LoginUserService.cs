using Plantjes.ViewModels.Interfaces;
using Plantjes.Dao;
using Plantjes.Models;
using Plantjes.Models.Classes;
using Plantjes.Models.Db;
using Plantjes.Models.Enums;
using Plantjes.Views.Home;
using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Plantjes.ViewModels.Services
{
    public class LoginUserService : ILoginUserService
    {
        //dao verklaren om data op te vragen en te setten in de databank
        private DAObase _dao;
        private Gebruiker gebruiker;

        public LoginUserService()
        {
            this._dao = DAObase.Instance();
        }

        //written by Warre
        /// <summary>
        /// Checks if a string is a valid email adress.
        /// </summary>
        /// <param name="input">The string to be parsed.</param>
        /// <returns>Returns a boolean depending on if the parser failed or not.</returns>
        private bool IsEmail(string input)
        {
            if (!string.IsNullOrEmpty(input) && input.IndexOf('@') != -1 && input[input.IndexOf('@')..].Contains('.'))
            {
                return true;
            }
            return false;
        }

        #region Login Region
        //written by Warre
        /// <summary>
        /// Checks if credentials are a user.
        /// </summary>
        /// <param name="emailInput"></param>
        /// <param name="passwordInput"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool IsLogin(string emailInput, string passwordInput)
        {   //Nieuw loginResult om te gebruiken, status op NotLoggedIn zetten
            //var loginResult = new LoginResult() {loginStatus = LoginStatus.NotLoggedIn};

            //check if email is valid email
            Gebruiker currentGebruiker;
            if (IsEmail(emailInput))
            {   //gebruiker zoeken in de databank
                currentGebruiker = _dao.GetGebruiker(emailInput);
            }
            else
            {//indien geen geldig emailadress, errorMessage opvullen
                throw new Exception("Dit is geen geldig emailadres.");
            }

            //omzetten van het ingegeven passwoord naar een gehashed passwoord
            var passwordBytes = Encoding.ASCII.GetBytes(passwordInput);
            var md5Hasher = new MD5CryptoServiceProvider();
            var passwordHashed = md5Hasher.ComputeHash(passwordBytes);

            if (currentGebruiker == null)
            {   // als de gebruiker niet gevonden wordt, errorMessage invullen
                throw new Exception($"Er is geen account gevonden voor {emailInput}.\r\nGelieve eerst te registreren!");
            }

            //passwoord controle
            if (currentGebruiker.HashPaswoord == null || !passwordHashed.SequenceEqual(currentGebruiker.HashPaswoord))
            {   //indien false errorMessage opvullen
                throw new Exception("Het ingegeven wachtwoord is niet juist, probeer opnieuw");
            }

            gebruiker = currentGebruiker;
            return true;
        }

        //written by Warre
        /// <summary>
        /// The message of the login user.
        /// </summary>
        /// <returns>A message as string.</returns>
        public string LoggedInMessage()
        {
            string message = string.Empty;
            if (gebruiker != null)
            {
                message = $"Ingelogd als: {gebruiker.Voornaam} {gebruiker.Achternaam}";
            }
            return message;
        }
        #endregion

        #region Register Region
        //written by Warre
        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="vivesNrInput">The r/u number of the user.</param>
        /// <param name="lastNameInput">The last name of the user.</param>
        /// <param name="firstNameInput">The first name of the user.</param>
        /// <param name="emailInput">The email of the user.</param>
        /// <param name="passwordInput">The password of the user.</param>
        /// <param name="passwordRepeatInput">The password to check the password.</param>
        /// <param name="rolInput">The role of the user.</param>
        /// <exception cref="Exception">Throws an execption with a message to be used.</exception>
        public void Register(string vivesNrInput, string lastNameInput,
                                   string firstNameInput, string emailInput,
                                   string passwordInput, string passwordRepeatInput)
        {
            //checken of alle velden ingevuld zijn
            if (vivesNrInput != null &&
                firstNameInput != null &&
                lastNameInput != null &&
                emailInput != null &&
                passwordInput != null &&
                passwordRepeatInput != null)
            {
                //checken als het emailadres een geldig vives email is.
                if (!IsEmail(emailInput))
                {
                    throw new Exception($"{emailInput} is geen geldig emailadres!");
                }
                if (_dao.GetGebruiker(emailInput) != null)
                {
                    throw new Exception($"{emailInput} is al geregistreert!");
                }
                //checken als het herhaalde wachtwoord klopt of niet.
                if (passwordInput != passwordRepeatInput)
                {
                    throw new Exception("Zorg dat de wachtwoorden overeen komen!");
                }


                _dao.AddUser(vivesNrInput, firstNameInput, lastNameInput, emailInput, passwordInput);

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                throw new Exception($"{firstNameInput}, u bent succesvol geregistreerd.\r\nUw gebruikersnaam is {emailInput}.\r\n" +
                                $"{firstNameInput}, je kan dit venster wegklikken en inloggen.");
            }
            else
            {
                throw new Exception("Zorg dat alle velden ingevuld zijn!");
            }
        }
        #endregion
    }

}