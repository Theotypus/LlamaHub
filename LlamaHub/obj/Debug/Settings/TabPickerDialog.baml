   M S B A M L   `   `   `  ����C  ?LlamaHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Hub.SettingsHub.Settings  5   	   "    Hub.Settings.TabPickerDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Hub.Settings     5   	     � Title$  TabPickerDialog��5   	   $	��500��6!   $	��700��6.    � Topmost .  � WindowStartupLocation$ CenterScreen=�6I   .��5	      �  � RowDefinitions 5
   
   �� 5      $��60 �6   �� 5      $��* �6   �� 5      $��50 �6   .+�5   
   � ���	#FF737373.+�5      �� $��Choose Tabs to add :��6   $
��Left=�63   $��20,0,0,0q�6N   $��Center=�6`   
�������$��16(�6�   $
��Bold"�6�   5      �� 5   
   -   5        $��ListBox��5      $��1��6!   $��Disabled=�6.   ��6d   Z� ��6{   Z� ��6�   Z� ��5      �� 5      .R�5      �  � Orientation$ 
Horizontal=�6     K� IsItemsHost . 69   5       � 	Resources 5      +  %Hub.Settings.PreviewIconPathConverter   	converter��%   &	          5      61   5      ��5      �� 5      .R�5      � -   5        $	��180��5      $	��130��6'   $��20q�64   .+�5       �� }�.  $��UniformToFill=�6{   ��6    �� *PreviewImage+ �� 	Converter# [  6{   �� 5!       �� Orientation$ 
Horizontal=�6%   $��Bottom=�6>   ���	#99575757	 J� Opacity$	 1Q�6�   $��45��6�   
 J� OpacityMask
 6p   Z� .+�5"      �� $
��Left=�6@   $��Center=�6[   
�������$
��Bold"�6�   $��15,0,0,0q�6�   ��6(   �� *Header+6�   5#      �� 5$      -   5        $
��Auto��5$   e   $��Right=�6s   $��Bottom=�6�   $��	0,0,20,15q�6�   $
��Auto��6�   5%      5&      5(      � 5*   
   $��2��6   $��
0,10,10,10q�6!   $��Right=�65   $��30��6Q   .+�5+      �� $��Cancel��6   $��45��6&   $��30��61    �� IsCancel . 6=   �� 5,      -   5        $��OK��5,      $��25��6"   $��30��6-   $��10,0,0,0q�69    �� 	IsDefault . 6\   5-      5/      50      