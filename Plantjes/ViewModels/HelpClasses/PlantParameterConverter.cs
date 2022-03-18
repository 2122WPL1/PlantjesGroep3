using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Plantjes.ViewModels.HelpClasses
{
    internal class PlantParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new List<object>() 
            { 
                values[0] as string,
                values[1] as string,
                values[2] as string,
                values[3] as string,
                values[4] as bool?,
                values[5] as bool?,
                values[6] as bool?,
                values[7] as bool?,
                values[8] as bool?,
                values[9] as string,
                values[10] as string,
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
