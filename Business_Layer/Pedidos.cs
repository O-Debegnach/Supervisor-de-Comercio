using Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_Layer
{
    public abstract class Pedidos
    {
        public static bool VerificarProductoRepetido(string Nombre, List<Articulo> articulos)
        {
            foreach(Articulo a in articulos)
            {
                if (string.Equals(Nombre, a.Producto, StringComparison.CurrentCulture)) return true;
            }
            return false;
        }

        public static List<Articulo> GetArticulosNombreParcial(string NombreParcial, List<Articulo> articulos)
        {
            if (articulos == null) return null;
            List<Articulo> coincidencias = new List<Articulo>();
            foreach(Articulo a in articulos)
            {
                if (a.Producto.StartsWith(NombreParcial)) coincidencias.Add(a);
            }
            return coincidencias;
        }

        public static Articulo GenerarArticulo(string codigo, string descripcion, decimal precio, TipoVenta tipoVenta, double stock_actual)
        {
            return new Articulo(codigo, descripcion, precio, tipoVenta, stock_actual);
        }
    }
}
