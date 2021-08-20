using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SupComercio.Resources.Converters
{
    //[ValueConversion(typeof(bool), typeof(Visibility))]
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? b = null;
            if (value is bool?) b = (bool?)value; 
            if (b.HasValue && b.Value == false) return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible) return false;
            return true;
        }
    }
}
