   M S B A M L   `   `   `  ����F  BNotebookOne, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:NotebookOneNotebookOne  5   	       NotebookOne.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:NotebookOne     5   	     � Title$  Notebook One��5   	   $	��450��6   $	��800��6+    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$  ApplicationCommands.SaveAs��5
      6>     5   
   -   5        $ ApplicationCommands.Save��5      6<     5   
   -   5        $
 Open��5      6(     5   
   -   5        $	 New��5      6'   .��5      ]� .+�5   
   o� $	��Top=�6   
�������
���   �.@�5      m� $
��File��6   .@�5      m� $	��New��6    m� Command$	 New��6(   m� 5      $
��Open��6   $
 Open��6)   m� 5      $
��Save��6   $ ApplicationCommands.Save��6)   m� 5      $��Save As��6   $  ApplicationCommands.SaveAs��6,   m� 5      $��Print��6   �� 5      m� 5      -   5        $
��Exit��5      6)   5      m� 5      $
��Edit��6   .@�5      m� $
��Copy��6   $ ApplicationCommands.Copy��6)   m� 5      $��Paste��6   $ ApplicationCommands.Paste��6*   m� 5      $
��Undo��6   $ ApplicationCommands.Undo��6)   m� 5      $
��Redo��6   $ ApplicationCommands.Redo��6)   5      m� 5       $
��View��6   .@�5!      m� -   5        $��About Us��5!      6-   m� 5"      -   5        $
��Help��5"      6)   5#      5$      c� 5%   
   $	��Top=�6   
�������.@�5&      �� -   5        $��SelectedFontFamily��5'      $��12(�5)      $��20��6!   $	��100��6.    � ToolTip$ Change Document Font��5*      ��5(      ��    System.Windows.Media.Fonts   SystemFontFamilies �� Source# Z ��5+      �� 5,      .R�5-      �� ��6$   �� *.+��6=   �� *Source+ 6U   �� *Source+5.      50      d� 52      -	   5        $��btnBold��52      $ ��EditingCommands.ToggleBold��6*   $
 Bold��6O   .��53      �� $��/Images/btnBold.png��6   $��20��65   $��20��6@   6N   54      d� 55      -
   5        $��	btnItalic��55      $"��EditingCommands.ToggleItalic��6,   $ Italic��6S   .��56      �� $��/Images/btnItalic.png��6   $��20��67   $��20��6B   6P   57      d� 58      -   5        $��btnUnderline��58      $%��EditingCommands.ToggleUnderline��6/   $ 	Underline��6Y   .��59      �� $��/Images/btnUnderline.png��6   $��20��6:   $��20��6E   6S   5:      �� 5;      -   5        $��btnIncreaseFontSize��5;      $&�� EditingCommands.IncreaseFontSize��60   $ Increase Font Size��6[   .��5<      �� $ ��/Images/btn_fontsizeup.png��6   $��22��6<   $��22��6G   6U   5=      �� 5>      -   5        $��btnDecreaseFontSize��5>      $&�� EditingCommands.DecreaseFontSize��60   $ Decrease Font Size��6[   .��5?      �� $"��/Images/btn_fontsizedown.png��6   $��22��6>   $��22��6I   6W   5@      d� 5A      -   5        $��btnToggleBullets��5A      $#��EditingCommands.ToggleBullets��63   $ Bullet Points��6[   .��5B      �� $��/Images/btn_bulletpnt.png��6   $��20��6;   $��20��6F   6T   5C      d� 5D      -   5        $��btnToggleNumbering��5D      $%��EditingCommands.ToggleNumbering��65   $ Number Points��6_   .��5E      �� $"��/Images/btn_numberingpnt.png��6   $��20��6>   $��20��6I   6W   5F      d� 5G      -   5        $��btnAlignLeft��5G      $��EditingCommands.AlignLeft��6/   $ 
Align Left��6q   .��5H      �� $��/Images/btnAlignLeft.png��6   $��20��6:   $��20��6E   5I      d� 5J      -   5        $��btnAlignCentre��5J      $!��EditingCommands.AlignCenter��61   $ Align Centre��6u   .��5K      �� $ ��/Images/btnAlignCentre.png��6   $��20��6<   $��20��6G   5L      d� 5M      -   5        $��btnAlignRight��5M      $ ��EditingCommands.AlignRight��60   $ Align Right��6s   .��5N      �� $��/Images/btnAlignRight.png��6   $��20��6;   $��20��6F   5O      5Q      � 5R   
    � ColumnDefinitions 5S      �� 5T      $
��auto �6#   �� 5U      $
��30px �6#   �� 5V      $��* �6#   �� 5W      $
��30px �6#    � RowDefinitions 5Y      �� 5Z      $
��10px �6    �� 5[      $��* �6    �� 5\      $
��10px �6    .+�5_      �� ���	#FF444781$��0,0,5,0q�6-   $��1��6G   $��0��6T   ���	#FF3E3E3E._�5`      � -   5        $��	FilesGrid��5`      $
��Left=�6(    5a      �� 5b      $
��10px �6+   �� 5c      $
��auto �6+   �� 5d      $
��auto �6+   �� 5e      $
��10px �6+    5g      �� 5h      $
��auto �6(   �� 5i      $
��auto �6(   .+�5k      �� $��0��6   $��1��6)   $��
Your Notes��69   
�������$��100px��6a   $
��30px��6o   $��5q�6}   $��SemiBold"�6�   $
��Left=�6�   $��16(�6�   5l      5m      �� 5o      -   5        $��rtbTextEditor��5o      $��2��6/   $��1��6?   $��0,0,0,14q�6�   	 � 	Resources	 5p      
 �� 
TargetType �� Property �� Value��%X   'J�       �� 5q      "
 J�6   .�5r      ��  �@��Margin$ 0q�63   5s      5t      . �5u      /� .T�5v      J� .*�6    .*�5w      �� $� ��6   5x      5y      5z      5}      5~      5      