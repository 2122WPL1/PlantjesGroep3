using Plantjes.Models.Classes;

namespace Plantjes.ViewModels.Interfaces
{/*written by kenny*/
    public interface IloginUserService
    {
        LoginResult CheckCredentials(string userNameInput, string passwordInput);
        string RegisterButton(string vivesNrInput, string lastNameInput,
            string firstNameInput, string emailAdresInput,
            string passwordInput, string passwordRepeatInput, string rolInput);
        string LoggedInMessage();

    }
}