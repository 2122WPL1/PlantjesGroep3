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
using Plantjes.ViewModels.HelpClasses;

namespace Plantjes.ViewModels.Services
{
    public class LoginUserService : ILoginUserService
    {
        public LoginUserService()
        {
        }

        //written by Warre
        public Gebruiker Gebruiker { get; private set; }

        #region Login Region
        //written by Warre, appended by Ian
        /// <summary>
        /// Checks if credentials are a user.
        /// </summary>
        /// <param name="emailInput"></param>
        /// <param name="passwordInput"></param>
        /// <returns>Returns a bool if given parameters are valid credentials.</returns>
        /// <exception cref="Exception"></exception>
        public bool IsLogin(string emailInput, string passwordInput)
        {   //Nieuw loginResult om te gebruiken, status op NotLoggedIn zetten
            //var loginResult = new LoginResult() {loginStatus = LoginStatus.NotLoggedIn};

            //check if email is valid email
            if (!Helper.IsEmail(emailInput))
            {
                //indien geen geldig emailadress, errorMessage opvullen
                throw new Exception("Dit is geen geldig emailadres.");
            }
            //gebruiker zoeken in de databank
            Gebruiker currentGebruiker = DaoUser.GetGebruiker(emailInput);

            //omzetten van het ingegeven passwoord naar een gehashed passwoord
            var passwordHashed = Helper.HashString(passwordInput);

            if (currentGebruiker == null)
            {   // als de gebruiker niet gevonden wordt, errorMessage invullen
                throw new Exception($"Er is geen account gevonden voor {emailInput}.\r\nGelieve eerst te registreren!");
            }

            //passwoord controle
            if (currentGebruiker.HashPaswoord == null || !passwordHashed.SequenceEqual(currentGebruiker.HashPaswoord))
            {   //indien false errorMessage opvullen
                throw new Exception("Het ingegeven wachtwoord is niet juist, probeer opnieuw");
            }

            Gebruiker = currentGebruiker;
            DaoUser.UpdateUserLogin(Gebruiker);
            return true;
        }

        //written by Warre
        /// <summary>
        /// The message of the logged in user.
        /// </summary>
        /// <returns>A message as string.</returns>
        public string LoggedInMessage()
        {
            string message = string.Empty;
            if (Gebruiker != null)
            {
                message = $"Ingelogd als: {Gebruiker.Voornaam} {Gebruiker.Achternaam}";
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
        /// <exception cref="Exception">Throws an exception with a message to be used.</exception>
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
                if (!Helper.IsEmail(emailInput))
                {
                    throw new Exception($"{emailInput} is geen geldig emailadres!");
                }
                if (DaoUser.GetGebruiker(emailInput) != null)
                {
                    throw new Exception($"{emailInput} is al geregistreert!");
                }
                //checken als het herhaalde wachtwoord klopt of niet.
                if (passwordInput != passwordRepeatInput)
                {
                    throw new Exception("Zorg dat de wachtwoorden overeen komen!");
                }

                DaoUser.AddUser(vivesNrInput, firstNameInput, lastNameInput, emailInput, passwordInput);

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