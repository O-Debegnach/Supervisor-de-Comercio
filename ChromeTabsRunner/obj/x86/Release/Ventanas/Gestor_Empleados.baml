   M S B A M L   `   `   `  ���� M  ISupervisorComercio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SupComercio.VentanasSupComercio.Ventanas  'clr-namespace:DragHelper
DragHelper  K GControlDeVentana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:ControlDeVentana;assembly=ControlDeVentanaControlDeVentana S.clr-namespace:SupComercio.Resources.Converters SupComercio.Resources.Converters  I EBusiness_Layer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ9clr-namespace:Business_Layer.Text;assembly=Business_LayerBusiness_Layer.Text *    $SupComercio.Ventanas.GestorEmpleados   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:SupComercio.Ventanas     )dragclr-namespace:DragHelper     UControlDeVentana8clr-namespace:ControlDeVentana;assembly=ControlDeVentana     X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  ?
Converters.clr-namespace:SupComercio.Resources.Converters  GtxtMode9clr-namespace:Business_Layer.Text;assembly=Business_Layer  -     �Name$  window�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � AllowsTransparency .  � Title$ GestorEmpleados��$	��724��$	��917�� � 	Resources ;   5SupComercio.Resources.Converters.IndexToBoolConverter   IndexToBoolConverterB   <SupComercio.Resources.Converters.EqualIndexesToBoolConverter  EqualIdexesToBoolConverter;   5SupComercio.Resources.Converters.MayorToBoolConverter  MayorToBoolC   =SupComercio.Resources.Converters.BitmapToImageSourceConverter   BitmapToImageSourceConverter��%<   &	        &	      &	 
     &	              .���  � RowDefinitions �� $��25 ��� $
��339* ��� $	��86* �����  �� ImageSource$C =/SupervisorComercio;component/Resources/Fondo de software.png�� j� Stretch$ UniformToFill=�.+��� !   DragHelper.EnableDragHelper	   
EnableDrag	 . $��25��$	��Top=����	#FF2D2D30%  ControlDeVentana.ControlVentana  -   
  Name$
 ctrlVentana��$��Right=�   HaveMaximize .  � -    �Name$ GdListadoEmpleados��$��1��$��2�� � ColumnDefinitions �� $	��400���� $	��400����  �� $
��128* ��� $	��35* ��� $��90 �.+�&   System.Windows.Controls.DataGrid  -     Name$ DGEmpleados��$��	10,10,7,2q�   
IsReadOnly .    AutoGenerateColumns .  $��2������ *ListaEmpleados+ �� ElementName$ window�� �� UpdateSourceTrigger$ PropertyChanged=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,	  &System.Windows.Controls.DataGridColumn 	  Header$ Nombre�� 	  Width5
  /System.Windows.Controls.DataGridLengthConverter$ 5*
 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Nombre+  $ Apellido��$ 3*
  �� *
Apellido+�� -    ��Name$ FotoEmpleado��$
��Left=�$	��100��$��2,10,0,0q�$	��Top=�$	��100��$��1��$��UniformToFill=����� *Imagen+$ window��$ PropertyChanged=� �� 	Converter# [ �� $��Nombres:��
�������$��DemiBold"�$��16(�$��1��$
��Left=�$��
107,10,0,0q�$	��Top=�$��31��$��81���� 
�������$��DemiBold"�$��16(�$��1��$��
189,10,0,0q�$	��Top=�$��31������ *Nombre+$ window��$ PropertyChanged=��� $��	Apellido:��
�������$��DemiBold"�$��16(�$��1��$
��Left=�$��
107,41,0,0q�$	��Top=�$��31��$��74���� 
�������$��DemiBold"�$��16(�$��1��$��
181,41,0,0q�$	��Top=�$��31������ *
Apellido+$ window��$ PropertyChanged=��� $��Cuil:��
�������$��DemiBold"�$��16(�$��1��$
��Left=�$��
107,72,0,0q�$	��Top=�$��31��$��41���� 
�������$��DemiBold"�$��16(�$��1��$��
148,72,0,0q�$	��Top=�$��31������ *Cuil+$ window��$ PropertyChanged=��� $��Precio Hora:��
�������$��DemiBold"�$��16(�$��1��$
��Left=�$��	2,115,0,0q�$	��Top=�$��31��$��99���� 
�������$��DemiBold"�$��16(�$��1��$��101,115,0,0q�$	��Top=�$��31������ *
PrecioHora+$ window��$ PropertyChanged=��� $��Horas Trabajadas:��
�������$��DemiBold"�$��16(�$��1��$
��Left=�$��	2,177,0,0q�$	��Top=�$��31��$	��138���� 
�������$��DemiBold"�$��16(�$��1��$��140,177,0,0q�$	��Top=�$��31������ *HorasTrabajadas+$ window��$ PropertyChanged=��� $��Sueldo:��
�������$��DemiBold"�$��16(�$��1��$
��Left=�$��	2,146,0,0q�$	��Top=�$��31��$��64���� 
�������$��DemiBold"�$��16(�$��1��$��
66,146,0,0q�$	��Top=�$��31������ *Sueldo+$ window�� �� NotifyOnSourceUpdated .  �� StringFormat$ {0:F2}���� $��Datos de Contacto��
�������$��DemiBold"�$��16(�$��1��$
��Left=�$��	2,224,0,0q�$	��Top=�$��31��$	��145���� $��Telefono Fijo:��
�������$��DemiBold"�$��16(�$��1��$
��Left=�$��	2,260,0,0q�$	��Top=�$��31��$	��107���� 
�������$��DemiBold"�$��16(�$��1��$��109,260,0,0q�$	��Top=�$��31������ *
Telefono+$ window��$ PropertyChanged=��� $��Celular:��
�������$��DemiBold"�$��16(�$��1��$
��Left=�$��	2,291,0,0q�$	��Top=�$��31��$��65���� 
�������$��DemiBold"�$��16(�$��1��$��
67,291,0,0q�$	��Top=�$��31������ *	Celular+$ window��$ PropertyChanged=��� $��Email:��
�������$��DemiBold"�$��16(�$��1��$
��Left=�$��	2,322,0,0q�$	��Top=�$��31��$��53���� 
�������$��DemiBold"�$��16(�$��1��$��
55,322,0,0q�$	��Top=�$��31������ *Email+$ window��$ PropertyChanged=��� $��
Domicilio:��
�������$��DemiBold"�$��16(�$��1��$
��Left=�$��	2,353,0,0q�$	��Top=�$��31��$��83���� 
�������$��DemiBold"�$��16(�$��1��$��
85,353,0,0q�$	��Top=�$��31������ *	Domicilio+$ window��$ Explicit=��� -    ��Name$ btEditar��$��Editar Empleado��$��	10,0,9,35q�$��20��$��Bottom=�$��2��{��� *SelectedIndex+# [  $ DGEmpleados���� -   $ 
btDespedir��$��Despedir Empleado��$��	10,0,9,10q�$��20��$��Bottom=�$��2��{��� *SelectedIndex+# [  $ DGEmpleados���� -   $ brCrear��$��Agregar Nuevo Empleado��$��	10,0,9,60q�$��20��$��Bottom=�$��2��  -	   $ DgVales�� .  $��1��$��10.333,30,9,10q�$ �� IsSynchronizedWithCurrentItem$
 TrueY�   CanUserDeleteRows .  . $��2��$��2��$��1������ $ window�� �� Path$ Vales�$ PropertyChanged=�   $ Producto��$
 1.5*
  �� *
Producto+   ClipboardContentBinding �� *
Producto+  $ Cantidad��$ *
  �� *
Cantidad+ �� *
Cantidad+  $ Precio��$ *
  �� *Precio+ �� *Precio+'  !ControlDeVentana.PlanillaHorarios  -
      Name$  PlanillaInfo��!   IntervaloHoras$! 1��$��8(�$��2��$��10,10,9.5,0q�
��������� -   $ BtPagarSueldo��$��Pagar sueldo��$��1��$��10.333,5,10.333,0q�$��1��$	��Top=�{��� *SelectedIndex+# [  $ DGEmpleados��� -   $ GdNuevoEmpleado��$��1��$y�	Collapsed=�$��2�� �� $	��146 ��� $	��146 ��� $	��294 ���  �� $
��205* ��� $
��334* ��� $��40 �.+�� $��2��$��10q�.+��� -   " ��Name$" SpSeleccion��# �� Orientation$# Vertical=�$	��Top=�$��0,21,0,0q�.+��� -   $ btSacarFoto��$��
Tomar foto��$��5q�{��� *IsCameraFinded+$ WebCam���� -   $ btDesdeArchivo��$��Cargar foto��$	��5 0q�$  ControlDeVentana.WebCamControl  -   $  Name$$ WebCam��$	��170��$	��224��$y�	Collapsed=�� -   $ BFoto��$y�	Collapsed=� �� �� $��32 �.+��� $��1q�
���   �._��� -   $
 Foto��$��UniformToFill=����� *Imagen+# [ $ window���� -   $ btDescartar��$��1��$��	Descartar��$��0,10,0,0q��� $��*_Nombre��$��DemiBold"�$
��Left=�$	��Top=�$��	11,10,0,0q�$��2�����	#FFDDDDDD$��26��$��60��% �� Target% �� $ 	txtNombre���� -   & ��Name$& 	txtNombre��$��23��$��
10,36,15,0q�' �� TextWrapping$
' Wrap=�$	��Top=�$��2������ *Nombre+$ window���� $��
*_Apellido��$��DemiBold"�$
��Left=�$��	11,59,0,0q�$	��Top=�$��2�����	#FFDDDDDD$��26��$��61��% �� $ txtApellido���� -   $& txtApellido��$��23��$��
10,85,15,0q�$
' Wrap=�$	��Top=�$��2������ *
Apellido+$ window���� $��*_Cuil��$��DemiBold"�$
��Left=�$��
11,108,0,0q�$	��Top=�$��2�����	#FFDDDDDD$��26��$��36��% �� $ txtCuil���� -   $& txtCuil��$��23��$��10,134,15,0q�$
' Wrap=�$	��Top=�$��2������ *Cuil+$ window���� $��*Ca_rgo��$��DemiBold"�$
��Left=�$��
11,157,0,0q�$	��Top=�$��2�����	#FFDDDDDD$��26��$��48��% �� $ txtCargo���� -   $& txtCargo��$��23��$��10,183,15,0q�$
' Wrap=�$	��Top=�$��2��$��2������ *Cargo+$ window���� $��*_Precio Hora��$��DemiBold"�$
��Left=�$	��Top=�$��11,6,0,0q����	#FFDDDDDD$��26��$��2��$��1��$��79��% �� $ txtPrecioHora���� -   $& txtPrecioHora��$��22��$��
10,32,15,0q�$
' Wrap=�$	��Top=�$��2��$��1������ *
PrecioHora+$ window���� $��Datos de Contacto��$
��Left=�$��	10,32,0,0q�$	��Top=����	#FFDDDDDD$��26��$	��109��$��1���� $��_Telefono Fijo��$
��Left=�$��	10,62,0,0q����	#FFDDDDDD$��77��$��1��$��26��$	��Top=�% �� $ txtTelefono���� -   $& txtTelefono��$��24��$��
10,88,10,0q�$
' Wrap=�$	��Top=�$��1��$��2������ *
Telefono+$ window���� $��_Celular��$
��Left=�$��
10,116,0,0q�$	��Top=����	#FFDDDDDD$��26��$��47��$��1��% �� $ 
txtCelular���� -   $& 
txtCelular��$��24��$��10,142,10,0q�$
' Wrap=�$	��Top=�$��1��$��2������ *	Celular+$ window���� $��_E-mail��$
��Left=�$��	10,62,0,0q�$	��Top=����	#FFDDDDDD$��2��$��26��$��43��$��1��% �� $ txtEmail���� -   $& txtEmail��$��24��$��
10,88,13,0q�$
' Wrap=�$	��Top=�$��2��$��1������ *Email+$ window���� $��*_Domicilio��$
��Left=�$��
10,116,0,0q�$	��Top=����	#FFDDDDDD$��2��$��26��$��65��$��1��% �� $ txtDomicilio���� -   $& txtDomicilio��$��24��$��10,142,13,0q�$
' Wrap=�$	��Top=�$��2��$��1������ *	Domicilio+$ window���� -   $ 	btAceptar��$��Aceptar��$��10q�$��2���� -   $ 
btCancelar��$��Cancelar��$��10q�$��2��$��1���� $ ��* Indica campo obligatorio��$��Center=�$��
67,0,75,10q����	#FFFF4F4F$��24��$��Bottom=�$��2��$��2��$	��152��  -   $  Planilla��(   Fill
( �   �$��14(�
���   �
�������$��
10,10,10,0q�$��3��$��2���� $��Entrada��$��DemiBold"�$	��Top=�$��10,178,15,0q����	#FFDDDDDD$��26��$��2��$��1��&   Xceed.Wpf.Toolkit.TimeSpanUpDown  -    )  Name$) 	tpEntrada���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.TimeSpan, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]*   UpdateValueOnEnterKey* .  $��2���  �Xceed.Wpf.Toolkit.Primitives.DateTimeUpDownBase`1[[System.Nullable`1[[System.TimeSpan, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]+   CurrentDateTimePartt$+ Minute=�$��Right=�$��
0,204,15,0q�$	��Top=�$	��133��,  &Xceed.Wpf.Toolkit.Primitives.InputBase,   TextAlignment$, Center=�-   Minimum$- 0X�.   Maximum$. 23:59:59X�/   DefaultValue$/ 12:0:0X�%0   DisplayDefaultValueOnEmptyText0 . 1   ShowDays1 .  2   ShowSeconds2 .  $��1��$��22��3   AllowTextInput3 .  �� -!   4 ��Name$4 cbDiaEntrada��$��2��$��10,204,153,0q�$	��Top=�$��1��$��22��5 �� SelectedIndex$5 0��.@��� $��Lunes���� $��Martes���� $��	Miercoles���� $��Jueves���� $��Viernes���� $��Sabado���� $��Domingo���� $��Salida��$��DemiBold"�$
��Left=�$	��Top=�$��
10,232,0,0q����	#FFDDDDDD$��26��$��2��$��1��$��43��  -"   $) tpSalida��3 .  $��2��$+ Minute=�$��Right=�$��
0,258,15,0q�$	��Top=�$	��133��$, Center=�$. 23:59:59X�$/ 12:0:0X�0 . 2 .  1 .  $��1��$��22��6   Value$6 12:0:0X�7   ClipValueToMinMax7 . ���� 8 �� 
TargetType"8  .��� 9 �� Property9 �@ Minimum: �� Value$: 00:00:00X�; �� Triggers; �� < �� Value< True= �� Binding= `� > `� 	Converter#> [ .1��� $ cbDiaSalida��$ SelectedIndex��� $ cbDiaEntrada��$ SelectedIndex�.a��� 9 �@ Minimum: �� $ 	tpEntrada��$ Value��� -#   $4 cbDiaSalida��$��2��$��10,258,153,0q�$	��Top=�$��1��$��22��$5 0��.@��� $��Lunes���� $��Martes���� $��	Miercoles���� $��Jueves���� $��Viernes���� $��Sabado���� $��Domingo���� -$   $ btAgregarHorario��$��Agregar a Horario��$��2��$��10,295,15,0q�$	��Top=�$��1��$��20��{�`� #> [ .1��� $ tpSalida��$ Value��� $ 	tpEntrada��$ Value��� $ cbDiaSalida��$ SelectedIndex��� $ cbDiaEntrada��$ SelectedIndex��� -%   $4 cbLimpiarHorario��$��2��$��10,367,153,0q�$	��Top=�$��1��$��22��$5 0��.@��� $��Lunes���� $��Martes���� $��	Miercoles���� $��Jueves���� $��Viernes���� $��Sabado���� $��Domingo���� $��Limpiar Horario��
�������$��DemiBold"�$��2��$
��Left=�$��
10,336,0,0q�$��1��$	��Top=��� -&   $ btLimpiarHorario��$��Limpiar Hoario��$��2��$
��Left=�$��146,367,0,0q�$��1��$	��Top=�$	��133��$��22��