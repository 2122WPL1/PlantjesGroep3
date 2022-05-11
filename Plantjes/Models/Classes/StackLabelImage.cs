using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Plantjes.Models.Classes
{
    internal class StackLabelImage : StackPanel
    {
        public StackLabelImage(string name, bool habitus)
        {
            Orientation = Orientation.Horizontal;
            Children.Add(new Image() { Source = new BitmapImage(new Uri(Environment.CurrentDirectory + $@"\Image\{(habitus ? "Habitus" : "Bloeiwijze")}\{name}.png", UriKind.Absolute)), Height = 20 });
            Children.Add(new Label() { Content = name });
        }
    }
}
