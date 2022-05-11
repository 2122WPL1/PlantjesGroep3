﻿using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Plantjes.Models.Classes
{
    public class StackLabelRect : StackPanel
    {
        public StackLabelRect(FenoKleur fenoKleur)
        {
            Orientation = Orientation.Horizontal;
            Children.Add(new Rectangle
            {
                Width = 20,
                Height = 20,
                Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#" + Convert.ToHexString(fenoKleur.HexWaarde)),
            });
            Children.Add(new Label { Content = fenoKleur.NaamKleur });
        }
    }
}
