   M S B A M L   `   `   `  ���� E  AWebBanHang, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:WebBanHang.Presentation_Layer.Convert%WebBanHang.Presentation_Layer.Convert  )    #WebBanHang.Presentation_Layer.Panel   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     J
converters3clr-namespace:WebBanHang.Presentation_Layer.Convert     c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   $	��720��$
��1080��  � WindowStartupLocation$  CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency$
 True��
������  � 	Resources    	BoolToVis<   6WebBanHang.Presentation_Layer.Convert.IsAdminConverter  isAdminConverter@   :WebBanHang.Presentation_Layer.Convert.RandomColorConverter  randomColorConverterC   =WebBanHang.Presentation_Layer.Convert.FirstCharacterConverter  firstCharacterConverterC   =WebBanHang.Presentation_Layer.Convert.BooleanToBrushConverter  booleanToBrushConverter��%K   &	        &	      &	 
     &	      &	      ��         .���� -   ���#eff2f7 �� CornerRadius$ 30��._��  � ColumnDefinitions �� $	��200 ��� $��* �.+��  � RowDefinitions �� $
��auto ��� $��* �.+��� $��
20 28 0 35q�  	topButton#��[ .��.  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$
 Menu=�$��15��$��15���� $��1�����#623ed0$ 	0 50 0 30��._�]� .+��� $��80��$��80�� �� Padding$ 3q�
�������$	 100��$	��Top=�$
��Left=�$��
20 -30 0 0q�._�A� ���� 	 �� ImageSource$	 /Images/avatar_admin.jpg���� -   
 ��Name$
 nameTextBlock��
�������$��22(�$��SemiBold"�$	��Top=� �� TextAlignment$ Center=�$��	5 25 0 10q��� $��Quản trị�����#d0bfff$��14(�$��SemiBold"�$	��Top=�$ Center=��� $��25q�$	��Top=�$	��0.8�����#7e5fdc�� $	��Top=�.+��� -     
menuButton#��[ ���#7b5cd6
�������.����  �� Orientation$ 
Horizontal=�.+�  $ HomeVariant=�  menuButtonIcon#��[ �� $��
Quản lý���� -   #��[ .���� $ 
Horizontal=�.+�  $ 
Salesforce=�#��[ �� $��Thẻ giảm giá���� #��[ ���#7b5cd6
�������.���� $ 
Horizontal=�.+�  $ AccountMultipleOutline=�#��[ �� $��Tài khoản���� -   #��[ .���� $ 
Horizontal=�.+�  $ ContentDuplicate=�#��[ �� $��
Danh mục���� -   #��[ .���� $ 
Horizontal=�.+�  $ CodeBracesBox=�#��[ �� $��Thương hiệu���� -   #��[ .���� $ 
Horizontal=�.+�  $
 Sack=�#��[ �� $��Sản phẩm���� -   #��[ $��Bottom=�$��Bottom=�$��
15 0 15 20q�.���� $ 
Horizontal=�.+�  $ Logout=�#��[ �� $��Đăng xuất��� $��1��$��30 20 20 20q� �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��* ��� $
��auto �.+�� $	��300��$
��Left=�.+��� $
��20 0q�$��Center=����#b0b9c6 J� IsHitTestVisible$ False�� K� ZIndex$ 1��$&�� Nhập từ khoá tìm kiếm...��y���  �� ElementName$ 	txtSearch�� �� Path$ Text.IsEmpty� �� 	Converter# [  �� -	    ��Name$ 	txtSearch��  textboxSearch#��[   $ Magnify=�$��15��$��15��$��Center=�$��0 0 15 0q����#b0b9c6$��Right=��� -
   #��[ $��
0 -30 10 0q����#dcdcdc$��Right=�.��  $ Close=����#d32f2f�� -   #��[ $��
0 -30 45 0q����#dcdcdc$��Right=�.��  $ WindowRestore=��� -   #��[ $��
0 -30 80 0q����#dcdcdc$��Right=�.��  $ WindowMinimize=����#6741d9�� $��Quản trị��$��1��$��32(�$��SemiBold"�$��0 20 0 0q�$
��Left=����#121518�� -    	 removeButton#��[	 $��0 1 8 0q�.���� $ 
Horizontal=�.+�  $ Delete=�$��Center=�$��11��$��11��$��0 1 8 0q��� $��Xoá đã chọn���� $��1��$��3�����#dae2ea$��	0 -1 0 10q��� $��2��$ 
Horizontal=�$��0 10 0 0q�.+��� $��Khách hàng�� 
 	tabButton#��[
 ���#784ff2�� -   $
 customerCountTextBlock��$��4��$��18(�$��SemiBold"����#121518$
��Left=�$��Center=��� $��Lọc dữ liệu��$��4��$��13(�$��SemiBold"�$��
 0 0 215 0q����#121518$��Right=�$��Center=�� $	��200��$��Right=�$��4��.+��� $
��15 0q�$��Center=����#b0b9c6$ False��$ 1��$ ��Nhập từ khoá lọc...��y��� $ 	txtFilter��$ Text.IsEmpty�# [  �� -   $ 	txtFilter��#��[ &   System.Windows.Controls.DataGrid  -     Name$ membersDataGrid��  DataGridStyle1#���    DataGridCellStyle1   	CellStyle# �    DataGridColumnHeaderStyle1   ColumnHeaderStyle# �    DataGridRowStyle1   RowStyle# �  $��5��   Columns 4  .System.Windows.Controls.DataGridCheckBoxColumn  ,  &System.Windows.Controls.DataGridColumn   CanUserResize$ False��   Width5	  /System.Windows.Controls.DataGridLengthConverter$
 auto	   CheckBoxStyle11
  +System.Windows.Controls.DataGridBoundColumn 
  ElementStyle# [    HeaderTemplate �� .R���  ��Name$ headerCheckbox��-   #��[ 0  *System.Windows.Controls.DataGridTextColumn     Header$	 STT��   
IsReadOnly$
 True��$ False��$
 auto	  
  Binding �� *STT+4  .System.Windows.Controls.DataGridTemplateColumn  $ 	Họ tên��$
 True��$ *	     CellTemplate  �� .R��� $ 
Horizontal=�.+��� ! ��Name$! border��$��25��$��25��$ 50��$��0 0 10 0q����� # [ ._��� $��Center=�$��Center=�
�������$��0 0 0 1q����� $ LastName�# [ �� �� ��� *
LastName+ �� ��� *	FirstName+  $ Quyền��$
 True��$ *	  �� $ IsAdmin�# [   $ Email��$
 True��$ *	  �� *Email+  $ Số điện thoại��$
 True��$ *	  �� *Phone+  $ Chức năng��$
 True��$
 auto	   �� .R��� $ 
Horizontal=�.+��� -     gridRemoveButton#��[ $��5 0 0 0q�.��  $ DeleteOutline=�  gridButtonIcon#��[ �� $��6��$��Right=����#dee4ec$��1q�$ 5��$ 5q�
�������$��
0 10 10 10q�._��� $ 
Horizontal=�.+��� -     pagingButton#��[ .��  $ ChevronLeft=�  pagingButtonIcon#��[ �� $��1�����#dee4ec$	��3 7q��� -   " ��Name$" paginationPanel������ *PageNumbers+���� .R��� $ 
Horizontal=����� .R��� #��[ ���� *Number+���� *DataContext.PaginationCommand+# �� RelativeSource# �� $ �� AncestorType$ ItemsControl���� *Number+�� $��1�����#dee4ec$	��3 7q��� -   #��[ .��  $ ChevronRight=�#��[ 