using Plantjes.Models.Db;
using System;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Plantjes.Models.Extensions
{
    internal static class PlantExtensions
    {
        //Written by Ian Dumalin on 05/05
        public static string PlantToString(this Plant plant, string delimiter)
        {
            string type = plant.Type.FirstToUpperRestToLower();
            string familie = plant.Familie.FirstToUpperRestToLower();
            string geslacht = plant.Geslacht.FirstToUpperRestToLower();
            string soort = plant.Soort;
            soort =
                soort == "__" ? string.Empty : plant.Soort.FirstToUpperRestToLower();
            string variant = plant.Variant.RemoveQuotes().FirstToUpperRestToLower();
            string nederlandsNaam = plant.NederlandsNaam.FirstToUpperRestToLower();
            return $"{type}{delimiter}{familie}{delimiter}{geslacht}{delimiter}{soort}{delimiter}" +
                   $"{variant}{delimiter}{nederlandsNaam}";
        }

        //written by Warre
        public static BitmapImage GetPlantImage(this Plant plant)
        {
            BitmapImage biImage = null;
            if (plant.Fotos.Count > 0)
                using (var ms = new MemoryStream((plant.Fotos.FirstOrDefault(f => f.Eigenschap == "habitus") ?? plant.Fotos.First()).Tumbnail))
                {
                    biImage = new BitmapImage();
                    biImage.BeginInit();
                    biImage.CacheOption = BitmapCacheOption.OnLoad;
                    biImage.StreamSource = ms;
                    biImage.EndInit();
                }
            return biImage ?? new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Image\default-plant.png", UriKind.Absolute));
        }

        //written by Warre
        public static string GetPlantName(this Plant plant)
            => plant.Variant.RemoveQuotes() ?? $"{plant.Geslacht.FirstToUpper()} {plant.Soort.FirstToUpper()}";
    }
}
