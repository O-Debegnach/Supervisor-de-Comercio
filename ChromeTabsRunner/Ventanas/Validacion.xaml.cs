using Business_Layer.Licencia;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using SupComercio.Properties;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Media;
using Res = SupComercio.Properties.Resources;

namespace SupComercio.Ventanas
{
    public class LicenciaValidadaEnventArgs
    {
        public LicenciaValidadaEnventArgs(string licencia, bool validada, DateTime fechaVencimiento)
        {
            Licencia = licencia;
            Validada = validada;
            FechaVencimiento = fechaVencimiento;
        }

        public string Licencia { get; private set; }
        public bool Validada { get; private set; }
        public DateTime FechaVencimiento { get; private set; }

    }

    /// <summary>
    /// Lógica de interacción para Validacion.xaml
    /// </summary>
    public partial class Validacion : Window
    {
        public delegate void DelegadoLicenciaValidada(LicenciaValidadaEnventArgs e);

        /// <summary>
        /// Se dispara al validar licencia, devuelve null en caso de no validar
        /// </summary>
        public event DelegadoLicenciaValidada LicenciaValidada;

        private readonly HtmlWeb oWeb = new HtmlWeb();
        private LicenseManager LicenceManager;
        private LicenciaValidadaEnventArgs licenciaValidadaEnventArgs;
        private string MAC;
        public Validacion()
        {
            InitializeComponent();
            LicenceManager = new LicenseManager(oWeb);
        }

        

        private void btValidar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLicencia.Text))
            {
                lblError.Content = Res.Error_Licencia_Vacia;
            }
            MAC = LicenceManager.GetMacAddress();
            string validated = LicenceManager.RequestAddLicense(txtLicencia.Text, MAC);
            if (validated.Contains("OK") || validated.Contains("C200"))
            {
                lblError.Content = "OK";
                lblError.Foreground = Brushes.Green;
                licenciaValidadaEnventArgs = new LicenciaValidadaEnventArgs(txtLicencia.Text, true, DateTime.MaxValue);
            }
            else
            {
                lblError.Content = Res.Error_Licencia_Invalida + $"({validated})";
                return;
            }
        }


        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            LicenciaValidada?.Invoke(null);
            if(MessageBox.Show(SupComercio.Resources.Strings.Mensajes.CancelarValidacion, SupComercio.Resources.Strings.Mensajes.MBoxInfo, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Close();
        }

        private void btAceptar_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.DireccionComercio = txtDireccion.Text;
            Settings.Default.NombreComercio = txtNombre.Text;
            Settings.Default.Licencia = txtLicencia.Text;
            Settings.Default.Mac = MAC;
            Settings.Default.FechaUltimaValidacion = DateTime.Now;
            Settings.Default.FechaExpiracion = DateTime.Now.AddYears(1);
            Settings.Default.Save();
            LicenciaValidada?.Invoke(licenciaValidadaEnventArgs);
            Close();
        }
    }
}
