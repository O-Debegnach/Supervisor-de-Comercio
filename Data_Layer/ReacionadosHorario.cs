using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Data_Layer
{
    [Serializable]
    public struct Lapso
    {
        public TimeSpan Inicio { get; set; }
        public TimeSpan Fin { get; set; }
        public TimeSpan Duracion => Fin - Inicio;

        public static bool operator !=(Lapso t1, Lapso t2)
        {
            return t1.Duracion.TotalSeconds != t2.Duracion.TotalSeconds;
        }
        public static bool operator ==(Lapso t1, Lapso t2)
        {
            return t1.Duracion.TotalSeconds == t2.Duracion.TotalSeconds;
        }

        public static bool operator <(Lapso t1, Lapso t2)
        {
            return t1.Fin < t2.Inicio;
        }

        public static bool operator >(Lapso t1, Lapso t2)
        {
            return t1.Inicio > t2.Fin;
        }


        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public enum Dias { Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo }

    [Serializable]
    public class StructHorario : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged members
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Campos Privados
        [NonSerialized]
        private TimeSpan CargaMaxina = new TimeSpan(24, 0, 0);

        #endregion Campos Privados

        #region Metodos
        private TimeSpan CargaActual(List<Lapso> dia)
        {
            TimeSpan ts = TimeSpan.Zero;
            dia.ForEach((x) => ts = ts.Add(x.Duracion));
            return ts;
        }


        public void Add(List<Lapso> ls, Lapso item)
        {
            if (ls == null)
            {
                ls = new List<Lapso>();
            }

            //if (CargaActual(ls) + item.Duracion < CargaMaxina)
            //{
            int pos = ls.FindIndex(x => x > item);
            ls.Insert(pos != -1 ? pos : ls.Count, item);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ls)));
            //}
            //else
            //{
            //    MessageBox.Show("No se puede agregar el turno al dia seleccionado\nDe hacerlo la carga horaria seria mayor a 24hs");
            //}
        }

        public void Clear()
        {
            Lunes.Clear();
            Martes.Clear();
            Miercoles.Clear();
            Jueves.Clear();
            Viernes.Clear();
            Sabado.Clear();
            Domingo.Clear();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("All"));
        }

        public void Clear(Dias dia)
        {
            HorarioCompleto[(int)dia].Clear();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(dia.ToString())); ;
        }
        #endregion Metodos

        public StructHorario(string name)
        {
            Name = name;
        }

        public StructHorario()
        {
        }

        #region Propiedades

        public bool Init { get; set; }
        public List<Lapso> Lunes { get; set; } = new List<Lapso>();
        public List<Lapso> Martes { get; set; } = new List<Lapso>();
        public List<Lapso> Miercoles { get; set; } = new List<Lapso>();
        public List<Lapso> Jueves { get; set; } = new List<Lapso>();
        public List<Lapso> Viernes { get; set; } = new List<Lapso>();
        public List<Lapso> Sabado { get; set; } = new List<Lapso>();
        public List<Lapso> Domingo { get; set; } = new List<Lapso>();
        public List<List<Lapso>> HorarioCompleto
        {
            get => new List<List<Lapso>>() { Lunes ?? new List<Lapso>(),
                                                                              Martes ?? new List<Lapso>(),
                                                                              Miercoles ?? new List<Lapso>(),
                                                                              Jueves ?? new List<Lapso>(),
                                                                              Viernes ?? new List<Lapso>(),
                                                                              Sabado ?? new List<Lapso>(),
                                                                              Domingo ?? new List<Lapso>() };
            set
            {
                Lunes = value[0];
                Martes = value[1];
                Miercoles = value[2];
                Jueves = value[3];
                Viernes = value[4];
                Sabado = value[5];
                Domingo = value[6];
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HorarioCompleto)));
            }
        }
        public string Name { get; private set; }

        #endregion Propiedades

        #region Operadores
        public static bool operator !=(StructHorario h1, StructHorario h2)
        {
            return !h1.Equals(h2);
        }
        public static bool operator ==(StructHorario h1, StructHorario h2)
        {
            return h1.Equals(h2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion Operadores
    }
}
