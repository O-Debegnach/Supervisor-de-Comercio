using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using Res = Data_Layer.Properties.Resources;

namespace Data_Layer
{
    [Serializable]
    public class Empleado : INotifyPropertyChanged
    {
        private bool _isPresente;
        private TimeSpan _horasTrabajadas;

        public Empleado() { }

        public Empleado(string nombre, string apellido, string cuil, string domicilio)
        {
            Nombre = nombre;
            Apellido = apellido;
            Cuil = cuil;
            Domicilio = domicilio;
        }

        public bool IsPresente
        {
            get => _isPresente;
            set
            {
                _isPresente = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPresente)));
            }
        }

        public bool IsShowed { get; set; } = true;
        public bool GestorEmpleados => !IsShowed;
        public bool IsActive { get; set; } = true;
        public decimal Precio_Hora { get; set; }
        public TimeSpan HorasTrabajadas 
        { 
            get=>_horasTrabajadas;
            set 
            {
                if (value != _horasTrabajadas)
                    _horasTrabajadas = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HorasTrabajadas)));
            }
        }
        public decimal Sueldo
        {
            get
            {
                decimal totalVales = 0;
                foreach (Vale v in Vales)
                {
                    totalVales += v.Precio;
                }
                return Precio_Hora * (decimal)HorasTrabajadas.TotalHours;
            }
        }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cuil { get; set; }
        public string Cargo { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellido}";
        public string Domicilio { get; set; }
        public Contacto DatosContacto { get; set; }
        public Bitmap FotoPerfil { get; set; } = Res.Icono_Usuario;
        public string FotoPerfilUrl { get; set; }
        public Usuario UsuarioAsociado { get; set; }
        public List<Vale> Vales { get; set; } = new List<Vale>();
        public StructHorario Horario { get; set; } = new StructHorario();
        public Pincel ColorEnPlanilla { get; set; }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> GetContacto()
        {
            return new List<string> { DatosContacto.Celular, DatosContacto.Telefono_fijo, DatosContacto.Email };
        }

        public bool Equals(Empleado e)
        {
            return string.Compare(Cuil, e.Cuil, StringComparison.OrdinalIgnoreCase) == 0;
        }

        [Serializable]
        public class Contacto
        {
            public Contacto() { }

            public string Telefono_fijo { get; set; }
            public string Celular { get; set; }
            public string Email { get; set; }
        }

    }

    [Serializable]
    public class Pincel
    {
        public Pincel(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public SolidColorBrush GetBrush()
        {
            return new SolidColorBrush(Color.FromArgb(0xFF, Red, Green, Blue));
        }

        public byte Red { get; private set; }
        public byte Green { get; private set; }
        public byte Blue { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is Pincel pincel &&
                   Red == pincel.Red &&
                   Green == pincel.Green &&
                   Blue == pincel.Blue;
        }

        public override int GetHashCode()
        {
            int hashCode = -1058441243;
            hashCode = hashCode * -1521134295 + Red.GetHashCode();
            hashCode = hashCode * -1521134295 + Green.GetHashCode();
            hashCode = hashCode * -1521134295 + Blue.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
