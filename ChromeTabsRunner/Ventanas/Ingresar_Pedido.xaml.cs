using Business_Layer;
using Business_Layer.Text;
using Data_Layer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Arch = SupComercio.Resources.Strings.Archivos;
using Res = SupComercio.Properties.Resources;
using Str = SupComercio.Resources.Strings.Mensajes;

namespace SupComercio.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Ingresar_Pedido.xaml
    /// </summary>
    public partial class IngresarPedido : Window
    {
        #region Variables
        private readonly List<EnableToggleText> enableToggles = new List<EnableToggleText>();
        private Articulo tempArticulo = null;

        public delegate void DelegadoAñadir(Articulo añadido);
        public event DelegadoAñadir Añadido;
        public event DelegadoAñadir Editado;

        private readonly SolidColorBrush foregroundError = new SolidColorBrush(Colors.Red);

        private bool AutoCompletado = false;

        private readonly List<Articulo> articulos;

        #endregion

        #region Constructores
        public IngresarPedido()
        {
            InitializeComponent();

            enableToggles.Add(new EnableToggleText(Box_Nombre_Producto, new string[2] { Res.InitNomb_Text, Res.Error_Nombre_Vacio }));
            enableToggles.Add(new EnableToggleText(Box_Codigo, Res.InitCodigo_Text));
            enableToggles.Add(new EnableToggleText(Box_Cantidad, new string[2] { Res.InitCantidad_Text, Res.Error_Cantidad }, TextMode.Numeric));
            enableToggles.Add(new EnableToggleText(Box_Precio, Res.InitPrecio_Text, TextMode.Money));
            enableToggles.Add(new EnableToggleText(txtCantidadEnvases, Res.InitEnvases, TextMode.Numeric));
            enableToggles.Add(new EnableToggleText(txtProveedor, Res.InitProveedor));

            #region Inicializar Texto
            InicializarCajaDeTexto();
            #endregion
            //articulos = Archivos.LeerArchivo<Articulo>(Arch.Articulos).ToList();
        }

        public IngresarPedido(List<Articulo> lista) : this()
        {
            if (lista == null)
            {
                throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, Str.ArgumentNullException, nameof(lista)));
            }

            if (lista.Count > 0)
            {
                articulos = new List<Articulo>(lista);
            }
            else
            {
                articulos = Archivos.LeerArchivo<Articulo>(Arch.Articulos).ToList();
            }
        }

        #endregion


        #region Funciones
        private void InicializarCajaDeTexto()
        {
            foreach (EnableToggleText enableToggle in enableToggles)
            {
                enableToggle.InicializarTexto();
            }

            Box_Codigo.IsEnabled = true;

            AutoCompletado = false;
        }

        public TipoVenta GetTipoVenta()
        {
            if (Combo_Tipo_Cantidad.SelectedIndex == 0)
            {
                return TipoVenta.PorMonto;
            }
            else if (Combo_Tipo_Cantidad.SelectedIndex == 1)
            {
                return TipoVenta.PorPeso;
            }
            else
            {
                return TipoVenta.PorUnidad;
            }
        }

        private bool GenerarPedido()
        {
            if (VerificarPedido())
            {
                string codigo;
                if (Box_Codigo.Text.Equals(Res.InitCodigo_Text, StringComparison.CurrentCulture) ||
                   string.IsNullOrWhiteSpace(Box_Codigo.Text))
                {
                    if (Box_Codigo.IsEnabled)
                    {
                        if (MessageBox.Show(this, "El articulo se registrara como \"sin codigo\" \n ¿Desea registrarlo?", Str.Advertencia, MessageBoxButton.YesNo, MessageBoxImage.Warning)
                            == MessageBoxResult.Yes)
                        {
                            codigo = string.Empty;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        codigo = string.Empty;
                    }
                }
                else
                {
                    codigo = Box_Codigo.Text;
                }

                double cantidad = 0;
                if (Convertible(Box_Cantidad.Text))
                {
                    cantidad = Convert.ToDouble(Box_Cantidad.Text, CultureInfo.CurrentCulture);
                }

                Articulo a;
                if (GetTipoVenta() != TipoVenta.PorMonto)
                {
                    a = new Articulo(codigo,
                                               Box_Nombre_Producto.Text,
                                               Convert.ToDecimal(Box_Precio.Text, CultureInfo.CurrentCulture),
                                               GetTipoVenta(),
                                               0);
                    var fecha = Calendario.SelectedDate ?? DateTime.MaxValue;
                    
                    if (AutoCompletado && tempArticulo != null && tempArticulo.Vencimientos != null)
                    {
                        if (tempArticulo.Vencimientos.ContainsKey(fecha) && tempArticulo.Vencimientos[fecha] + cantidad < 0)
                        {
                            MessageBox.Show($"Esta queriendo eliminar {cantidad} {tempArticulo.Producto} con fecha de vencimiento {fecha.ToShortDateString()}\n" +
                                $"Pero solo se detectaron {tempArticulo.Vencimientos[fecha]} {tempArticulo.Producto} con dicho vencimiento\n" +
                                $"Por lo tanto solo se eliminaran los/las {tempArticulo.Producto} que contengan dicha fecha de vencimiento");

                            cantidad = tempArticulo.Vencimientos[fecha];
                        }
                        else if(fecha != DateTime.MaxValue && cantidad < 0)
                        {
                            MessageBox.Show($"No se registran {tempArticulo.Producto} con fecha de vencimiento {fecha.ToShortDateString()}\n" +
                                $"y no puede haber stocks negaticos con vencimiento");
                            return false;
                        }
                    }
                    a.AddStock(cantidad, fecha);
                }
                else
                {
                    a = new Articulo(codigo,
                                               Box_Nombre_Producto.Text,
                                               1,
                                               GetTipoVenta(),
                                               0);
                    var fecha = Calendario.SelectedDate ?? DateTime.MaxValue;
                    if (AutoCompletado && tempArticulo != null && tempArticulo.Vencimientos[fecha] - cantidad < 0)
                    {
                        MessageBox.Show($"Esta queriendo eliminar {cantidad} {tempArticulo.Producto} con fecha de vencimiento {fecha.ToShortDateString()}\n" +
                            $"Pero solo se detectaron {tempArticulo.Vencimientos[fecha]} {tempArticulo.Producto} con dicho vencimiento\n" +
                            $"Por lo tanto solo se eliminaran los/las {tempArticulo.Producto} que contengan dicha fecha de vencimiento");
                        cantidad = tempArticulo.Vencimientos[fecha];
                    }
                    a.AddStock(Convert.ToDouble(Box_Precio.Text, CultureInfo.CurrentCulture), 
                                                fecha);
                }
                if (checkBox.IsChecked == true && Convertible(txtCantidadEnvases.Text))
                {
                    a.IsRetornable = true;
                    a.CantidadEnvases = Convert.ToInt32(txtCantidadEnvases.Text, CultureInfo.CurrentCulture);
                }
                else if (checkBox.IsChecked == true && !Convertible(txtCantidadEnvases.Text))
                {
                    MessageBox.Show("La cantidad ingresada es invalida.");
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(txtProveedor.Text) && !txtProveedor.Text.Equals(Res.InitProveedor, StringComparison.CurrentCulture)) a.Proveedor = txtProveedor.Text;

                

                if (AutoCompletado)
                {

                    if (GetTipoVenta() != a.TipoVenta) {
                        MessageBoxResult res = MessageBox.Show(Str.CambioUnidad_message, Str.CambioUnidad_caption, MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (res == MessageBoxResult.Yes)
                        {
                            Editado?.Invoke(a);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        Editado?.Invoke(a);
                    }
                }
                else
                {
                    Añadido?.Invoke(a);
                }

                InicializarCajaDeTexto();
                return true;
            }
            return false;
        }

        private bool VerificarPedido()
        {
            bool flag = true;

            if (Pedidos.VerificarProductoRepetido(Box_Nombre_Producto.Text ,Box_Codigo.Text, articulos) && !AutoCompletado)
            {
                MessageBoxResult respusta = MessageBox.Show("El codigo o nombre del producto ya se encuentra registrado \n¿Desea cargar la informacion dicho producto?", Str.Advertencia, MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (respusta == MessageBoxResult.Yes)
                {
                    foreach (Articulo a in articulos)
                    {
                        if (a.Codigo.Equals(Box_Codigo.Text, StringComparison.Ordinal) || a.Producto.Equals(Box_Nombre_Producto.Text, StringComparison.CurrentCultureIgnoreCase))
                        {
                            tempArticulo = a;
                            AutoCompletado = true;
                            Box_Nombre_Producto.Text = a.Producto;
                            Box_Precio.Text = a.TipoVenta != TipoVenta.PorMonto ? a.Precio.ToString(CultureInfo.CurrentCulture) : Res.InitPrecio_Text;
                            checkBox.IsChecked = a.IsRetornable;
                            checkBoxVencimiento.IsChecked = (a.Vencimientos != null);
                            txtProveedor.Text = a.Proveedor ?? "Proveedor";
                            if (!string.IsNullOrWhiteSpace(a.Codigo))
                            {
                                Box_Codigo.Text = a.Codigo;
                            }
                            else
                            {
                                Box_Codigo.Text = Res.InitCodigo_Text;
                            }

                            Box_Codigo.IsEnabled = false;
                            return false;
                        }
                    }
                }
                else if (respusta == MessageBoxResult.No)
                {
                    InicializarCajaDeTexto();
                    return false;
                }
                else
                {
                    return false;
                }
            }
            if (string.IsNullOrWhiteSpace(Box_Nombre_Producto.Text) ||
                Res.InitNomb_Text.Equals(Box_Nombre_Producto.Text, StringComparison.OrdinalIgnoreCase))
            {
                Box_Nombre_Producto.Foreground = foregroundError;
                Box_Nombre_Producto.Text = Res.Error_Nombre_Vacio;
                flag = false;
            }
            if (GetTipoVenta() != TipoVenta.PorMonto && (string.IsNullOrWhiteSpace(Box_Cantidad.Text) ||
                !Convertible(Box_Cantidad.Text)) && !AutoCompletado)
            {
                Box_Cantidad.Foreground = foregroundError;
                Box_Cantidad.Text = Res.Error_Cantidad;
                flag = false;
            }
            if (string.IsNullOrWhiteSpace(Box_Precio.Text) ||
                !Convertible(Box_Precio.Text))
            {
                Box_Precio.Foreground = foregroundError;
                Box_Precio.Text = Res.Error_Precio;
                flag = false;
            }
            if(checkBoxVencimiento.IsChecked == true)
            {
                if(!Calendario.SelectedDate.HasValue)
                {
                    MessageBox.Show("Por favor seleccione una fecha");
                    flag = false;
                }
            }
            return flag;
        }

        private static bool Convertible(string text)
        {
            try
            {
                Convert.ToDouble(text, CultureInfo.CurrentCulture);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException) { return false; }
        }

        #endregion

        #region Eventos

        private void Box_Nombre_Producto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                List_Nombres.Visibility = Visibility.Collapsed;
                Lista_Codigo.Visibility = Visibility.Collapsed;
            }
            else
            {
                List<Articulo> coincidencias = Pedidos.GetArticulosNombreParcial(Box_Nombre_Producto.Text, articulos);
                List_Nombres.Items.Clear();
                foreach (Articulo a in coincidencias)
                {
                    List_Nombres.Items.Add(a);
                }
                if (List_Nombres.Items.Count > 0)
                {
                    List_Nombres.Visibility = Visibility.Visible;
                }
                else
                {
                    List_Nombres.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void List_Nombres_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            tempArticulo = List_Nombres.SelectedItem as Articulo;
            Box_Codigo.IsEnabled = false;
            if (tempArticulo != null && tempArticulo.Codigo != null)
            {
                Box_Codigo.Text = tempArticulo.Codigo;
            }
            Combo_Tipo_Cantidad.SelectedIndex = (int)tempArticulo.TipoVenta;
            Box_Nombre_Producto.Text = tempArticulo.Producto;
            Box_Precio.Text = tempArticulo.TipoVenta == TipoVenta.PorMonto ? tempArticulo.Stock_Actual.ToString(CultureInfo.CurrentCulture) : tempArticulo.Precio.ToString(CultureInfo.CurrentCulture);
            txtCantidadEnvases.Text = (0).ToString(CultureInfo.CurrentCulture);
            checkBox.IsChecked = tempArticulo.IsRetornable;
            AutoCompletado = true;
            List_Nombres.Visibility = Visibility.Collapsed;
        }

        private void Box_Nombre_Producto_LostFocus(object sender, RoutedEventArgs e)
        {
            List_Nombres.Visibility = Visibility.Collapsed;
        }

        private void List_Nombres_GotFocus(object sender, RoutedEventArgs e)
        {
            List_Nombres.Visibility = Visibility.Visible;
        }

        private void Boton_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (GenerarPedido())
            {
                Close();
            }
        }

        private void Boton_Ingresar_Otro_Click(object sender, RoutedEventArgs e)
        {
            GenerarPedido();
        }

        #endregion

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape && (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift && AutoCompletado)
            {
                InicializarCajaDeTexto();
                AutoCompletado = false;
            }
        }

        private void Box_Codigo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Lista_Codigo.Visibility = Visibility.Collapsed;
            }
            if (e.Key == Key.Enter)
            {
                if (Lista_Codigo.Items.Count > 0)
                {
                    tempArticulo = Lista_Codigo.Items[0] as Articulo;
                    Box_Codigo.IsEnabled = false;
                    if (tempArticulo.Codigo != null)
                    {
                        Box_Codigo.Text = tempArticulo.Codigo;
                    }
                    Combo_Tipo_Cantidad.SelectedIndex = (int)tempArticulo.TipoVenta;
                    Box_Nombre_Producto.Text = tempArticulo.Producto;
                    Box_Precio.Text = tempArticulo.Precio.ToString(CultureInfo.CurrentCulture);
                    txtCantidadEnvases.Text = (0).ToString(CultureInfo.CurrentCulture);
                    checkBox.IsChecked = tempArticulo.IsRetornable;
                    AutoCompletado = true;
                    Lista_Codigo.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                List<Articulo> coincidencias = Pedidos.GetArticulosCodigoParcial(Box_Codigo.Text, articulos);
                Lista_Codigo.Items.Clear();
                foreach (Articulo a in coincidencias)
                {
                    Lista_Codigo.Items.Add(a);
                }
                if (Lista_Codigo.Items.Count > 0)
                {
                    Lista_Codigo.Visibility = Visibility.Visible;
                }
                else
                {
                    Lista_Codigo.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void Lista_Codigo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tempArticulo = Lista_Codigo.SelectedItem as Articulo;
            Box_Codigo.IsEnabled = false;
            if (tempArticulo.Codigo != null)
            {
                Box_Codigo.Text = tempArticulo.Codigo;
            }
            Combo_Tipo_Cantidad.SelectedIndex = (int)tempArticulo.TipoVenta;
            Box_Nombre_Producto.Text = tempArticulo.Producto;
            Box_Precio.Text = tempArticulo.Precio.ToString(CultureInfo.CurrentCulture);
            txtCantidadEnvases.Text = (0).ToString(CultureInfo.CurrentCulture);
            checkBox.IsChecked = tempArticulo.IsRetornable;
            AutoCompletado = true;
            Lista_Codigo.Visibility = Visibility.Collapsed;
        }

        private void Box_Codigo_LostFocus(object sender, RoutedEventArgs e)
        {
            Lista_Codigo.Visibility = Visibility.Collapsed;
        }
    }
}
