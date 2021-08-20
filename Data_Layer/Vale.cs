using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    [Serializable]
    public class Vale
    {
        public Vale() { }
        public Vale(string Producto, string Codigo, double Cantidad, decimal Precio)
        {
            this.Producto = Producto;
            this.Codigo = Codigo;
            this.Cantidad = Cantidad;
            this.Precio = Precio;
            Random r = new Random();
            var s = "";
            for(int i = 0; i<100; i++)
            {
                s += r.Next(0, 10).ToString();
            }
            Id = s;
        }

        public string Producto { get; set; }
        public string Codigo { get; set; } 
        public double Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Id { get; set; }
    }
}
