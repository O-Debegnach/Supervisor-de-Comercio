   M S B A M L   `   `   `  ���� E  AChromeTabs, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:ChromeTabs;assembly=ChromeTabs
ChromeTabs  K GControlDeVentana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:ControlDeVentana;assembly=ControlDeVentanaControlDeVentana M ISupervisorComercio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:DragHelper
DragHelper )clr-namespace:SupComercioSupComercio ?$clr-namespace:SupComercio.PropertiesSupComercio.Properties O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem S.clr-namespace:SupComercio.Resources.Converters SupComercio.Resources.Converters    SupComercio.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ;my,clr-namespace:ChromeTabs;assembly=ChromeTabs     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     UControlDeVentana8clr-namespace:ControlDeVentana;assembly=ControlDeVentana     )dragclr-namespace:DragHelper     +localclr-namespace:SupComercio     4rec$clr-namespace:SupComercio.Properties     9System&clr-namespace:System;assembly=mscorlib     X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  ?
Converters.clr-namespace:SupComercio.Resources.Converters  -     �Name$  VentanaPrincipal�� J� SnapsToDevicePixels .  � Title$ Supervisor de Comercios��$��338.5��$	��500��$	��800��$��759.5�� � WindowStartupLocation$ CenterOwner=� � AllowsTransparency .  � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � Icon$7 1/SupervisorComercio;component/Resources/icono.ico�� � 	Resources ;  5Xceed.Wpf.Toolkit.Core.Converters.NullToBoolConverter   NullToBoolConverter: 4SupComercio.Resources.Converters.ExpirationConverter  ExpirationConverter   BooleanToVisibilityConverterC  =SupComercio.Resources.Converters.BitmapToImageSourceConverter   BitmapToImageSourceConverter%  SupComercio.ArticulosCollection  ArticulosCollection  
cvsEnvases	 �� Source��%h   &	        &	      &	 
     &	      &	      &	      7      ��     �� -   8	   .��� 
 � RowDefinitions
 �� ����  j� Stretch$ UniformToFill=� �� ImageSource$C =/SupervisorComercio;component/Resources/Fondo de software.png��.+�!   ChromeTabs.ChromeTabControl  -     Name$ 
chrometabs�����	#FF2D2D2D.@�   ChromeTabs.ChromeTabItem  -     Name$ AdministrarTab��$y�Visible=�$��Administrar��.���  � ColumnDefinitions �� �� 
 �� $	��27* ��� $	��50* ��� $	��49* ��� $	��50* ��� $	��50* ��� $	��50* ��� $	��50* ��� $	��43* ��� $
��104* �.+��� -    ��Name$ Boton_Planilla_Horarios��$��Ver Planilla de Horarios��$��43,6.812,75,8.11q�$��2��$��20(�$
��Bold"�$y�	Collapsed=��� -   $  Boton_Planilla_Proveedores��$!��Ver Planilla de Proveedores��$��43,9.89,75,10.119q�$��3��$��16(�$y�	Collapsed=��� -   $ Boton_Usuarios��$��Usuarios��$��10,1.821,10,10.188q�$��1��$��20(��� -   $ Boton_Agregar_Empleados��$��Gestor de Personal��$��10,0,10,10.372q�$��3��$��20(�$��1��y��� *" EmpleadoLogueado.GestorEmpleados+ �� 	Converter# [  �� ElementName$ VentanaPrincipal���� -	   $ Boton_Ingreso_30_dias��$*��$Dinero Ingresado los Ultimos 30 dias��$��
43,0,75,62q�$��8��$��36��$��Bottom=�$y�	Collapsed=��� -
   $ Boton_Gastos_30_dias��$(��"Dinero Gastado los Ultimos 30 dias��$��
43,0,75,23q�$��8��$��36��$��Bottom=�$y�	Collapsed=��� -   $ Boton_Finalizar��$y�	Collapsed=�$��	Finalizar��$��8��$��36(�$��218,6.367,221,17q�$��2���� -   $ Boton_Registrar_Ingreso��$��20(�$-��'Registrar Ingreso o Egreso de Empleados��$��10,0,10,10.11q�$��2��$��1���� -   $��Configurar Ticketera��$��20(�$��1��$y�Visible=�$��
10,0,10,10q�$��1���� -   $ 
btVincular��$��Vinculacion de equipos��$��20(�$��1��$��	10,0,10,8q�$��2���� -   $(��"Copia de seguridad y restauración��$��20(�$��1��$��
10,0,10,10q�$��3���� -    ��Name$ lblVencimiento��
�������$��Center=�$��8��$��Bottom=�$
��0 10q�$��2���� -   $ lblConection��
�������$��Center=�$��0,43,0,0q�$��8��$	��Top=�$��2��  $��Stock��$y�Visible=�.��� -    �Name$ Grid_ControlStock��
 �� �� $��80 ��� $��80 � �� $
��601* ��� $
��189* �.+�&   System.Windows.Controls.DataGrid  -     Name$ Data_Grid_Stock��   AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .     CanUserResizeRows .     ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$
 Auto  � DataContext$ 	articulos��   HeadersVisibility$ Column=�$��10,39,10.086,10q�   
IsReadOnly �� *EmpleadoLogueado.IsShowed+$ VentanaPrincipal��   Columns 0	  *System.Windows.Controls.DataGridTextColumn	  ,
  &System.Windows.Controls.DataGridColumn 
  Header$ Producto��  
  Width$	  50* 1  +System.Windows.Controls.DataGridBoundColumn!   Binding! �� *
Producto+	  $ Codigo��$
   30* " 
  
IsReadOnly" . ! �� *Codigo+	  $ Cantidad��$	  20* " . ! �� *
StrStock+	  $ Precio��$	  20* ! �� *Precio+	  $  Prox. Vencimiento��" . $	  20* ! �� *StrProximoVencimiento+# �� UpdateSourceTrigger$# PropertyChanged=�$   	CellStyle$ �� % �� 
TargetType*  $System.Windows.Controls.DataGridCell"%  & �� Triggers& P� ' P� Property' �@ 
IsSelected( P� Value( .  . ��� ) �� Property) � ��* �� Value
* �����^� + ^� 
Conditions+ �� , �� Binding, �� *ProximoVencimiento+# [ - �� Value- True�� , �� *Vencimientos+# [  	- False.0��� ) � ��
* �  ��� $��10q�$��2��$y�Visible=�$��1�� �� �� .+��� $#��_Nombre o Codigo del Producto�����	#FFDDDDDD$
��Left=�$��0,-1,0,0q�$	��Top=��� -   . ��Name$. txtProductoVale��$
��Left=�$��23��/ �� TextWrapping$/ NoWrap=�$	��Top=�$	��291��$��0,20,0,0q����� *ProductoVale+$ VentanaPrincipal���� -   0 ��Name$0 lsProductosVale��1 K� ZIndex$1 1��2 �� DisplayMemberPath$2 Producto��$
��Left=�$y�	Collapsed=�$��70��$��0,43,0,0q�$	��Top=�$	��290���� $��	_Cantidad�����	#FFDDDDDD$
��Left=�$��0,48,0,0q�$	��Top=��� -   $. txtCantidadVale��$
��Left=�$��23��$/ NoWrap=�$	��Top=�$	��291��$��0,69,0,0q��� $��	_Empleado�����	#FFDDDDDD$
��Left=�$��0,97,0,0q�$	��Top=��� -   3 ��Name$3 brEmpleadoVale��$��1q�$
��Left=�$��-1,118,0,-1q�$	��Top=�$	��293��
���   �$��2��._��� -   4 ��Name$4 cbEmpleadoVale��$2 NombreCompleto������ *	Empleados+$ VentanaPrincipal��$# PropertyChanged=��� $!��Informacion Actual del Vale�����	#FFDDDDDD$
��Left=�$��10,0,0,0q�$	��Top=�$��1���� $��
Producto: ��
�������$��1��$
��Left=�$��	10,31,0,0q�$	��Top=��� -   $ lblProductoVale��
�������$��1��$
��Left=�$��	74,31,0,0q�$	��Top=��� $��
Cantidad: ��
�������$��1��$
��Left=�$��	10,62,0,0q�$	��Top=��� -   $ lblCantidadVale��$�� ��
�������$��1��$
��Left=�$��	73,62,0,0q�$	��Top=��� $��Precio Total: ��
�������$��1��$
��Left=�$��	10,93,0,0q�$	��Top=��� -   $ lblPrecioTotalVale��
�������$��1��$
��Left=�$��	87,93,0,0q�$	��Top=��� -   $! Boton_Ingresar_Pedido_Stock��$y�Visible=�$��Ingresar producto��$��54��$��	0,10,10,0q�$	��169��$��1��$��14(�&   System.Windows.Media.TextOptions5   TextFormattingMode$5 Display=�$��1��$	��Top=�$��Right=��� -   $ Boton_Generar_Vale��$y�Visible=�$��Generar Vale��$��54��$��	0,10,10,0q�$	��169��$��2��$��14(�$5 Display=�$��1��$	��Top=�$��Right=�  $��1��$��9.914,10,10,10q� .  ���� 6 �� Source#6 [  	  $ Nombre��$  * ! �� *
Producto+	  $ Cantidad��$  60 ! �� *CantidadEnvases+�� -   $. txtBusquedaStock��$��
10,10,10,0q�$)��#Buscar producto por nombre o codigo��$/ NoWrap=�$��22��$	��Top=�
���   ����	#FFFFF1F1  $��Ventas��$y�Visible=�.��� -   $ Grid_Ventas��
 �� $
��233* ��� $
��117* ��� $��110.009 � �� $��310.327 ��� �� $	��111 �.+��� $��Total: $��$��1,1,0,1q�
����������	#FFABABAB$��48(�$��2��$��	0,22,0,10q�$��1��$
��Left=�$	��162���� -    $ Label_Total��$��0,1,1,1q�
����������	#FFABABAB$��48(�$��2��$��162,22,10,10q�$��1������ *Total+$ VentanaPrincipal��$# PropertyChanged=��� -!   $. txtBuscarArticulo��$��1,1,1,1q�$��
10,10,10,0q�$)��#Buscar producto por nombre o codigo��$/ NoWrap=�$��22��$	��Top=�
���   ����	#FFFFF1F1� $��3��$��	310,0,0,0q�$��2��  -"   $ Lista_Venta��$��3��$��310,10,10,0q�$ Column=�7   SelectionMode$7 Single=�$ Articulo_Venta�� .  $��2������ *Ventas+$ VentanaPrincipal��$# PropertyChanged=� 	  $ Producto��" . 8 
  CanUserReorder8 .  9 
  CanUserResize9 .  $
  100* ! �� *Descripcion+	  $ Cantidad��" .  8 .  9 .  $	  30* ! �� *
Cantidad+	  $ Precio��" . 8 .  9 .  $	  30* ! �� *Precio_Total+�� $��2q�$��10q�
�������$
��Auto=�$��1��$��2��.���� -#   : ��Name$: Elementos_Virtuales��$��10,10,10,10q�; �� CanVerticallyScroll; . < �� Orientation$< Vertical=�  -$   $ 
DgBusqueda��=   CanUserSortColumns= .  .  $��3��$��10,32,10.327,10q�$ Column=� .   .   .   . ���� *	articulos+$ VentanaPrincipal�� 	  8 .  $ Codigo��$  2* ! �� *Codigo+	  8 .  $ Producto��$  4* ! �� *
Producto+	  8 .  $ Precio��$  2* ! �� *Precio+�� -%   $ Boton_Terminar_Venta��$��2��$��10,22,12,10q�$��2��  Button_Tilde#���    $
��Caja��$y�Visible=�.��� -&   $ 	Grid_Caja�� �� �� 
 �� $	��79* ��� $	��79* ��� $	��79* ��� $	��40* ��� $	��39* ��� $	��79* ��� $	��79* �.+��� -'   $ Boton_Finalizar_Turno��$��1��$��15,20,15,41q�$��5��$��2��$��36(�
���   �.��(� > (� Stretch$> Uniform=�$
��Left=�.���� $��Finalizar Turno���� $��15,26q�
����������	#FFABADB3? �� Padding$? 0q�$��1q�._�(� $> Uniform=�$
��Left=�.��]� ��. $��Stretch=�$��Right=�.+��� -(   $. txtFCRecibido��$��0,0,1,0q�$��00,00��$��Right=�$��Stretch=�$/ NoWrap=�$��292.5��$��Center=��� -)   $��Fondo de caja recibido:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��14,26q�
����������	#FFABADB3$��1q�$��1��._�(� $> Uniform=�$
��Left=�.��]� ��. .+��� -*   $. txtFCEntregado��$��00,00��$��Right=�$��Stretch=�$/ NoWrap=�$	��282��$��1,0,1,0q�$��Center=��� -+   $��Fondo de caja entregado:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��15,26q�
����������	#FFABADB3$��1q�$��0��$��1��._�(� $> Uniform=�$
��Left=�.��]� ��. .+��� -,   $. txtEfectivo��|�.  $��Right=�$��Stretch=�$/ NoWrap=�$	��345��$��1,0,1,0q����� *EfectivoRecibido+$ VentanaPrincipal��$# PropertyChanged=��� --   $��Efectivo recibido:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��15,26q�
����������	#FFABADB3$��1q�$��0��$��2��._�(� $> Uniform=�$
��Left=�.��]� ��. .+��� -.   $. 	txtDebito��|�.  $��Right=�$��Stretch=�$/ NoWrap=�$	��374��$��0q�
���   ����� *DebitoRecibido+$ VentanaPrincipal��$# PropertyChanged=��� -/   $��Debito recibido:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��15,26q�
����������	#FFABADB3$��1q�$��1��$��2��@ J� RenderTransformOrigin$@ 0.5,0.5&�._�(� $> Uniform=�$
��Left=�.��]� ��. $
��Left=�.+��� -0   $. 
txtSueldos��|�.  $��Right=�$��Stretch=�$/ NoWrap=�$	��352��$��1,0,1,0q��� -1   $��Sueldos o bonos:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��15,3.25,15,10q�
����������	#FFABADB3$��1q�$��0��$��4��._�(� $> Uniform=�$
��Left=�.��]� ��. .+��� -2   $. txtDeposito��|�.  $��Right=�$��Stretch=�$/ NoWrap=�$	��432��$��1,0,1,0q�$��Center=����� *	Depositos+$ VentanaPrincipal���� -3   $��Depósitos:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��15,26q�
����������	#FFABADB3$��1q�$��1��$��1��._�(� $> Uniform=�$
��Left=�.��]� ��. .+��� -4   $. txtPagoProveedores��$��00,00��$��Right=�$��Stretch=�$/ NoWrap=�$	��316��$��1,0,1,0q�$��Center=��� -5   $��Pagos a proveedores:��$��18(�$��DemiBold"�$��Center=�$��Center=��� $��15,3.25,15,10q�
����������	#FFABADB3$��1q�$��1��$��4��._�(� $> Uniform=�$
��Left=�.��]� ��. .+��� -6   $. txtExtracciones��|�.  $��00,00��$��Right=�$��Stretch=�$/ NoWrap=�$	��405��$��1,0,1,0q�$��Center=��� -7   $��Extracciones:��$��18(�$��DemiBold"�$��Center=�$��Center=��� -8   $��93,20q�$��Recaudacion Total:�����	#FFDDDDDD$��24(�$��Center=�$��DemiBold"�$��Center=�$��5���� -9   $. txtRecaudacion��$��Center=�$��Center=�|�.  $��6��$/ NoWrap=�$��
15,0,16,43q����� *RecaudacionTotal+$ VentanaPrincipal��A �� StringFormat$A {0:C}���� $��15,12,125,1.75q�
����������	#FFABADB3$��1q�$��0��$��3��._�(� $> Uniform=�$
��Left=�.��]� ��. .+�%  Xceed.Wpf.Toolkit.DecimalUpDown  -:   B  Name$B txtRealizarDeposito��$��Right=��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]C   ShowButtonSpinnerC .  D   	AllowSpinD .  E   ClipValueToMinMaxE . F   Minimum$F 0X�$
��Left=�$	��207��G   MaximumG �� *	Content+$ label���� -;   $��Depositar dinero:��$��18(�$��DemiBold"�$��Center=�$��Center=��� -<   $��	0,12,15,3q�$��3��$��Right=�$	��105��.��(� $> Uniform=�.���� -=   $��Realizar deposito���� $��15,12,132,1.75q�
����������	#FFABADB3$��1q�$��1��$��3��._�(� $> Uniform=�$
��Left=�.��]� ��. .+�  ->   $B txtRealizarExtraccion��$��Right=�C .  D .  E . $F 0X�$
��Left=�$	��226��G �� *
DineroCaja+$ VentanaPrincipal���� -?   $��Extraer dinero:��$��18(�$��DemiBold"�$��Center=�$��Center=��� -@   $��1��$��Right=�$��	0,12,15,2q�$��3��$	��112��.��(� $> Uniform=�.���� -A   $��Realizar extraccion���� !  DragHelper.EnableDragHelperH   
EnableDragH . 
������ 
������ $��27��$	��Top=�$��	521,0,0,0q�._��� -B   $ lblEmpleadoLogueado��$��Right=�$��0,0,85,0q�$�� ��
���������Z� �� $y�	Collapsed=�H . 
�������$��1q�$��25��$��25��$��	0,0,100,0q�$��Right=�$	��Top=�._��� -C   I ��Name$I FotoEmpleadoLogueado������ *EmpleadoLogueado.FotoPerfil+# [ $ VentanaPrincipal���� H . 
������ 
������ $��27��$	��Top=�$��0,-1,0,0q�$
��Left=�$��50��� J � Points%J �   ������@x}@�� h�	
������ 
������ H . � %J �    h�	��!
�� ��
 h�	
������ 
������ H . � %J �   @�E@�����W�@�E
������ 
������ H . � %J �   ��> �����p��>
������ 
������ H . � %J �   @�@�@x}@� �`!
������ 
������ H . %  ControlDeVentana.ControlVentana  -D   K  Name$K ctrlVentana��$��Right=�$	��Top=�$��25��$��75��