using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    [Serializable]
    public class Venta
    {
        private DateTime hora;
        private bool efectivo;
        private double total;
        public DateTime Hora { get => hora; set => hora = value; }
        public double Total { get => total; set => total = value; }
        public bool Efectivo { get => efectivo; set => efectivo = value; }

        public Venta(DateTime Hora, double Total, bool Efectivo)
        {
            hora = Hora;
            total = Total;
            efectivo = Efectivo;
        }
    }
}
