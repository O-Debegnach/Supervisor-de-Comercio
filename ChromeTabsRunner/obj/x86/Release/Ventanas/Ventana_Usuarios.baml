   M S B A M L   `   `   `  ���� M  ISupervisorComercio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SupComercio.VentanasSupComercio.Ventanas  'clr-namespace:DragHelper
DragHelper  K GControlDeVentana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:ControlDeVentana;assembly=ControlDeVentanaControlDeVentana *    $SupComercio.Ventanas.VentanaUsuarios   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:SupComercio.Ventanas     )dragclr-namespace:DragHelper     UControlDeVentana8clr-namespace:ControlDeVentana;assembly=ControlDeVentana     -     �Name$  Ventana�� � WindowStyle$
 None=� � AllowsTransparency .  � SizeToContent$ WidthAndHeight=� � Title$ Usuarios��$	��389�� � Topmost . .���  � RowDefinitions �� $��25 ��� ����  �� ImageSource$C =/SupervisorComercio;component/Resources/Fondo de software.png�� j� Stretch$ UniformToFill=�.+��� $��25��$	��Top=����	#FF2D2D30$��2��%  ControlDeVentana.ControlVentana  $��Right=�$��25��$	��Top=�	   HaveMaximize	 .  $��75��� -   
 �Name$
 
GdRegistro��$��1�� �� �� $��35 �.+��� 
���   �$��1q�$	��102��$��	30,30,0,0q�$
��Left=�$	��Top=�$	��102��._��� -    ��Name$ imgFoto��$��UniformToFill=��� $��Empleado Asociado��$��DemiBold"�$
��Left=�$��
137,30,0,0q�$	��Top=�$	��121�����	#FFDDDDDD�� -    ��Name$ cbEmpleados�� �� DisplayMemberPath$ Nombre��$
��Left=�$��137,56,30,0q�$	��Top=�$	��223������ *	Empleados+ �� ElementName$ Ventana���� $��Nombre de Usuario��$��DemiBold"�$
��Left=�$��
137,83,0,0q�$	��Top=�$	��121�����	#FFDDDDDD�� -    ��Name$ 
txtUsuario��$
��Left=�$��23�� �� TextWrapping$
 Wrap=�$	��Top=�$	��223��$��137,109,30,0q����� *	Usuario+$ Ventana���� $��Contraseña��$��DemiBold"�$
��Left=�$��
30,180,0,0q�$	��Top=����	#FFDDDDDDG� -    G�Name$ txtContraseña��$
��Left=�$��
30,210,0,0q�$	��Top=�$	��330���� -    ��Name$ lblErrorPswr��$y�	Collapsed=�
���  ��$�� ��$��Stretch=�$	��330��$
��Left=�$��
30,204,0,0q�$	��Top=�$��25���� $��Confirmar Contraseña�����	#FFDDDDDD$��DemiBold"�$
��Left=�$��
30,235,0,0q�$	��Top=�G� -   $ txtConfirmarContraseña��$
��Left=�$��30,260,30,10q�$	��Top=�$	��330���� -	   $ lblErrorPswr2��$y�	Collapsed=�
���  ��$�� ��$
��Left=�$��
30,254,0,0q�$	��330��$	��Top=��� $,��&Contraseña de Administrador Principal�����	#FFDDDDDD$��DemiBold"�$
��Left=�$��
30,283,0,0q�$	��Top=�G� -
   $ txtContraseñaAdmin��$
��Left=�$��
30,314,0,0q�$	��Top=�$	��330���� -   $ lblErrorPswrAdmin��$y�Visible=�
���  ��$�� ��$
��Left=�$��
30,309,0,0q�$	��330��$	��Top=��� -    ��Name$ 	btAceptar��$��Aceptar��$
��Left=�$��152,6.494,0,-0.04q�$	��Top=�$	��100��$��1���� -   $ 
btCancelar��$��Cancelar��$
��Left=�$��260,6.494,0,-0.04q�$	��Top=�$	��100��$��1����  �� Orientation$ 
Horizontal=�$��Center=�$��31��$��
30,144,0,0q�$	��Top=�.+�	� $��Administrador��$��0 0 50 0q����	#FFDDDDDD d� 	IsChecked ��  �� Path$ Administrador�$ Ventana�� �� Mode$ TwoWay=�	� $��Usuario�����	#FFDDDDDD$
 TrueY�