using System;
using System.Globalization;
using System.Windows.Data;

namespace SupComercio.Resources.Converters
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class IndexToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int p;
            if (parameter != null && int.TryParse(parameter.ToString(), out p))
            {
                    return (int)value != p;
            }

            return value is int && (int)value != -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
