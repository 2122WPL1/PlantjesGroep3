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
using Plantjes.ViewModels.HelpClasses;

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
            panel.Children.Add(new TextBox() { Width = 400, Height = 50, Foreground = Brushes.Black });

            panel.Children.Add(new Label() { Content = "Maand:", VerticalAlignment = VerticalAlignment.Center });
            Menu months = new Menu() { Width = 150, Height = 25 };
            MenuItem header = new MenuItem() { VerticalAlignment = VerticalAlignment.Center, Header = "Maanden selecteren...", ItemsSource = MakeMonths().ToList() };
            months.Items.Add(header);
            panel.Children.Add(months);

            panel.Children.Add(new Label() { Content = "Frequentie per jaar:", VerticalAlignment = VerticalAlignment.Center });
            panel.Children.Add(new TextBox() { Width = 30, Height = 25 });

            panel.Children.Add(new Label() { Content = "m²/u:", VerticalAlignment = VerticalAlignment.Center });
            panel.Children.Add(new TextBox() { Width = 30, Height = 25 });

            Content = panel;
        }

        private IEnumerable<MenuItem> MakeMonths()
        {
            foreach (string item in Helper.GetMonthsList())
            {
                yield return new MenuItem()
                {
                    IsCheckable = true,
                    StaysOpenOnClick = true,
                    Header = item,
                    Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F9F9F9")),
                    Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3E4239"))
                };
            }
        }

        public string BeheersdaadText 
        { 
            get { return ((Content as StackPanel).Children[1] as TextBox).Text;  }
            set { ((Content as StackPanel).Children[1] as TextBox).Text = value; }
        }

        public List<MenuItem> Months
        {
            get { return (((Content as StackPanel).Children[3] as Menu).Items[0] as MenuItem).ItemsSource as List<MenuItem>; }
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
