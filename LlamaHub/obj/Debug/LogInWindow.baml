   M S B A M L   `   `   `  ����C  ?LlamaHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HubHub  5   	       Hub.LogInWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:Hub     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � AllowsTransparency . 
������  � 
ResizeMode$ CanMinimize=�5   	    � Title$ LlamaHub��5   	   $	��600��6   $	��800��6'    � WindowStyle$
 None=�63   .��5      � -   5        $
��Root��5      ��5   
   �� 5       �� ImageSource$ LogScreen.png��6    � ColumnDefinitions 5   
   �� 5      $	��400 �6   �� 5      $	��100 �6   �� 5      $	��180 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $	��270 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5       $��50 �6   �� 5!      $��* �6   .+�5%   
   �� $��1��6   $��1��6   $��Username��6-   $��Center=�6@   $��16(�6[   �� 5&   
   -   5        $��Username��5&      $��2��6"   $��1��62   $��14(�6?   $��Center=�6M   �� 5(   
   $��3��6   $��1��6   $��Password��6-   $��Center=�6@   $��16(�6[   G� 5)   
   -   5        $��Password��5)      $��2��6&   $��3��66   $��14(�6C   $��Center=�6Q   � 5+   
   $��2��6   $��5��6$    � Orientation$ 
Horizontal=�61   $��Right=�6J   $��Center=�6f   .+�5,      �� -   5        $��SignUp��5,      $��0,0,5,0q�6&   $��5q�67   $��16(�6X   �� 5-      -   5        $��LogIn��5-      $��5,0,0,0q�6%   $��5q�66   $��16(�6V   5.      5/      50      