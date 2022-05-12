using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using Plantjes.Dao;
using Plantjes.Models.Db;
using Plantjes.Models.Extensions;

namespace Plantjes.ViewModels.HelpClasses
{
    internal class CSVHelper
    {
        public static List<Gebruiker> ImportNewMembersFromCSV()
        {
            // Written by Ian Dumalin on 11/05
            List<Gebruiker> readGebruikers = new List<Gebruiker>();
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "CSV file(*.csv)|*.csv";
            try
            {
                if (open.ShowDialog() ?? false)
                {
                    StreamReader reader = new StreamReader(open.FileName);
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        var readList = line.Split(";");

                        // error handling for CSV reading.
                        if (!Helper.IsEmail(readList[5]) && readList[5] != "Emailadres") throw new Exception("E-mail syntax verkeerd.");
                        if (readList.Length != 6) throw new Exception("CSV File niet in juiste opdeling.");

                        //making a list item for each readline.
                        readGebruikers.Add(new Gebruiker()
                        {
                            Emailadres = readList[5],
                            Achternaam = readList[2],
                            Voornaam = readList[1],
                            Vivesnr = readList[0]
                        });
                        line = reader.ReadLine();
                    }
                }
                return readGebruikers;
            }
            catch (IOException e)
            {
                MessageBox.Show($"{e.Message}\r\n\r\nFile in gebruik! Sluit de te-openen file en probeer opnieuw.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}\r\n\r\nCSV file niet ingedeeld zoals opgegeven.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return null;
        }

        /// <summary>
        /// Exports a list of plants to a CSV with their values
        /// </summary>
        /// <param name="plantList">List of plants to get parameters from</param>
        public static void ExportPlantsToCSV(IEnumerable<Plant> plantList)
        {
            if (plantList == null || plantList.Count() == 0)
                return;
            IEnumerable<string> lines = plantList.Select(p => p.PlantToString());
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "CSV file(*.csv)|*.csv";
            string output = $"Type;Familie;Geslacht;Soort;Variant;Nederlandse naam\r\n";
            if (save.ShowDialog() ?? false)
            {
                foreach (string s in lines)
                {
                   output += $"{s}\r\n";
                }
                File.WriteAllText(save.FileName, output);
            }
        }

        //Written by Ian Dumalin on 11/05
        /// <summary>
        /// Exports beheersdaden of a plant to a CSV
        /// </summary>
        /// <param name="plant">Plant to get beheersdaden from</param>
        public static void ExportPlantDetailsToCSV(Plant plant)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "CSV file(*.csv)|*.csv";
            string output = $"Beheersdaad;Omschrijving;Maand\r\n";
            if (save.ShowDialog() ?? false)
            {
                foreach (var beheersdaad in plant.BeheerMaands)
                {
                    string months = null;
                    if (beheersdaad.Jan != null && (bool)beheersdaad.Jan) months += "januari ";
                    if (beheersdaad.Feb != null && (bool)beheersdaad.Feb) months += "februari ";
                    if (beheersdaad.Mrt != null && (bool)beheersdaad.Mrt) months += "maart ";
                    if (beheersdaad.Apr != null && (bool)beheersdaad.Apr) months += "april ";
                    if (beheersdaad.Mei != null && (bool)beheersdaad.Mei) months += "mei ";
                    if (beheersdaad.Jun != null && (bool)beheersdaad.Jun) months += "juni ";
                    if (beheersdaad.Jul != null && (bool)beheersdaad.Jul) months += "juli ";
                    if (beheersdaad.Aug != null && (bool)beheersdaad.Aug) months += "augustus ";
                    if (beheersdaad.Sept != null && (bool)beheersdaad.Sept) months += "september ";
                    if (beheersdaad.Okt != null && (bool)beheersdaad.Okt) months += "oktober ";
                    if (beheersdaad.Nov != null && (bool)beheersdaad.Nov) months += "november ";
                    if (beheersdaad.Dec != null && (bool)beheersdaad.Dec) months += "december";
                    output += $"{beheersdaad.Beheerdaad};{beheersdaad.Omschrijving};{months}\r\n";
                }
                File.WriteAllText(save.FileName, output);
            }
        }

        public static void ExportUsersToCSV(IEnumerable<Gebruiker> gebruikers)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "CSV file(*.csv)|*.csv";
                string output = "Studentennummer;Voornaam;Familienaam;Tweede naam;Correspondentie;Emailadres\r\n";
                if (save.ShowDialog() ?? false)
                {
                    foreach (var gebruiker in gebruikers)
                    {
                        output +=
                            $"{gebruiker.Vivesnr};{gebruiker.Voornaam};{gebruiker.Achternaam};;;{gebruiker.Emailadres}\r\n";
                    }
                    File.WriteAllText(save.FileName, output);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show($"{e.Message}\r\n\r\nFile in gebruik! Sluit de te-openen file en probeer opnieuw.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [Obsolete("Members are registered to database and not a CSV file")]
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
    }
}
