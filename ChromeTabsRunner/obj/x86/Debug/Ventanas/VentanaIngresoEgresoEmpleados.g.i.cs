﻿#pragma checksum "..\..\..\..\Ventanas\VentanaIngresoEgresoEmpleados.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F74130B8687D2EBB8E69D9C5D2F08808FEA3267A635EB21D84C2B93A4F6AFD1C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using ControlDeVentana;
using DragHelper;
using SupComercio.Resources.Converters;
using SupComercio.Ventanas;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace SupComercio.Ventanas {
    
    
    /// <summary>
    /// VentanaIngresoEgresoEmpleados
    /// </summary>
    public partial class VentanaIngresoEgresoEmpleados : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\..\Ventanas\VentanaIngresoEgresoEmpleados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal SupComercio.Ventanas.VentanaIngresoEgresoEmpleados VentanaPresencias;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Ventanas\VentanaIngresoEgresoEmpleados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ControlDeVentana.ControlVentana ctrlVentana;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Ventanas\VentanaIngresoEgresoEmpleados.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DGEmpleados;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SupervisorComercio;component/ventanas/ventanaingresoegresoempleados.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Ventanas\VentanaIngresoEgresoEmpleados.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.VentanaPresencias = ((SupComercio.Ventanas.VentanaIngresoEgresoEmpleados)(target));
            return;
            case 2:
            
            #line 15 "..\..\..\..\Ventanas\VentanaIngresoEgresoEmpleados.xaml"
            ((System.Windows.Data.CollectionViewSource)(target)).Filter += new System.Windows.Data.FilterEventHandler(this.CollectionViewSource_Filter);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ctrlVentana = ((ControlDeVentana.ControlVentana)(target));
            return;
            case 4:
            this.DGEmpleados = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            
            #line 58 "..\..\..\..\Ventanas\VentanaIngresoEgresoEmpleados.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 59 "..\..\..\..\Ventanas\VentanaIngresoEgresoEmpleados.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

