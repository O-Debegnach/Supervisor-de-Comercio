using Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_Layer
{
    public abstract class Pedidos
    {
        public static bool VerificarProductoRepetido(string Nombre, string codigo, List<Articulo> articulos)
        {
            foreach(Articulo a in articulos)
            {
                if (a.Codigo != null && string.Equals(codigo, a.Codigo, StringComparison.CurrentCulture) || string.Equals(Nombre, a.Producto, StringComparison.CurrentCulture)) return true;
            }
            return false;
        }

        public static List<Articulo> GetArticulosCodigoNombreParcial(string CodigoParcial, string Nombre, List<Articulo> articulos)
        {
            if (articulos == null) return null;
            List<Articulo> coincidencias = new List<Articulo>();
            foreach (Articulo a in articulos)
            {
                if ((a.Codigo != null && a.Codigo.IndexOf(CodigoParcial, StringComparison.CurrentCultureIgnoreCase) != -1) ||a.Producto.IndexOf(Nombre, StringComparison.CurrentCultureIgnoreCase) != -1) coincidencias.Add(a);
            }
            return coincidencias;
        }

        public static List<Articulo> GetArticulosCodigoParcial(string CodigoParcial, List<Articulo> articulos)
        {
            if (articulos == null) return null;
            List<Articulo> coincidencias = new List<Articulo>();
            foreach (Articulo a in articulos)
            {
                if (a.Codigo != null && a.Codigo.StartsWith(CodigoParcial)) coincidencias.Add(a);
            }
            return coincidencias;
        }

        public static List<Articulo> GetArticulosNombreParcial(string NombreParcial, List<Articulo> articulos)
        {
            if (articulos == null) return null;
            List<Articulo> coincidencias = new List<Articulo>();
            coincidencias = articulos.FindAll(x => x.Producto.IndexOf(NombreParcial, StringComparison.CurrentCultureIgnoreCase) >= 0);
            
            return coincidencias;
        }

        public static Articulo GenerarArticulo(string codigo, string descripcion, decimal precio, TipoVenta tipoVenta, double stock_actual)
        {
            return new Articulo(codigo, descripcion, precio, tipoVenta, stock_actual);
        }
    }
}
