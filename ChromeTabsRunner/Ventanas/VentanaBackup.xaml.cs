using Business_Layer;
using Data_Layer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
using Arch = SupComercio.Resources.Strings.Archivos;

namespace SupComercio.Ventanas
{
    /// <summary>
    /// Lógica de interacción para VentanaBackup.xaml
    /// </summary>
    public partial class VentanaBackup : Window, INotifyPropertyChanged
    {
        public delegate void DelegadoRespaldo(string path);
        public event DelegadoRespaldo CargarCopia;
        public event DelegadoRespaldo GuardarCopia;
        public event PropertyChangedEventHandler PropertyChanged;


        private string DefaultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\V&D\\Supervisor de Comercio";

        private ObservableCollection<FileInfo> filesList;

        public ObservableCollection<FileInfo> FilesList 
        { 
            get => filesList;
            set
            {
                if(value != filesList)
                {
                    filesList = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilesList)));
                }
            } 
        }

        public VentanaBackup()
        {  

            InitializeComponent();
            //lista.ItemsSource = FilesList;
            GetBackups(null);

        }

        private void GetBackups(string path)
        {
            string p = path ?? DefaultPath;
            DirectoryInfo dir = new DirectoryInfo(p);
            if (dir.Exists)
            {
                FilesList = new ObservableCollection<FileInfo>(dir.GetFiles("*.stk"));
            }
            //filesList[0].Length
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var empleados = Archivos.LeerArchivo<Empleado>(Arch.Empleado);
            ContraseñaAdmin contraseña = new ContraseñaAdmin(empleados);
            contraseña.Confirmado += Contraseña_Confirmado;
            contraseña.ShowDialog();
        }

        private void Contraseña_Confirmado(bool isConfirmed)
        {
            CargarCopia?.Invoke(FilesList[lista.SelectedIndex].FullName);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var empleados = Archivos.LeerArchivo<Empleado>(Arch.Empleado);
            ContraseñaAdmin contraseña = new ContraseñaAdmin(empleados);
            contraseña.Confirmado += Contraseña_Confirmado_Guardado;
            contraseña.ShowDialog();
        }

        private void Contraseña_Confirmado_Guardado(bool isConfirmed)
        {
            GuardarCopia?.Invoke($"Backup-{DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss", CultureInfo.InvariantCulture)}-Manual.stk");
            FilesList = new ObservableCollection<FileInfo>(new DirectoryInfo(DefaultPath).GetFiles("*.stk"));
        }
    }
}
