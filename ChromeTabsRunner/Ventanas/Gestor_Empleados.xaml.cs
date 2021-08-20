using Business_Layer;
using Business_Layer.Text;
using Data_Layer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Arch = SupComercio.Resources.Strings.Archivos;
using Res = SupComercio.Properties.Resources;
using Str = SupComercio.Resources.Strings.Mensajes;

namespace SupComercio.Ventanas
{
    public class EmpleadoEditadoArgs
    {
        public Empleado ValorAnterior { get; set; }
        public Empleado NuevoValor { get; set; }

        public EmpleadoEditadoArgs(Empleado anterior, Empleado nuevo)
        {
            ValorAnterior = anterior;
            NuevoValor = nuevo;
        }
    }

    public class SueldoPagadoArgs
    {
        public Empleado Empleado { get; set; }
        public decimal Sueldo => Empleado.Sueldo;
    }
    /// <summary>
    /// Lógica de interacción para GestorEmpleados.xaml
    /// </summary>
    [Serializable]
    public partial class GestorEmpleados : Window, INotifyPropertyChanged
    {
        #region Delegados y Eventos
        public delegate void DelegadoEmpleadoGenerado(Empleado empleado);
        public event DelegadoEmpleadoGenerado EmpleadoGenerado;


        public delegate void DelegadoEmpleadoEditado(EmpleadoEditadoArgs empleado);
        public event DelegadoEmpleadoEditado EmpleadoEditado;

        public delegate void DelegadoEmpleadoDespedido(Empleado empleado);
        public event DelegadoEmpleadoDespedido EmpleadoDespedido;

        public delegate void DelegadoSueldoPagado(SueldoPagadoArgs e);
        public event DelegadoSueldoPagado SueldoPagado;
        #endregion

        #region Campos Privados
        private bool isEditing = false;
        [NonSerialized]
        private Bitmap _imagen;
        private readonly List<EnableToggleText> enableToggles = new List<EnableToggleText>();
        private readonly Usuario usuario = null;

        private string _nombre;
        private string _apellido;
        private string _cuil;
        private string _telefono;
        private string _celular;
        private string _email;
        private string _domicilio;
        private string _cargo;
        private string _precioHora;
        private string _sueldo;
        private TimeSpan _horasTrabajadas;
        private StructHorario _horario;
        private TimeSpan _max;
        private TimeSpan _min;
        private ObservableCollection<Empleado> _listaEmpleados;
        private DateTime UltimaActividad = DateTime.Now;
        private List<Vale> _vales;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Las propiedades de colección deben ser de solo lectura", Justification = "<pendiente>")]
        public ObservableCollection<Empleado> ListaEmpleados
        {
            get => _listaEmpleados;
            set
            {
                _listaEmpleados = value;
                OnPropertyChanged(nameof(ListaEmpleados));
            }
        }
        #endregion

        #region Propiedades
        public Empleado Empleado => new Empleado()
        {
            Apellido = Apellido,
            Nombre = Nombre,
            Cargo = Cargo,
            Cuil = Cuil,
            Horario = this.Horario,
            ColorEnPlanilla = null
        };
        public Bitmap Imagen
        {
            get => _imagen;
            set
            {
                _imagen = value;
                if (value != null)
                {
                    BFoto.Visibility = Visibility.Visible;
                    SpSeleccion.Visibility = Visibility.Collapsed;
                    WebCam.Visibility = Visibility.Collapsed;
                }
                else
                {
                    BFoto.Visibility = Visibility.Collapsed;
                    SpSeleccion.Visibility = Visibility.Visible;
                    WebCam.Visibility = Visibility.Collapsed;
                }
                OnPropertyChanged(nameof(Imagen));
            }
        }

        public List<Vale> Vales
        {
            get => _vales;
            set
            {
                _vales = value;
                OnPropertyChanged(nameof(Vales));
            }
        }

        private TimeSpan Maximum
        {
            get => _max;
            set
            {
                _max = value;
                OnPropertyChanged(nameof(Maximum));
            }
        }
        private TimeSpan Minimum
        {
            get => _min;
            set
            {
                _min = value;
                OnPropertyChanged(nameof(Minimum));
            }
        }
        public string Nombre
        {
            get => _nombre;
            set
            {
                _nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }
        public string Apellido
        {
            get => _apellido;
            set
            {
                _apellido = value;
                OnPropertyChanged(nameof(Apellido));
            }
        }
        public string Cuil
        {
            get => _cuil;
            set
            {
                _cuil = value;
                OnPropertyChanged(nameof(Cuil));
            }
        }
        public string Telefono
        {
            get => _telefono;
            set
            {
                _telefono = value;
                OnPropertyChanged(nameof(Telefono));
            }
        }
        public string Celular
        {
            get => _celular;
            set
            {
                _celular = value;
                OnPropertyChanged(nameof(Celular));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Domicilio
        {
            get => _domicilio;
            set
            {
                _domicilio = value;
                OnPropertyChanged(nameof(Domicilio));
            }
        }
        public string Cargo
        {
            get => _cargo;
            set
            {
                _cargo = value;
                OnPropertyChanged(nameof(Cargo));
            }
        }
        public string PrecioHora
        {
            get => string.Format(CultureInfo.CurrentCulture, "{0:#.00}", _precioHora);
            set
            {
                _precioHora = value;
                OnPropertyChanged(nameof(PrecioHora));
            }
        }
        public string Sueldo
        {
            get => _sueldo;
            set
            {
                _sueldo = value;
                OnPropertyChanged(nameof(Sueldo));
            }
        }
        public TimeSpan HorasTrabajadas
        {
            get => _horasTrabajadas;
            set
            {
                _horasTrabajadas = value;
                OnPropertyChanged(nameof(HorasTrabajadas));
            }
        }
        public StructHorario Horario
        {
            get => _horario;
            set
            {
                if (IsLoaded)
                {
                    Horario.PropertyChanged += Horario_PropertyChanged;
                }

                _horario = value;
                OnPropertyChanged(nameof(Horario));
            }
        }
        #endregion

        #region Constructores
        public GestorEmpleados(Window owner)
        {
            Owner = owner;
            InitializeComponent();
            WebCam.SnapshotTaken += WebCam_SnapshotTaken;
            WebCam.IsSnapshotDeleted += WebCam_IsSnapshotDeleted;

            Horario = new StructHorario(nameof(Horario));

            InicializarCajaDeTexto();
            if (Owner == null || (Owner as MainWindow).Empleados == null)
            {
                ListaEmpleados = Archivos.LeerArchivo<Empleado>(Arch.Empleado, x => x.IsActive && x.IsShowed).ToObservableCollection();
            }
            else
            {
                ListaEmpleados = (Owner as MainWindow).Empleados.ToObservableCollection();
            }
            DGEmpleados.ItemsSource = ListaEmpleados;
            ListaEmpleados.CollectionChanged += ListaEmpleados_CollectionChanged;
            Loaded += GestorEmpleados_Loaded;

            ctrlVentana.MaximizeWindow();
        }


        public void ActualizarLista()
        {
            var i = DGEmpleados.SelectedIndex;
            DGEmpleados.ItemsSource = null;
            DGEmpleados.ItemsSource = ListaEmpleados;
            DGEmpleados.SelectedIndex = i;
        }
        private void GestorEmpleados_Loaded(object sender, RoutedEventArgs e)
        {
            Maximum = new TimeSpan(23, 59, 59);
            Minimum = new TimeSpan(0, 0, 0);
            if (string.IsNullOrWhiteSpace(tpEntrada.Text))
            {
                tpEntrada.Text = tpEntrada.DefaultValue.ToString();
            }

            if (string.IsNullOrWhiteSpace(tpSalida.Text))
            {
                tpSalida.Text = tpSalida.DefaultValue.ToString();
            }

            tpEntrada.DisplayDefaultValueOnEmptyText = true;
            tpSalida.DisplayDefaultValueOnEmptyText = true;
        }

        private void ListaEmpleados_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //DGEmpleados.ItemsSource = null;
            //DGEmpleados.ItemsSource = ListaEmpleados;
        }

        private void InicializarCajaDeTexto()
        {
            if (enableToggles.Count == 0)
            {
                enableToggles.Add(new EnableToggleText(txtDomicilio, new string[2] { "", Res.Error_Domicilio }));
                enableToggles.Add(new EnableToggleText(txtApellido, new string[2] { "", Res.Error_Apellido }, TextMode.Alphbetic));
                enableToggles.Add(new EnableToggleText(txtCelular, new string[2] { "", Res.Error_Celular }, TextMode.Numeric));
                enableToggles.Add(new EnableToggleText(txtCuil, new string[2] { "", Res.Error_Cuil }, TextMode.Numeric));
                enableToggles.Add(new EnableToggleText(txtEmail, new string[2] { "", Res.Error_Email }));
                enableToggles.Add(new EnableToggleText(txtTelefono, new string[2] { "", Res.Error_Telefono }, TextMode.Numeric));
                enableToggles.Add(new EnableToggleText(txtNombre, new string[2] { "", Res.Error_Nombre_Vacio }, TextMode.Alphbetic));
                enableToggles.Add(new EnableToggleText(txtCargo, new string[2] { "", Res.Error_Cargo_Vacio }));
                enableToggles.Add(new EnableToggleText(txtPrecioHora, "", TextMode.Money));
            }
            foreach (EnableToggleText enableToggle in enableToggles)
            {
                enableToggle.InicializarTexto();
            }
        }

        #endregion

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

        #endregion

        #region Eventos GUI

        #region Externos
        private void WebCam_IsSnapshotDeleted()
        {
            Imagen = null;
        }

        private void WebCam_SnapshotTaken(BitmapImage snap)
        {
            Imagen = snap.ToBitmap();
            //Foto.Source = Imagen.ToBitmapImage();
        }

        #endregion Externos

        #region Generales
        private void Window_StateChanged(object sender, System.EventArgs e)
        {
            ctrlVentana.State = WindowState;
        }

        //public enum Dias { Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo }
        private void btLimpiarHorario_Click(object sender, RoutedEventArgs e)
        {
            Horario.Clear((Dias)cbLimpiarHorario.SelectedIndex);
            //OnPropertyChanged(nameof(Horario));
        }

        private void Horario_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Planilla.Horario = Horario;
            PlanillaInfo.Horario = Horario;
        }
        #endregion Generales


        #region Eventos Listado Empleados

        private void BtEditar_Click(object sender, RoutedEventArgs e)
        {
            isEditing = true;
            if (DGEmpleados.SelectedItem is Empleado emp)
            {
                SyncPropEmpleado(emp);
            }
            GdListadoEmpleados.Visibility = Visibility.Collapsed;
            GdNuevoEmpleado.Visibility = Visibility.Visible;
        }

        private void BtDespedir_Click(object sender, RoutedEventArgs e)
        {
            EmpleadoDespedido?.Invoke(ListaEmpleados[DGEmpleados.SelectedIndex]);
            ListaEmpleados.RemoveAt(DGEmpleados.SelectedIndex);
            DGEmpleados.ItemsSource = null;
            DGEmpleados.ItemsSource = ListaEmpleados;
        }

        private void DGEmpleados_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach(Empleado empleado in e.RemovedItems)
            {
                empleado.PropertyChanged -= Emp_PropertyChanged;
            }

            if (DGEmpleados.SelectedItem is Empleado emp)
            {
                (DGEmpleados.SelectedItem as Empleado).PropertyChanged += Emp_PropertyChanged;
                var a = Horario;
                SyncPropEmpleado(emp);
                PlanillaInfo.Horario = emp.Horario;
            }
        }

        private void Emp_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SyncPropEmpleado(sender as Empleado);
        }

        private void ButtonCrear_Click(object sender, RoutedEventArgs e)
        {
            isEditing = false;
            GdListadoEmpleados.Visibility = Visibility.Collapsed;
            GdNuevoEmpleado.Visibility = Visibility.Visible;
            DGEmpleados.SelectedIndex = -1;
            DGEmpleados.SelectedItem = null;
            InitProp();
        }
        #endregion Eventos Listado Empleados



        #region Eventos Registro Empleados
        private void BtAgregarHorario_Click(object sender, RoutedEventArgs e)
        {
            Lapso l = new Lapso();
            TimeSpan tpe = tpEntrada.Value.Value;
            TimeSpan tps = tpSalida.Value.Value;
            TimeSpan finDia = new TimeSpan(23, 59, 59);

            l.Inicio = tpe;
            int diaSalida = cbDiaSalida.SelectedIndex;
            int diaEntrada = cbDiaEntrada.SelectedIndex;
            string messageBoxText = $"Se asumirá que el dia de salida es igual al de entrada {cbDiaEntrada.SelectedItem}\n¿Desea continuar?";


            if (diaEntrada == -1)
            {
                MessageBox.Show("Debe seleccionar el dia de entrada");
                return;
            }

            if (diaSalida == -1)
            {
                if (MessageBox.Show(messageBoxText, Str.Advertencia, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                    diaSalida = diaEntrada;
                }
            }

            if (diaEntrada == diaSalida)
            {
                l.Fin = tps;
                Horario.Add(Horario.HorarioCompleto[diaEntrada], l);
            }
            else
            {
                l.Fin = finDia;
                Horario.Add(Horario.HorarioCompleto[diaEntrada], l);
                diaEntrada++;
                if (diaEntrada > 6) diaEntrada = 0;
                while (diaEntrada != diaSalida)
                {
                    l.Inicio = TimeSpan.Zero;
                    l.Fin = finDia;
                    Horario.Add(Horario.HorarioCompleto[diaEntrada], l);
                    diaEntrada++;
                    if (diaEntrada > 6)
                    {
                        diaEntrada = 0;
                    }
                }
                l.Inicio = TimeSpan.Zero;
                l.Fin = tps;
                Horario.Add(Horario.HorarioCompleto[diaEntrada], l);
            }
        }

        private void BtAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarEmpleado())
            {
                Empleado emp = new Empleado()
                {
                    Apellido = Apellido,
                    Cuil = Cuil,
                    Cargo = Cargo,
                    Domicilio = Domicilio,
                    Nombre = Nombre,
                    FotoPerfil = Imagen,
                    Precio_Hora = Convert.ToDecimal(PrecioHora.Trim('$', '¤', '€'), CultureInfo.CurrentCulture),
                    //Horario = new StructHorario()
                    //{
                    //    Lunes = Horario.Lunes,
                    //    Martes = Horario.Martes,
                    //    Miercoles = Horario.Miercoles,
                    //    Jueves = Horario.Jueves,
                    //    Viernes = Horario.Viernes,
                    //    Sabado = Horario.Sabado,
                    //    Domingo = Horario.Domingo
                    //},
                    DatosContacto = new Empleado.Contacto()
                    {
                        Celular = Celular,
                        Email = Email,
                        Telefono_fijo = Telefono
                    },
                    UsuarioAsociado = usuario
                };
                Horario.Lunes.ForEach((x) => emp.Horario.Lunes.Add(x));
                Horario.Martes.ForEach((x) => emp.Horario.Martes.Add(x));
                Horario.Miercoles.ForEach((x) => emp.Horario.Miercoles.Add(x));
                Horario.Jueves.ForEach((x) => emp.Horario.Jueves.Add(x));
                Horario.Viernes.ForEach((x) => emp.Horario.Viernes.Add(x));
                Horario.Sabado.ForEach((x) => emp.Horario.Sabado.Add(x));
                Horario.Domingo.ForEach((x) => emp.Horario.Domingo.Add(x));
                if (!isEditing)
                {
                    EmpleadoGenerado?.Invoke(emp);
                    ListaEmpleados.Add(emp);
                    ActualizarLista();
                }
                else
                {
                    Empleado ant = ListaEmpleados[DGEmpleados.SelectedIndex];
                    ListaEmpleados[DGEmpleados.SelectedIndex] = emp;
                    EmpleadoEditado?.Invoke(new EmpleadoEditadoArgs(ant, emp));
                }
                DGEmpleados.ItemsSource = null;
                DGEmpleados.ItemsSource = ListaEmpleados;
                InicializarCajaDeTexto();
                GdNuevoEmpleado.Visibility = Visibility.Collapsed;
                GdListadoEmpleados.Visibility = Visibility.Visible;
                InitProp();
            }
        }

        private void BtCancelar_Click(object sender, RoutedEventArgs e)
        {
            GdNuevoEmpleado.Visibility = Visibility.Collapsed;
            GdListadoEmpleados.Visibility = Visibility.Visible;
            InitProp();
        }

        private void BtSacarFoto_Click(object sender, RoutedEventArgs e)
        {
            SpSeleccion.Visibility = Visibility.Collapsed;
            WebCam.Visibility = Visibility.Visible;
        }

        private void BtDesdeArchivo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                CheckFileExists = true,
                Filter = "Archivos de imágenes (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png|Todos los archivos (*.*)|*.*"
            };
            bool? res = dialog.ShowDialog();
            if (res != true)
            {
                return;
            }

            //Image img = Image.FromFile(dialog.FileName);
            //Imagen = img.Convert();
            Imagen = new Bitmap(dialog.FileName);
        }

        private void BtDescartar_Click(object sender, RoutedEventArgs e)
        {
            Imagen = null;
            WebCam.Inicializar();
        }

        #endregion

        #endregion

        #region Funciones
        private void SyncPropEmpleado(Empleado emp)
        {
            //emp.PropertyChanged += Emp_PropertyChanged;
            Apellido = emp.Apellido;
            
            Cuil = emp.Cuil;
            Domicilio = emp.Domicilio;
            HorasTrabajadas = emp.HorasTrabajadas;
            Nombre = emp.Nombre;
            PrecioHora = emp.Precio_Hora.ToString("0.00", CultureInfo.InvariantCulture)  /*string.Format(CultureInfo.CurrentCulture, "{0:F}", emp.Precio_Hora.ToString(CultureInfo.CurrentCulture))*/;
            Sueldo = emp.Sueldo.ToString("0.00", CultureInfo.CurrentCulture);
            for (int i = 0; i < emp.Horario.HorarioCompleto.Count; i++)
            {
                emp.Horario.HorarioCompleto[i].ForEach((x) => Horario.HorarioCompleto[i].Add(x));
            }
            Imagen = emp.FotoPerfil;
            Vales = emp.Vales;

            if (emp.DatosContacto != null)
            {
                Celular = emp.DatosContacto.Celular;
                Email = emp.DatosContacto.Email;
                Telefono = emp.DatosContacto.Telefono_fijo;
            }

            OnPropertyChanged(nameof(Horario));
        }

        private void InitProp()
        {
            Nombre = Apellido = Cuil = Telefono = string.Empty;
            Celular = Email = Domicilio = Cargo = string.Empty;
            PrecioHora = Sueldo = "00.00";
            Imagen = null;
            HorasTrabajadas = new TimeSpan(0);
            if (Horario != null)
            {
                Horario.PropertyChanged += Horario_PropertyChanged;
                Horario.Clear();
                return;
            }
            else
            {
                Horario = new StructHorario();
                Horario.PropertyChanged += Horario_PropertyChanged;
            }
        }

        private bool ValidarEmpleado()
        {
            bool flag = true;
            SolidColorBrush red = new SolidColorBrush(Colors.Red);
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                txtNombre.Text = Res.Error_Nombre_Vacio;
                txtNombre.Foreground = red;
                flag = false;
            }
            if (string.IsNullOrWhiteSpace(Apellido))
            {
                txtApellido.Text = Res.Error_Apellido;
                txtApellido.Foreground = red;
                flag = false;
            }
            if (string.IsNullOrWhiteSpace(Cuil))
            {
                txtCuil.Text = Res.Error_Cuil;
                txtCuil.Foreground = red;
                flag = false;
            }
            if (string.IsNullOrWhiteSpace(Domicilio))
            {
                txtDomicilio.Text = Res.Error_Domicilio;
                txtDomicilio.Foreground = red;
                flag = false;
            }
            if (string.IsNullOrWhiteSpace(Cargo))
            {
                txtCargo.Text = Res.Error_Cargo_Vacio;
                txtCargo.Foreground = red;
            }
            return flag;
        }

        #endregion

        private void BtPagarSueldo_Click(object sender, RoutedEventArgs e)
        {
            if (DGEmpleados.SelectedItem is Empleado emp)
            {
                SueldoPagado?.Invoke(new SueldoPagadoArgs()
                {
                    Empleado = emp
                });
                emp.HorasTrabajadas = TimeSpan.Zero;
                emp.Vales.Clear();
                SyncPropEmpleado(new Empleado());
                DGEmpleados.ItemsSource = null;
                DGEmpleados.ItemsSource = ListaEmpleados;
                DGEmpleados.SelectedIndex = -1;
            }
        }

        private void IntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PlanillaInfo.IntervaloHoras = (int)e.NewValue;
        }

        private void window_LostFocus(object sender, RoutedEventArgs e)
        {
            UltimaActividad = DateTime.Now;
        }

        private void window_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UltimaActividad.AddMinutes(1) < DateTime.Now)
            {
                ListaEmpleados = Archivos.LeerArchivo<Empleado>(Arch.Empleado, x => x.IsActive && x.IsShowed).ToObservableCollection();
                DGEmpleados.ItemsSource = null;
                DGEmpleados.ItemsSource = ListaEmpleados;
            }
        }

    }
}
