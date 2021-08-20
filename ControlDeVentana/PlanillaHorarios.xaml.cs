using Data_Layer;
using SupComercio;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ControlDeVentana
{
    /// <summary>
    /// Lógica de interacción para PlanillaHorarios.xaml
    /// </summary>
    public partial class PlanillaHorarios : UserControl, INotifyPropertyChanged
    {
        #region Campos Privados
        private readonly Brush blanco = new SolidColorBrush(Color.FromArgb(100, 0xFF, 0xFF, 0xFF));
        private readonly Brush negro = new SolidColorBrush(Color.FromArgb(100, 0x00, 0x00, 0x00));
        private bool grillaGenerada = false;
        private int _intervaloHoras;
        #endregion

        #region DependencyProperties
        //public static int GetIntervaloHoras(DependencyObject obj)
        //{
        //    return (int)obj.GetValue(IntervaloHorasProperty);
        //}

        //public static void SetIntervaloHoras(DependencyObject obj, int value)
        //{
        //    obj.SetValue(IntervaloHorasProperty, value);
        //}

        //// Using a DependencyProperty as the backing store for IntervaloHoras.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IntervaloHorasProperty =
        //    DependencyProperty.RegisterAttached("IntervaloHoras", typeof(int), typeof(PlanillaHorarios), new PropertyMetadata(0));


        #endregion DependencyProperties

        #region Propiedades

        public int IntervaloHoras
        {
            get { return _intervaloHoras; }
            set
            {
                _intervaloHoras = value > 0 ? value : 1;
                if (IsLoaded) GenerarFilas();
            }
        }
        public StructHorario Horario
        {
            get => (StructHorario)GetValue(HorarioProperty);
            set
            {
                SetValue(HorarioProperty, value);
                if (IsLoaded)
                {
                    GenerarFilas();
                }
            }
        }
        public static StructHorario GetHorario(DependencyObject obj)
        {
            return (StructHorario)obj.GetValue(HorarioProperty);
        }

        public static void SetHorario(DependencyObject obj, StructHorario value)
        {
            obj.SetValue(HorarioProperty, value);
        }

        // Using a DependencyProperty as the backing store for GetHorario.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorarioProperty =
            DependencyProperty.RegisterAttached("Horario",
                                                typeof(StructHorario),
                                                typeof(PlanillaHorarios),
                                                new PropertyMetadata(new StructHorario("Horario")));

        public Brush Fill { get; set; }


        #endregion Propiedades
        public PlanillaHorarios()
        {
            IntervaloHoras = 1;
            InitializeComponent();
            Loaded += ControlLoaded;
            Foreground = Brushes.Black;
            Fill = Brushes.Black;
        }

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            GenerarFilas();
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        #endregion

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (grillaGenerada)
            {
                GenerarFilas();
            }
        }

        private void GenerarFilas()
        {
            Headers.Children.Clear();
            foreach (UIElement el in GridHorario.Children)
            {
                if (el is Grid gd)
                {
                    gd.Children.Clear();
                    gd.Children.Add(new Line()
                    {
                        X1 = 0,
                        X2 = 0,
                        Y1 = -25,
                        Y2 = RenderSize.Height,
                        Stroke = Foreground,
                        StrokeThickness = 1
                    }); ;
                }
            }
            #region Cabeceras ----------------------------------------------------------------
            for (int i = 0; i < 24; i += IntervaloHoras)
            {
                double m = ((double)i).Map(0, 24, 0, RenderSize.Height - 25);
                Brush c = i % (2*IntervaloHoras) != 0 ? blanco : negro;
                Label l = new Label()
                {
                    Content = new TimeSpan(i, 0, 0).ToString(@"hh\:mm"),
                    Margin = new Thickness(0, i != 0 ? m : m, 0, 0),
                    Padding = new Thickness(0),
                    VerticalAlignment = VerticalAlignment.Top,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Background = c,
                    Height = (Headers.RenderSize.Height) / (24/IntervaloHoras),
                    Foreground = Foreground,
                    BorderBrush = Fill,
                    BorderThickness = new Thickness(0, 1, 0, 0)
                };
                Headers.Children.Add(l);
            }
            #endregion Cabeceras -------------------------------------------------------------

            #region Dias--------------------------------------------------------------
            for (int i = 1; i <= Horario.HorarioCompleto.Count; i++)
            {
                foreach (Lapso l in Horario.HorarioCompleto[i - 1])
                {
                    TimeSpan inicio = l.Inicio;
                    double m = l.Inicio.TotalHours.Map(0, 24, 0, GridHorario.RenderSize.Height);
                    double m1 = l.Inicio.TotalHours.Map(0, 24, 0, GridHorario.RenderSize.Height);
                    double test = m.Map(0, GridHorario.RenderSize.Height, 0, 24);

                    Rectangle r = new Rectangle()
                    {
                        Height = l.Duracion.TotalHours.Map(0, 24, 0, GridHorario.RenderSize.Height),
                        Fill = Brushes.LightGreen,
                        Margin = new Thickness(10, m1, 10, 0),
                        VerticalAlignment = VerticalAlignment.Top
                    };
                    (GridHorario.Children[i] as Grid).Children.Add(r);
                }
            }
            #endregion Dias-----------------------------------------------------------
            grillaGenerada = true;
        }
    }
}
