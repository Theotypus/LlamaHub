   M S B A M L   `   `   `  ����C  ?LlamaHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Hub.TabMessageHub.TabMessage  5      %    Hub.TabMessage.ConversationMenu   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .localclr-namespace:Hub.TabMessage     5        � 	Resources  5
         EditIcon �� 	UriSource��%6   &	        �� 5   
   $ Icons/EditIcon.png?�6'   5      .��5      � $	��250��6   ���#e6e6e6 � RowDefinitions 5   
   �� 5      $	��250 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��40 �6   .+�5   
   �� $	��250��6   $	��250��6   ���	#FF00994D$��2��6C   ._�5      �� $��Center=�6-   $��Center=�6J   ��6   �� *Image+6J   5      �� 5   
   -   5        $��ConvName��5      
�������$��18(�6M   $
��Bold"�6[   $��20, 10q�6m   $��2��6~   ��6$   �� *Name+6~   �� 5   
   -   5        $��1��5      $��Right=�64   $	��Top=�6P   $��	0,10,10,0q�6h   $��30��6{   $��30��6�   ��6�   Z� ��5       �� 5!       �� ImageSource# [  6   5#      �� 5%   
   -   5        $��ConvUser��5%   D   $��1��6   $��2��6�   ��6   �� *Participants+��6T   Z� ��6j   Z� ��5&      �� 5'      .R�5(      �  � Orientation$ 
Horizontal=�6    .+�5)      A� $��25��6"   $��25��6-   $��10, 5q�69   $
��Left=�6H   $��Center=�6c   ��5*      �� 5+   "    �� Color$ 	#FF00994D��62   5-      �� 5.      $��16(�6>   $��Normal"�6L   $��5q�6`   ��6$   �� *
Username+6`   5/      50      52      �� 53   
   -   5        $��2��53      $
��Left=�6_   $��2��6�   ��62   Z� ��6H   Z� .��54      �� $��40��6    �� Orientation$ 
Horizontal=�6%   .+�55      �� $��Icons/LeaveIcon.png��6   $��25��65   $��0, 0, 10, 0q�6@   �� 56      $��Leave��6   57      58      59      5:      