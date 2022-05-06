using Plantjes.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Plantjes.Models.Classes
{
    public class PlantEigenschap<T, TMulti> : StackPanel
        where T : class 
        where TMulti : class
    {
        public PlantEigenschap(IEnumerable<T> list)
        {
            Children.Add(new Label() { Content = typeof(T).Name.SpaceOnUpper(), FontWeight = FontWeights.Bold });
            foreach (T item in list)
            {
                foreach (var prop in item.GetType().GetProperties())
                {
                    if (!new List<string>() { "PlantId", "Plant", "Id" }.Any(n => n == prop.Name) && prop.GetValue(item) != null)
                    {
                        if (prop.GetValue(item) is not bool)
                            Children.Add(new Label() { Content = $"{prop.Name.SpaceOnUpper()}: {prop.GetValue(item)}" });
                        else
                            Children.Add(new Label() { Content = prop.Name.SpaceOnUpper() });
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
                Children.Add(new Label() { Content = $"{eigenschap.SpaceOnUpper()}: {stringWaarde}" });
            }
        }
    }
}
