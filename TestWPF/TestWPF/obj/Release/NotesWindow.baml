   M S B A M L   `   `   `  ���� B  >TestWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TestWPFTestWPF      TestWPF.NotesWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:TestWPF     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  -     � Title$  NotesWindow��$	��450��$	��500�� � 
ResizeMode$ NoResize=� � 	Resources    strikethroughGreyText �� 
TargetType �� Property �� Value��%h   &	        �� " ��.���  � ��$ Strikethrough{���  � ��
 �������  � ��$
 Wrap=�.��� .+��� -    ��Name$ ScrollViewerNotes��.���� -    ��Name$ 
stackPanel��$	��500��.+�� -    �Name$
 grid��$	��450��$��-20,10,10,10q�	 � ColumnDefinitions	 �� $
��Auto ��� $��* ��� $
��Auto �.+�	� -   $��0��$��10,0,0,0q�
 d� 	IsChecked$
 FalseY�$��Center=��� -    ��Name$ textBox��$��1��$��15,0,0,0q�$
��Wrap=��� -    ��Name$ 
timeButton��$��2��$��10,0,0,0q�$
��Time��