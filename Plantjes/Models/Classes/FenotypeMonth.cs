using Plantjes.Models.Extensions;
using Plantjes.ViewModels.HelpClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Plantjes.Models.Classes
{
    public class FenotypeMonth : GroupBox
    {
        public FenotypeMonth()
        {
            StackPanel panel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 0),
            };

            panel.Children.Add(new Label() { Content = "Maand:", VerticalAlignment = VerticalAlignment.Center });
            //Menu months = new Menu() { Width = 150, Height = 25 };
            //MenuItem header = new MenuItem() { VerticalAlignment = VerticalAlignment.Center, Header = "Maanden selecteren...", ItemsSource = makeMonths() };
            //months.Items.Add(header);
            panel.Children.Add(new ComboBox() { Width = 150, Height = 25, ItemsSource = Helper.GetMonthsList() });

            panel.Children.Add(new Label() { Content = "Bladgrootte tot:", VerticalAlignment = VerticalAlignment.Center });
            panel.Children.Add(new TextBox() { Width = 30, Height = 25 });

            panel.Children.Add(new Label() { Content = "Bladhoogte max:", VerticalAlignment = VerticalAlignment.Center });
            panel.Children.Add(new TextBox() { Width = 30, Height = 25 });

            Content = panel;
        }

        public string SelectedMonth
        {
            get { return ((Content as StackPanel).Children[1] as ComboBox).SelectedItem as string; }
        }

        public string Bladgrootte
        {
            get { return ((Content as StackPanel).Children[3] as TextBox).Text; }
            set { ((Content as StackPanel).Children[3] as TextBox).Text = value; }
        }

        public string Bladhoogte
        {
            get { return ((Content as StackPanel).Children[5] as TextBox).Text; }
            set { ((Content as StackPanel).Children[5] as TextBox).Text = value; }
        }
    }
}
