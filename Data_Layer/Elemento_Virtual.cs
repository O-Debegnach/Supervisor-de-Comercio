using Data_Layer.Properties;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Data_Layer
{

    public enum TipoVenta : byte
    {
        PorUnidad = 2,
        PorPeso = 1,
        PorMonto = 0
    }

    [Serializable]
    public class ElementoVirtual
    {

        #region Variables
        [field: NonSerialized]
        private readonly Button button = new Button();
        [field: NonSerialized]
        private readonly DockPanel elemento = new DockPanel();
        [field: NonSerialized]
        private readonly TextBox textBox = new TextBox();
        [field: NonSerialized]
        private readonly Label label = new Label();
        public bool Activo { get; set; } = true;
        private readonly TipoVenta tipoVentaEnum;
        private decimal precio;

        public delegate void Resultado(Articulo_Venta articulo);
        public event Resultado ArticuloGenerado;
        #endregion

        #region Constructores
        /// <summary>
        /// Crea un Articulo sin Codigo de Barras
        /// </summary>
        /// <param name="nombre">Nombre que se le da al articulo</param>
        /// <param name="tipoVenta"></param>
        public ElementoVirtual(string nombre, TipoVenta tipoVenta)
        {
            Nombre = nombre;
            tipoVentaEnum = tipoVenta;
            Inicializar();
        }

        public ElementoVirtual(string nombre, decimal precio, TipoVenta tipoVenta)
        {
            Nombre = nombre;
            tipoVentaEnum = tipoVenta;
            this.precio = precio;
            Inicializar();
        }

        public ElementoVirtual(string nombre, decimal precio, TipoVenta tipoVenta, Articulo articulo)
        {
            Nombre = nombre;
            tipoVentaEnum = tipoVenta;
            this.precio = precio;
            Articulo = articulo;
            Inicializar();
        }

        private void Inicializar()
        {
            elemento.LastChildFill = true;
            elemento.Height = 60;
            elemento.Background = new SolidColorBrush(Colors.White);
            elemento.Margin = new Thickness(0, 0, 0, 10);

            label.Content = Nombre;
            label.Height = 30;
            label.BorderThickness = new Thickness(1, 1, 1, 0);
            label.BorderBrush = new SolidColorBrush(Colors.Gray);
            DockPanel.SetDock(label, Dock.Top);

            textBox.PreviewTextInput += new TextCompositionEventHandler(Preview_Text);
            textBox.GotFocus += new RoutedEventHandler(Txt_Got_Focus);
            textBox.BorderThickness = new Thickness(1, 1, 0, 1);
            textBox.BorderBrush = new SolidColorBrush(Colors.Gray);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.FontSize = 15;

            button.Width = 30;
            button.Click += new RoutedEventHandler(Click);
            button.Style = (Style)Application.Current.FindResource("Button_Tilde_Black");
            button.BorderBrush = new SolidColorBrush(Colors.Gray);

            elemento.Children.Add(label);
            elemento.Children.Add(button);
            elemento.Children.Add(textBox);
        }

        #endregion
        private void Txt_Got_Focus(object sender, RoutedEventArgs e)
        {
            textBox.Text = string.Empty;
            textBox.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void Preview_Text(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,.]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Click(object sender, EventArgs e)
        {
            try
            {
                if (tipoVentaEnum == TipoVenta.PorUnidad)
                {
                    Articulo_Venta articuloVenta = new Articulo_Venta(Nombre, Convert.ToInt32(textBox.Text, CultureInfo.CurrentCulture), precio, tipoVentaEnum);
                    ArticuloGenerado?.Invoke(articuloVenta);
                }
                else if (tipoVentaEnum == TipoVenta.PorPeso)
                {
                    Articulo_Venta articuloVenta = new Articulo_Venta(Nombre, Convert.ToDouble(textBox.Text, CultureInfo.CurrentCulture), precio, tipoVentaEnum);
                    ArticuloGenerado?.Invoke(articuloVenta);
                }
                else if (tipoVentaEnum == TipoVenta.PorMonto)
                {
                    Articulo_Venta articuloVenta = new Articulo_Venta(Nombre, Convert.ToDouble(textBox.Text, CultureInfo.InvariantCulture), Convert.ToDecimal(textBox.Text, CultureInfo.InvariantCulture), tipoVentaEnum);
                    ArticuloGenerado?.Invoke(articuloVenta);
                }
                textBox.Text = string.Empty;
            }
            catch (FormatException)
            {
                textBox.Foreground = new SolidColorBrush(Colors.Red);
                textBox.Text = Resources.Error_Precio;
            }
        }

        public bool Equals(ElementoVirtual elementoVirtual)
        {
            return elementoVirtual.Nombre.Equals(Nombre, StringComparison.Ordinal);
        }

        public string Nombre { get; set; }
        public DockPanel Elemento => elemento;

        //public Articulo_Venta ArticuloVenta { get; set; }

        private readonly string[] tipos_venta = { Resources.Por_Unidad, Resources.Por_Peso, Resources.Por_Monto };
        public string Tipo_Venta => tipos_venta[(int)tipoVentaEnum];
        public decimal Precio { get => precio; set => precio = value; }
        public Articulo Articulo { get; }
        public TipoVenta TipoVentaEnum => tipoVentaEnum;
    }

}
