   M S B A M L   `   `   `  ���� M  ISupervisorComercio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SupComercio.VentanasSupComercio.Ventanas  ?$clr-namespace:SupComercio.PropertiesSupComercio.Properties  'clr-namespace:DragHelper
DragHelper  K GControlDeVentana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:ControlDeVentana;assembly=ControlDeVentanaControlDeVentana S.clr-namespace:SupComercio.Resources.Converters SupComercio.Resources.Converters  )    #SupComercio.Ventanas.IngresarPedido   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:SupComercio.Ventanas     4res$clr-namespace:SupComercio.Properties     )dragclr-namespace:DragHelper     UControlDeVentana8clr-namespace:ControlDeVentana;assembly=ControlDeVentana     E
Converters.clr-namespace:SupComercio.Resources.Converters     -     � SizeToContent$  Height=� � Title$ Ingresar_Pedido��$��400.5�� � AllowsTransparency .  � WindowStyle$
 None=� � 	Resources ;   5SupComercio.Resources.Converters.IndexToBoolConverter   IndexToBoolConverter   BooleanToVisibilityConverter��%   &	        &	        �� .���  � RowDefinitions �� $��25 ��� ����  �� ImageSource$C =/SupervisorComercio;component/Resources/Fondo de software.png�� j� Stretch$ UniformToFill=�.+��� !   DragHelper.EnableDragHelper   
EnableDrag . $��25��$	��Top=����	#FF2D2D30%  ControlDeVentana.ControlVentana  $
��Left=�$��25��$��	323,0,0,0q�$	��Top=�	   HaveMaximize	 .  $��77��� -   
 �Name$
 gd��$��1�� �� $��68 ��� $��55 ��� $��53.75 ��� $��54 ��� $
��Auto ��� $
��Auto ��� $��50 � � ColumnDefinitions �� $
��350* ��� $	��69* ��� $
��281* �.+��� -    ��Name$ Boton_Aceptar��$��Aceptar��$��7��$��2��$��1��$��9.75,0,10.5,10q�$��18(�$��28��$��Bottom=��� -   $ Boton_Ingresar_Otro��$��Ingresar Otro��$��7��$��18(�$��10,0,10.25,10q�$��28��$��Bottom=��� -    ��Name$ Box_Nombre_Producto��InitNomb�� �� TextWrapping$
 Wrap=�$��10,10,10.5,10q�$��24(�$��Center=�$��Center=�$��3��$��Nombre���� -   $ 
Box_Codigo��$��10,10,9.728,10q�$��Codigo��$
 Wrap=�$��1��$��Center=�$��Center=�$��18(�$��2��]� $��0��$��2����. $��2��$��10,10,9.728,10.75q�$��1��.+��� -   $ Box_Cantidad��$��0,0,4,0q�$��Cantidad��$ NoWrap=�$��2��$��18(�$��Center=�$��Center=�$��2��$	��123��{��� *SelectedIndex+ �� ConverterParameter$ 0�� �� 	Converter# [   �� ElementName$ Combo_Tipo_Cantidad���� -    ��Name$ Combo_Tipo_Cantidad�� �� SelectedIndex$ 0��$��15(�$��Center=�.@��� $��Monto���� $��Peso(Kg)���� $��Unidades��]� $��2��$��1��$��2����. $��10.272,10,10.5,10.75q�.+��� -	    ��Name$ Label_Precio��$��$��$��0q�
�������$��1,1,0,1q�$��0q�$��36(�$��Center=�$��30�����	#FFABADB3�� -
   $ 
Box_Precio��$��2��$��0q�$��1��$��00,00��$
 Wrap=�$��0,1,1,1q�$��2��$��36(�$��Center=� �� SelectionBrush �	#FFABADB3�� -    ��Name$ List_Nombres�� �� DisplayMemberPath$ Producto��$
��Left=�$y�	Collapsed=�$��	10,58,0,0q�$	��Top=�$	��380��$��3��$��3���� -   $ txtProveedor��$��10,10.152,10.5,9.848q�$��18(�$��Center=�$��Center=�$��0q�$��3��$��3��$
 Wrap=�$��	Proveedor���� $��4��$��3��.+��� -    ��Name$ checkBox��
�������$��¿Producto retornable?��$��10q��� -   $ txtCantidadEnvases��$
��10 0q�y��� *	IsChecked+# [ $ checkBox���� -   $ Lista_Codigo��$ Producto��$
��Left=�$y�	Collapsed=�$��90��$��	10,45,0,0q�$��1��$��3��$	��Top=�$	��220��$��2���� $��5��$��3��.+��� -   $ checkBoxVencimiento��
�������$!��¿Producto con vencimiento?��$��10q�&   System.Windows.Controls.Calendar  -     Name$ 
Calendario��y��� *	IsChecked+# [ $ checkBoxVencimiento��