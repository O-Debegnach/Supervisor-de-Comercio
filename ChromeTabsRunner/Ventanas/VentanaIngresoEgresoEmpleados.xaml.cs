using Data_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace SupComercio.Ventanas
{

    public class EmpleadosCollection : ObservableCollection<Empleado>
    {
        public Empleado Find(Predicate<Empleado> predicate)
        {
            foreach(Empleado e in this)
            {
                if (predicate(e)) return e;
            }
            return null;
        }
    }
    /// <summary>
    /// Lógica de interacción para VentanaIngresoEgresoEmpleados.xaml
    /// </summary>
    public partial class VentanaIngresoEgresoEmpleados : Window, INotifyPropertyChanged
    {
        public delegate void DelegadoIngresoMarcado(ArgumentoIngresoEgreso e);
        public event DelegadoIngresoMarcado IngresoMarcado;

        public delegate void DelegadoEgresoMarcado(ArgumentoIngresoEgreso e);
        public event DelegadoEgresoMarcado EgresoMarcado;
        private List<Empleado> ListaEmpleados { get; } = new List<Empleado>();
        public VentanaIngresoEgresoEmpleados(IEnumerable<Empleado> empleados)
        {
            if (empleados is null)
            {
                throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, SupComercio.Resources.Strings.Mensajes.ArgumentNullException, nameof(empleados)));
            }

            InitializeComponent();
            ListaEmpleados = empleados.ToList();

            DGEmpleados.ItemsSource = ListaEmpleados;
            DGEmpleados.Items.Filter = (x) => x != null && (x as Empleado).IsShowed;
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListaEmpleados)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContraseñaAdmin admin = new ContraseñaAdmin(ListaEmpleados);
            admin.Confirmado += Admin_Ingreso_Confirmado;
            admin.ShowDialog();
        }

        private void Admin_Ingreso_Confirmado(bool isConfirmed)
        {
            if (isConfirmed)
            {
                
                var ls = new List<Empleado>();
                foreach (Empleado empleado in DGEmpleados.SelectedItems)
                {
                    ListaEmpleados.Find(X => X.Cuil.Equals(empleado.Cuil, StringComparison.Ordinal)).IsPresente = true;
                    ls.Add(empleado);
                }
                IngresoMarcado?.Invoke(new ArgumentoIngresoEgreso(ls));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ContraseñaAdmin adminEgreso = new ContraseñaAdmin(ListaEmpleados);
            adminEgreso.Confirmado += Admin_Egreso_Confirmado;
            adminEgreso.ShowDialog();
        }

        private void Admin_Egreso_Confirmado(bool isConfirmed)
        {
            if (isConfirmed)
            {
                var ls = new List<Empleado>();
                foreach (Empleado empleado in DGEmpleados.SelectedItems)
                {
                    ListaEmpleados.Find(X => X.Cuil.Equals(empleado.Cuil, StringComparison.Ordinal)).IsPresente = false;

                    ls.Add(empleado);
                }
                EgresoMarcado?.Invoke(new ArgumentoIngresoEgreso(ls));
            }
        }

        private void CollectionViewSource_Filter(object sender, System.Windows.Data.FilterEventArgs e)
        {
            Empleado emp = e.Item as Empleado;
            if (emp.IsShowed) e.Accepted = true;
            else e.Accepted = false;
        }
    }

    public class ArgumentoIngresoEgreso
    {
        public List<Empleado> Empleados { get; } = new List<Empleado>();

        public ArgumentoIngresoEgreso(List<Empleado> e)
        {
            Empleados = e;
        }
    }
}
