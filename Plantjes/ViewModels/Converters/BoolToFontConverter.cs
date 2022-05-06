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
    internal class BoolToFontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
                return new FontFamily("Segoe UI");
            var font = new FontFamily(new Uri("file:///" + AppDomain.CurrentDomain.BaseDirectory + @"Font", UriKind.Absolute), "./password");
            return font;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
