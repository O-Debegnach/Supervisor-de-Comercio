   M S B A M L   `   `   `  ���� M  ISupervisorComercio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SupComercio.VentanasSupComercio.Ventanas  'clr-namespace:DragHelper
DragHelper  K GControlDeVentana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:ControlDeVentana;assembly=ControlDeVentanaControlDeVentana      SupComercio.Ventanas.Login   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:SupComercio.Ventanas     )dragclr-namespace:DragHelper     UControlDeVentana8clr-namespace:ControlDeVentana;assembly=ControlDeVentana     -     �Name$  window�� � Title$ Login��$
��Auto��$��384.021�� � AllowsTransparency .  � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � Icon$7 1/SupervisorComercio;component/Resources/icono.ico�� � SizeToContent$ WidthAndHeight=���Z� .���  � RowDefinitions �� $��25 ��� ����  �� ImageSource$C =/SupervisorComercio;component/Resources/Fondo de software.png��	 j� Stretch$	 UniformToFill=�.+��� !   DragHelper.EnableDragHelper
   
EnableDrag
 . $��30��$	��Top=����	#FF2D2D30%  ControlDeVentana.ControlVentana  $��Right=�   IsOnlyClose . � -    �Name$ Grid_Login_Empleados��$��1�� �� $	��118 ��� $
��Auto ��� $��45 �.+��� -    ��Name$ Boton_Cancelar_Login��$��Cancelar��$��0,0,7,10q�$��2�� �� IsCancel . $��25��$��Bottom=�$��Right=�$��70���� -   $ Boton_Aceptar_Login��$��Aceptar��$��	0,0,82,10q�$��2�� �� 	IsDefault . $��25��$��Bottom=�$��Right=�$��70���� -    ��Name$ Imagen_Usuario��$��Center=�$	��Top=�$F��@/SupervisorComercio;component/Resources/Styles/Icono_Usuario.png��$��Uniform=�$	��100��$	��100��$��142,5,142,0q�� -   $ gdLogin��$��1��$y�Visible=�.+��� -    ��Name$ combo_Empleados��$
��10,0q�$	��Top=� �� SelectedIndex$ 0�� �� DisplayMemberPath$ UsuarioAsociado.Nombre��$��20��$��1������ *ListaEmpleados+ �� ElementName$ window���� -    ��Name$  Label_Contraseña_Empleado��$��Center=�$��Contraseña��$��
10,27,10,0q�$	��Top=�$��1��
�������$��25��G� -	    G�Name$ txtContraseña��$��
10,60,10,0q�$��1��$	��Top=�$��25�� J� 	AllowDrop .  �� -
   $ lblError��$��Contraseña Incorrecta��
���  ��$y�	Collapsed=�$
��Left=�$��	10,59,0,0q�$��1��$	��Top=�$	��364��� -   $ 
gdRegistro��$��1��$y�	Collapsed=�.+��� $��Usuario�����	#FFDDDDDD$��10,10,10, 0q�$	��Top=��� -    ��Name$ txtNuevoUsuario��$��
10,36,10,0q� �� TextWrapping$ NoWrap=�$	��Top=�$��25������ *	Usuario+$ window���� -   $ Label_Reg_Contraseña��$��Contraseña��$��
10,66,10,0q�$	��Top=����	#FFDDDDDDG� -   $ txtRegContraseña��$��
10,92,10,0q�$	��Top=�$��25���� -   $ lblRegError��$��Contraseña Invalida��$y�	Collapsed=�
���  ��$
��Left=�$��	10,90,0,0q�$	��Top=�$	��364���� -   $% Label_Reg_Confirmar_Contraseña��$��Confirmar Contraseña��$��10,122,10,0q�$	��Top=����	#FFDDDDDDG� -   $ txtRegConfContraseña��$��10,148,10,0q�$	��Top=�$��25���� -   $ lblRegError1��$y�	Collapsed=�$#��Las Contraseñas no coinciden��
���  ��$
��Left=�$��
10,146,0,0q�$	��Top=�$	��364��