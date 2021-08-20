using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SupComercio.Resources.Converters
{
    class ByteToKiloByteonverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (value is long val)
            {
                string[] sufix = { "b", "Kb", "Mb", "Gb" };
                long prev = val;
                int count = 0;
                while(count < sufix.Length)
                {
                    prev = val;
                    val /= 1024;
                    if(val < 1)
                    {
                        return prev.ToString(culture) + sufix[count];
                    }
                    count++;
                }
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
