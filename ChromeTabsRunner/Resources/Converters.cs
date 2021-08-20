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

    public class BooleanAndConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values == null)
            {
                return false;
            }

            foreach (object value in values)
            {
                if ((value is bool) && (bool)value == false)
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

    public class IndexToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int && (int)value != -1)
            {
                return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


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
                    if (tsRef == TimeSpan.Zero && !tsSet) tsRef = (TimeSpan)n;
                    if ((TimeSpan)n > tsRef)
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
