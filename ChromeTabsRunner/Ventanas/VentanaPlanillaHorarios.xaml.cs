using Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para VentanaPlanillaHorarios.xaml
    /// </summary>
    public partial class VentanaPlanillaHorarios : Window
    {
        private List<Empleado> Empleados;
        public VentanaPlanillaHorarios(List<Empleado> empleados)
        {
            InitializeComponent();
            Empleados = empleados;
            
        }
    }
}
