using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Res = SupComercio.Properties.Resources;

namespace SupComercio.Resources.Converters
{
    [ValueConversion(typeof(System.Drawing.Bitmap), typeof(ImageSource))]
    public class BitmapToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bmp = value as System.Drawing.Bitmap;
            if (bmp == null)
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        Res.Icono_Usuario.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
            }
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        bmp.GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
