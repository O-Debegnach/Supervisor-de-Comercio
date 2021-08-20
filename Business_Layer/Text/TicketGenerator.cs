using Business_Layer.General;
using Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_Layer.Text
{
    public abstract class TicketGenerator
    {
        public static string GenerarString(int ancho, List<Articulo_Venta> articulos, decimal total)
        {
            string resultado =string.Empty;
            foreach (Articulo_Venta articulo in articulos)
            {
                var s = articulo.ToString().Split('-');
                //resultado += s[0] + s[1].Center(ancho - s[0].Length - s[2].Length) + s[2] + "\n";
            }

            resultado += "\nTolal: " + total.ToString().PadLeft(ancho - 7);
            return resultado;
        }

        public static string GenerarString(int ancho, string nombreComercio, string direccion, string asunto, decimal total)
        {
            string resultado = nombreComercio.Center(ancho);
            resultado += "\n" + direccion.Center(ancho);
            resultado += "\n" + DateTime.Now.ToString().Center(ancho);
            resultado += "\n" + asunto;
            resultado += "\n$" + total;

            return resultado;
        }

        
    }
}
