using Plantjes.Models.Db;
using Plantjes.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Plantjes.Models.Classes
{
    public class PlantItem : Grid
    {
        private Plant plant;

        public PlantItem(bool isEmptyPlant = false)
        {
            Margin = new Thickness(0, 50, 0, 0);
            if (isEmptyPlant)
                Children.Add(new Label() { Content = "Geen planten gevonden!", HorizontalAlignment = HorizontalAlignment.Center, FontSize = 24, Foreground = Brushes.Gray });
        }

        public PlantItem(Plant plant)
        {
            this.plant = plant;

            DockPanel panel = new DockPanel();
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            Margin = new Thickness(10);
            /*Height = 300;
            Width = 500;*/

            BitmapImage biImage = null;
            if (plant.Fotos.Count > 0)
                using (var ms = new System.IO.MemoryStream(plant.Fotos.First().Tumbnail))
                {
                    biImage = new BitmapImage();
                    biImage.BeginInit();
                    biImage.CacheOption = BitmapCacheOption.OnLoad;
                    biImage.StreamSource = ms;
                    biImage.EndInit();
                }
            Image image = new Image() { Source = biImage ?? new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"Image\default-plant.png", UriKind.Absolute)) };
            DockPanel.SetDock(image, Dock.Top);
            panel.Children.Add(image);

            Label nameLabel = new Label() 
            { 
                Content = plant.Variant.RemoveQuotes() ?? $"{plant.Geslacht.FirstToUpper()} {plant.Soort.FirstToUpper()}",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            DockPanel.SetDock(nameLabel, Dock.Bottom);
            panel.Children.Add(nameLabel);
            Children.Add(panel);
        }

        public Plant Plant { get { return plant; } }
    }
}
