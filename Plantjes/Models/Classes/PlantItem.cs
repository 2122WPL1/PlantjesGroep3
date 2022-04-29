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
    internal class PlantItem : GroupBox
    {
        private string Name { get; set; }
        private Image foto { get; set; }

        public PlantItem(string name, BitmapImage foto)
        {
            StackPanel panel = new StackPanel();
            this.Name = name;
            this.foto.Source = foto;
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;

            panel.Children.Add(new Image(){Source = foto});
            panel.Children.Add(new TextBox() { Text = Name });

            
        }
    }
}
