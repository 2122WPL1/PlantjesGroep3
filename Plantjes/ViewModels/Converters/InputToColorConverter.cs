using Plantjes.Models.Db;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Plantjes.ViewModels.Converters
{
    internal class InputToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Brushes.Red;
            if (value is int index && index == -1) 
                return Brushes.Red;
            if (value is string input && string.IsNullOrEmpty(input.Trim()))
                return Brushes.Red;
            return Brushes.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
