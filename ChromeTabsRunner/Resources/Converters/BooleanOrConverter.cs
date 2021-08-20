using System;
using System.Windows.Data;

namespace SupComercio.Resources.Converters
{
    public class BooleanOrConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values == null)
            {
                return false;
            }

            foreach (object value in values)
            {
                if ((value is bool) && (bool)value == true)
                {
                    return true;
                }
            }
            return false;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:No pasar cadenas literal como parámetros localizados", Justification = "<pendiente>")]
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
    }
}
