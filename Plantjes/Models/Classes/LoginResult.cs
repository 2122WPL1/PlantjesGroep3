using System;
using System.Collections.Generic;
using System.Text;
using Plantjes.Models;
using Plantjes.Models.Db;
using Plantjes.Models.Enums;
//written by kenny
namespace Plantjes.Models.Classes
{  
    public class LoginResult
    {
        //Dit is de gebruiker in de databank. Dit is de gebruiker
        //die geregistreerd, of gecontroleerd wordt door de LoginUserService
        public Gebruiker gebruiker { get; set; }

        //Dit is een enum waarmee de LoginUserService kan controleren of een
        //Gebruiker ingelogd is of niet.
        public LoginStatus loginStatus {get; set; }

        //Deze string wordt opgevuld in de LoginUserService met een foutmelding
        //die kan weergegeven worden in de LoginWindow en RegisterWindow
        public string errorMessage { get; set; }

    }
}
