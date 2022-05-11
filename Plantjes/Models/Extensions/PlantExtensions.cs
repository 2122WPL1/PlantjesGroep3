using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Plantjes.Models.Db;

namespace Plantjes.Models.Extensions
{
    //Written by Ian Dumalin on 05/05
    internal static class PlantExtensions
    {
        public static string PlantToString(this Plant plant)
        {
            string type = plant.Type.FirstToUpper();
            string familie = plant.Familie.FirstToUpper();
            string geslacht = plant.Geslacht.FirstToUpper();
            string soort = plant.Soort;
            soort =
                soort == "__" ? string.Empty : plant.Soort.FirstToUpper();
            string variant = plant.Variant.RemoveQuotes().FirstToUpper();
            string nederlandsNaam = plant.NederlandsNaam.FirstToUpper();
            return $"{type};{familie};{geslacht};{soort};" +
                   $"{variant};{nederlandsNaam}";
        }

        public static BitmapImage GetPlantImage(this Plant plant)
        {
            BitmapImage biImage = null;
            if (plant.Fotos.Count > 0)
                using (var ms = new MemoryStream(plant.Fotos.First().Tumbnail))
                {
                    biImage = new BitmapImage();
                    biImage.BeginInit();
                    biImage.CacheOption = BitmapCacheOption.OnLoad;
                    biImage.StreamSource = ms;
                    biImage.EndInit();
                }
            return biImage ?? new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Image\default-plant.png", UriKind.Absolute));
        }

        public static string GetPlantName(this Plant plant)
            => plant.Variant.RemoveQuotes() ?? $"{plant.Geslacht.FirstToUpper()} {plant.Soort.FirstToUpper()}";
    }
}
