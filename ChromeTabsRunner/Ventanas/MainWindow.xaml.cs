using Business_Layer;
using Business_Layer.Server;
using Business_Layer.Text;
using Business_Layer.Ticket;
using Data_Layer;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using SupComercio.Properties;
using SupComercio.Ventanas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Arch = SupComercio.Resources.Strings.Archivos;
using EnableToggle = Business_Layer.EnableToggleText;
using Res = SupComercio.Properties.Resources;
using ResCaja = SupComercio.Resources.Strings.StringsCierreDeCaja;
using Str = SupComercio.Resources.Strings.Mensajes;
using Timer = System.Timers.Timer;

namespace SupComercio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1001:Los tipos que poseen campos descartables deben ser descartables", Justification = "<pendiente>")]
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion INotifyPropertyChanged members

        #region Variables

        #region Campos Privados
        private readonly List<ElementoVirtual> elementos = new List<ElementoVirtual>();
        private Login login = new Login();
        private readonly List<EnableToggle> toggleVales = new List<EnableToggle>();
        private readonly Stopwatch sw = new Stopwatch();
        private readonly Timer timer = new Timer(1000);
        private readonly Timer CodeClearer = new Timer(1000);
        private Cliente cliente;
        private Server server;
        private bool ValeSeteado = false;

        private static DateTime Vencimiento = new DateTime(2022, 1, 30);
        private string StrVencimiento = $"Usted dispone de la versión pro y todas sus actualizaciones disponibles hasta el {Vencimiento.AddMonths(-1).ToShortDateString()}";

        private readonly Empleado AdminPrincipal;

        private List<Articulo> articulos;
        private readonly ArticulosCollection Articulos;
        private readonly List<Empleado> EmpleadosPresentes = new List<Empleado>();
        private Articulo articuloVale;
        private decimal _total;
        private decimal _fcEntregado;
        private decimal _fcRecibido;
        private decimal _efectivoRecivido;
        private decimal _debitoRecibido;
        private decimal _depositos;
        private decimal _extracciones;
        private decimal _pagoProveedores;
        private decimal _recaudacionTotal;
        private GestorEmpleados GestorEmpleados;
        private bool adminLogued;
        private readonly DateTime InicioTurno;
        private string CodigoStock = string.Empty;
        private string nick;

        //private readonly HtmlAgilityPack.HtmlWeb oWeb = new HtmlAgilityPack.HtmlWeb();
        #endregion Campos Privados

        #region Propiedades
        private ContraseñaAdmin ContraseñaAdmin { get; set; }
        public int PestañaSeleccionada { get; private set; }
        public string NombreComercio { get; set; }
        public string Direccion { get; set; }
        public List<Empleado> Empleados { get; }
        public string ProductoVale { get; set; }
        public string CantidadVale { get; set; }
        private Empleado EmpleadoLogueado { get; set; } = null;
        private List<Articulo_Venta> Ventas { get; } = new List<Articulo_Venta>();
        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        #region Props Caja
        private decimal FCEntregado
        {
            get => _fcEntregado;
            set
            {
                _fcEntregado = value;
                txtFCEntregado.Text = FCEntregado.ToString(CultureInfo.CurrentCulture);
                OnPropertyChanged(nameof(FCEntregado));
            }
        }
        private decimal FCRecibido
        {
            get => _fcRecibido;
            set
            {
                _fcRecibido = value;
                txtFCRecibido.Text = FCRecibido.ToString(CultureInfo.CurrentCulture);
                OnPropertyChanged(nameof(FCRecibido));
            }
        }
        private decimal EfectivoRecibido
        {
            get => _efectivoRecivido;
            set
            {
                _efectivoRecivido = value;
                txtEfectivo.Text = EfectivoRecibido.ToString(CultureInfo.CurrentCulture);
                OnPropertyChanged(nameof(EfectivoRecibido));
            }
        }
        private decimal DebitoRecibido
        {
            get => _debitoRecibido;
            set
            {
                _debitoRecibido = value;
                txtDebito.Text = DebitoRecibido.ToString(CultureInfo.CurrentCulture);
                OnPropertyChanged(nameof(Depositos));
            }
        }
        private decimal Depositos
        {
            get => _depositos;
            set
            {
                _depositos = value;
                OnPropertyChanged(nameof(Depositos));
            }
        }
        private decimal Extracciones
        {
            get => _extracciones;
            set
            {
                _extracciones = value;
                OnPropertyChanged(nameof(Extracciones));
            }
        }
        private decimal PagoProveedores
        {
            get => _pagoProveedores;
            set
            {
                _pagoProveedores = value;
                OnPropertyChanged(nameof(PagoProveedores));
            }
        }
        private decimal RecaudacionTotal
        {
            get => _recaudacionTotal;
            set
            {
                _recaudacionTotal = value;
                txtRecaudacion.Text = RecaudacionTotal.ToString(CultureInfo.CurrentCulture);
                OnPropertyChanged(nameof(RecaudacionTotal));
            }
        }

        private decimal DineroCaja { get; set; } = 0;
        public decimal Sueldos { get; private set; }
        public bool GenerarPDFTicket { get; private set; }
        public string RutaPDF { get; private set; }
        #endregion Props Caja

        #endregion Propiedades
        #endregion Variables


        #region Constructores
        public MainWindow()
        {
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                nick += r.Next(0, 10).ToString(CultureInfo.InvariantCulture);
            }

            //articulos = new List<Articulo>();
            //for(int i = 500; i < 1500; i++)
            //{
            //    articulos.Add(new Articulo(i.ToString(), $"Articulo {i}", i, TipoVenta.PorMonto));
            //}

            articulos = Archivos.LeerArchivo<Articulo>(Arch.Articulos).ToList();

            if (articulos.Count < Settings.Default.CantidadArticulos)
            {
                MessageBox.Show("Debido a un error del archivo, es posible que haya una perdida de datos.\n" +
                    $"Recuento de articulos anterior: {Settings.Default.CantidadArticulos}\n" +
                    $"Recuento de articulos actual: {articulos.Count}\n" +
                    $"De ser necesario cargue una copia de seguridad");
            }

            Empleados = Archivos.LeerArchivo<Empleado>(Arch.Empleado, x => x.IsActive).ToList();

            Direccion = Settings.Default.DireccionComercio;
            NombreComercio = Settings.Default.NombreComercio;

            InitializeComponent();
            if (DateTime.Now > Vencimiento)
            {
                MessageBox.Show("La licencia del programa ha caducado");
                Close();
            }

            lblVencimiento.Content = StrVencimiento;

            Articulos = (ArticulosCollection)Resources["ArticulosCollection"];
            articulos.ForEach((x) => { /*if (x.Codigo == null) x.Codigo = string.Empty; if (x.TipoVenta == TipoVenta.PorMonto) x.TipoVenta = TipoVenta.PorUnidad;*/  Articulos.Add(x); });
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;


            //if (string.IsNullOrWhiteSpace(Settings.Default.Licencia))
            //{
            //    Validacion validacion = new Validacion();
            //    validacion.LicenciaValidada += LicenciaValidada;
            //    validacion.ShowDialog();
            //}
            //else
            //{
            //    try
            //    {
            //        Business_Layer.Licencia.LicenseManager licenseManager = new Business_Layer.Licencia.LicenseManager(oWeb);
            //        string result = licenseManager.VerificarLicencia(Settings.Default.Licencia, Settings.Default.Mac);

            //        if (!result.Contains("M200"))
            //        {
            //            MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Res.ErrorVerificacionLicencia, result));
            //            Close();
            //            Application.Current.Shutdown();
            //        }
            //        else
            //        {
            //            Settings.Default.FechaUltimaValidacion = DateTime.Now;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        if (Settings.Default.FechaUltimaValidacion.AddYears(1) < DateTime.Now
            //            || Settings.Default.FechaExpiracion.AddDays(15) < DateTime.Now)
            //        {
            //            MessageBox.Show(Str.LicenciaVencida + "\nPongase en contacto con Soporte Tecnico");
            //            Close();
            //            Application.Current.Shutdown();
            //        }
            //    }
            //}

            //var currentScreen = SystemParameters.WorkArea;

            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            Margin = new Thickness(20);

            if (File.Exists(Arch.Caja))
            {
                List<Caja> c = Archivos.LeerArchivo<Caja>(Arch.Caja).ToList();
                if (c.Count > 0 && c[0] != null)
                {
                    FCEntregado = c[0].FCEntregado;
                    FCRecibido = c[0].FCRecibido;
                    PagoProveedores = c[0].PagoProveedores;
                    Depositos = c[0].Depositos;
                    DebitoRecibido = c[0].DebitoRecibido;
                    EfectivoRecibido = c[0].EfectivoRecibido;
                    Extracciones = c[0].Extracciones;
                    RecaudacionTotal = c[0].RecaudacionTotal;
                    Sueldos = c[0].Sueldos;
                    ActualizarCaja();
                }
            }
            //EmpleadoLogueado = new Empleado() { IsShowed = true };
            login.UsuarioRegistrado += UsuarioRegistrado;
            login.Logueado += Login_Logueado;
            login.ShowDialog();

            InicioTurno = DateTime.Now;
            sw.Start();
            MouseMove += MainWindow_MouseMove;

            _ = new EnableToggle(txtBuscarArticulo, Res.InitBuscNombre);
            _ = new EnableToggle(txtFCEntregado, "00,00", TextMode.Money);
            _ = new EnableToggle(txtFCRecibido, "00,00", TextMode.Money);
            _ = new EnableToggle(txtDeposito, "00,00", TextMode.Money);
            _ = new EnableToggle(txtPagoProveedores, "00,00", TextMode.Money);
            _ = new EnableToggle(txtExtracciones, "00,00", TextMode.Money);
            _ = new EnableToggle(txtBusquedaStock, Res.InitBuscNombre);

            int p = Empleados.FindIndex(x => !x.IsShowed);
            AdminPrincipal = p > -1 ? Empleados[p] : null;
            if (p > -1)
            {
                Empleados.RemoveAt(p);
                OnPropertyChanged(nameof(Empleados));
            }

            Data_Grid_Stock.ItemsSource = articulos;
            SearchElementos();
            RefreshElementos();

            InitVale();

            if (EmpleadoLogueado != null)
            {
                timer.Elapsed += EventoTemporizado;
                timer.AutoReset = true;
                timer.Enabled = true;
            }


            OnPropertyChanged(nameof(Empleados));
        }

        //private void Cliente_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    txtBuscarArticulo.Text = cliente.InformacionRecibida;
        //}

        private void LicenciaValidada(LicenciaValidadaEnventArgs e)
        {
            if (e is null)
            {
                Close();
            }
        }

        #endregion

        #region Eventos

        #region Generales
        private void LogueoAdministrador()
        {
            //if ((chrometabs.SelectedItem as ChromeTabs.ChromeTabItem).Header.Equals(AdministrarTab.Header) && !EmpleadoLogueado.UsuarioAsociado.Administrador)
            //{
            //    if (ContraseñaAdmin == null)
            //    {
            //        ContraseñaAdmin = new ContraseñaAdmin(Empleados);
            //        ContraseñaAdmin.Closed += ContraseñaAdmin_Closed; ;
            //        ContraseñaAdmin.Confirmado += ContraseñaAdmin_Confirmado; ;
            //        ContraseñaAdmin.Owner = this;
            //        ContraseñaAdmin.ShowDialog();
            //    }
            //}
        }

        private void ContraseñaAdmin_Confirmado(bool isConfirmed)
        {
            if (!isConfirmed)
            {
                chrometabs.SelectedIndex = PestañaSeleccionada;
            }
            adminLogued = isConfirmed;
        }

        private void ContraseñaAdmin_Closed(object sender, EventArgs e)
        {
            ContraseñaAdmin = null;
        }

        private void chrometabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chrometabs.SelectedIndex != 3)
            {
                PestañaSeleccionada = chrometabs.SelectedIndex;
                if (EmpleadoLogueado != null)
                {
                    adminLogued = EmpleadoLogueado.UsuarioAsociado.Administrador;
                }
            }
        }

        private bool flagETFiles = false;
        private bool flagBackUp = false;
        private void EventoTemporizado(object sender, ElapsedEventArgs e)
        {
            Empleados.ForEach((x) => { if (x.IsPresente) { x.HorasTrabajadas += new TimeSpan(0, 0, 1); } });
            if (!flagBackUp && sw.Elapsed.Hours > 0 && sw.Elapsed.Hours % 2 == 0)
            {
                Archivos.DoBackup(articulos);
                flagBackUp = true;
            }
            else if(flagBackUp && sw.Elapsed.Hours % 2 != 0)
            {
                flagBackUp = false;
            }

            if (!flagETFiles && sw.Elapsed.Minutes > 0 && sw.Elapsed.Minutes % 2 == 0)
            {
                Archivos.SobreescribirArchivo(Arch.Articulos, articulos, backUp: true);
                if (Archivos.ActualizarArchivo(Arch.Empleado, Empleados.Add(AdminPrincipal, 0), false, (x, y) => x.Cuil.Equals(y.Cuil, StringComparison.Ordinal)) == 2)
                {
                    Archivos.SobreescribirArchivo(Arch.Empleado, Empleados.Add(AdminPrincipal, 0));
                }
                flagETFiles = true;
            }
            else if (flagETFiles && sw.Elapsed.Minutes % 2 != 0)
            {
                flagETFiles = true;
            }

            if (GestorEmpleados != null)
            {
                GestorEmpleados.ListaEmpleados = Empleados.ToObservableCollection();
            }
            OnPropertyChanged(nameof(Empleados));
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var empTMP = Empleados.Find(x => x.Cuil == "----");
            if (AdminPrincipal != null && empTMP == null)
            {
                Empleados.Insert(0, AdminPrincipal);
            }


            //timer.Stop();
            //timer.Dispose();

            Application.Current.Shutdown();
        }

        private void VentanaPrincipal_Closed(object sender, EventArgs e)
        {
            if (server != null)
            {
                server.Detener();
            }

            if (cliente != null)
            {
                cliente.Stop();
            }

            Settings.Default.CantidadArticulos = articulos.Count;
            Settings.Default.Save();

            //for (int i = 0; i < 15; i++)
            //{
            //    Empleados.Add(AdminPrincipal);
            //}
            _ = Archivos.SobreescribirArchivo(Arch.Articulos, articulos, backUp: true);
            _ = Archivos.SobreescribirArchivo(Arch.Empleado, Empleados, condicion: x => x.IsActive);
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            ctrlVentana.State = VentanaPrincipal.WindowState;
        }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Control tmp = sender as Control;
            tmp.FontSize = (e.NewSize.Height - e.NewSize.Height * .1d) / tmp.FontFamily.LineSpacing;
        }
        #endregion Generales

        #region Externos

        private void UsuarioRegistrado(UserRegisteredEventArgs e)
        {
            Empleados.Add(e.Empleado);
            if (Archivos.ActualizarArchivo(Arch.Empleado, e.Empleado, false, x => true) == 2)
            {
                Archivos.SobreescribirArchivo(Arch.Empleado, Empleados.Add(AdminPrincipal, 0));
            }
        }

        private void EV_ArticuloGenerado(Articulo_Venta articulo)
        {
            AgregarVenta(articulo);
        }

        private void Login_Logueado(LoguedEventArgs e)
        {
            if (!e.Logueado)
            {
                Close();
            }

            EmpleadoLogueado = e.Empleado;
            if (EmpleadoLogueado != null)
            {
                if (EmpleadoLogueado.IsShowed && EmpleadoLogueado.Cuil != null)
                {
                    Empleado emp = Empleados.Find(x => x.Cuil.Equals(EmpleadoLogueado.Cuil, StringComparison.OrdinalIgnoreCase));
                    if (emp != null)
                    {
                        emp.IsPresente = true;
                    }

                    lblEmpleadoLogueado.Content = $"{EmpleadoLogueado.Nombre} {EmpleadoLogueado.Apellido}";
                    FotoEmpleadoLogueado.Margin = new Thickness(0, 0, 100 + lblEmpleadoLogueado.RenderSize.Width, 0);
                    EmpleadosPresentes.Add(EmpleadoLogueado);
                }
            }
        }

        private void UsuarioCreado(ArgumentoUsuarioCreado e)
        {
            bool p(Empleado x)
            {
                return string.Equals(x.Cuil, e.EmpleadoAsociado.Cuil, StringComparison.Ordinal);
            }
            int pos = Empleados.FindIndex(p);
            if (pos != -1)
            {
                Empleados[pos].UsuarioAsociado = e.Usuario;
            }

            OnPropertyChanged(nameof(Empleados));
            if (Archivos.ActualizarArchivo(Arch.Empleado, e.EmpleadoConUsuario, false, p) == 2)
            {
                Archivos.SobreescribirArchivo(Arch.Empleado, Empleados.Add(AdminPrincipal, 0));
            }

        }

        private void Pedido_Editado(Articulo e)
        {
            int pos = articulos.FindIndex(x => (x.Codigo != null && x.Codigo.Equals(e.Codigo, StringComparison.CurrentCulture)) || x.Producto.Equals(e.Producto, StringComparison.CurrentCulture));
            if (pos != -1)
            {
                articulos[pos].AddStock(e.Vencimientos);

                articulos[pos].Stock_Ideal = e.Stock_Ideal;
                articulos[pos].TipoVenta = e.TipoVenta;
                articulos[pos].Precio = e.Precio;
                articulos[pos].Proveedor = e.Proveedor;
                articulos[pos].Producto = e.Producto;
                articulos[pos].CantidadEnvases = e.CantidadEnvases;
                articulos[pos].IsRetornable = e.IsRetornable;

                if (articulos[pos].IsRetornable)
                {
                    articulos[pos].CantidadEnvases += e.CantidadEnvases - (int)e.Stock_Actual;
                    Articulos.Clear();
                    articulos.ForEach((w) => Articulos.Add(w));
                }


                PostAndSaveStock(e);

                Articulos.Clear();
                articulos.ForEach((w) => Articulos.Add(w));
            }
            else
            {
                Pedido_Añadido(e);
            }

            if (string.IsNullOrWhiteSpace(e.Codigo))
            {
                elementos.Add(new ElementoVirtual(e.Producto, e.Precio, e.TipoVenta));
            }
            RefreshStock();
            RefreshElementos();
        }

        private void Pedido_Añadido(Articulo añadido)
        {
            int pos = articulos.FindIndex(x => (x.Codigo != null && x.Codigo.Equals(añadido.Codigo, StringComparison.CurrentCulture)) || x.Producto.Equals(añadido.Producto, StringComparison.CurrentCulture));
            if (pos == -1)
            {
                Articulos.Add(añadido);
                articulos.Add(añadido);
            }
            else
            {
                Pedido_Editado(añadido);
            }

            //Archivos.ActualizarArchivo(Arch.Articulos, añadido, añadido.Equals);
            PostAndSaveStock(añadido);
            if (string.IsNullOrWhiteSpace(añadido.Codigo))
            {
                elementos.Add(new ElementoVirtual(añadido.Producto, añadido.Precio, añadido.TipoVenta));
            }
            RefreshStock();
            RefreshElementos();
        }

        private void TituloTicket(CreaTicket Ticket)
        {
            Ticket.AbreCajon();  //abre el cajon
            Ticket.TextoCentro("    La Nueva Alternativa"); // imprime en el centro "Venta mostrador"
            Ticket.TextoIzquierda("Belgrano 1498 esq. Richardson");
            Ticket.TextoIzquierda($"Fecha y Hora: {DateTime.Now}");
            Ticket.LineasGuion(); // imprime una linea de guiones
            Ticket.EncabezadoVenta(); // imprime encabezados
        }


        private void PagoRealizado(ArgumentoPagoRealizado e)
        {
            CreaTicket Ticket = new CreaTicket(Settings.Default.NombreTicketera, Settings.Default.TamanioTicketera);
            bool finalizar = false;
            try
            {
                bool GenerarTicket = MessageBox.Show(Str.GenerarTicket_message, Str.GenerarTicket_caption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
                if (GenerarTicket)
                {
                    TituloTicket(Ticket);
                }
                if (e.TipoDePago == TipoPago.Efectivo)
                {
                    EfectivoRecibido += Total;
                }
                else
                {
                    DebitoRecibido += Total;
                }
                List<Articulo> ls = Articulos != null ? Articulos.ToList() : new List<Articulo>();
                List<Articulo> arts = new List<Articulo>();
                foreach (Articulo_Venta x in Ventas)
                {
                    int pos = articulos.FindIndex(y => y.Producto.Equals(x.Descripcion, StringComparison.CurrentCulture));
                    if (pos != -1)
                    {
                        //articulos[pos].Stock_Actual -= x.Cantidad;
                        if (ls.Count > 0)
                        {
                            Articulo ar = ls.Find(j => j.Producto.Equals(articulos[pos].Producto, StringComparison.CurrentCulture));
                            if (ar != null)
                            {
                                ar.CantidadEnvases += (int)x.Cantidad;
                            }
                        }
                        Articulo a = new Articulo(articulos[pos]);
                        a.ForceStock(0);
                        a.Vencimientos = null;
                        a.Stock_Actual = x.tipoVenta != TipoVenta.PorPeso ? -x.Cantidad : -x.Cantidad / 1000;
                        arts.Add(a);
                    }
                    if (GenerarTicket)
                    {
                        Ticket.AgregaArticulo(x.Descripcion, x.Cantidad, (double)x.Precio, (double)x.Precio_Total);
                    }
                }
                //Articulos.Clear();
                //ls.ForEach((w) => Articulos.Add(w));
                if (GenerarTicket)
                {
                    Ticket.LineasTotales();
                    Ticket.AgregaTotales("Total          ", (double)Total);
                    Ticket.CortaTicket();
                }
                ActualizarStockLista(arts);
                if (Archivos.ActualizarArchivo(Arch.Articulos, articulos, true, (x, y) => x.Producto.Equals(y.Producto, StringComparison.CurrentCulture)) == 2)
                {
                    Archivos.SobreescribirArchivo(Arch.Articulos, articulos, backUp: true);
                }
                Data_Grid_Stock.ItemsSource = null;
                Data_Grid_Stock.ItemsSource = articulos;
                Lista_Venta.ItemsSource = null;
                Ventas.Clear();
                ActualizarCaja();
                finalizar = true;
            }
            catch (Exception)
            {
                MessageBox.Show(Str.ErrorFinalizarPago);
            }
            if (finalizar)
            {
                Ticket.Imprimir();
            }
        }

        private void EmpleadoEditado(EmpleadoEditadoArgs empleado)
        {
            int ind = Empleados.FindIndex(x => x.Cuil == empleado.ValorAnterior.Cuil);
            if (ind != -1)
            {
                Empleados[ind] = empleado.NuevoValor;
            }

            if (Archivos.ActualizarArchivo(Arch.Empleado, empleado.NuevoValor, false, empleado.NuevoValor.Equals) == 2)
            {
                Archivos.SobreescribirArchivo(Arch.Empleado, Empleados.Add(AdminPrincipal, 0));
            }
            OnPropertyChanged(nameof(Empleados));
        }

        private void EmpleadoGenerado(Empleado empleado)
        {
            Empleados.Add(empleado);
            if (Archivos.ActualizarArchivo(Arch.Empleado, empleado, false, empleado.Equals) == 2)
            {
                Archivos.SobreescribirArchivo(Arch.Empleado, Empleados.Add(AdminPrincipal, 0));
            }
            OnPropertyChanged(nameof(Empleados));
        }

        private void EmpleadoDespedido(Empleado empleado)
        {
            int i = Empleados.FindIndex(x => x.Cuil == empleado.Cuil);
            if (i != -1)
            {
                Empleados.RemoveAt(i);
            }

            empleado.IsActive = false;
            if (Archivos.ActualizarArchivo(Arch.Empleado, empleado, false, empleado.Equals) == 2)
            {
                Archivos.SobreescribirArchivo(Arch.Empleado, Empleados.Add(AdminPrincipal, 0), condicion: x => x.IsActive);
            }
            OnPropertyChanged(nameof(Empleados));
        }
        #endregion Externos

        #region Pestaña Ventas
        private void Lista_Venta_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Lista_Venta.ItemsSource = null;
                Lista_Venta.ItemsSource = Ventas;
            }
            ActualizarTotal();
        }

        private void Lista_Venta_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                Articulo_Venta a = e.Row.Item as Articulo_Venta;
                Articulo art = articulos.Find(x => x.Producto.Equals(a.Descripcion, StringComparison.CurrentCulture));
                TextBox el = e.EditingElement as TextBox;
                decimal.TryParse(el.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal c);
                decimal cantidad = art.TipoVenta == TipoVenta.PorPeso ? c / 1000 : c;
                try
                {
                    string pregunta = "\n¿Desea realizar el cambio de todas formas?";
                    if (art.Stock_Actual - (double)cantidad < 0)
                    {
                        string mensaje;
                        if (art.TipoVenta == TipoVenta.PorMonto)
                        {
                            mensaje = $"Stock de {art.Producto} insuficiente\nSe registran ${art.Stock_Actual} restantes";
                        }
                        else if (art.TipoVenta == TipoVenta.PorPeso)
                        {
                            mensaje = $"Stock de {art.Producto} insuficiente\nSe registran {art.Stock_Actual}Kg restantes";
                        }
                        else
                        {
                            mensaje = $"Stock de {art.Producto} insuficiente\nSe registran {art.Stock_Actual} unidades restantes";
                        }

                        MessageBoxResult mBox = MessageBox.Show(mensaje + pregunta, Str.Error, MessageBoxButton.YesNo, MessageBoxImage.Error);

                        if (mBox == MessageBoxResult.No)
                        {
                            e.Cancel = true;
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        private void Boton_Terminar_Venta_Click(object sender, RoutedEventArgs e)
        {
            ActualizarTotal();
            if (Ventas.Count > 0)
            {
                VentanaPagos pagos = new VentanaPagos(Total)
                {
                    Owner = this
                };
                pagos.PagoRealizado += PagoRealizado;

                if (pagos != null)
                {
                    pagos.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show(Res.Error_Lista_Ventas_Vacia);
            }
        }

        private readonly Stopwatch keyUpWatcher = new Stopwatch();
        private void TxtBuscarArticulo_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (keyUpWatcher.ElapsedMilliseconds > 100 || txtBuscarArticulo.Text.Length <= 1)
            {
                List<Articulo> l = Pedidos.GetArticulosCodigoNombreParcial(txtBuscarArticulo.Text, txtBuscarArticulo.Text, articulos);
                DgBusqueda.ItemsSource = l;
                if (e.Key == Key.Enter && !string.IsNullOrWhiteSpace(txtBuscarArticulo.Text) && (sender as TextBox).Name.Equals(txtBuscarArticulo.Name, StringComparison.Ordinal))
                {
                    if (l.Count > 0)
                    {
                        Articulo art = l[0];
                        if (art != null)
                        {
                            AgregarVenta(art.GetArticuloVenta());
                            txtBuscarArticulo.Text = string.Empty;
                        }
                    }
                }
            }
            else
            {
                if (e.Key == Key.Enter)
                {
                    Articulo art = articulos.Find(x => x.Codigo.Equals(txtBuscarArticulo.Text, StringComparison.Ordinal));
                    if (art != null)
                    {
                        AgregarVenta(art.GetArticuloVenta());
                        txtBuscarArticulo.Text = string.Empty;
                    }
                }
            }
            keyUpWatcher.Restart();
        }

        private void DgBusqueda_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Articulo a = DgBusqueda.SelectedItem as Articulo;
            if (a != null)
            {
                AgregarVenta(a.GetArticuloVenta());
                Lista_Venta.ItemsSource = null;
                Lista_Venta.ItemsSource = Ventas;
                OnPropertyChanged(nameof(Ventas));
            }
        }
        #endregion Pestaña Ventas

        #region Pestaña Stock
        private void Data_Grid_Stock_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            List<Articulo> a = articulos;
            if (Archivos.ActualizarArchivo(Arch.Articulos, articulos, true, (x, y) => x.Producto.Equals(y.Producto, StringComparison.CurrentCulture)) == 2)
            {
                Archivos.SobreescribirArchivo(Arch.Articulos, articulos);
            }
        }

        private void txtBusquedaStock_KeyUp(object sender, KeyEventArgs e)
        {
            List<Articulo> ls = Pedidos.GetArticulosCodigoNombreParcial(txtBusquedaStock.Text, txtBusquedaStock.Text, articulos);
            Data_Grid_Stock.ItemsSource = ls;
        }

        private void Data_Grid_Stock_KeyDown(object sender, KeyEventArgs e)
        {
            if (!EmpleadoLogueado.IsShowed || EmpleadoLogueado.UsuarioAsociado.Administrador && !isEditingStock)
            {
                if (e.Key == Key.Delete && Data_Grid_Stock.SelectedItem != null && Data_Grid_Stock.SelectedItem is Articulo a)
                {
                    MessageBoxResult res = MessageBox.Show("¿Desea borrar el articulo seleccionado?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.Yes)
                    {
                        articulos.Remove(a);
                        Articulos.Remove(a);
                        Data_Grid_Stock.ItemsSource = null;
                        Data_Grid_Stock.ItemsSource = articulos;
                    }
                }
            }
        }

        private void chrometabs_MouseMove(object sender, MouseEventArgs e)
        {
            LogueoAdministrador();
        }

        private void chrometabs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LogueoAdministrador();

        }

        private void chrometabs_KeyUp(object sender, KeyEventArgs e)
        {
            string s = (chrometabs.SelectedItem as ChromeTabs.ChromeTabItem).Header.ToString();
            LogueoAdministrador();
            if (s == "Stock")
            {
                if (!CodeClearer.Enabled)
                {
                    CodeClearer.Start();
                    CodeClearer.Elapsed += CodeClearer_Elapsed;
                }
                string key = e.Key.ToString();
                if (key.Length == 1)
                {
                    CodigoStock += key;
                }
                else if (key.Length == 2)
                {
                    CodigoStock += key.Remove(0, 1);
                }
            }
        }

        private void CodeClearer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CodigoStock = string.Empty;
            CodeClearer.Enabled = false;
        }

        private void TxtProductoVale_KeyUp(object sender, KeyEventArgs e)
        {
            List<Articulo> ls = Pedidos.GetArticulosCodigoNombreParcial(txtProductoVale.Text, txtProductoVale.Text, articulos);
            //!string.IsNullOrWhiteSpace(txtProductoVale.Text) ? articulos.FindAll(x => x.Producto.StartsWith(txtProductoVale.Text, StringComparison.CurrentCulture) || x.Codigo.StartsWith(txtProductoVale.Text, StringComparison.CurrentCulture)) : null;
            lsProductosVale.ItemsSource = null;
            lsProductosVale.ItemsSource = ls;
            if (e.Key == Key.Enter && ls != null && ls.Count > 0)
            {
                articuloVale = ls[0];
                SetearVale(articuloVale);
                ls = null;
            }
            lsProductosVale.Visibility = ls != null ? Visibility.Visible : Visibility.Collapsed;
        }
        private void LsProductosVale_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            articuloVale = lsProductosVale.SelectedItem as Articulo;
            if (articuloVale != null)
            {
                SetearVale(articuloVale);
            }
        }

        private void TxtCantidadVale_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (articuloVale != null)
            {
                SetearVale(articuloVale);
            }
        }

        private void Boton_Generar_Vale_Click(object sender, RoutedEventArgs e)
        {
            if (ValeSeteado)
            {
                double.TryParse(lblCantidadVale.Content.ToString(), NumberStyles.Float, CultureInfo.CurrentCulture, out double cantidad);
                decimal.TryParse(lblPrecioTotalVale.Content.ToString(), NumberStyles.Float, CultureInfo.CurrentCulture, out decimal precio);

                Vale vale = new Vale(lblProductoVale.Content.ToString(),
                                     articuloVale.Codigo,
                                     Convert.ToDouble(lblCantidadVale.Content, CultureInfo.CurrentCulture),
                                     Convert.ToDecimal(lblPrecioTotalVale.Content, CultureInfo.CurrentCulture));
                if (cbEmpleadoVale.SelectedIndex != -1)
                {
                    string messageBoxText = $"Se generará un vale a nombre de {Empleados[cbEmpleadoVale.SelectedIndex].NombreCompleto} con la siguiene informacion:\nProducto: {vale.Producto}\nCantidad: {vale.Cantidad}\nPrecio total: {vale.Precio}";
                    if (MessageBox.Show(messageBoxText, Str.Advertencia, MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
                    {
                        Empleados[cbEmpleadoVale.SelectedIndex].Vales.Add(vale);
                        InitVale();
                        OnPropertyChanged(nameof(Empleados));
                    }
                }
                else
                {
                    brEmpleadoVale.BorderBrush = new SolidColorBrush(Colors.Red);
                }
            }
        }

        private void CbEmpleadoVale_GotFocus(object sender, RoutedEventArgs e)
        {
            brEmpleadoVale.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0xAC, 0xAC, 0xAC));
        }
        #endregion Pestaña Stock

        #region Pestaña Caja
        private void TxtRealizarExtraccion_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (txtRealizarExtraccion.Value.HasValue && (decimal)e.NewValue > DineroCaja)
            {
                txtRealizarExtraccion.Value = DineroCaja;
            }
        }

        private void TxtRealizarDeposito_GotFocus(object sender, RoutedEventArgs e)
        {
            txtRealizarDeposito.Text = string.Empty;
            txtRealizarDeposito.Value = 0;
        }

        private void TxtRealizarExtraccion_GotFocus(object sender, RoutedEventArgs e)
        {
            txtRealizarExtraccion.Text = string.Empty;
            txtRealizarExtraccion.Value = 0;
        }

        private void Boton_Finalizar_Turno_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                CheckPathExists = true,
                Filter = "Archivos PDF (*.pdf)|*.pdf|Todos los archivos|*.*"
            };
            if (fileDialog.ShowDialog(this) == true)
            {
                Font font = new Font(Font.FontFamily.HELVETICA, 22);
                FileStream st = new FileStream(fileDialog.FileName, FileMode.Create);

                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, st);
                doc.Open();

                doc.Add(new Paragraph(ResCaja.Titulo, new Font(Font.FontFamily.HELVETICA, 36)));
                doc.Add(new Paragraph(Settings.Default.NombreComercio, new Font(Font.FontFamily.HELVETICA, 28)));
                doc.Add(new Paragraph(Settings.Default.DireccionComercio, new Font(Font.FontFamily.HELVETICA, 22)));
                doc.Add(new Paragraph(string.Format(CultureInfo.CurrentCulture,
                                                    ResCaja.Turno, InicioTurno, DateTime.Now),
                                      font));
                Paragraph personalAcargo = new Paragraph(ResCaja.Personal_Acargo + "\n" + EmpleadoLogueado.NombreCompleto + "(" + EmpleadoLogueado.UsuarioAsociado.Nombre + ")", font)
                {
                    Alignment = 2
                };
                doc.Add(personalAcargo);

                string empleadosPresentes = ResCaja.Personal_Presente;
                foreach (Empleado empleado in EmpleadosPresentes)
                {
                    empleadosPresentes += "\n" + empleado.NombreCompleto + "(" + empleado.UsuarioAsociado.Nombre + ")";
                }
                Paragraph personalPresente = new Paragraph(empleadosPresentes, font)
                {
                    Alignment = 2
                };
                doc.Add(personalPresente);

                doc.Add(new Paragraph(ResCaja.FCRecibido + FCRecibido.ToString(CultureInfo.CurrentCulture), font));
                doc.Add(new Paragraph(ResCaja.FCEntregado + FCEntregado.ToString(CultureInfo.CurrentCulture), font));
                doc.Add(new Paragraph(ResCaja.Efectivo + EfectivoRecibido.ToString(CultureInfo.CurrentCulture), font));
                doc.Add(new Paragraph(ResCaja.Debito + DebitoRecibido.ToString(CultureInfo.CurrentCulture), font));
                doc.Add(new Paragraph(ResCaja.Depositos + Depositos.ToString(CultureInfo.CurrentCulture), font));
                doc.Add(new Paragraph(ResCaja.Extracciones + Extracciones.ToString(CultureInfo.CurrentCulture), font));
                doc.Add(new Paragraph(ResCaja.Sueldos + Sueldos.ToString(CultureInfo.CurrentCulture), font));
                doc.Add(Chunk.NEWLINE);
                doc.Add(new Paragraph(ResCaja.RTotal + RecaudacionTotal.ToString(CultureInfo.CurrentCulture), font));

                doc.Close();
                writer.Close();
                st.Dispose();

                if (File.Exists(Arch.Caja))
                {
                    File.Delete(Arch.Caja);
                }
                if (EmpleadoLogueado != null)
                {
                    Empleado emp = Empleados.Find(x => x.Cuil.Equals(EmpleadoLogueado.Cuil, StringComparison.OrdinalIgnoreCase));
                    if (emp != null)
                    {
                        emp.IsPresente = false;
                    }
                }
                InicializarCaja();
                EmpleadoLogueado.IsPresente = false;
                login = new Login();
                login.UsuarioRegistrado += UsuarioRegistrado;
                login.Logueado += Login_Logueado;
                login.ShowDialog();
                InicializarCaja();
                for (int i = 0; i < chrometabs.Items.Count; i++)
                {
                    if ((chrometabs.Items[i] as ChromeTabs.ChromeTabItem).Header.Equals("Ventas"))
                    {
                        chrometabs.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void KeyUpCaja(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ActualizarCaja();
            }
        }

        private void Button_Realizar_Deposito_Click(object sender, RoutedEventArgs e)
        {
            Depositos += txtRealizarDeposito.Value ?? 0;
            txtRealizarDeposito.Text = string.Empty;
            ActualizarCaja();
        }

        private void Button_RealizarExtraccion_Click(object sender, RoutedEventArgs e)
        {
            Extracciones += txtRealizarExtraccion.Value ?? 0;
            txtRealizarExtraccion.Text = "";
            ActualizarCaja();
        }
        #endregion Pestaña Caja

        #region Pestaña Administrar
        private void BtConfigurarTicketera_Click(object sender, RoutedEventArgs e)
        {
            ConfiguracionTicketera cfgTicketera = new ConfiguracionTicketera();
            cfgTicketera.Show();
        }

        private void Boton_Planilla_Horarios_Click(object sender, RoutedEventArgs e)
        {
            VentanaPlanillaHorarios planillaHorarios = new VentanaPlanillaHorarios(Empleados);
            planillaHorarios.Show();
        }

        private void Boton_Registrar_Ingreso_Egreso_Click(object sender, RoutedEventArgs e)
        {
            VentanaIngresoEgresoEmpleados IEEMpleados = new VentanaIngresoEgresoEmpleados(Empleados.Concat(new List<Empleado>() { AdminPrincipal }));
            IEEMpleados.IngresoMarcado += IngresoMarcado;
            IEEMpleados.EgresoMarcado += EgresoMarcado;
            IEEMpleados.Show();
        }

        private void EgresoMarcado(ArgumentoIngresoEgreso e)
        {
            foreach (Empleado empleado in e.Empleados)
            {
                Empleados.Find(x => x.Cuil.Equals(empleado.Cuil, StringComparison.Ordinal)).IsPresente = false;
            }
        }

        private void IngresoMarcado(ArgumentoIngresoEgreso e)
        {
            foreach (Empleado empleado in e.Empleados)
            {
                if (EmpleadosPresentes.FindIndex(x => x.Cuil.Equals(empleado.Cuil, StringComparison.Ordinal)) == -1)
                {
                    Empleados.Find(x => x.Cuil.Equals(empleado.Cuil, StringComparison.Ordinal)).IsPresente = true;
                    EmpleadosPresentes.Add(empleado);
                    EmpleadosPresentes.Add(empleado);
                }
            }
        }

        private void Boton_Finalizar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Boton_Ingresar_Pedido_Click(object sender, RoutedEventArgs e)
        {
            IngresarPedido pedido = new IngresarPedido(articulos);
            pedido.Show();
            pedido.Añadido += Pedido_Añadido;
            pedido.Editado += Pedido_Editado;
        }

        private void Boton_Agregar_Empleados_Click(object sender, RoutedEventArgs e)
        {
            GestorEmpleados = new GestorEmpleados(this);
            GestorEmpleados.EmpleadoGenerado += EmpleadoGenerado;
            GestorEmpleados.EmpleadoEditado += EmpleadoEditado;
            GestorEmpleados.EmpleadoDespedido += EmpleadoDespedido;
            GestorEmpleados.SueldoPagado += SueldoPagado;
            GestorEmpleados.Show();
        }

        private void SueldoPagado(SueldoPagadoArgs e)
        {
            Sueldos += e.Sueldo;
            ActualizarCaja();
            int ind = Empleados.FindIndex(x => x.Cuil.Equals(e.Empleado.Cuil, StringComparison.Ordinal));
            if (ind > -1)
            {
                Empleados[ind].HorasTrabajadas = TimeSpan.Zero;
                OnPropertyChanged(nameof(Empleados));
            }

            if (Archivos.ActualizarArchivo(Arch.Empleado, e.Empleado, false, x => x.Cuil.Equals(e.Empleado.Cuil, StringComparison.Ordinal)) == 2)
            {
                Archivos.SobreescribirArchivo(Arch.Empleado, Empleados.Add(AdminPrincipal, 0));
            }
        }

        private void Boton_Usuarios_Click(object sender, RoutedEventArgs e)
        {
            VentanaUsuarios usrs = new VentanaUsuarios((new List<Empleado>() { AdminPrincipal }.Concat(Empleados)).ToList());
            usrs.UsuarioCreado += UsuarioCreado;
            usrs.Show();
        }
        #endregion Pestaña Administrar

        #endregion Eventos

        #region Funciones
        private void SetearVale(Articulo producto)
        {
            string s = txtCantidadVale.Text;
            double.TryParse(s, NumberStyles.Float, CultureInfo.CurrentCulture, out double cantidad);
            double cant = 0;
            if (articuloVale != null)
            {
                lblCantidadVale.Content = cantidad <= articuloVale.Stock_Actual ? cantidad : articuloVale.Stock_Actual;
                double.TryParse(lblCantidadVale.Content.ToString(), NumberStyles.Float, CultureInfo.CurrentCulture, out cant);
            }
            lblCantidadVale.Content = !string.IsNullOrWhiteSpace(txtCantidadVale.Text) ? cant : 1;




            ValeSeteado = true;
            lsProductosVale.Visibility = Visibility.Collapsed;
            txtProductoVale.Text = producto.Producto;
            lblProductoVale.Content = producto.Producto;
            lblPrecioTotalVale.Content = producto.Precio * (decimal)cant;


        }

        private void InicializarCaja()
        {
            DebitoRecibido = 0;
            Depositos = 0;
            EfectivoRecibido = 0;
            Extracciones = 0;
            FCEntregado = 0;
            FCRecibido = 0;
            PagoProveedores = 0;
            RecaudacionTotal = 0;
            Sueldos = 0;
            txtFCEntregado.Text = "00.00";
            txtFCRecibido.Text = "00.00";
            ActualizarCaja();
        }

        private void ActualizarCaja()
        {
            FCRecibido = Convert.ToDecimal(txtFCRecibido.Text, CultureInfo.CurrentCulture);
            txtDeposito.Text = Depositos.ToString(CultureInfo.CurrentCulture);
            txtEfectivo.Text = EfectivoRecibido.ToString(CultureInfo.CurrentCulture);
            txtDebito.Text = DebitoRecibido.ToString(CultureInfo.CurrentCulture);

            FCEntregado = Convert.ToDecimal(txtFCEntregado.Text, CultureInfo.CurrentCulture);
            PagoProveedores = Convert.ToDecimal(txtPagoProveedores.Text, CultureInfo.CurrentCulture);
            txtExtracciones.Text = Extracciones.ToString(CultureInfo.CurrentCulture);
            txtSueldos.Text = Sueldos.ToString(CultureInfo.CurrentCulture);

            DineroCaja = FCEntregado + EfectivoRecibido + Depositos - Extracciones;
            RecaudacionTotal = FCRecibido + EfectivoRecibido + DebitoRecibido + Depositos - Extracciones - Sueldos - FCEntregado - PagoProveedores;
            Archivos.ActualizarArchivo<Caja>(Arch.Caja, new Caja
            {
                DebitoRecibido = DebitoRecibido,
                Depositos = Depositos,
                EfectivoRecibido = EfectivoRecibido,
                Extracciones = Extracciones,
                FCEntregado = FCEntregado,
                FCRecibido = FCRecibido,
                PagoProveedores = PagoProveedores,
                RecaudacionTotal = RecaudacionTotal,
                Sueldos = Sueldos
            }, false, x => true);
            txtRecaudacion.Text = RecaudacionTotal.ToString(CultureInfo.CurrentCulture);
        }

        private void ActualizarTotal()
        {
            Total = 0;
            foreach (Articulo_Venta a in Ventas)
            {
                Total += a.Precio_Total;
            }
            //Label_Total.Content = String.Format(CultureInfo.CurrentCulture, "Total: ${0}", Total);
        }

        private void InitVale()
        {
            if (toggleVales.Count == 0)
            {
                toggleVales.Add(new EnableToggle(txtCantidadVale, "", TextMode.Numeric));
                toggleVales.Add(new EnableToggle(txtProductoVale, ""));
            }
            else
            {
                toggleVales.ForEach((x) => x.InicializarTexto());
                lblCantidadVale.Content = lblPrecioTotalVale.Content = lblProductoVale.Content = "";
                cbEmpleadoVale.SelectedIndex = -1;
            }
        }

        private void RefreshElementos()
        {
            Elementos_Virtuales.Children.Clear();
            foreach (ElementoVirtual e in elementos)
            {
                Elementos_Virtuales.Children.Add(e.Elemento);
                e.ArticuloGenerado += EV_ArticuloGenerado;
            }
        }

        private void SearchElementos()
        {
            //foreach (Articulo a in articulos)
            //{
            //if (string.IsNullOrWhiteSpace(a.Codigo))
            //    {
            //        elementos.Add(new ElementoVirtual(a.Producto, a.Precio, a.TipoVenta));
            //    }
            //}
        }

        private void ActualizarStockLista(IEnumerable<Articulo> lista)
        {
            //DirectoryInfo dif = new DirectoryInfo(Settings.Default.Ruta_articulos);
            //dif.Create();
            List<Articulo> list = lista.ToList();
            //var artRaded = Archivos.LeerArchivo<Articulo>(Arch.Articulos).ToList();
            //if (artRaded.Count > 0) articulos = artRaded;
            foreach (Articulo item in lista)
            {
                int pos = articulos.FindIndex(x => (!string.IsNullOrWhiteSpace(x.Codigo) && x.Codigo.Equals(item.Codigo, StringComparison.CurrentCulture))
                              || x.Producto.Equals(item.Producto, StringComparison.CurrentCulture));
                if (pos != -1)
                {
                    Articulo art = articulos[pos];
                    art.Stock_Actual += item.Stock_Actual;
                    if (cliente != null && cliente.Connected)
                    {
                        cliente.Post(item.ToString());
                    }

                    list.Remove(item);
                }
            }
            list.ForEach((x) => articulos.Add(x));

            if (cliente == null || !cliente.Connected || server != null)
            {
                if (Archivos.ActualizarArchivo(Arch.Articulos, articulos, true, (x, y) => (!string.IsNullOrWhiteSpace(x.Codigo) && x.Codigo.Equals(y.Codigo, StringComparison.CurrentCulture))
                                               || x.Producto.Equals(y.Producto, StringComparison.CurrentCulture)) == 2)
                {
                    Archivos.SobreescribirArchivo(Arch.Articulos, Articulos);
                }
            }
        }

        private void ActualizarStock(Articulo item, bool post)
        {
            //var artRaded = Archivos.LeerArchivo<Articulo>(Arch.Articulos).ToList();
            //if (artRaded.Count > 0) articulos = artRaded;
            if (item == null)
            {
                return;
            }

            int pos = articulos.FindIndex(x => (!string.IsNullOrWhiteSpace(x.Codigo) && x.Codigo.Equals(item.Codigo, StringComparison.CurrentCulture))
                              || x.Producto.Equals(item.Producto, StringComparison.CurrentCulture));

            if (pos != -1)
            {
                if (item.Vencimientos != null)
                {
                    articulos[pos].AddStock(item.Vencimientos);
                }
                else if (item.Stock_Actual > 0)
                {
                    articulos[pos].AddStock(item.Stock_Actual);
                }
                else
                {
                    articulos[pos].Stock_Actual += item.Stock_Actual;
                }
                articulos[pos].Stock_Ideal = item.Stock_Ideal;
                articulos[pos].TipoVenta = item.TipoVenta;
                articulos[pos].Precio = item.Precio;
                articulos[pos].Proveedor = item.Proveedor;
                articulos[pos].Producto = item.Producto;
                articulos[pos].CantidadEnvases = item.CantidadEnvases;
                articulos[pos].IsRetornable = item.IsRetornable;
                if (articulos[pos].IsRetornable)
                {
                    articulos[pos].CantidadEnvases += item.CantidadEnvases - (int)item.Stock_Actual;
                }
            }
            else
            {
                articulos.Add(item);
            }

            if (cliente == null || !cliente.Connected || server != null)
            {
                if (Archivos.ActualizarArchivo(Arch.Articulos, articulos, false, (x, y) => (!string.IsNullOrWhiteSpace(x.Codigo) && x.Codigo.Equals(y.Codigo, StringComparison.CurrentCulture))
                                                   || x.Producto.Equals(y.Producto, StringComparison.CurrentCulture)) == 2)
                {
                    Archivos.SobreescribirArchivo(Arch.Articulos, articulos);
                }
            }
            if (cliente != null && cliente.Connected && post)
            {
                cliente.Post(item.ToString());
            }
        }

        private void PostAndSaveStock(Articulo item)
        {
            if (item == null)
            {
                return;
            }

            if (cliente == null || !cliente.Connected || server != null)
            {
                if (Archivos.ActualizarArchivo(Arch.Articulos, articulos, false, (x, y) => (!string.IsNullOrWhiteSpace(x.Codigo) && x.Codigo.Equals(y.Codigo, StringComparison.CurrentCulture))
                                                   || x.Producto.Equals(y.Producto, StringComparison.CurrentCulture)) == 2)
                {
                    Archivos.SobreescribirArchivo(Arch.Articulos, articulos);
                }
            }
            if (cliente != null && cliente.Connected)
            {
                cliente.Post(item.ToString());
            }
            RefreshStock();
        }
        private void RefreshStock()
        {
            articulos.ForEach(x => Articulos.Add(x));
            Data_Grid_Stock.ItemsSource = null;
            Data_Grid_Stock.ItemsSource = articulos;
        }

        private void AgregarVenta(Articulo_Venta articulo_Venta)
        {
            Articulo e = articulos.Find(x => x.Producto.Equals(articulo_Venta.Descripcion, StringComparison.CurrentCulture));
            string message = $"El stock del producto {e.Producto} es insuficiente";
            string caption = Str.Error;
            if (/*e != null && e.Stock_Actual - articulo_Venta.Cantidad >= 0*/true)
            {
                int ind = Ventas.FindIndex(x => x.Descripcion.Equals(articulo_Venta.Descripcion, StringComparison.CurrentCultureIgnoreCase));

                if (ind >= 0 && articulo_Venta.tipoVenta != TipoVenta.PorMonto)
                {
                    if (/*Ventas[ind].Cantidad + articulo_Venta.Cantidad <= e.Stock_Actual*/true)
                    {
                        Ventas[ind].Cantidad += articulo_Venta.Cantidad;
                    }
                    else
                    {
                        MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    Ventas.Add(articulo_Venta);
                }
            }
            else
            {
                MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Lista_Venta.ItemsSource = null;
            Lista_Venta.ItemsSource = Ventas;
            ActualizarTotal();
        }
        #endregion

        #region Filtros
        private void CollectionViewSource_Filter(object sender, System.Windows.Data.FilterEventArgs e)
        {
            Articulo a = e.Item as Articulo;
            if (a != null)
            {
                if (a.IsRetornable)
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }
        #endregion Filtros

        private void BtVincular_Click(object sender, RoutedEventArgs e)
        {
            VentanaVinculacion vinc = new VentanaVinculacion();
            vinc.IniciarServidor += IniciarServidor;
            vinc.ConectarCliente += ConectarCliente;
            vinc.Show();
        }

        private bool ConectarCliente(string ip)
        {
            cliente = new Cliente(nick);
            cliente.InfoRecibida += Cliente_InfoRecibida;
            btVincular.IsEnabled = false;
            bool b = cliente.Conectar(ip);
            if (b)
            {
                articulos.Clear();
                cliente.Post("cmdSynk");
                lblConection.Content = $"Conectado a {ip}";
                btVincular.IsEnabled = false;
            }
            return b;
        }

        private void Cliente_InfoRecibida(string s)
        {
            if (!string.IsNullOrWhiteSpace(s))
            {
                string[] nick = s.Split('þ');
                if (!nick[0].Equals(this.nick, StringComparison.OrdinalIgnoreCase))
                {
                    if (nick[1].Equals("cmdSynk", StringComparison.OrdinalIgnoreCase) && cliente.Nick.Equals("admPrn", StringComparison.Ordinal))
                    {
                        foreach (Articulo a in articulos)
                        {
                            cliente.Post(nick[0] + "~" + a.ToString());
                        }
                        cliente.Post("cmdSynkEnd");
                        return;
                    }
                    else if (nick[1].Equals("cmdSynkEnd", StringComparison.OrdinalIgnoreCase))
                    {
                        Data_Grid_Stock.Dispatcher.Invoke(new Action(RefreshStock));
                        return;
                    }
                    else if (nick[1].StartsWith(this.nick, StringComparison.OrdinalIgnoreCase))
                    {
                        string[] n = nick[1].Split('~');
                        Articulo articulo = Articulo.FromString(n[1]);

                        ActualizarStock(articulo, false);
                        Data_Grid_Stock.Dispatcher.Invoke(new Action(delegate () { Data_Grid_Stock.ItemsSource = null; }));

                        return;
                    }
                    if (nick.Length == 2)
                    {
                        Articulo art = Articulo.FromString(nick[1]);
                        ActualizarStock(art, false);
                        Data_Grid_Stock.Dispatcher.Invoke(new Action(RefreshStock));
                    }
                }
            }
        }

        private void IniciarServidor()
        {
            server = new Server();
            lblConection.Content = $"Servidor abierto en {server.ipendpoint.Address}";
            nick = "admPrn";
            cliente = new Cliente(nick);
            cliente.Conectar(server.ipendpoint.Address.ToString());
            cliente.InfoRecibida += Cliente_InfoRecibida;
            btVincular.IsEnabled = false;

        }

        #region Server-Cliente

        #endregion Server-Cliente

        private void Data_Grid_Stock_KeyUp(object sender, KeyEventArgs e)
        {
            if (isEditingStock && e.Key == Key.Enter && 0 <= indexEditing && indexEditing < Data_Grid_Stock.Items.Count)
            {
                Articulo a = Data_Grid_Stock.Items[indexEditing] as Articulo;
                a.Vencimientos = null;
                a.Stock_Actual = 0;
                ActualizarStock(a, true);
                indexEditing = -1;
                isEditingStock = false;
            }
            
    }

        private int indexEditing = -1;
        private bool isEditingStock = false;
        private void Data_Grid_Stock_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            indexEditing = Data_Grid_Stock.SelectedIndex;
            isEditingStock = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VentanaBackup backup = new VentanaBackup();
            backup.CargarCopia += Backup_CargarCopia;
            backup.GuardarCopia += Backup_GuardarCopia;
            backup.Show();
        }

        private void Backup_GuardarCopia(string path)
        {
            Archivos.DoBackup(articulos);
        }

        private void Backup_CargarCopia(string path)
        {
            articulos = Archivos.LeerArchivoRutaCompleta<Articulo>(path).ToList();
            if (cliente != null)
            {
                foreach (Articulo item in articulos)
                {
                    cliente.Post(item.ToString());
                }
            }
            RefreshStock();
        }
    }

    [Serializable]
    public class Caja
    {

        #region Props Caja
        public decimal FCEntregado { get; set; }
        public decimal FCRecibido { get; set; }
        public decimal EfectivoRecibido { get; set; }
        public decimal DebitoRecibido { get; set; }
        public decimal Depositos { get; set; }
        public decimal Extracciones { get; set; }
        public decimal PagoProveedores { get; set; }
        public decimal RecaudacionTotal { get; set; }
        public decimal Sueldos { get; set; }
        #endregion Props Caja

    }
    public class ArticulosCollection : ObservableCollection<Articulo>
    {

    }
}
