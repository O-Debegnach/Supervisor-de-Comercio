   M S B A M L   `   `   `  ���� M  ISupervisorComercio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SupComercio.VentanasSupComercio.Ventanas  'clr-namespace:DragHelper
DragHelper  K GControlDeVentana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:ControlDeVentana;assembly=ControlDeVentanaControlDeVentana 1    +SupComercio.Ventanas.ConfiguracionTicketera   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:SupComercio.Ventanas     )dragclr-namespace:DragHelper     N	Controles8clr-namespace:ControlDeVentana;assembly=ControlDeVentana       � WindowStyle$
  None=� � AllowsTransparency .  � Title$ ConfiguracionTicketera�� � SizeToContent$ WidthAndHeight=�$��126.334��.���  � RowDefinitions �� $��25 ��� $��59 ��� $��0 ��� $��40 �����  �� ImageSource$C =/SupervisorComercio;component/Resources/Fondo de software.png�� j� Stretch$ UniformToFill=�.+��� !   DragHelper.EnableDragHelper   
EnableDrag . $��25��$	��Top=����	#FF2D2D30%  ControlDeVentana.ControlVentana  -     Name$ ctrlVentana��$��Right=�	   HaveMaximize	 .  $��75���� $��
_Impresora��
�������$
��Left=�$��	10,10,0,0q�$��1��$	��Top=�
 �� Target
 ��  �� ElementName$ txtNOmbreTicketera���� $(��"_Cantidad de Caracteres por Linea:��$��Center=�
�������$
��Left=�$��10q�$��2��$	��Top=�$y�	Collapsed=�
 �� $ txtCaracteres���� -    ��Name$ txtCaracteres��$
��Left=�$��24��$��Center=�$��
200,10,0,0q�$��2�� �� TextWrapping$
 Wrap=�$	��Top=�$	��142��$y�	Collapsed=��� -   $��Aceptar��$
��Left=� �� 	IsDefault . $��
187,10,0,0q�$��3��$	��Top=�$��75���� -   $��Cancelar��$��267,10,10,10q�$��3�� �� IsCancel . �� -    ��Name$ cbImpresora��$��
10,38,10,0q�$��1��$	��Top=�