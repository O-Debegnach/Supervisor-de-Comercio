using Business_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SupComercio.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaVinculacion.xaml
    /// </summary>
    public partial class VentanaVinculacion : Window
    {
        #region Propiedades
        private bool IsClientEnabled { get; set; }
        private bool IsServerEnabled { get; set; }
        #endregion

        #region Delegados y Eventos
        public delegate void DelegadoIniciarServidor();
        public event DelegadoIniciarServidor IniciarServidor;

        public delegate bool DelegadoConectarCliente(string ip);
        public event DelegadoConectarCliente ConectarCliente;
        #endregion Delegados y Eventos
        public VentanaVinculacion()
        {
            InitializeComponent();
        }

        public VentanaVinculacion(bool isClientEnabled, bool isServerEnabled) : this()
        {
            IsClientEnabled = isClientEnabled;
            IsServerEnabled = isServerEnabled;
        }

        private void btServer_Click(object sender, RoutedEventArgs e)
        {
            IniciarServidor?.Invoke();
            MessageBox.Show($"Servidor creado exitosamente");
            Close();
        }

        private void btCliente_Click(object sender, RoutedEventArgs e)
        {
            var b = ConectarCliente?.Invoke(txtIP.Text);
            if (b.HasValue && b.Value)
            {
                MessageBox.Show(SupComercio.Resources.Strings.Mensajes.MBoxConexionExistosa);
                Close();
            }
        }
    }
}
