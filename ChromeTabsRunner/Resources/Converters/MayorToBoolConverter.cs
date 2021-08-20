using System;
using System.Globalization;
using System.Windows.Data;

namespace SupComercio.Resources.Converters
{
    public class MayorToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool tsSet = false;
            int indexRef = -1;
            TimeSpan tsRef = TimeSpan.Zero;
            bool iguales = true;
            bool mayor = true;
            if (values == null)
            {
                return false;
            }
            foreach (object n in values)
            {
                if (n is TimeSpan)
                {
                    if (tsRef == TimeSpan.Zero && !tsSet) { tsRef = (TimeSpan)n; continue; }
                    if ((TimeSpan)n >= tsRef)
                    {
                        mayor = false;
                    }
                }
                else if (n is int)
                {
                    if (indexRef == -1) indexRef = (int)n;
                    else if (indexRef != (int)n) iguales = false;
                }
            }
            return !iguales || (iguales && mayor);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:No pasar cadenas literal como parámetros localizados", Justification = "<pendiente>")]
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndConverter is a OneWay converter.");
        }
    }
}
