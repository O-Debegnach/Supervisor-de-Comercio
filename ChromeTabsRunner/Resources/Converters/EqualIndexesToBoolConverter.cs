using System;
using System.Globalization;
using System.Windows.Data;

namespace SupComercio.Resources.Converters
{
    public class EqualIndexesToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
            {
                return false;
            }

            foreach (int index in values)
            {
                if (index != (int)values[0])
                {
                    return false;
                }
            }
            return true;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:No pasar cadenas literal como parámetros localizados", Justification = "<pendiente>")]
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
    }
}
