   M S B A M L   `   `   `  ����C  ?LlamaHub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Hub.TabMusicHub.TabMusic  5      !    Hub.TabMusic.ArtistListView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Hub.TabMusic     5      .��5	      � -   5        $
��Root��5	        � 	Resources  5
   
      SongTemplate �� Orientation��%  &	        �� 5      .R�5      �� -   5        $ 
Horizontal=�5      $��60��66   .+�5      �� $��40��6   $��40��6'   ��63   �� *Cover+�� 5      $ Vertical=�6!   .+�5      �� $
��Left=�6;   $��Center=�6V   ��6$   �� *Title+6V   �� 5      $
��Left=�6<   $��Center=�6W   ��6$   �� *Artist+6W   5      5      5      5      .+�5   
   �� $ 
Horizontal=�6   .+�5      R� -   5        $��
ArtistsBox��5      $��0q�6)   $��	100,0,0,0q�64    %� IsVirtualizing .  %� VirtualizationMode$ 	Recycling=�6u   ��5      �� 5      .R�5      �� -   5        $ 
Horizontal=�5   %   $��60��6>   .+�5      �� $��40��6$   $��40��6/   ��6;   �� *Cover+�� 5      $
��Left=�6D   $��Center=�6_   ��6(   �� *
Data[name]+6_   5      5      5      �� 5!      $��1��6   $��2��6(   $��Disabled=�68   $
��Auto=�6a   .��5"      �� -   5        $��
ArtistView��5"      $ Vertical=�6/   5#      5$      5&      5'      