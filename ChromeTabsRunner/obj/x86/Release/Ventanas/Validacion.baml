   M S B A M L   `   `   `  ���� M  ISupervisorComercio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SupComercio.VentanasSupComercio.Ventanas  K GControlDeVentana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:ControlDeVentana;assembly=ControlDeVentanaControlDeVentana 'clr-namespace:DragHelper
DragHelper  %    SupComercio.Ventanas.Validacion   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:SupComercio.Ventanas     N	Controles8clr-namespace:ControlDeVentana;assembly=ControlDeVentana     )dragclr-namespace:DragHelper     X TXceed.Wpf.Toolkit, Version=3.8.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  -     �Name$  window�� � AllowsTransparency .  � WindowStyle$
 None=� � Title$ 
Validacion��$��737.591�� � SizeToContent$ Height=� � 	Resources     BooleanToVisibilityConverter��%   &	        �� .���  � RowDefinitions �� $��25 ��� ����  �� ImageSource$C =/SupervisorComercio;component/Resources/Fondo de software.png�� j� Stretch$ UniformToFill=�.+��� !   DragHelper.EnableDragHelper	   
EnableDrag	 . $��25��$	��Top=����	#FF2D2D30$��2��%  ControlDeVentana.ControlVentana  $��Right=�$��25��$	��Top=�
   HaveMaximize
 .  $��75���� $��Nombre del Comercio:��$��18(�
�������$
��Left=�$��	10,10,0,0q�$��1��$	��Top=��� -    ��Name$ 	txtNombre��$
��Left=�$��18(�$��	10,44,0,0q�$��1�� �� TextWrapping$
 Wrap=�$	��Top=�$	��718���� $��Direccion del Comercio��$��18(�
�������$
��Left=�$��	10,75,0,0q�$��1��$	��Top=�
������ �� -   $ txtDireccion��$
��Left=�$��18(�$��
10,109,0,0q�$��1��$ NoWrap=�$	��Top=�$	��718���� $��Código de Licencia��$��18(�
�������$
��Left=�$��
10,140,0,0q�$��1��$	��Top=�
������ �� -   $ txtLicencia��$
��Left=�$��18(�$��
10,174,0,0q�$��1��$ NoWrap=�$	��Top=�$	��718���� -    ��Name$ 
btCancelar��$��Cancelar��$��Right=�$��	0,0,10,10q�$��1��$��Bottom=�$	��164���� -   $ 	btAceptar��$��Aceptar��$��Right=�$��
0,0,183,10q�$��1��$��Bottom=�$	��164���� -    ��Name$ lblError��$�� ��
���  ��$
��Left=�$��10,205,0,10q�$��1���� -   $ 	btValidar��$��Validar��$��Right=�$��
0,0,352,10q�$��1��$��Bottom=�$	��164��