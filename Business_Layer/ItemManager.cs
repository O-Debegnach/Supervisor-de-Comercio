using Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Business_Layer
{
    public abstract class ItemManager
    {
         public static void AddVenta(DataGrid grid, Articulo articulo)
        {
            grid.Items.Add(Articulo_Venta.FromArticulo(articulo));
        }
    }
}
