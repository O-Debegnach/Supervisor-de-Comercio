using Business_Layer;
using Data_Layer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Arch = SupComercio.Resources.Strings.Archivos;
using EnableToggle = Business_Layer.EnableToggleText;
using Res = SupComercio.Properties.Resources;
using Str = SupComercio.Resources.Strings.Mensajes;

namespace SupComercio.Ventanas
{
    /// <summary>
    /// Argumento del evento UsuarioCreado
    /// </summary>
    public class ArgumentoUsuarioCreado
    {

        public Usuario Usuario { get; set; }
        public Empleado EmpleadoAsociado { get; set; }
        public Empleado EmpleadoConUsuario
        {
            get
            {
                Empleado e = EmpleadoAsociado;
                e.UsuarioAsociado = Usuario;
                return e;
            }
        }
        public ArgumentoUsuarioCreado()
        {

        }
    }

    /// <summary>
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class VentanaUsuarios : Window
    {
        #region Propiedades
        public List<Empleado> Empleados { get; }
        public string Contraseña { get; set; }
        public string ConfContraseña { get; set; }
        public string Usuario { get; set; }
        public bool Administrador { get; set; } = false;
        public Bitmap Foto { get; set; }
        #endregion

        #region Campos Privados
        private readonly List<EnableToggle> enableToggles = new List<EnableToggle>();
        private readonly Empleado admin;
        private bool errorEmpleado = false;
        #endregion

        #region Delegados y Eventos

        #region Argumentos
        
        #endregion

        public delegate void DelegadoUsuarioCreado(ArgumentoUsuarioCreado e);
        public event DelegadoUsuarioCreado UsuarioCreado;
        #endregion
        public VentanaUsuarios(List<Empleado> empleados)
        {
            if (empleados == null) throw new ArgumentNullException(nameof(empleados), Str.ArgumentNullException);
            Empleados = empleados;
            InitializeComponent();

            //Empleados = Archivos.LeerArchivo<Empleado>(Arch.Empleado, x => x.IsActive).ToList();
            int pos = Empleados.FindIndex(x => x!=null && !x.IsShowed);
            if (pos != -1)
            {
                admin = Empleados[pos];
                Empleados.RemoveAt(pos);
            }
            cbEmpleados.ItemsSource = Empleados;
            InicializarTextos();
        }

        #region Eventos
        private void BtAceptar_Click(object sender, RoutedEventArgs e)
        {
            Focus();
            if (VerificarUsuario())
            {
                Empleado em = Empleados[cbEmpleados.SelectedIndex];
                Usuario usr = new Usuario()
                {
                    Nombre = Usuario,
                    Contraseña = txtContraseña.Password,
                    Administrador = Administrador
                };

                ArgumentoUsuarioCreado a = new ArgumentoUsuarioCreado()
                {
                    EmpleadoAsociado = em,
                    Usuario = usr
                };

                Empleados[cbEmpleados.SelectedIndex].UsuarioAsociado = usr;

                UsuarioCreado?.Invoke(a);

                InicializarTextos();
                cbEmpleados.SelectedIndex = -1;
                Close();
            }
        }

        private void CbEmpleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbEmpleados.SelectedIndex >= 0 && Empleados[cbEmpleados.SelectedIndex].FotoPerfil != null)
            {
                imgFoto.Source = Empleados[cbEmpleados.SelectedIndex].FotoPerfil.ToBitmapImage();
            }
        }

        private void CbEmpleados_DropDownOpened(object sender, System.EventArgs e)
        {
            if (errorEmpleado)
            {
                Empleados.RemoveAt(0);
                cbEmpleados.Foreground = new SolidColorBrush(Colors.Black);
                cbEmpleados.SelectedIndex = -1;
                errorEmpleado = false;
            }
        }

        private void LblErrorPswr2_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Label lbl = sender as Label;
            lbl.Visibility = Visibility.Collapsed;
            if (string.Equals(lbl.Name, lblErrorPswr2.Name, System.StringComparison.InvariantCulture))
            {
                txtConfirmarContraseña.Focus();
            }
            else if (string.Equals(lbl.Name, lblErrorPswrAdmin.Name, System.StringComparison.InvariantCulture))
            {
                txtContraseñaAdmin.Focus();
            }
            else
            {
                txtContraseña.Focus();
            }
        }


        private void TxtContraseña_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pswrd = sender as PasswordBox;
            if (string.Equals(pswrd.Name, txtContraseña.Name, System.StringComparison.InvariantCulture))
            {
                lblErrorPswr.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblErrorPswr2.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Funciones
        private bool VerificarUsuario()
        {
            SolidColorBrush red = new SolidColorBrush(Colors.Red);
            bool flag = true;
            if (!txtContraseñaAdmin.Password.Equals(admin.UsuarioAsociado.Contraseña, System.StringComparison.CurrentCulture))
            {
                lblErrorPswrAdmin.Visibility = Visibility.Visible;
            }
            if (cbEmpleados.SelectedIndex == -1)
            {
                flag = false;
                Empleados.Insert(0, new Empleado() { Nombre = "Seleccione un empleado" });
                //cbEmpleados.Text = "Seleccione un empleado";
                cbEmpleados.Foreground = red;
                cbEmpleados.SelectedIndex = 0;
                errorEmpleado = true;
            }
            if (Empleados.FindIndex(x =>
            {
                Usuario usuarioAsociado = x.UsuarioAsociado;
                return usuarioAsociado != null ? string.Equals(x.UsuarioAsociado.Nombre, Usuario, System.StringComparison.CurrentCulture) : false;
            }) != -1)
            {
                flag = false;
                txtUsuario.Foreground = red;
                txtUsuario.Text = Res.Error_Usuario_Repetido;
            }
            if (string.IsNullOrWhiteSpace(Usuario))
            {
                flag = false;
                txtUsuario.Foreground = red;
                txtUsuario.Text = Res.Error_Nombre_Vacio;
            }
            if (string.IsNullOrWhiteSpace(txtContraseña.Password))
            {
                flag = false;
                lblErrorPswr.Content = Res.Error_Contraseña_Invalida;
                txtContraseña.Password = string.Empty;
                lblErrorPswr.Visibility = Visibility.Visible;
            }
            if (!string.Equals(txtContraseña.Password,
                             txtConfirmarContraseña.Password,
                             System.StringComparison.Ordinal))
            {
                flag = false;
                lblErrorPswr2.Content = Res.Error_Contraseña_No_Coincide;
                txtConfirmarContraseña.Password = string.Empty;
                lblErrorPswr2.Visibility = Visibility.Visible;
            }
            if (Empleados[cbEmpleados.SelectedIndex].UsuarioAsociado != null)
            {
                MessageBox.Show("El empleado ya cuenta con un usuario.\n¿Desea editarlo?", Str.Advertencia, MessageBoxButton.YesNo, MessageBoxImage.Question);
            }
            return flag;
        }

        private void InicializarTextos()
        {
            if (enableToggles.Count == 0)
            {
                enableToggles.Add(new EnableToggle(txtUsuario, new string[3] { "", Res.Error_Nombre_Vacio, Res.Error_Usuario_Repetido }));
            }
            else
            {
                enableToggles.ForEach(x => x.InicializarTexto());
                txtConfirmarContraseña.Password = txtContraseña.Password = string.Empty;
            }
        }
        #endregion

    }
}
