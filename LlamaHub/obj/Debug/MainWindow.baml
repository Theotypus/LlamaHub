   M S B A M L   `   `   `  ����C  ?LlamaHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:HubHub  5   	       Hub.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:Hub     5   	     � Title$  LlamaHub��5   	   $	��350��6   $	��525��6'    � WindowStyle$
 None=�63    � WindowState$ 	Maximized=�6F   .��5
      � .+�5   
      Hub.BubbledBackground   K� ZIndex$ -5��6"   �� 5   
   -   5        $��
TabControl��5      ��6'   Z�  � 	Resources 5       �� 
TargetType �� Property �� Value��%�   '��       �� 5      " ��6   .�5      ��  � ��$ 50��6/   �� 5       � ��$
 20,2q�60   �� 5       � ��$ 16(�61   �� 5       � ��
 �����63   5      5      ��5      �� 5       �� 
TargetType" ��6"   .R�5      � }�. -  'System.Windows.Input.KeyboardNavigation	   TabNavigation$	 Local=�6/   
 J� SnapsToDevicePixels
 .  � ColumnDefinitions 5      �� 5       �Name$ ColumnDefinition0��6/   $��* �6H   �� 5      $ ColumnDefinition2��6/   $��0 �6H    � RowDefinitions 5      �� 5       $ RowDefinition0��6,   $
��Auto �6B   �� 5!      $ RowDefinition1��6,   $��* �6B   .+�5#      ]� $��HeaderPanel��5$      $��0��5%      $��0��5&      $��2,2,2,0q�5'      $ 1��5(       K� IsItemsHost .    TabIndex$ 1��5*      �� 5+      $��ContentPanel��5,      $��0��5-      $��1��5.      #��z��5/      #��z��50      #��z��51         DirectionalNavigation$ 	Contained=�52      $ 2��53      $	 Local=�54      ._�55      �� $��PART_SelectedContentHost��56      #��z��57       �� SelectedContent#��z 58      $��SelectedContent��59      " �� SelectedContentStringFormat �� ContentStringFormat# z 5:       �� SelectedContentTemplate#��z 5;      #
 z
 5<      5=      5>      5?      5A      5B      5C      