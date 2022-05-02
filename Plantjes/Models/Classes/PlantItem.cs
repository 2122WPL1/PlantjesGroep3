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
using System.Windows.Media.Imaging;

namespace Plantjes.Models.Classes
{
    public class PlantItem : GroupBox
    {
        private Plant plant;

        public PlantItem(Plant plant)
        {
            this.plant = plant;

            DockPanel panel = new DockPanel();
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            Height = 300;
            Width = 500;

            BitmapImage image = null;
            if (plant.Fotos.Count > 0)
                using (var ms = new System.IO.MemoryStream(plant.Fotos.First().Tumbnail))
                {
                    image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                }
            panel.Children.Add(new Image(){ Source = image ?? new BitmapImage() });
            Label nameLabel = new Label() 
            { 
                Content = plant.Variant.RemoveQuotes() ?? $"{plant.Geslacht.FirstToUpper()} {plant.Soort.FirstToUpper()}",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            DockPanel.SetDock(nameLabel, Dock.Bottom);
            panel.Children.Add(nameLabel);
            Content = panel;
        }

        public Plant Plant { get { return plant; } }
    }
}
