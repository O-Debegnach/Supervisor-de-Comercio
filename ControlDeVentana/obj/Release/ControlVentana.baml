   M S B A M L   `   `   `  ���� K  GControlDeVentana, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:ControlDeVentanaControlDeVentana  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem %    ControlDeVentana.ControlVentana   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:ControlDeVentana     6sys&clr-namespace:System;assembly=mscorlib       � 	Resources     	Thickness  	Resaltado �� Color  
Precionado 
 Cerrar �� 
TargetType �� 	Resources  Key �� Property  CloseCircleWidth �� Value �� 
TargetType ��Name  CloseButtonOffset �� Left
	 �� Top  CloseButtonWidth	
 �� X1	 �� Y1	 �� X2	 �� Y2 �� Triggers P� Property P� Value  	Minimizar 	 CloseButtonOffsetX 
 CloseButtonOffsetY  	Restaurar  CloseButtonHeight  CloseButtonOffsetX1  CloseButtonOffsetY1  	Maximizar��%�  &	        &	      &	       &	 5     7  7  7  7  7  7  7  7  7   7  7  7   &	 �    7  7  7 	 7 
 7  7  7  7   7  7  &	     7  7  7  7  7   7   7   7   7   7  7  &	 g    7  7  7  7  7   7  7  X�2Q��� $ 	#AA555555���� $ 	#AA333333���� " �� ��X�CloseButtonWidth 15Q�X�CloseCircleWidth 10Q�X�CloseButtonOffset 5Q�.���  � ��8   ��  � ��8  ��  � ��$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��
 �������  � �� �� " ��.R��� $ CloseButton��#��z��.+��� 8  8	  
������ 8�� 8�� .+��� $
 0��$ 0��8  8  #��z��8�� $��16��$��16���� $
 0��8 	 8 
 $ 0��#��z��8�� $��16��$��16�� P�  �@J�IsMouseOver . . ���  � �� �	#CCFF0000��  � ��
 �����P�  � �� . . ���  � �� �	#CC960000��  � ��
 ������� " �� ��X�CloseButtonWidth 15Q�X�CloseCircleWidth 10Q�X�CloseButtonOffsetY 20Q�X�CloseButtonOffsetX 5Q�.���  � ��8   ��  � ��8  ��  � ��$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��
 �������  � �� �� " ��.R��� $ CloseButton��#��z��.+��� 8  8	  
������ 8�� 8�� .+��� $
 0��$ 0��8  $ 0��#��z��8��  P�  �@J�IsMouseOver . . ���  � ��8  ��  � ��
 �����P�  � �� . . ���  � ��8 	 ��  � ��
 ������� " �� ��X�CloseButtonWidth 25Q�X�CloseButtonHeight 25Q�X�CloseButtonOffsetY1 0Q�X�CloseButtonOffsetX1 0Q�X�CloseButtonOffsetY2 0Q�X�CloseButtonOffsetX2 0Q�.���  � ��8   ��  � ��8  ��  � ��$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��
 �������  � �� �� " ��.R��� $ CloseButton��#��z��.+��� 8  8	  
������ .+��� $��13��$��13��#��z��8�� $	 8��$ 5���� $
 10��$ 10��$ 8��$ 4��8�� #��z���� $
 9��$ 22��$ 5��$ 5��8�� #��z���� $
 22��$ 22��$ 4��$ 19��8�� #��z���� $
 16��$ 22��$ 18��$ 18��8�� #��z�� P�  �@J�IsMouseOver . . ���  � ��8 	 ��  � ��
 �����P�  � �� . . ���  � ��8 
 ��  � ��
 ������� " �� ��X�CloseButtonWidth 25Q�X�CloseButtonHeight 25Q�X�CloseButtonOffsetY1 0Q�X�CloseButtonOffsetX1 0Q�X�CloseButtonOffsetY2 0Q�X�CloseButtonOffsetX2 0Q�.���  � ��8   ��  � ��8  ��  � ��$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��
 �������  � �� �� " ��.R��� $ CloseButton��#��z��.+��� 8  8	  
������ .+��� $��15��$��15��#��z��8�� $	 6��$ 5�� P�  �@J�IsMouseOver . . ���  � ��8  ��  � ��
 �����P�  � �� . . ���  � ��8  ��  � ��
 �����.��� -    �Name$ grMain��.+��� -    ��Name$ btMinimizar��$
��Left=�$	��Top=�$��25��$��25��#���  �� -   $ btMaximizar��$
��Left=�$	��Top=�$��25��$��25��#���  $��25,0,0,0q��� -   $ btRestaurar��$
��Left=�$	��Top=�$��25��$��25��#���  $��25,0,0,0q�$y�Hidden=��� -   $ btCerrar��$
��Left=�$	��Top=�$��25��$��25��#���  $��50,0,0,0q�