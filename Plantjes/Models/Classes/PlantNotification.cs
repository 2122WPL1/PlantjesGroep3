using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Plantjes.Models.Classes
{
    internal class PlantNotification:GroupBox
    {
        public PlantNotification(string plantnaam)
        {
            StackPanel pael = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0),
            };

            pael.Children.Add(new Label());
        }
    }
}
