using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ControlDeVentana
{
    /// <summary>
    /// Lógica de interacción para WebCamControl1.xaml
    /// </summary>
    public partial class WebCamControl : UserControl
    {
        #region Delegados y Eventos
        public delegate void IsCamarasEncontradasHandler(bool encontradas);
        public event IsCamarasEncontradasHandler IsCamarasEncontradas;

        public delegate void SnapshotTakenHandler(BitmapImage snap);
        public event SnapshotTakenHandler SnapshotTaken;

        public delegate void IsSnapshotDeletedHandler();
        public event IsSnapshotDeletedHandler IsSnapshotDeleted;
        #endregion

        #region Propiedades
        public ObservableCollection<FilterInfo> Camaras { get; set; }
        public bool IsCameraFinded { get; set; } = false;
        public FilterInfo CamaraActual
        {
            get => _camaraActual;
            set 
            {
                _camaraActual = value;
                //OnPropertyChanged(new DependencyPropertyChangedEventArgs(Camara_Actual, _camaraActual, value));
                //this.OnPropertyChanged("CamaraActual");
            }
        }

        //private DependencyProperty Camara_Actual = DependencyProperty.RegisterAttached("CamaraActual", typeof(FilterInfo), typeof(WebCamControl));
        #endregion

        #region Campos Privados

        private FilterInfo _camaraActual;
        private IVideoSource _fuenteVideo;
        private Window parent;
        private BitmapImage image;
        private bool loaded = false;

        #endregion
        public WebCamControl()
        {
            InitializeComponent();
            Inicializar();
            Loaded += WebCamControl_Loaded;
        }

        #region Funciones

        public void Inicializar()
        {
            ObtenerCamaras();
            if (loaded)
            {
                GdSeleccionarCamara.Visibility = Visibility.Visible;
                GdTomarFoto.Visibility = Visibility.Collapsed;
                GdDescartar.Visibility = Visibility.Collapsed;

                Border_Foto.Visibility = Visibility.Collapsed;
                Border_Camara.Visibility = Visibility.Visible;

                DesactivarCamara();
                Camara_Control.Source = null;
                ComboCamaras.SelectedIndex = 0;
                CamaraActual = Camaras[0];
            }
        }

        private void GetWindowParent()
        {
            try
            {
                DependencyObject parent = this.Parent;
                parent = LogicalTreeHelper.GetParent(parent);
                Window objLogin = parent as Window;
                while (parent != null && !(parent is Window))
                {
                    parent = LogicalTreeHelper.GetParent(parent);
                }
                this.parent = parent as Window;
            }
            catch (Exception) { }
        }

        private void ObtenerCamaras()
        {
            Camaras = new ObservableCollection<FilterInfo>();
            foreach (FilterInfo filterInfo in new FilterInfoCollection(FilterCategory.VideoInputDevice))
            {
                Camaras.Add(filterInfo);
            }
            if (Camaras.Any())
            {
                ComboCamaras.ItemsSource = Camaras;
                CamaraActual = Camaras[0];
                IsCameraFinded = true;
                IsCamarasEncontradas?.Invoke(true);
            }
            else
            {
                IsCameraFinded = false;
                IsCamarasEncontradas?.Invoke(false);
            }
        }

        private void ActivarCamara()
        {
            if(CamaraActual != null)
            {
                _fuenteVideo = new VideoCaptureDevice(CamaraActual.MonikerString);
                _fuenteVideo.NewFrame += New_Frame;
                _fuenteVideo.Start();
            }
        }

        private void New_Frame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                BitmapImage bi;
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    bi = bitmap.ToBitmapImage();
                    image = bi;
                }
                bi.Freeze(); // avoid cross thread operations and prevents leaks
                Dispatcher.BeginInvoke(new ThreadStart(delegate { Camara_Control.Source = bi; }));
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error on _videoSource_NewFrame:\n" + exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DesactivarCamara();
            }
        }

        private void DesactivarCamara()
        {
            if (_fuenteVideo != null && _fuenteVideo.IsRunning)
            {
                _fuenteVideo.SignalToStop();
                _fuenteVideo.NewFrame -= new NewFrameEventHandler(New_Frame);
            }
        }
        #endregion


        #region Eventos
        private void WebCamControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (parent == null) GetWindowParent();
            if (parent != null) parent.Closing += Parent_Closing;
            loaded = true;
        }

        private void Parent_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DesactivarCamara();
        }


        private void btSeleccionarCamara_Click(object sender, RoutedEventArgs e)
        {
            //DesactivarCamara();
            ActivarCamara();
            GdTomarFoto.Visibility = Visibility.Visible;
            GdSeleccionarCamara.Visibility = Visibility.Collapsed;
        }

        private void ComboCamaras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CamaraActual = Camaras[ComboCamaras.SelectedIndex];
            }
            catch (Exception) { }
        }

        private void btCambiarCamara_Click(object sender, RoutedEventArgs e)
        {
            GdTomarFoto.Visibility = Visibility.Collapsed;
            GdSeleccionarCamara.Visibility = Visibility.Visible;
        }

        private void btTomarFoto_Click(object sender, RoutedEventArgs e)
        {
            Foto_Control.Source = Camara_Control.Source as BitmapImage;
            Border_Camara.Visibility = Visibility.Collapsed;
            Border_Foto.Visibility = Visibility.Visible;
            GdDescartar.Visibility = Visibility.Visible;
            SnapshotTaken?.Invoke(Foto_Control.Source as BitmapImage);
        }

        private void btDescartar_Click(object sender, RoutedEventArgs e)
        {
            Border_Camara.Visibility = Visibility.Visible;
            Border_Foto.Visibility = Visibility.Collapsed;
            GdDescartar.Visibility = Visibility.Collapsed;
            GdTomarFoto.Visibility = Visibility.Visible;
            IsSnapshotDeleted?.Invoke();
        }
        #endregion

        private void GdSeleccionarCamara_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ComboCamaras.Width = e.NewSize.Width - btSeleccionarCamara.Width - 10;
        }
    }
}
