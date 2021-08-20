using Business_Layer;
using Data_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using ResException = SupComercio.Resources.Strings.Mensajes;


namespace SupComercio.Ventanas
{
    /// <summary>
    /// Lógica de interacción para ContraseñaAdmin.xaml
    /// </summary>
    public partial class ContraseñaAdmin : Window, INotifyPropertyChanged
    {
        #region Delegados y eventos
        public delegate void DelegadoConfirmado(bool isConfirmed);
        public event DelegadoConfirmado Confirmado;
        #endregion

        private bool? _isValid = null;

        public event PropertyChangedEventHandler PropertyChanged;

        private List<Empleado> ListaEmpleados { get; } = new List<Empleado>();
        public bool? IsValid
        {
            get { return _isValid; }
            set
            {
                _isValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid)));
            }
        }


        public ContraseñaAdmin(IEnumerable<Empleado> empleados)
        {
            if (empleados is null)
            {
                throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, ResException.ArgumentNullException, nameof(empleados)));
            }

            InitializeComponent();
            foreach (Empleado empleado in empleados)
            {
                if (empleado.UsuarioAsociado != null && (empleado.UsuarioAsociado.Administrador || !empleado.IsShowed))
                {
                    ListaEmpleados.Add(empleado);
                }
            }
            //if (ListaEmpleados.Count == 0) ListaEmpleados = Archivos.LeerArchivo<Empleado>(SupComercio.Resources.Strings.Archivos.Empleado, x => x.UsuarioAsociado != null && (x.UsuarioAsociado.Administrador || !x.IsShowed)).ToList();
            cbAdmins.ItemsSource = ListaEmpleados;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListaEmpleados)));
        }

        private void Validar()
        {
            if (cbAdmins.SelectedItem is Empleado e)
            {
                if (pswrd.Password.Equals(e.UsuarioAsociado.Contraseña, StringComparison.Ordinal))
                {
                    IsValid = true;
                }
                else IsValid = false;
            }
        }

        private void BtAceptar_Click(object sender, RoutedEventArgs e)
        {
            Validar();
            if (IsValid.HasValue && IsValid.Value)
            {
                Confirmado?.Invoke(true);
                Close();
            }
        }

        private void BtCancelar_Click(object sender, RoutedEventArgs e)
        {
            Confirmado?.Invoke(false);
            Close();
        }
    }
}
