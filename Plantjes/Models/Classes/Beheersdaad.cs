using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Plantjes.Models.Extensions;

namespace Plantjes.Models.Classes
{
    internal class Beheersdaad : StackPanel
    {
        public Beheersdaad()
        {
            Orientation = Orientation.Horizontal;
            VerticalAlignment = System.Windows.VerticalAlignment.Center;
            Margin = new System.Windows.Thickness(0, 5, 0, 0);
            Children.Add(new Label() { Content = "Beheersdaad:", VerticalAlignment = System.Windows.VerticalAlignment.Center });
            TextBox beheersdaad = new TextBox() { Width = 400, Height = 50 };
            BeheersdaadText = beheersdaad.Text;
            Children.Add(beheersdaad);
            Children.Add(new Label() { Content = "Maand:", VerticalAlignment = System.Windows.VerticalAlignment.Center });
            Menu months = new Menu() { Width = 200, Height = 25 };
            Months = makeMonths().ToList();
            months.Items.Add(new MenuItem() { ItemsSource = Months });
            Children.Add(months);
            Children.Add(new Label() { Content = "Frequentie per jaar:", VerticalAlignment = System.Windows.VerticalAlignment.Center });
            TextBox frequency = new TextBox() { Width = 200, Height = 25 };
            Frequency = frequency.Text;
            Children.Add(frequency);
            Children.Add(new Label() { Content = "m²/u:", VerticalAlignment = System.Windows.VerticalAlignment.Center });
            TextBox area = new TextBox() { Width = 200, Height = 25 };
            Area = area.Text;
            Children.Add(area);
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

        public List<MenuItem> Months { get; set; }

        public string BeheersdaadText { get; set; }

        public string Frequency { get; set; }

        public string Area { get; set; }
    }
}
