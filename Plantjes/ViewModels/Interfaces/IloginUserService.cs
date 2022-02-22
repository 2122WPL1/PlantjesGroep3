using Plantjes.Models.Classes;
using Plantjes.Models.Db;

namespace Plantjes.ViewModels.Interfaces
{/*written by kenny*/
    public interface IloginUserService
    {
        bool IsLogin(string userNameInput, string passwordInput);
        string RegisterButton(string vivesNrInput, string lastNameInput,
            string firstNameInput, string emailAdresInput,
            string passwordInput, string passwordRepeatInput, string rolInput);
        string LoggedInMessage();

    }
}