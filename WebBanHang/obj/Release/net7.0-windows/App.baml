   M S B A M L   `   `   `  ���� b  ^PresentationFramework.Aero2, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35hKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2Microsoft.Windows.Themes   
WebBanHang   WebBanHang.App   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   YthemeKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2     �� 	Resources     	topButton �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value  
menuButton  menuButtonIcon.  (MahApps.Metro.IconPacks.PackIconMaterial  textboxSearch	 ��Name
 J� SnapsToDevicePixels ��Name �� Triggers �� 
TargetName  	addButton  removeButton  	tabButton  textboxFilter �� BasedOn  pagingButton �� Padding 	 pagingButtonIcon 
 gridEditButton  gridRemoveButton  gridButtonIcon �� 
ResourceId �� TypeInTargetAssembly&   System.Windows.Controls.DataGrid ��Name �Name J� Opacity � Points �� Stretch  DataGridStyle15  /System.Windows.Controls.DataGridLengthConverter � ColumnDefinitions � RowDefinitions   SelectAllCommand7  1System.Windows.Controls.DataGridHeadersVisibility
   All �� ConverterParameter!   HeadersVisibilityConverter �� 	Converter �� RelativeSource �� AncestorTypeG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter   Name!   Column" ��Name# �� CanContentScroll$ ��Name% �� ScrollableHeight& � Maximum' �� Orientation( �� ViewportHeight) �� ViewportSize** �� #ComputedVerticalScrollBarVisibility+ � Value, �� Mode- �� Mode. �� ScrollableWidth/ �� ViewportWidth,0 �� %ComputedHorizontalScrollBarVisibility1 ]� 
Conditions2 �� Property3 �� Value  DataGridCellStyle1*  $System.Windows.Controls.DataGridCell  ColumnHeaderGripperStyle  DataGridColumnHeaderStyle1=  7System.Windows.Controls.Primitives.DataGridColumnHeader3   -Microsoft.Windows.Themes.DataGridHeaderBorder4   	IsPressed5 J� IsMouseOver6   	IsHovered7   CanUserSort8   IsClickable9   SortDirection:   SortDirection;   SeparatorVisibility<   SeparatorVisibility=   SeparatorBrush>   SeparatorBrush? n�Name  DataGridRowStyle1)	  #System.Windows.Controls.DataGridRow?
  9System.Windows.Controls.Primitives.SelectiveScrollingGrid?  9System.Windows.Controls.Primitives.DataGridCellsPresenter@ 	  
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenterA 	  DetailsVisibility$B 
  SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientationC   Vertical#D   RowDetailsScrollingConverter:  4System.Windows.Controls.Primitives.DataGridRowHeader
E   Row   OptionMark.Static.BackgroundF �� Color  OptionMark.Static.Border  OptionMark.Static.Glyph # OptionMark.MouseOver.Background  OptionMark.MouseOver.Border  OptionMark.MouseOver.Glyph ! OptionMark.Pressed.Background  OptionMark.Pressed.Border  OptionMark.Pressed.Glyph " OptionMark.Disabled.Background  OptionMark.Disabled.Border  OptionMark.Disabled.Glyph  CheckBoxStyle1G �NameH F�NameI ��Name��%�'  &	        &	 H    &	 �    &	     &	 %    &	 �    &	 �    &	 
    7  &	 �    &		 �    &	
 >    &	 �    7 
 &	 #    (�� v    $" DataGridSelectAllButtonStyle��"  )&	     &	     &	 I    &	 �    7  7  &	 M    &	 �    &	     &	     &	 /    &	 D    &	 Y    &	 n    &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    7  7  7  7  7  7  7  7  7  7  7  7  7  �� " ��.���  � ��
 ���� ��  � �� �#6e7783��  �@�FocusVisualStyle Z� ��  � ��$ 30����  � ��$ 30����  � ��$
 Left=���  � �� �� " ��.R��� #��z��$ 50��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ���  � �� �#dcdcdc�� " ��.���  � ��
 ���� ��  � �� �#d0c0ff��  �@�FocusVisualStyle Z� ��  � ��$ 35����  � ��$
 15 3q���  � ��$ 13(���  � �� �� " ��.R��� #��z��$ 7��._��� $
��Left=�$��Center=� P�  �@J�IsMouseOver$
 True��. ���  � �� �#7b5cd6��  � �� �#ffffff�� "  .���  � ��$ 15����  � ��$ 15����  � ��$ Center=���  � ��$ 	20 2 20 0q��� " ��.���  � �� �#ffffff��  � �� �#b0b9c6��  � ��$ 0q���  � ��$ 12(���  � ��$ 15 10q���  � ��$ Center=���  � ��$
 0 10q���  � �� �� " ��.R��� $	 border��$ 20��#��z��$

 True��$��1q����#e0e6ed._��� $ PART_ContentHost��$|�False��$��Hidden=�$��Hidden=� P�  �@J�IsMouseOver$
 True��. ��� $ border�� � �� �#d9d9d9P�  �@J�IsKeyboardFocused$
 True��. ��� $ border�� � �� �#d9d9d9�� " ��.���  � �� �#6741d9��  � �� �#ffffff��  �@�FocusVisualStyle Z� ��  � ��$ 90����  � ��$ 35����  � ��$ Right=���  � ��$ 0 20 0 0q���  � ��$ 13(���  � ��$ 1����  � �� �� " ��.R��� #��z��$ 7��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ���  � �� �#7b5cd6��  � �� �#ffffff�� " ��.���  � �� �#d32f2f��  � �� �#ffffff��  �@�FocusVisualStyle Z� ��  � ��$	 130����  � ��$ 35����  � ��$ Right=���  � ��$ 
0 20 100 0q���  � ��$ 13(���  � ��$ 1����  � �� �� " ��.R��� #��z��$ 7��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ���  � �� �#7b5cd6��  � �� �#ffffff�� " ��.���  � ��
 ���� ��  � �� �#121518��  �@�FocusVisualStyle Z� ��  � ��$ 80����  � ��$ 45����  � ��
 ���� ��  � ��$ 14(���  � ��$ SemiBold"���  � �� �� " ��.R��� #��z��$��0 0 0 1q�#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ���  � �� �#784ff2�� " ��8   .���  � ��$
 10 7q���  � �� �� " ��.R��� $	 border��$ 7��#��z��$

 True��$��1q����#e0e6ed._��� $ PART_ContentHost��$|�False��$��Hidden=�$��Hidden=� P�  �@J�IsMouseOver$
 True��. ��� $ border�� � �� �#d9d9d9P�  �@J�IsKeyboardFocused$
 True��. ��� $ border�� � �� �#d9d9d9�� " ��.���  � ��
 ���� ��  � �� �#6c7682��  �@�FocusVisualStyle Z� ��  � ��$ 13(���  � ��$	 1 0q���  � �� �� " ��.R��� #��z��$ 5��$
 10 5q�._��� $��Center=�$��Center=�$��0 0 0 1q� P�  �@J�IsMouseOver$
 True��. ���  � �� �#7950f2��  � �� �#ffffff�� "  .���  � ��$ 10����  � ��$ 10����  � ��$ Center=��� " ��.���  � �� �#512da8��  � �� �#4a148c��  � �� �#ffffff��  �@�FocusVisualStyle Z� ��  � ��$ Center=���  � �� �� " ��.R��� #��z��$ 5��$ 5q�$��1q�#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True��. ���  � �� �#673ab7�� " ��8   .���  � �� �#d32f2f��  � �� �#b71c1c P�  �@J�IsMouseOver$
 True��. ���  � �� �#f44336�� "  .���  � ��$ 12����  � ��$ 12����  � ��$ Center=��� " ��.���  � �� �� " ��.R�� .+��� $ Border��#��� ��$

 True��� $ Arrow��
���   �$��Right=�$��8,8,3,3q�$
 0.15Q� �   ��� ��� ��� ��� $ Uniform=�$��Bottom=� P�  �@J�IsMouseOver$
 True��. ��� $ Border��# � �� � ��P�  � ��$
 True��. ��� $ Border��# � �� � ��P�  � {�$ False��. ��� $ Arrow�� � y�$ 	Collapsed=��� "  .���  � ��
 ���� ��  � �� �#222528��  � ��$ 0q���   �@ RowDetailsVisibilityMode$ VisibleWhenSelected=���  � ��$
 true����  �@��PanningMode$
 Both=���  �@��IsFlicksEnabled$ False����  �@ AutoGenerateColumns$ False����  �@ HeadersVisibility$ Column=���  � ��$ 0 10 0 0q���  �@ SelectionMode$ Extended=���  �@ SelectionUnit$ FullRow=���  �@ CanUserAddRows$ False����  �@ CanUserDeleteRows$ False����  �@ CanUserResizeColumns$ False����  �@ CanUserReorderColumns$ False����  � ��$ 13(���  �@ ColumnWidth$ * ��  �@ GridLinesVisibility$
 None=���  � �� �� "  .R��� #��z��#��z��#��z��# z��$

 True��._��� $ DG_ScrollViewer��$|�false������ " ��.R��  �� $
��Auto ��� $��* ��� $
��Auto � �� $
��Auto ��� $��* ��� $
��Auto �.+��� #��Z $|�false����C� *�� $" DataGridSelectAllButtonStyle��"  +y��� *HeadersVisibility+# Z # Z  �� "  ���� *CellsPanelHorizontalOffset+ �� "    $!  PART_ColumnHeadersPresenter��$��1��y��� *HeadersVisibility+# Z! # Z  �� "  �� $!" PART_ScrollContentPresenter��## z��$��2��$��1���� $$ PART_VerticalScrollBar��$��2��#& z% $' Vertical=�$��1��#) z( #y�z* + �� *VerticalOffset+$, OneWay=� �� $- TemplatedParent=�� $��1��$��2�� �� ���� **(NonFrozenColumnsViewportHorizontalOffset+ �� "  �� $��* �.+��� $$ PART_HorizontalScrollBar��$��1��#& z. $' 
Horizontal=�#) z/ #y�z0 + �� *HorizontalOffset+$, OneWay=� �� $- TemplatedParent=�.���� #
 z
  ]� 1 �� 2 �@��
IsGrouping$
3 true���� "2 �@%�IsVirtualizingWhenGrouping$3 false��./���  � ��$ false���� "  .���  � ��
 ���� ��  � ��$ Center=���  �@�FocusVisualStyle Z� ��  � ��$
 15 0q���  � �� �� "  .R��� #��z��$��0q�$

 True��._��� #
 z
 �� " n�.���  � ��$ 8����  � ��
 ���� ��  �@�Cursor$ SizeWE����  � �� �� " n�.R��� #��z��# z���� "  .���  � ��$ Center=���  � ��
 ���� ��  � �� �#69717d��  � ��$ 
15 0 15 10q���  � �� �� "  .R�� .+�  #��z��#��z��#��z��#4 z��#6 z5 #8 z7 # z��#: z9 #< z; #> z= ._��� #��z��$
��True��#
 z
 #��z��n� $? PART_LeftHeaderGripper��$
��Left=�8��  n� $? PART_RightHeaderGripper��$��Right=�8�� �� " 	 .���  � ��
 �������  � ��$ 0 4 0 0q���  �@J�SnapsToDevicePixels$
 true����  �@=�ErrorTemplate Z� ��  �@	 ValidationErrorTemplate �� .R��� 
���  ��$��2,0,0,0q�$��!��$��Center=���  � �� �� " 	 .R��� $	 
DGR_Border��$	 0 8q�#��z��$ 5��$��0q�$

 True��._�
   �� $
��Auto ��� $��* � �� $��* ��� $
��Auto �.+�  $��1��#��z@ #
 z
   $��1��$��1��#y�zA B �� *AreRowDetailsFrozen+# ZC # ZD  �� "    $��2��$B Vertical=�y��� *HeadersVisibility+# ZE # Z  �� "   P�  �@J�IsMouseOver$
 True��. ���  � �� �#623ed0��  � �� �#ffffffP�  �@	 
IsSelected$
 True��. ���  � �� �#623ed0��  � �� �#ffffff]� 1 �� 2 �@	 
IsSelected$
3 True���� 2 �@��IsSelectionActive$3 False��./���  � �� �#dfdfdf�� $F 	#FFFFFFFF���� $F 	#FF707070���� $F 	#FF212121���� $F 	#FFF3F9FF���� $F 	#FF5593FF���� $F 	#FF212121���� $F 	#FFD9ECFF���� $F 	#FF3C77DD���� $F 	#FF212121���� $F 	#FFE6E6E6���� $F 	#FFBCBCBC���� $F 	#FF707070���� " ��.���  �@�FocusVisualStyle Z� ��  � ��8   ��  � �� �#dae2ea��  � ��# � ����  � ��$ 1q���  � �� �� " ��.R�� $G templateRoot��
������ $

 True�� �� $
��Auto ��� $��* �.+��� $	 checkBoxBorder��$ 1q�$ 3��#��z��#��z��#��z��#��z��$��1q�#��z��._�� $G markGrid��.+�F� $H 
optionMark��Y�����4� �� Ѽ�G dԊ Ѩ�D dԊ �    ^>O ��z r�5 ��@ ��\ 
ܺ��� @�j�  �>��4� �� 8�� $��1q�$ 0Q�$
 None=��� $ indeterminateMark��8�� $��2q�$ 0Q��� $I contentPresenter��$��1��$|�False��#��z��#��z��$
��True��#
 z
 #��z�� P�  � ��$
 true��. ���  Z�  �@�FocusVisualStyle��  � ��$ 4,-1,0,0q�P�  �@J�IsMouseOver$
 true��. ��� $ checkBoxBorder��8   � ���� $ checkBoxBorder�� � �� �#9575cd�� $ 
optionMark��8   � ���� $ indeterminateMark��8   � ��P�  � {�$ false��. ��� $ checkBoxBorder��8   � ���� $ checkBoxBorder��8   � ���� $ 
optionMark��8   � ���� $ indeterminateMark��8 	  � ��P�  � ��$
 true��. ��� $ checkBoxBorder��8 
  � ���� $ checkBoxBorder�� � �� �#9575cd�� $ 
optionMark��8   � ���� $ indeterminateMark��8   � ��P�  �@d�	IsChecked$
 trueY�. ��� $ 
optionMark�� �@J�Opacity$ 1Q��� $ indeterminateMark�� �@J�Opacity$ 0Q�P�  Z�  �@d�	IsChecked. ��� $ 
optionMark�� �@J�Opacity$ 0Q��� $ indeterminateMark�� �@J�Opacity$ 1Q�