   M S B A M L   `   `   `  ���� B  >TestWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TestWPFTestWPF      TestWPF.ChatWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:TestWPF     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol    � Title$  
ChatWindow��$	��500��$	��400�� � 
ResizeMode$ NoResize=�.��� $	��390��$	��500��$
��Left=�$	��400�� � RowDefinitions �� $��* ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -    ��Name$ ScrollViewer��$��0��$��0,0,0,39q�$��2��.���� -    ��Name$ messageStackPanel��$��10q�.+�� $��0 0 0 10q� � ColumnDefinitions �� $
��Auto ��� $��* �� $��0 0 0 10q� �� $
��201* ��� $	��41* ��� $	��41* ��� $	��80* ��� $
��Auto �� $��1��$��10q� �� $��* ��� $
��Auto �.+��� -    ��Name$ messageInput��$��0��$��0 0 10 0q��� -   $��1��$
��Send��$��80��)  #HandyControl.Controls.BorderElement   CornerRadius$ 15��� $��2��$��10q� �� $��* ��� $��* ��� $��* �.+��� -   $��0��$��DoneM��$��80��$ 15���� -   $��1��$��DeleteM��$��80��$ 15���� -   $��2��$
��Addm��$��80��$ 15��� $��3��$��10q� �� $��* ��� $��* ��� $��* �.+��� -   $��0��$
��GetM��$��80��$ 15���� -	   $��1��$��ClearM��$��80��$ 15���� -
   $��2��$��80��$��Clear��$ 15��