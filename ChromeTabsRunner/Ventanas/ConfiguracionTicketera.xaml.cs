using SupComercio.Properties;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
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
    /// Lógica de interacción para ConfiguracionTicketera.xaml
    /// </summary>
    public partial class ConfiguracionTicketera : Window
    {
        public ConfiguracionTicketera()
        {
            InitializeComponent();
            if(!string.IsNullOrWhiteSpace(Settings.Default.NombreTicketera))
                txtCaracteres.Text = Settings.Default.TamanioTicketera.ToString(CultureInfo.InvariantCulture);

            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                cbImpresora.Items.Add(pkInstalledPrinters);
                if(pkInstalledPrinters == Settings.Default.NombreTicketera)
                {
                    cbImpresora.SelectedIndex = i;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int i;
            Settings.Default.NombreTicketera = cbImpresora.Text;
            if(int.TryParse(txtCaracteres.Text, out i)) Settings.Default.TamanioTicketera = i;
            Settings.Default.Save();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
