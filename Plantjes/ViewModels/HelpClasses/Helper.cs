using GalaSoft.MvvmLight.Ioc;
using Plantjes.Dao;
using Plantjes.Models.Db;
using Plantjes.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Plantjes.ViewModels.HelpClasses
{
    /// <summary>
    /// Static helper class for commonly used methods
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Checks if a string is a valid email adress.
        /// </summary>
        /// <param name="input">The string to be parsed.</param>
        /// <returns>Returns a boolean depending on if the parser failed or not.</returns>
        public static bool IsEmail(string input)
        {
            if (!string.IsNullOrEmpty(input) && input.IndexOf('@') != -1 && input[input.IndexOf('@')..].Contains('.'))
            {
                return true;
            }
            return false;
        }
        public static void SwitchTabAndReset<TCurrent, T>(string tab, Func<TCurrent> thisFactory, Func<T> factory) 
            where T : class
            where TCurrent : class
        {
            if (SimpleIoc.Default.IsRegistered<TCurrent>() || SimpleIoc.Default.ContainsCreated<T>())
                SimpleIoc.Default.Unregister<TCurrent>();
            SimpleIoc.Default.Register(thisFactory);
            SwitchTab<T>(tab, factory);
        }

        public static void SwitchTab<T>(string tab, Func<T> factory = null) where T : class
        {
            if (factory != null)
            {
                if (SimpleIoc.Default.IsRegistered<T>() || SimpleIoc.Default.ContainsCreated<T>())
                    SimpleIoc.Default.Unregister<T>();
                SimpleIoc.Default.Register(factory);
            }
            SimpleIoc.Default.GetInstance<ViewModelMain>().OnNavigationChanged(tab);
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
            var md5Hasher = MD5.Create();
            var passwordHashed = md5Hasher.ComputeHash(passwordBytes);
            return passwordHashed;
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
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F9F9F9")),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3E4239"))
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
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F9F9F9")),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3E4239"))
                };
            }
        }

        //Written by Ian Dumalin on 27/04 and 28/04
        /// <summary>
        /// Loops through all de available users in the database, adding missing users from a .csv file.
        /// </summary>
        public static IEnumerable<Gebruiker> PopulateDb(IEnumerable<Gebruiker> gebruiker)
        {
            var gebruikerListCsv = CsvHelper.ImportNewMembersFromCsv();
            Dictionary<string, string> gebruikerDictionary = new();
            foreach (Gebruiker g in gebruiker)
            {
                gebruikerDictionary.Add(g.Vivesnr, g.Emailadres);
            }

            if (gebruikerListCsv != null)
            {
                foreach (Gebruiker g in gebruikerListCsv)
                {
                    if (!gebruikerDictionary.ContainsKey(g.Vivesnr) && !gebruikerDictionary.ContainsValue(g.Emailadres))
                    {
                        yield return DaoUser.UserToDb(g.Vivesnr, g.Voornaam, g.Achternaam, g.Emailadres, g.HashPaswoord);
                    }
                } 
            }
        }

        // written by Warre
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

        // written by Warre
        public static bool? RadioButtonToBool(bool? ja, bool? nee)
            => !(bool)ja && !(bool)nee ? null : (bool)ja ? true : null;

        public static BitmapImage ToImage(byte[] bytes)
        {
            BitmapImage biImage = null;
            using (var ms = new MemoryStream(bytes))
            {
                biImage = new BitmapImage();
                biImage.BeginInit();
                biImage.CacheOption = BitmapCacheOption.OnLoad;
                biImage.StreamSource = ms;
                biImage.EndInit();
            }
            return biImage;
        }
    }
}
