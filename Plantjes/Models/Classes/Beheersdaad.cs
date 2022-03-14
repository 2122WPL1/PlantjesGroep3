using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Plantjes.Models.Classes
{
    internal class Beheersdaad : StackPanel
    {
        public Beheersdaad()
        {
            Orientation = Orientation.Horizontal;
            Children.Add(new Label() { Content = "Beheersdaad:" });
            TextBlock beheersdaad = new TextBlock();
            BeheersdaadText = beheersdaad.Text;
            Children.Add(beheersdaad);
            Children.Add(new Label() { Content = "Maand:" });
            Menu months = new Menu();
            Months = makeMonths().ToList();
            months.Items.Add(new MenuItem() { ItemsSource = Months });
            Children.Add(months);
            Children.Add(new Label() { Content = "Frequentie per jaar:" });
            TextBox frequency = new TextBox();
            Frequency = frequency.Text;
            Children.Add(frequency);
            Children.Add(new Label() { Content = "m²/u:" });
            TextBox area = new TextBox();
            Area = area.Text;
            Children.Add(area);
        }

        private IEnumerable<MenuItem> makeMonths()
        {
            foreach (string item in CultureInfo.GetCultureInfo("nl-BE").DateTimeFormat.MonthNames)
            {
                yield return new MenuItem()
                {
                    IsCheckable = true,
                    Header = item
                };
            }
        }

        public List<MenuItem> Months { get; set; }

        public string BeheersdaadText { get; set; }

        public string Frequency { get; set; }

        public string Area { get; set; }
    }
}
