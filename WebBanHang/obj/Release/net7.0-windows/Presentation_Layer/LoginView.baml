   M S B A M L   `   `   `  ���� E  AWebBanHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:WebBanHang.Presentation_LayerWebBanHang.Presentation_Layer  -    'WebBanHang.Presentation_Layer.LoginView   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     =local+clr-namespace:WebBanHang.Presentation_Layer     -     � Title$  	LoginView��$	��550��$	��800�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�
������  � AllowsTransparency$
 True��.����  �� CornerRadius$ 12������  �� ImageSource$ /Images/back-image.jpg�� j� Stretch$
 None=�._��� $ 10��$��2q� J� Opacity$
 0.95Q����� 	 �� 
StartPoint$		 0,0&�
 �� EndPoint$	
 1,1&�.���  � Color$ #462AD8�� � Offset$ 0Q�� $ #DA34AE��$
 0.75Q�� $ #8A16C1��$ 1Q����� $		 0,1&�$	
 1,0&�.��� $ #060531��$ 0Q�� $ #1B1448��$ 1Q�._��  � RowDefinitions �� $��30 ��� .+�� $��0�� � ColumnDefinitions �� �� $��25 ��� $��25 ��� $��5 �.+��� $��Đăng Nhập��
�������$��10(�$��
Montserrat)�$��0��$��Center=�$��10,0,0,0q��� -    ��Name$ btnMinimize��$��0q�$��-��
�������$��16(�$��
Montserrat)� � Cursor$
 Hand��$��1������  �� 
TargetType" ��.���  �� Property � �� �� Value �#28AEED �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  � �� �#278BEF����  �� 
TargetType" ��.R��� $��18��$��18��$ 9��#��z��._��� $��Center=�$��Center=��� -   $ btnClose��$��0q�$��X��
�������$��12(�$��
Montserrat)�$
 Hand��$��2������ " ��.���  � �� �#DA34AE P�  �@J�IsMouseOver$
 True��. ���  � �� �#C62DAE���� " ��.R��� $��18��$��18��$ 9��#��z��._��� $��Center=�$��Center=��� $	��220��$��1�� �� Orientation$ Vertical=�$��0,35,0,0q�.+��� $��/Images/Logo.png��$	��100��$	��100���� $��Admin��
�������$��25(�$��Medium"�$��
Montserrat)�$��Center=��� $0��*Đăng nhập quản trị shop bán hàng��
�������$��12(�$��Medium"�$��
Montserrat)�$
��Wrap=� �� TextAlignment$ Center=�$��0,5,0,0q��� $��Email��
�������$��12(�$��Medium"�$��
Montserrat)�$��0,35,0,0q��� -    ��Name$ txtUser��$��13(�$��Medium"�$��
Montserrat)�
������� �� 
CaretBrush
 �����
�������$��0,0,0,2q�$��28��$��Center=�$��0,5,0,0q�$��20,0,0,0q�.������ $ /Images/user-icon.png��$
 None=� j� 
AlignmentX$
 Left=�.���� $��Mật khẩu��
�������$��12(�$��Medium"�$��
Montserrat)�$��0,15,0,0q�G� -    G�Name$ txtPass��$��13(�$��Medium"�$��
Montserrat)�
������� G� 
CaretBrush
 �����
�������$��0,0,0,2q�$��28��$��Center=�$��0,5,0,0q�$��20,0,0,0q����� $ /Images/key-icon.png��$
 None=�$
 Left=��� -   $ btnLogin��$��0q�$��Đăng nhập��
�������$��12(�$��
Montserrat)�$
 Hand��$��0,50,0,0q����� " ��.���  � �� �#462AD8 P�  �@J�IsMouseOver$
 True��. ���  � �� �#28AEED���� " ��.R��� $	��150��$��40��$ 20��#��z��._��� $��Center=�$��Center=��� $ 
Horizontal=�$��Center=�$��0,15,0,0q�