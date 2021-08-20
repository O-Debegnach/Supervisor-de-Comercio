using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlDeVentana
{
    /// <summary>
    /// Lógica de interacción para UserControl1.xaml
    /// </summary>
    public partial class ControlVentana : UserControl
    {
        private WindowState state;
        private Window window;
        private bool isOnlyClose;
        private bool haveMaximize = true;

        private enum WinState { Normal, Minimized, Maximized }

        #region Control de Tamaño
        private Rect _restoreLocation;

        
        public void MaximizeWindow()
        {
            if (window == null)
            {
                GetWindowParent();
            }
            _restoreLocation = new Rect { Width = window.Width, Height = window.Height, X = window.Left, Y = window.Top};
            var currentScreen = System.Windows.SystemParameters.WorkArea;
            window.Height = currentScreen.Height;
            window.Width = currentScreen.Width;
            window.Left = currentScreen.X;
            window.Top = currentScreen.Y;
        }

        public void Restore()
        {
            window.Height = _restoreLocation.Height;
            window.Width = _restoreLocation.Width;
            window.Left = _restoreLocation.X;
            window.Top = _restoreLocation.Y;
        }
        #endregion

        #region Propiedades

        public bool HaveMaximize
        {
            get { return haveMaximize; }
            set
            {
                haveMaximize = value;
                btMinimizar.Margin = new Thickness(25, 0, 0, 0);
                if (!isOnlyClose)
                {
                    if(haveMaximize)
                    {
                        btMinimizar.Margin = new Thickness(0);
                        if (state == WindowState.Maximized)
                        {
                            btRestaurar.Visibility = Visibility.Visible;
                        }
                        else if (state == WindowState.Normal)
                        {
                            btMaximizar.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        btMaximizar.Visibility = Visibility.Hidden;
                        btRestaurar.Visibility = Visibility.Hidden;
                    }
                }
                UpdateLayout();
            }
        }
        public bool IsOnlyClose { 
            get { return isOnlyClose; } 
            
            set 
            {
                isOnlyClose = value;
                if (value == true)
                {
                    btMaximizar.Visibility = Visibility.Hidden;
                    btMinimizar.Visibility = Visibility.Hidden;
                    btRestaurar.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (haveMaximize)
                    {
                        if (state == WindowState.Maximized)
                        {
                            btRestaurar.Visibility = Visibility.Visible;
                        }
                        else if (state == WindowState.Normal)
                        {
                            btMaximizar.Visibility = Visibility.Visible;
                        }
                        btMinimizar.Margin = new Thickness(0);
                    }
                    btMinimizar.Visibility = Visibility.Visible;
                }
            }
        }
        public WindowState State
        {
            get => state;
            set
            {
                state = value;
                if (!isOnlyClose)
                {
                    if (state == WindowState.Maximized)
                    {
                        btMaximizar.Visibility = Visibility.Hidden;
                        btRestaurar.Visibility = Visibility.Visible;
                    }
                    else if (state == WindowState.Normal)
                    {
                        btMaximizar.Visibility = Visibility.Visible;
                        btRestaurar.Visibility = Visibility.Hidden;
                    }
                    UpdateLayout();
                }
            }
        }
        #endregion

        public ControlVentana()
        {
            InitializeComponent();
            
        }

        #region Funciones
        private void GetWindowParent()
        {
            try {
                DependencyObject parent = this.Parent;
                parent = LogicalTreeHelper.GetParent(parent);
                Window objLogin = parent as Window;
                while (parent != null && !(parent is Window))
                {
                    parent = LogicalTreeHelper.GetParent(parent);
                }
                window = parent as Window;
            }
            catch (Exception) { }
        }

        #endregion

        #region Eventos
        private void btCerrar_Click(object sender, RoutedEventArgs e)
        {
            if (window == null)
            {
                GetWindowParent();
            }
            if (window != null)
                window.Close();
        }

        private void btRestaurar_Click(object sender, RoutedEventArgs e)
        {
            if (window == null)
            {
                GetWindowParent();
            }
            if (window != null)
            {
                if (window.AllowsTransparency)
                {
                    State = WindowState.Normal;
                    Restore();
                }
                else State = window.WindowState = WindowState.Normal;
            }
        }

        private void btMinimizar_Click(object sender, RoutedEventArgs e)
        {
            if (window == null)
            {
                GetWindowParent();
            }
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        private void btMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if (window == null)
            {
                GetWindowParent();
            }
            if (window != null)
            {
                if (window.AllowsTransparency)
                {
                    State = WindowState.Maximized;
                    MaximizeWindow();
                }
                else State = window.WindowState = WindowState.Maximized;
            }
        }
        #endregion

    }
}
