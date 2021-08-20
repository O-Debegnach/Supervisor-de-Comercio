using Data_Layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SupComercio.Ventanas.Testeos
{
    /// <summary>
    /// Lógica de interacción para Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public Test()
        {
            InitializeComponent();
            EnvasesCollection p = (EnvasesCollection)Resources["Envases"];
        }

        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            Persona p = e.Item as Persona;
            if (p != null)
            {
                if (p.Edad > 10)
                {
                    e.Accepted = true;
                }
                else e.Accepted = false;
            }
        }
    }

    public class Persona : INotifyPropertyChanged, IEditableObject
    {
        private bool _editing;
        private Persona temp_Persona;

        public Persona()
        {
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set 
            { 
                _nombre = value;
                NotifyPropertyChanged(nameof(Nombre));
            }
        }

        private int _edad;

        public int Edad
        {
            get { return _edad; }
            set 
            { 
                _edad = value;
                NotifyPropertyChanged(nameof(Edad));
            }
        }
            

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void BeginEdit()
        {
            if (!_editing)
            {
                temp_Persona = MemberwiseClone() as Persona;
                _editing = true;
            }
        }

        public void CancelEdit()
        {
            if (_editing)
            {
                _nombre = temp_Persona._nombre;
                _nombre = temp_Persona._nombre;
                _editing = false;
            }
        }

        public void EndEdit()
        {
            if (_editing)
            {
                temp_Persona = null;
                _editing = false;
            }
        }
    }
    public class EnvasesCollection: ObservableCollection<Persona>
    {

    }
}
