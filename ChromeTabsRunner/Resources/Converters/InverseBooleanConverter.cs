using System;
using System.Globalization;
using System.Windows.Data;

namespace SupComercio.Resources.Converters
{
    [ValueConversion(typeof(bool?), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:No pasar cadenas literal como parámetros localizados", Justification = "<pendiente>")]
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool?))
            {
                throw new InvalidOperationException("The target must be a nullable boolean");
            }
            bool? b = (bool?)value;
            return b.HasValue && !b.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(value as bool?);
        }

        #endregion
    }
}
