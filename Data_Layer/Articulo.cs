using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Data_Layer
{

    [Serializable]
    public class Articulo : INotifyPropertyChanged, IEditableObject
    {
        #region Campos Privados
        private bool _editing;
        private Articulo temp_Articulo;
        private decimal _precio;
        private string _codigo;
        private double _stockActual;
        private double _stockIdeal;
        private string _producto;
        private bool _isRetornable;
        private int _envases;
        private string _proveedor;
        private DateTime _proximoVencimiento;
        private static readonly char separador = 'ð';
        #endregion Campos Privados

        #region Constructores
        public Articulo(Articulo articulo)
        {
            Codigo = articulo.Codigo;
            Producto = articulo.Producto;
            Precio = articulo.Precio;
            Stock_Actual = articulo.Stock_Actual;
            Stock_Ideal = articulo.Stock_Ideal;
            Proveedor = articulo.Proveedor;
            IsRetornable = articulo.IsRetornable;
            CantidadEnvases = articulo.CantidadEnvases;
            TipoVenta = articulo.TipoVenta;
        }

        public Articulo(string codigo, string descripcion, decimal precio, TipoVenta tipoVenta, double stock_actual = 0, double stock_ideal = 0)
        {
            Codigo = codigo;
            Producto = descripcion;
            Precio = precio;
            Stock_Actual = stock_actual;
            Stock_Ideal = stock_ideal;
            TipoVenta = tipoVenta;
        }

        #endregion Constructores

        #region IPropertyChangedMembers
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Metodos

        #region Metodos de Interfaces
        public void BeginEdit()
        {
            if (!_editing)
            {
                temp_Articulo = MemberwiseClone() as Articulo;
                _editing = true;
            }
        }

        public void EndEdit()
        {
            if (_editing)
            {
                temp_Articulo = null;
                _editing = false;
            }
        }

        public void CancelEdit()
        {
            if (_editing)
            {
                Producto = temp_Articulo.Producto;
                Precio = temp_Articulo.Precio;
                Stock_Actual = temp_Articulo.Stock_Actual;
                Stock_Ideal = temp_Articulo.Stock_Ideal;
                Codigo = temp_Articulo.Codigo;
            }
        }
        #endregion Metodos de Interfaces

        public Articulo_Venta GetArticuloVenta()
        {
            return new Articulo_Venta(Producto, 1, Precio, TipoVenta);
        }

        public bool Equals(Articulo articulo)
        {
            return Producto.Equals(articulo.Producto);
        }
        public override string ToString()
        {
            string tV = "";
            switch (TipoVenta)
            {
                case TipoVenta.PorUnidad:
                    tV = "u";
                    break;
                case TipoVenta.PorPeso:
                    tV = "p";
                    break;
                case TipoVenta.PorMonto:
                    tV = "m";
                    break;
                default:
                    tV = "";
                    break;
            }
            string s = Codigo + separador + Producto + separador + Precio.ToString(CultureInfo.InvariantCulture) + separador + (IsRetornable ? "true" : "false") +
                separador + CantidadEnvases.ToString(CultureInfo.InvariantCulture) + separador + tV + separador + Stock_Actual.ToString(CultureInfo.InvariantCulture) + separador + ProximoVencimiento.ToShortDateString();
            return s;
        }

        public static Articulo FromString(string s)
        {
            string[] strings = s.Split(separador);
            if (strings.Length == 8)
            {
                TipoVenta tipoVenta;
                if (strings[5] == "u")
                {
                    tipoVenta = TipoVenta.PorUnidad;
                }
                else if (strings[5] == "p")
                {
                    tipoVenta = TipoVenta.PorPeso;
                }
                else if (strings[5] == "m")
                {
                    tipoVenta = TipoVenta.PorMonto;
                }
                else
                {
                    tipoVenta = TipoVenta.PorUnidad;
                }

                bool isRetornable = strings[3] == "true";

                DateTime fecha = DateTime.MaxValue;
                DateTime.TryParse(strings[7], CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out fecha);

                Articulo art = new Articulo(strings[0], strings[1], Convert.ToDecimal(strings[2], CultureInfo.InvariantCulture), tipoVenta, Convert.ToDouble(strings[6], CultureInfo.InvariantCulture))
                {
                    CantidadEnvases = Convert.ToInt32(strings[4], CultureInfo.InvariantCulture),
                    IsRetornable = isRetornable,
                    ProximoVencimiento = fecha
                };
                art.AddStock(Convert.ToDouble(strings[6], CultureInfo.InvariantCulture), fecha);
                return art;
            }
            return null;
        }

        public void AddStock(double cantidad, DateTime vencimiento)
        {
            if (Vencimientos == null)
            {
                Vencimientos = new Dictionary<DateTime, double>();
            }

            if (Vencimientos.ContainsKey(vencimiento))
            {
                Vencimientos[vencimiento] += cantidad;
                if (Vencimientos[vencimiento] <= 0) Vencimientos.Remove(vencimiento);
                _stockActual += cantidad;
            }
            else
            {
                if (cantidad <= 0) return;
                Vencimientos.Add(vencimiento, ((_stockActual > 0) ? cantidad : cantidad + _stockActual));
                _stockActual += cantidad;
            }
            if (vencimiento < ProximoVencimiento || ProximoVencimiento == DateTime.MinValue)
            {
                ProximoVencimiento = vencimiento;
            }
            NotifyPropertyChanged(nameof(Stock_Actual));
        }

        public void AddStock(Dictionary<DateTime, double> vencimientos)
        {
            if (vencimientos == null)
            {
                return;
            }

            foreach (DateTime date in vencimientos.Keys)
            {
                AddStock(vencimientos[date], date);
            }
        }

        public void AddStock(double cantidad)
        {
            AddStock(cantidad, DateTime.MaxValue);
        }

        private DateTime RemoveStock(double cantidad)
        {
            cantidad *= cantidad >= 0 ? 1 : -1;
            if (Vencimientos == null || Vencimientos.Count <= 0)
            {
                return DateTime.MaxValue;
            }

            DateTime fechaCercana = GetProximoVencimiento();
            if (cantidad < Vencimientos[fechaCercana])
            {
                Vencimientos[fechaCercana] -= cantidad;
                if (Vencimientos[fechaCercana] <= 0)
                {
                    Vencimientos.Remove(fechaCercana);
                    if (Vencimientos.Count == 0) Vencimientos = null;
                }

                return fechaCercana;
            }
            else
            {
                while (cantidad > 0)
                {
                    double prev = Vencimientos[fechaCercana];
                    Vencimientos[fechaCercana] -= cantidad;
                    if (Vencimientos[fechaCercana] <= 0)
                    {
                        Vencimientos.Remove(fechaCercana);
                        if (Vencimientos.Count == 0) Vencimientos = null;
                    }

                    cantidad -= prev;
                    if (Vencimientos != null)
                    {
                        fechaCercana = GetProximoVencimiento();
                    }
                    else
                    {
                        return ProximoVencimiento;
                    }
                }
                return fechaCercana;
            }

        }

        private DateTime GetProximoVencimiento()
        {
            DateTime fechaCercana = DateTime.MaxValue;
            foreach (DateTime fecha in Vencimientos.Keys)
            {
                if (fecha < fechaCercana)
                {
                    fechaCercana = fecha;
                }
            }

            return fechaCercana;
        }

        public void ForceStock(double value)
        {
            _stockActual = value;
        }

        #endregion Metodos

        #region Propiedades
        public Dictionary<DateTime, double> Vencimientos { get; set; }

        public TipoVenta TipoVenta { get; set; }

        public double Stock_Actual
        {
            get => _stockActual;
            set
            {
                double dif = value - Stock_Actual;
                if (dif < 0)
                {
                    ProximoVencimiento = RemoveStock(dif);
                    _stockActual = value;
                    NotifyPropertyChanged(nameof(Stock_Actual));
                }

            }
        }

        public string StrProximoVencimiento
        {
            get
            {
                bool condicion = DateTime.MinValue.Date < ProximoVencimiento.Date && ProximoVencimiento.Date < DateTime.MaxValue.Date;
                return condicion ? ProximoVencimiento.ToShortDateString() : "No posee";
            }
        }

        public DateTime ProximoVencimiento
        {
            get => _proximoVencimiento;
            set
            {
                if (value != _proximoVencimiento)
                {
                    _proximoVencimiento = value;
                    NotifyPropertyChanged(nameof(ProximoVencimiento));
                    NotifyPropertyChanged(nameof(StrProximoVencimiento));
                }
            }
        }

        public decimal Precio
        {
            get => _precio;
            set
            {
                _precio = value;
                NotifyPropertyChanged(nameof(Precio));
            }
        }

        public string Codigo
        {
            get => _codigo;
            set
            {
                _codigo = value;
                NotifyPropertyChanged(nameof(Codigo));
            }
        }

        public double Stock_Ideal
        {
            get => _stockIdeal;
            set
            {
                _stockIdeal = value;
                NotifyPropertyChanged(nameof(Stock_Ideal));
            }
        }


        public string Proveedor
        {
            get => _proveedor;
            set
            {
                _proveedor = value;
                NotifyPropertyChanged(nameof(Proveedor));
            }
        }

        public string StrStock
        {
            get
            {
                switch (TipoVenta)
                {
                    case TipoVenta.PorUnidad:
                        string s = Stock_Actual > 1 ? " unidades" : " unidad";
                        return Stock_Actual.ToString(CultureInfo.CurrentCulture) + s;
                    case TipoVenta.PorPeso:
                        return Stock_Actual.ToString(CultureInfo.CurrentCulture) + "Kg";
                    case TipoVenta.PorMonto:
                        return "$" + Stock_Actual.ToString(CultureInfo.CurrentCulture);
                    default:
                        return Stock_Actual.ToString(CultureInfo.CurrentCulture);
                }
            }
        }
        public int CantidadEnvases
        {
            get => _envases;
            set
            {
                _envases = value;
                NotifyPropertyChanged(nameof(CantidadEnvases));
            }
        }
        public string Producto
        {
            get => _producto;
            set
            {
                _producto = value;
                NotifyPropertyChanged(nameof(Producto));
            }

        }

        public bool IsRetornable
        {
            get => _isRetornable;
            set
            {
                _isRetornable = value;
                NotifyPropertyChanged(nameof(IsRetornable));
            }
        }

        #endregion Propiedades
    }
}
