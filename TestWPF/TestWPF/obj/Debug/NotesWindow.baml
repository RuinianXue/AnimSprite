   M S B A M L   `   `   `  ����B  >TestWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TestWPFTestWPF  5   	       TestWPF.NotesWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:TestWPF     5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	   -   5          � Title$  NotesWindow��5	   	   $	��450��6   $	��500��6*    � 
ResizeMode$ NoResize=�5
   	    � 	Resources 5         strikethroughGreyText �� 
TargetType �� Property �� Value��%�   &	        �� 5   
   " ��6.   .�5      ��  � ��$ Strikethrough{�60   �� 5       � ��
 �����6+   �� 5       � ��$
 Wrap=�6-   5      5      .��5      � .+�5   
   �� -   5         ��Name$ ScrollViewerNotes��5      .��5      �� -   5         ��Name$ 
stackPanel��5      $	��500��6.   .+�5      � -   5         �Name$
 grid��5   :   $	��450��6   $��-20,10,10,10q�6$   	 � ColumnDefinitions	 5      �� 5      $
��Auto �6+   �� 5      $��* �6+   �� 5      $
��Auto �6+   .+�5      	� -   5        $��0��5   "   $��10,0,0,0q�62   
 d� 	IsChecked$
 FalseY�6D   $��Center=�6t   �� 5      -   5         ��Name$ textBox��5   B   $��1��6    $��15,0,0,0q�60   $
��Wrap=�6S   5      �� 5       -   5         ��Name$ 
timeButton��5    ?   $��2��6   $��10,0,0,0q�6-   $
��Time��6S   6b   5!      5"      5#      5$      5%      