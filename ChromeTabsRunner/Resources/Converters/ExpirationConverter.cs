using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SupComercio.Resources.Converters
{
    [ValueConversion(typeof(DateTime), typeof(bool))]
    class ExpirationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if(value is DateTime fecha)
            {
                if(fecha < DateTime.Today && fecha.Date != DateTime.MinValue.Date)
                {
                    return true;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
