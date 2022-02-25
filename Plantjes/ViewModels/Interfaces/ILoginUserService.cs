using Plantjes.Models.Classes;
using Plantjes.Models.Db;

namespace Plantjes.ViewModels.Interfaces
{
    /*written by kenny*/
    public interface ILoginUserService
    {
        bool IsLogin(string userNameInput, string passwordInput);
        void Register(string vivesNrInput, string lastNameInput,
            string firstNameInput, string emailAdresInput,
            string passwordInput, string passwordRepeatInput);
        string LoggedInMessage();
    }
}
