   M S B A M L   `   `   `  ���� M  ISupervisorComercio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:DragHelper
DragHelper  K GControlDeVentana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:ControlDeVentana;assembly=ControlDeVentanaControlDeVentana S.clr-namespace:SupComercio.Resources.Converters SupComercio.Resources.Converters  '    !SupComercio.Ventanas.VentanaPagos   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )dragclr-namespace:DragHelper     UControlDeVentana8clr-namespace:ControlDeVentana;assembly=ControlDeVentana     X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  ?
Converters.clr-namespace:SupComercio.Resources.Converters  -     �Name$  VentanaPago�� � SizeToContent$ Height=� � Title$ Ventana Pagos��$
��Auto��$	��800�� � WindowStyle$
 None=� � AllowsTransparency .  � 
ResizeMode$ NoResize=� � Icon$7 1/SupervisorComercio;component/Resources/icono.ico�� � 	Resources >   8SupComercio.Resources.Converters.InverseBooleanConverter   InverseBooleanConverter   BooleanToVisibilityConverter9   3SupComercio.Resources.Converters.BooleanOrConverter  BooleanOrConverter��%-   &	        &	      &	 
       ��   .��� $
��Auto��$
��Auto�� � RowDefinitions �� $��25 ��� $��190.018 ��� $
��Auto ��� $	��109 �	 � ColumnDefinitions	 �� �� ���� 
 �� ImageSource$C
 =/SupervisorComercio;component/Resources/Fondo de software.png�� j� Stretch$ UniformToFill=�.+��� !   DragHelper.EnableDragHelper   
EnableDrag . $��30��$	��Top=����	#FF2D2D30$��2��%  ControlDeVentana.ControlVentana  $��25��$��Center=�$��Right=�$��83��$��1���� $��	Paga Con:��$
��Left=�$��Center=�$��Center=�$��	10,10,0,0q�$	��Top=�$��83��$	��780��$��48(�$��2��$��1��d� -    d�Name$ Boton_Pago_Debito��$��Debito��$��Center=�$��10,108,10,0q�$	��Top=�$��72��$	��380��$��48(�$��1��d� -   $ Boton_Pago_Efectivo��$��Efectivo��$��Center=�$��10,108,10,0q�$	��Top=�$��72��$	��380��$��48(�$��1��$��1�� d� 	IsChecked �� *	IsChecked+ �� 	Converter# [   �� ElementName$ Boton_Pago_Debito���� -    ��Name$ Boton_Finalizar��$��	Finalizar��$��
10,0,10,10q�$��Bottom=�$��72��$��48(�$��3�� �� 	IsDefault . {�`�  `� 	Converter# [ .1��� $ Boton_Pago_Debito�� �� Path$ 	IsChecked��� $ Boton_Pago_Efectivo��$ 	IsChecked�� -    �Name$ Grid_Pago_Efectivo��$��2��$��2��y��� *	IsChecked+# [ $ Boton_Pago_Efectivo��	 �� �� .+�]� ��. $��
10,10,10,0q�$��60��$	��Top=�.+��� -    ��Name$ Label_Paga_Con��
�������$��Paga con: $��$��1,1,0,1q�
�������$��Center=�$��24(�$
��Left=�$	��135���� -    ��Name$ Box_Paga_Con��$��00,00�� �� SelectionBrush
 ����$��0,1,1,1q�
�������$
��Left=�$��Center=�$��24(�]� $��1����. $��
10,10,10,0q�$��60��$	��Top=�.+��� $��Vuelto:     $��
�������$��1,1,0,1q�
�������$��60��$��24(�$��Center=�$	��135���� -   $ label_Vuelto��$��00,00��
�������$��0,1,1,1q�
�������$��24(�$��Center=��� -	   $ Boton_Cancelar��$��Cancelar��$��
10,0,10,10q�$��Bottom=�$��72��$��48(�$��3��$��1�� �� IsCancel . 