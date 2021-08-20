using Business_Layer;
using Business_Layer.Text;
using System;
using System.Globalization;
using System.Windows;
using EnableToggle = Business_Layer.EnableToggleText;

namespace SupComercio.Ventanas
{
    /// <summary>
    /// Lógica de interacción para Ventana_Pagos.xaml
    /// </summary>
    /// 

    public enum TipoPago { Efectivo, Debito, Credito }
    public class ArgumentoPagoRealizado
    {
        public TipoPago TipoDePago { get; set; }

        public ArgumentoPagoRealizado() { }
    }

    public partial class VentanaPagos : Window
    {
        public delegate void DelegadoPagoRealizado(ArgumentoPagoRealizado e);
        public event DelegadoPagoRealizado PagoRealizado;

        private readonly EnableToggle togglePagaCon;
        private readonly decimal Total;
        public VentanaPagos(decimal total)
        {
            Total = total;
            InitializeComponent();
            togglePagaCon = new EnableToggle(Box_Paga_Con, "00,00", TextMode.Money);
        }


        private void Boton_Finalizar_Click(object sender, RoutedEventArgs e)
        {
            PagoRealizado?.Invoke(new ArgumentoPagoRealizado()
            {
                TipoDePago = Boton_Pago_Debito.IsChecked == true ? TipoPago.Debito : TipoPago.Efectivo
            });
            Close();
        }

        private void Boton_Finalizar_Debito_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Box_Paga_Con_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            label_Vuelto.Content = Convert.ToDecimal(Box_Paga_Con.Text, CultureInfo.CurrentCulture) - Total;
        }
    }
}