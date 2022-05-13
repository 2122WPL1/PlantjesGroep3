using Plantjes.Models.Db;
using Plantjes.Models.Extensions;
using Plantjes.ViewModels.HelpClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Plantjes.Models.Classes
{
    public class PlantEigenschap<T, TMulti> : Grid
        where T : class 
        where TMulti : class
    {
        private readonly StackPanel panel = new StackPanel();

        public PlantEigenschap(IEnumerable<T> list)
        {
            Margin = new Thickness(5);
            Children.Add(new Border() { CornerRadius = new CornerRadius(3), BorderThickness = new Thickness(1), BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3E4239")),
                                        Background = Brushes.White });
            Children.Add(panel);
            panel.Children.Add(new Label() { Content = typeof(T).Name.SpaceOnUpper(), FontWeight = FontWeights.Bold });
            foreach (T item in list)
            {
                foreach (var prop in item.GetType().GetProperties())
                {
                    if (!new List<string>() { "PlantId", "Plant", "Id", "Fotoid", "UrlLocatie", "PlantNavigation" }.Any(n => n == prop.Name) && prop.GetValue(item) != null)
                    {
                        if (prop.GetValue(item) is bool)
                            panel.Children.Add(new Label() { Content = prop.Name.SpaceOnUpper(), Margin = new Thickness(5, 0, 0, 0) });
                        else if (item is Foto)
                        {
                            if (prop.GetValue(item) is byte[] bytes)
                                panel.Children.Add(new Image() { Source = Helper.ToImage(bytes), MaxHeight = 100, Margin = new Thickness(5) });
                            else
                                panel.Children.Add(new Label() { Content = (prop.GetValue(item) as string).FirstToUpper() + ":", HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(5, 0, 0, 0) });
                        }
                        else
                            panel.Children.Add(new Label() { Content = $"{prop.Name.SpaceOnUpper()}: {prop.GetValue(item)}", Margin = new Thickness(5, 0, 0, 0) });
                    }
                }
            }
        }

        public PlantEigenschap(IEnumerable<T> list, IEnumerable<TMulti> listMultis) : this(list)
        {
            List<string> eigenschappen = new List<string>();
            foreach (TMulti item in listMultis)
            {
                string eigenschap = item.GetType().GetProperty("Eigenschap").GetValue(item) as string;
                if (!eigenschappen.Any(n => n == eigenschap))
                    eigenschappen.Add(eigenschap);
            }
            foreach (string eigenschap in eigenschappen)
            {
                string stringWaarde =
                    string.Join(", ", listMultis
                    .Where(m => m.GetType().GetProperty("Eigenschap").GetValue(m) as string == eigenschap)
                    .Select(m => m.GetType().GetProperty("Waarde").GetValue(m)));
                panel.Children.Add(new Label() { Content = $"{eigenschap.SpaceOnUpper().FirstToUpper()}: {stringWaarde}", Margin = new Thickness(5, 0, 0, 0) });
            }
        }
    }
}
