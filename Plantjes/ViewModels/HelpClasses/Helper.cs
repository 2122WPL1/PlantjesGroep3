using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Plantjes.Dao;
using Plantjes.Models.Db;
using Plantjes.Models.Extensions;

namespace Plantjes.ViewModels.HelpClasses
{
    /// <summary>
    /// Static helper class for commonly used methods
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Converts the read .csv into a list of Gebruikers.
        /// </summary>
        /// <param name="csvLocation">Location of the .csv file.</param>
        /// <returns>A list of Gebruikers</returns>
        public static List<Gebruiker> CSVToMemberList(string csvLocation)
        {
            // Written by Ian Dumalin on 27/04
            List<Gebruiker> valueList = new List<Gebruiker>();
            using (StreamReader reader = new StreamReader(csvLocation))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] readValues = line.Split(';');
                    if (readValues[0] != "Studentennummer")
                    {
                        valueList.Add(new Gebruiker(readValues[0], readValues[1], readValues[2], readValues[5], Helper.HashString(readValues[0])));
                    }
                }
            }
            return valueList;
        }

        public static void RegisterMemberToCSV(string csvLocation, string email, string rNumber, string password)
        {
            using (FileStream fileStream = new FileStream(csvLocation, FileMode.Append, FileAccess.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine($"{email};{rNumber};{password}");
                }
            }
        }

        // Written by Warre, converted to help method by Ian
        /// <summary>
        /// Converts a string to a collection of bytes
        /// </summary>
        /// <param name="password">string password</param>
        /// <returns>a collection of bytes as a safer way to store passwords</returns>
        public static byte[] HashString(string password)
        {
            var passwordBytes = Encoding.ASCII.GetBytes(password);
            var md5Hasher = new MD5CryptoServiceProvider();
            var passwordHashed = md5Hasher.ComputeHash(passwordBytes);
            return passwordHashed;
        }

        //Written by Ian Dumalin on 27/04 and 28/04
        /// <summary>
        /// Loops through all de available users in the database, adding missing users from a .csv file.
        /// </summary>
        public static void PopulateDB()
        {
            string path = Directory.GetCurrentDirectory() + "\\CSV\\members.csv";
            var gebruikerList = DaoUser.GetGebruikerList();
            var gebruikerListCSV = Helper.CSVToMemberList(path);
            Dictionary<string, string> gebruikerDictionary = new Dictionary<string, string>();
            foreach (Gebruiker g in gebruikerList)
            {
                gebruikerDictionary.Add(g.Vivesnr, g.Emailadres);
            }

            foreach (Gebruiker g in gebruikerListCSV)
            {
                if (!gebruikerDictionary.Keys.Contains(g.Vivesnr) && !gebruikerDictionary.Values.Contains(g.Emailadres))
                {
                    DaoUser.AddUser(g.Vivesnr, g.Voornaam, g.Achternaam, g.Emailadres, g.HashPaswoord);
                }
            }
        }

        // written by Warre
        /// <summary>
        /// Makes an MenuItem list with all colors in the database.
        /// </summary>
        /// <returns>Returns a menuitem list with all color previews and names.</returns>
        public static IEnumerable<MenuItem> MakeColorMenuItemList()
        {
            foreach (FenoKleur item in DaoBase.GetList<FenoKleur>())
            {
                yield return new MenuItem()
                {
                    Width = double.NaN,
                    IsCheckable = true,
                    StaysOpenOnClick = true,
                    Header = new Rectangle()
                    {
                        Width = 20,
                        Height = 20,
                        Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#" + Convert.ToHexString(item.HexWaarde)),
                    },
                    InputGestureText = item.NaamKleur.FirstToUpper(),
                };
            }
        }

        // written by Warre
        /// <summary>
        /// Makes a MenuItem list with preset settings.
        /// </summary>
        /// <typeparam name="TEntity">The entity to be used for selector.</typeparam>
        /// <param name="selector">The name of the TEntity.</param>
        /// <returns>Returns a list of menu items with the name of TEntity.</returns>
        public static IEnumerable<MenuItem> MakeMenuItemList<TEntity>(Func<TEntity, string> selector) where TEntity : class
        {
            foreach (TEntity item in DaoBase.GetList<TEntity>())
            {
                if (selector(item).Contains('-') ||
                    (item is AbioGrondsoort && selector(item).Length > 1) ||
                    (item is CommStrategie && selector(item).Length > 1))
                    continue;
                yield return new MenuItem()
                {
                    Width = double.NaN,
                    IsCheckable = true,
                    StaysOpenOnClick = true,
                    Header = selector(item).FirstToUpper(),
                };
            }
        }

        /// <summary>
        /// Gets a list of all months.
        /// </summary>
        /// <returns>Returns a list of all months in dutch.</returns>
        public static IEnumerable<string> GetMonthsList(bool isUpper = true)
        {
            foreach (string item in CultureInfo.GetCultureInfo("nl-BE").DateTimeFormat.MonthNames[..^1])
            {
                yield return isUpper ? item.FirstToUpper() : item;
            }
        }
    }
}
