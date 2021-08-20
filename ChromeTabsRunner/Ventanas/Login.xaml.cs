using Business_Layer;
using Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Arch = SupComercio.Resources.Strings.Archivos;
using EnableToggle = Business_Layer.EnableToggleText;
using Res = SupComercio.Properties.Resources;

namespace SupComercio.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    /// 

    public class LoguedEventArgs
    {
        public Empleado Empleado { get; }
        public bool Logueado { get; }

        public LoguedEventArgs(bool logued, Empleado empleado)
        {
            Empleado = empleado;
            Logueado = logued;
        }
    }

    public class UserRegisteredEventArgs
    {
        public Usuario Usuario { get; set; }
        public Empleado Empleado { get; set; }

        public UserRegisteredEventArgs() { }
    }

    public partial class Login : Window
    {
        #region Delegados y Eventos
        public delegate void DelegadoLogueado(LoguedEventArgs e);
        public event DelegadoLogueado Logueado;

        public delegate void DelegadoUsuarioRegistrado(UserRegisteredEventArgs e);
        public event DelegadoUsuarioRegistrado UsuarioRegistrado;
        #endregion

        #region Campos Privados
        private bool logueado = false;
        private Empleado empleado = null;
        private bool isInvoked = false;
        //private readonly EnableToggle enableToggleUsuario;
        #endregion

        #region Propiedades
        private List<Empleado> ListaEmpleados { get; set; }

        public string Usuario { get; set; }
        public string Contraseña => txtRegContraseña.Password;
        #endregion

        #region Constructores
        public Login()
        {
            bool flag = false;
            InitializeComponent();

            _ = new EnableToggle(txtNuevoUsuario, new string[3] { "", Res.Error_Nombre_Repetido, Res.Error_Nombre_Vacio });

            ListaEmpleados = Archivos.LeerArchivo<Empleado>(Arch.Empleado, x => x.UsuarioAsociado != null).ToList();
            ListaEmpleados.ForEach((x) =>
            {
                if (x.UsuarioAsociado != null)
                {
                    flag = true;
                }
                else
                {
                    ListaEmpleados.Remove(x);
                }
            });
            if (!flag)
            {
                gdLogin.Visibility = Visibility.Collapsed;
                gdRegistro.Visibility = Visibility.Visible;
            }
            combo_Empleados.ItemsSource = ListaEmpleados;
            txtContraseña.Focus();
        }

        #endregion

        #region Eventos
        private void Combo_Empleados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Imagen_Usuario.Source = ListaEmpleados[combo_Empleados.SelectedIndex].FotoPerfil.ToBitmapImage();
            }
            catch (Exception) { }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isInvoked)
            {
                Logueado?.Invoke(new LoguedEventArgs(logueado, empleado));
            }
        }

        #endregion

        #region Funciones
        private bool Verificar()
        {
            bool flag = true;
            Focus();
            if (!string.Equals(ListaEmpleados[combo_Empleados.SelectedIndex].UsuarioAsociado.Contraseña, txtContraseña.Password, StringComparison.CurrentCulture))
            {
                flag = false;
                lblError.Visibility = Visibility.Visible;
                txtContraseña.Password = string.Empty;
            }
            return flag;
        }

        private bool VerificarRegistro()
        {
            bool flag = true;
            Focus();
            if (string.IsNullOrWhiteSpace(Usuario))
            {
                flag = false;
                txtNuevoUsuario.Text = Res.Error_Nombre_Vacio;
            }
            if (string.IsNullOrWhiteSpace(Contraseña))
            {
                flag = false;
                txtRegContraseña.Clear();
                lblRegError.Visibility = Visibility.Visible;
            }
            if (!Contraseña.Equals(txtRegConfContraseña.Password, StringComparison.CurrentCulture))
            {
                flag = false;
                txtRegConfContraseña.Clear();
                lblRegError1.Visibility = Visibility.Visible;
            }
            return flag;
        }

        #endregion

        private void LblError_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label s = sender as Label;
            if (s.Name.Equals(lblError.Name, StringComparison.Ordinal))
            {
                lblError.Visibility = Visibility.Collapsed;
                txtContraseña.Focus();
            }
            else if (s.Name.Equals(lblRegError.Name, StringComparison.Ordinal))
            {
                lblRegError.Visibility = Visibility.Collapsed;
                txtRegContraseña.Focus();
            }
            else if (s.Name.Equals(lblRegError1.Name, StringComparison.Ordinal))
            {
                lblRegError1.Visibility = Visibility.Collapsed;
                txtRegConfContraseña.Focus();
            }
        }

        private void Boton_Aceptar_Login_Click(object sender, RoutedEventArgs e)
        {
            Focus();
            if (gdLogin.Visibility == Visibility.Visible && Verificar())
            {
                logueado = true;
                empleado = ListaEmpleados[combo_Empleados.SelectedIndex];
                Logueado?.Invoke(new LoguedEventArgs(logueado, empleado));
                isInvoked = true;
                Close();
            }
            else if (gdRegistro.Visibility == Visibility.Visible && VerificarRegistro())
            {
                Usuario usr = new Usuario()
                {
                    Administrador = true,
                    Nombre = Usuario,
                    Contraseña = Contraseña
                };
                empleado = new Empleado("", "", "----", "")
                {
                    IsShowed = false,
                    UsuarioAsociado = usr
                };
                logueado = true;
                UsuarioRegistrado?.Invoke(new UserRegisteredEventArgs { Empleado = empleado, Usuario = usr });
                isInvoked = true;
                Close();
            }
        }


        private void Boton_Cancelar_Login_Click(object sender, RoutedEventArgs e)
        {
            logueado = false;
            Logueado?.Invoke(new LoguedEventArgs(logueado, empleado));
            isInvoked = true;
            Close();
        }

        private void TxtRegContraseña_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox s = sender as PasswordBox;
            if (s.Name.Equals(txtRegContraseña.Name, StringComparison.Ordinal))
            {
                lblRegError.Visibility = Visibility.Collapsed;
            }
            else if (s.Name.Equals(txtRegConfContraseña.Name, StringComparison.Ordinal))
            {
                lblRegError1.Visibility = Visibility.Collapsed;
            }
        }
    }
}
