using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Image = System.Drawing.Image;

namespace SupComercio
{

    static class Helpers
    {
        #region Lista
        public static void Añadir<T>(this List<T> ls, T item, DataGrid enlazada)
        {
            ls.Add(item);
            enlazada.ItemsSource = null;
            enlazada.ItemsSource = ls;
        }

        public static void Insertar<T>(this List<T> ls, int index, T item, DataGrid enlazada)
        {
            ls.RemoveAt(index);
            ls.Insert(index, item);
            enlazada.ItemsSource = null;
            enlazada.ItemsSource = ls;
        }
        #endregion

        #region IEnumerable
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> ts)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();
            foreach (T item in ts)
            {
                collection.Add(item);
            }
            return collection;
        }

        public static int FindIndex<T>(this IEnumerable<T> ts, Predicate<T> p)
        {
            for(int i = 0; i<ts.Count(); i++)
            {
                if (p(ts.ElementAt(i))) return i;
            }
            return -1;
        }
        #endregion

        #region Math
        static public double Map(this double value, double istart, double istop, double ostart, double ostop)
        {
            return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
        }
        #endregion Math
        public static BitmapImage Convert(this Image img)
        {
            using (var memory = new MemoryStream())
            {
                img.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        public static Bitmap BitmapImage2Bitmap(this BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

    }
}
