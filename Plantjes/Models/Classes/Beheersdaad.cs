using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Plantjes.Models.Extensions;

namespace Plantjes.Models.Classes
{
    internal class Beheersdaad : GroupBox
    {
        public Beheersdaad()
        {
            StackPanel panel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 0),
            };

            panel.Children.Add(new Label() { Content = "Beheersdaad:", VerticalAlignment = VerticalAlignment.Center });
            Border border = new Border() 
            { 
                BorderBrush = Brushes.Black, BorderThickness = new Thickness(0.5), 
                Child = new TextBlock() { Width = 400, Height = 50, Foreground = Brushes.Black, IsHitTestVisible = true }
            };
            panel.Children.Add(border);

            panel.Children.Add(new Label() { Content = "Maand:", VerticalAlignment = VerticalAlignment.Center });
            Menu months = new Menu() { Width = 150, Height = 25 };
            MenuItem header = new MenuItem();
            header.Items.Add(new MenuItem() { ItemsSource = makeMonths() });
            months.Items.Add(header);
            panel.Children.Add(months);

            panel.Children.Add(new Label() { Content = "Frequentie per jaar:", VerticalAlignment = VerticalAlignment.Center });
            TextBox frequency = new TextBox() { Width = 30, Height = 25 };
            panel.Children.Add(frequency);

            panel.Children.Add(new Label() { Content = "m²/u:", VerticalAlignment = VerticalAlignment.Center });
            TextBox area = new TextBox() { Width = 30, Height = 25 };
            panel.Children.Add(area);

            Content = panel;
        }

        private IEnumerable<MenuItem> makeMonths()
        {
            foreach (string item in CultureInfo.GetCultureInfo("nl-BE").DateTimeFormat.MonthNames[..^1])
            {
                yield return new MenuItem()
                {
                    IsCheckable = true,
                    Header = item.FirstToUpper()
                };
            }
        }

        public string BeheersdaadText 
        { 
            get { return (((Content as StackPanel).Children[1] as Border).Child as TextBlock).Text;  }
            set { (((Content as StackPanel).Children[1] as Border).Child as TextBlock).Text = value; }
        }

        public List<MenuItem> Months
        {
            get { return ((((Content as StackPanel).Children[3] as Menu).Items[0] as MenuItem).Items[0] as MenuItem).ItemsSource as List<MenuItem>; }
        }

        public string Frequency
        {
            get { return ((Content as StackPanel).Children[5] as TextBox).Text; }
            set { ((Content as StackPanel).Children[5] as TextBox).Text = value; }
        }

        public string Area
        {
            get { return ((Content as StackPanel).Children[7] as TextBox).Text; }
            set { ((Content as StackPanel).Children[7] as TextBox).Text = value; }
        }
    }
}
