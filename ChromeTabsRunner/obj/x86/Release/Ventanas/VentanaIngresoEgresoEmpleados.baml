   M S B A M L   `   `   `  ���� M  ISupervisorComercio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SupComercio.VentanasSupComercio.Ventanas  'clr-namespace:DragHelper
DragHelper  K GControlDeVentana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:ControlDeVentana;assembly=ControlDeVentanaControlDeVentana S.clr-namespace:SupComercio.Resources.Converters SupComercio.Resources.Converters  8    2SupComercio.Ventanas.VentanaIngresoEgresoEmpleados   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:SupComercio.Ventanas     )dragclr-namespace:DragHelper     N	Controles8clr-namespace:ControlDeVentana;assembly=ControlDeVentana     E
Converters.clr-namespace:SupComercio.Resources.Converters     -     �Name$  VentanaPresencias�� � WindowStyle$
 None=� � AllowsTransparency .  � 
ResizeMode$ NoResize=� � Title$# VentanaIngresoEgresoEmpleados��$	��450��$��453.846�� � 	Resources ;   5SupComercio.Resources.Converters.IndexToBoolConverter   IndexToBoolConverter.   (SupComercio.Ventanas.EmpleadosCollection  	empleados  cvsEmpleados �� Source��%;   &	        &	      &	 
     7      �� -   8   .���  � ColumnDefinitions �� ��  � RowDefinitions �� $��25 ��� �� $��40 ����� 	 �� ImageSource$C	 =/SupervisorComercio;component/Resources/Fondo de software.png��
 j� Stretch$
 UniformToFill=�.+��� !   DragHelper.EnableDragHelper   
EnableDrag . $��25��$	��Top=����	#FF2D2D30$��2��%  ControlDeVentana.ControlVentana  -     Name$ ctrlVentana��$��Right=�$��1��&   System.Windows.Controls.DataGrid  -     Name$ DGEmpleados��$��
10,10,10,7q�   
IsReadOnly .    AutoGenerateColumns .  $��1��$��2������ *ListaEmpleados+ �� ElementName$ window��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Nombre��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding �� *Nombre+  $ Apellido��$ *  �� *
Apellido+   	CellStyle ��  �� 
TargetType*
  $System.Windows.Controls.DataGridCell" 
  �� Triggers P�  P� Property �@
 
IsSelected P� Value .  . ���  �� Property � �� �� Value
 �������  �� Binding �� *
IsPresente+ �� Value True.a���  � ��
 � � ��� -   $��Marcar Ingreso��$
��Left=�$��	10,10,0,0q�$��2��$	��Top=�$	��207��{��� *SelectedIndex+ �� 	Converter# [  $ DGEmpleados���� -   $��Marcar Egreso��$
��Left=�$��	10,10,0,0q�$��2��$	��Top=�$	��207��$��1��{��� *SelectedIndex+# [  $ DGEmpleados��