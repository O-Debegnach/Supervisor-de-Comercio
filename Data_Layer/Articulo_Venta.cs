using System;
using System.ComponentModel;

namespace Data_Layer
{
    [Serializable]
    public class Articulo_Venta
    {
        #region Variables
        private string _descripcion;
        private int _cantidad = -1;
        private decimal _precio;
        private double _cantidadD;

        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public decimal Precio { get => _precio; set => _precio = value; }
        public double Cantidad
        {
            get
            {
                if (_cantidad != -1)
                {
                    return _cantidad;
                }
                else
                {
                    return _cantidadD;
                }
            }
            set
            {
                if (_cantidad != -1)
                {
                    _cantidad = Convert.ToInt32(value);
                }
                else
                {
                    _cantidadD = value;
                }
            }
        }
        public decimal Precio_Total
        {
            get
            {
                if (_cantidad != -1)
                {
                    if (tipoVenta == TipoVenta.PorUnidad)
                    {
                        return _precio * _cantidad;
                    }
                    else if (tipoVenta == TipoVenta.PorPeso)
                    {
                        return _precio * ((decimal)_cantidad / 1000);
                    }
                    else
                    {
                        return _cantidad;
                    }
                }
                else
                {
                    if (tipoVenta == TipoVenta.PorUnidad)
                    {
                        return _precio * (decimal)_cantidadD;
                    }
                    else if (tipoVenta == TipoVenta.PorPeso)
                    {
                        return _precio * ((decimal)_cantidadD / 1000);
                    }
                    else
                    {
                        return (decimal)_cantidadD;
                    }
                }

            }
        }

        public TipoVenta tipoVenta { get; set; }
        #endregion


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        #endregion INotifyPropertyChanged Members

        public Articulo_Venta(string Descripcion, double Cantidad, decimal Precio, TipoVenta tipoVenta)
        {
            _cantidadD = Cantidad;
            _descripcion = Descripcion;
            _precio = Precio;
            this.tipoVenta = tipoVenta;
        }

        public override string ToString()
        {
            string u = tipoVenta == TipoVenta.PorUnidad ? " unidad" : "g";
            u += Cantidad > 1 ? "es" : "";
            return Descripcion + " " + Cantidad + u + " " + Precio_Total;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
