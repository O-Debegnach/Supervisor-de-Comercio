   M S B A M L   `   `   `  ���� E  AChromeTabs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:ChromeTabs;assembly=ChromeTabs
ChromeTabs  K GControlDeVentana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:ControlDeVentana;assembly=ControlDeVentanaControlDeVentana M ISupervisorComercio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:DragHelper
DragHelper )clr-namespace:SupComercioSupComercio ?$clr-namespace:SupComercio.PropertiesSupComercio.Properties O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem S.clr-namespace:SupComercio.Resources.Converters SupComercio.Resources.Converters #   SupComercio.MainWindowPrecios   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ;my,clr-namespace:ChromeTabs;assembly=ChromeTabs     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     UControlDeVentana8clr-namespace:ControlDeVentana;assembly=ControlDeVentana     )dragclr-namespace:DragHelper     +localclr-namespace:SupComercio     4rec$clr-namespace:SupComercio.Properties     9System&clr-namespace:System;assembly=mscorlib     X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  ?
Converters.clr-namespace:SupComercio.Resources.Converters  -     �Name$  VentanaPrincipal�� J� SnapsToDevicePixels .  � Title$ Supervisor de Comercios��$��338.5��$	��500��$	��800��$��759.5�� � WindowStartupLocation$ CenterOwner=� � AllowsTransparency .  � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � Icon$7 1/SupervisorComercio;component/Resources/icono.ico�� � 	Resources ;  5Xceed.Wpf.Toolkit.Core.Converters.NullToBoolConverter   NullToBoolConverter: 4SupComercio.Resources.Converters.ExpirationConverter  ExpirationConverter   BooleanToVisibilityConverterC  =SupComercio.Resources.Converters.BitmapToImageSourceConverter   BitmapToImageSourceConverter%  SupComercio.ArticulosCollection  ArticulosCollection  
cvsEnvases	 �� Source��%h   &	        &	      &	 
     &	      &	      &	      7      ��     �� -   8	   .��� 
 � RowDefinitions
 �� ����  j� Stretch$ UniformToFill=� �� ImageSource$C =/SupervisorComercio;component/Resources/Fondo de software.png��.+�!   ChromeTabs.ChromeTabControl  -     Name$ 
chrometabs�����	#FF2D2D2D.@�   ChromeTabs.ChromeTabItem  $��Stock��$y�Visible=�.��� -    �Name$ Grid_ControlStock��
 �� �� $��80 ��� $��80 � � ColumnDefinitions �� $
��601* ��� $
��189* �.+�&   System.Windows.Controls.DataGrid  -     Name$ Data_Grid_Stock��   AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserResizeRows .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto  � DataContext$ 	articulos��   HeadersVisibility$ Column=�   
IsReadOnly . $��10,39,10.086,10q�$��2��   Columns 0	  *System.Windows.Controls.DataGridTextColumn	  ,
  &System.Windows.Controls.DataGridColumn 
  Header$ Producto�� 
  Width$	 50* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
Producto+	  $ Codigo��$
  30*  
  
IsReadOnly .  �� *Codigo+	  $ Cantidad��$	 20*  .  �� *
StrStock+	  $ Precio��$	 20*  �� *Precio+	  $  Prox. Vencimiento�� . $	 20*  �� *StrProximoVencimiento+ �� UpdateSourceTrigger$ PropertyChanged=�   	CellStyle ��   �� 
TargetType*  $System.Windows.Controls.DataGridCell"   ! �� Triggers! P� " P� Property" �@ 
IsSelected# P� Value# .  . ��� $ �� Property$ � ��% �� Value
% �����^� & ^� 
Conditions& �� ' �� Binding' �� *ProximoVencimiento+( �� 	Converter#( [ ) �� Value) True�� ' �� *Vencimientos+#( [  	) False.0��� $ � ��
% �  ��  $��1��$��9.914,10,10,10q� .  $��3������ * �� Source#* [  	  $ Nombre��$ *  �� *
Producto+	  $ Cantidad��$ 60  �� *CantidadEnvases+�� -   + ��Name$+ txtBusquedaStock��$��
10,10,10,0q�$)��#Buscar producto por nombre o codigo��, �� TextWrapping$, NoWrap=�$��22��$	��Top=�
���   ����	#FFFFF1F1�� -   - ��Name$- 
btVincular��$��Vinculacion de equipos��$��20(�$��10q�$��2��  -   .  Name$. AdministrarTab��$y�	Collapsed=�$��Administrar��.���  �� �� 
 �� $	��27* ��� $	��50* ��� $	��49* ��� $	��50* ��� $	��50* ��� $	��50* ��� $	��50* ��� $	��43* ��� $
��104* �.+��� -	   $- Boton_Planilla_Horarios��$��Ver Planilla de Horarios��$��43,6.812,75,8.11q�$��2��$��20(�$
��Bold"�$y�	Collapsed=��� -
   $ - Boton_Planilla_Proveedores��$!��Ver Planilla de Proveedores��$��43,9.89,75,10.119q�$��3��$��16(�$y�	Collapsed=��� -   $- Boton_Usuarios��$��Usuarios��$��10,1.821,10,10.188q�$��1��$��20(��� -   $- Boton_Agregar_Empleados��$��Gestor de Personal��$��10,0,10,10.372q�$��3��$��20(�$��1��y��� *" EmpleadoLogueado.GestorEmpleados+#( [ / �� ElementName$/ VentanaPrincipal���� -   $- Boton_Ingreso_30_dias��$*��$Dinero Ingresado los Ultimos 30 dias��$��
43,0,75,62q�$��8��$��36��$��Bottom=�$y�	Collapsed=��� -   $- Boton_Gastos_30_dias��$(��"Dinero Gastado los Ultimos 30 dias��$��
43,0,75,23q�$��8��$��36��$��Bottom=�$y�	Collapsed=��� -   $- Boton_Finalizar��$y�	Collapsed=�$��	Finalizar��$��8��$��36(�$��218,6.367,221,17q�$��2���� -   $- Boton_Registrar_Ingreso��$��20(�$-��'Registrar Ingreso o Egreso de Empleados��$��10,0,10,10.11q�$��2��$��1���� -   $��Configurar Ticketera��$��20(�$��1��$y�Visible=�$��
10,0,10,10q�$��1���� -   $- btVincular3��$��Vinculacion de equipos��$��20(�$��1��$��	10,0,10,8q�$��2���� -   $(��"Copia de seguridad y restauración��$��20(�$��1��$��
10,0,10,10q�$��3���� $a��[Usted dispone de la verison pro y todas sus actualizaciones disponibles hasta el 10/05/2020��
�������$��Center=�$��8��$��Bottom=�$
��0 10q�$��2���� -   0 ��Name$0 lblConection��
�������$��Center=�$��0,43,0,0q�$��8��$	��Top=�$��2��  $��Ventas��$y�	Collapsed=�.��� -   $ Grid_Ventas��
 �� $
��233* ��� $
��117* ��� $��110.009 � �� $��310.327 ��� �� $	��111 �.+��� $��Total: $��$��1,1,0,1q�
����������	#FFABABAB$��48(�$��2��$��	0,22,0,10q�$��1��$
��Left=�$	��162���� -   $0 Label_Total��$��0,1,1,1q�
����������	#FFABABAB$��48(�$��2��$��162,22,10,10q�$��1������ *Total+$/ VentanaPrincipal��$ PropertyChanged=��� -   $+ txtBuscarArticulo��$��1,1,1,1q�$��
10,10,10,0q�$)��#Buscar producto por nombre o codigo��$, NoWrap=�$��22��$	��Top=�
���   ����	#FFFFF1F1� $��3��$��	310,0,0,0q�$��2��  -   $ Lista_Venta��$��3��$��310,10,10,0q�$ Column=�1   SelectionMode$1 Single=�$ Articulo_Venta�� .  $��2������ *Ventas+$/ VentanaPrincipal��$ PropertyChanged=� 	  $ Producto�� . 2 
  CanUserReorder2 .  3 
  CanUserResize3 .  $
 100*  �� *Descripcion+	  $ Cantidad�� .  2 .  3 .  $	 30*  �� *
Cantidad+	  $ Precio�� . 2 .  3 .  $	 30*  �� *Precio_Total+�� $��2q�$��10q�
�������$
��Auto=�$��1��$��2��.���� -   4 ��Name$4 Elementos_Virtuales��$��10,10,10,10q�5 �� CanVerticallyScroll5 . 6 �� Orientation$6 Vertical=�  -   $ 
DgBusqueda��7   CanUserSortColumns7 .  .  $��3��$��10,32,10.327,10q�$ Column=� .   .   .   . ���� *	articulos+$/ VentanaPrincipal�� 	  2 .  $ Codigo��$ 2*  �� *Codigo+	  2 .  $ Producto��$ 4*  �� *
Producto+	  2 .  $ Precio��$ 2*  �� *Precio+�� -   $- Boton_Terminar_Venta��$��2��$��10,22,12,10q�$��2��  Button_Tilde#���    $
��Caja��$y�	Collapsed=�.��� -   $ 	Grid_Caja�� �� �� 
 �� $	��79* ��� $	��79* ��� $	��79* ��� $	��40* ��� $	��39* ��� $	��79* ��� $	��79* �.+��� -   $- Boton_Finalizar_Turno��$��1��$��15,20,15,41q�$��5��$��2��$��36(�
���   �.��(� 8 (� Stretch$8 Uniform=�$
��Left=�.���� $��Finalizar Turno���� $��15,26q�
����������	#FFABADB39 �� Padding$9 0q�$��1q�._�(� $8 Uniform=�$
��Left=�.��]� ��. $��Stretch=�$��Right=�.+��� -   $+ txtFCRecibido��$��0,0,1,0q�$��00,00��$��Right=�$��Stretch=�$, NoWrap=�$��292.5��$��Center=��� -   $��Fondo de caja recibido:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��14,26q�
����������	#FFABADB3$��1q�$��1��._�(� $8 Uniform=�$
��Left=�.��]� ��. .+��� -    $+ txtFCEntregado��$��00,00��$��Right=�$��Stretch=�$, NoWrap=�$	��282��$��1,0,1,0q�$��Center=��� -!   $��Fondo de caja entregado:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��15,26q�
����������	#FFABADB3$��1q�$��0��$��1��._�(� $8 Uniform=�$
��Left=�.��]� ��. .+��� -"   $+ txtEfectivo��|�.  $��Right=�$��Stretch=�$, NoWrap=�$	��345��$��1,0,1,0q����� *EfectivoRecibido+$/ VentanaPrincipal��$ PropertyChanged=��� -#   $��Efectivo recibido:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��15,26q�
����������	#FFABADB3$��1q�$��0��$��2��._�(� $8 Uniform=�$
��Left=�.��]� ��. .+��� -$   $+ 	txtDebito��|�.  $��Right=�$��Stretch=�$, NoWrap=�$	��374��$��0q�
���   ����� *DebitoRecibido+$/ VentanaPrincipal��$ PropertyChanged=��� -%   $��Debito recibido:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��15,26q�
����������	#FFABADB3$��1q�$��1��$��2��: J� RenderTransformOrigin$: 0.5,0.5&�._�(� $8 Uniform=�$
��Left=�.��]� ��. $
��Left=�.+��� -&   $+ 
txtSueldos��|�.  $��Right=�$��Stretch=�$, NoWrap=�$	��352��$��1,0,1,0q��� -'   $��Sueldos o bonos:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��15,3.25,15,10q�
����������	#FFABADB3$��1q�$��0��$��4��._�(� $8 Uniform=�$
��Left=�.��]� ��. .+��� -(   $+ txtDeposito��|�.  $��Right=�$��Stretch=�$, NoWrap=�$	��432��$��1,0,1,0q�$��Center=����� *	Depositos+$/ VentanaPrincipal���� -)   $��Depósitos:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��15,26q�
����������	#FFABADB3$��1q�$��1��$��1��._�(� $8 Uniform=�$
��Left=�.��]� ��. .+��� -*   $+ txtPagoProveedores��$��00,00��$��Right=�$��Stretch=�$, NoWrap=�$	��316��$��1,0,1,0q�$��Center=��� -+   $��Pagos a proveedores:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��15,3.25,15,10q�
����������	#FFABADB3$��1q�$��1��$��4��._�(� $8 Uniform=�$
��Left=�.��]� ��. .+��� -,   $+ txtExtracciones��|�.  $��00,00��$��Right=�$��Stretch=�$, NoWrap=�$	��405��$��1,0,1,0q�$��Center=��� --   $��Extracciones:��$��18(�$��DemiBold"�$��Center=�$��Center=��� -.   $��93,20q�$��Recaudacion Total:�����	#FFDDDDDD$��24(�$��Center=�$��DemiBold"�$��Center=�$��5���� -/   $+ txtRecaudacion��$��Center=�$��Center=�|�.  $��6��$, NoWrap=�$��
15,0,16,43q����� *RecaudacionTotal+$/ VentanaPrincipal��; �� StringFormat$; {0:C}���� $��15,12,125,1.75q�
����������	#FFABADB3$��1q�$��0��$��3��._�(� $8 Uniform=�$
��Left=�.��]� ��. .+�%  Xceed.Wpf.Toolkit.DecimalUpDown  -0   <  Name$< txtRealizarDeposito��$��Right=��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]=   ShowButtonSpinner= .  >   	AllowSpin> .  ?   ClipValueToMinMax? . @   Minimum$@ 0X�$
��Left=�$	��207��A   MaximumA �� *	Content+$/ label���� -1   $��Depositar dinero:��$��18(�$��DemiBold"�$��Center=�$��Center=��� -2   $��	0,12,15,3q�$��3��$��Right=�$	��105��.��(� $8 Uniform=�.���� -3   $��Realizar deposito���� $��15,12,132,1.75q�
����������	#FFABADB3$��1q�$��1��$��3��._�(� $8 Uniform=�$
��Left=�.��]� ��. .+�  -4   $< txtRealizarExtraccion��$��Right=�= .  > .  ? . $@ 0X�$
��Left=�$	��226��A �� *
DineroCaja+$/ VentanaPrincipal���� -5   $��Extraer dinero:��$��18(�$��DemiBold"�$��Center=�$��Center=��� -6   $��1��$��Right=�$��	0,12,15,2q�$��3��$	��112��.��(� $8 Uniform=�.���� -7   $��Realizar extraccion���� !  DragHelper.EnableDragHelperB   
EnableDragB . 
������ 
������ $��27��$	��Top=�$��	178,0,0,0q�._��� -8   $0 lblEmpleadoLogueado��$��Right=�$��0,0,85,0q�$�� ��
���������Z� �� $y�	Collapsed=�B . 
�������$��1q�$��25��$��25��$��	0,0,100,0q�$��Right=�$	��Top=�._��� -9   C ��Name$C FotoEmpleadoLogueado������ *EmpleadoLogueado.FotoPerfil+#( [ $/ VentanaPrincipal���� B . 
������ 
������ $��27��$	��Top=�$��0,-1,0,0q�$
��Left=�$��50��� D � Points%D �   ������@x}@�� h�	
������ 
������ B . � %D �    h�	��!
�� ��
 h�	
������ 
������ B . � %D �   @�E@�����W�@�E
������ 
������ B . � %D �   ��> �����p��>
������ 
������ B . � %D �   @�@�@x}@� �`!
������ 
������ B . %  ControlDeVentana.ControlVentana  -:   E  Name$E ctrlVentana��$��Right=�$	��Top=�$��25��$��75��