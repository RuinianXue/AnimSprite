   M S B A M L   `   `   `  ����B  >TestWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TestWPFTestWPF  5   	       TestWPF.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5   	   !localclr-namespace:TestWPF  5   	   S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5   	   -   5          � SizeToContent$  WidthAndHeight=�5
   	    � Title$ DeskSparite��5   	   $	��100��5      $	��100��6    � WindowStyle$
 None=�5       � AllowsTransparency . 
������  � Topmost .  � WindowStartupLocation$ Manual=�5       � Left$ 0��6,   
 � Top$ 0��65    � 
ResizeMode$ CanResizeWithGrip=�5      	 � 	Resources	 5         MyButtonStyle
 �� 
TargetType �� Property �� Value  MyArrowStyle��%  &	        &	 
    �� 5   
   "
 ��6&   .�5      ��  �@J�RenderTransformOrigin$ 3.174,-1.997&�66   �� 5       � ��$ 143,19,10,153q�6'   �� 5       � ��$ 47��6&   �� 5       � ��$ 26��6'   �� 5       �@J�Opacity$ 0Q�6(   5      �� 5    
   "
 ��6%   .�5!      ��  �@J�RenderTransformOrigin$ 3.174,-1.997&�66   �� 5"       � ��$ 	79,55,0,0q�6'   �� 5#       � ��$ 21��6'   �� 5$       � ��$ 21��6&   �� 5%       � ��$	 Top=�62   5&      5'      .��5*      � ��6   �� *ActualWidth+ �� RelativeSource �� *FindAncestor+ �� AncestorType" ���5+      �� *ActualHeight+ �� *FindAncestor+" � � RowDefinitions 5,   
   �� 5-      $	��93* �6   �� 5.      $��7* �6   .+�51   
   �� -   5         ��Name$ GifImage��51      $��UniformToFill=�6"   $��2��6g   "  WpfAnimatedGif.ImageBehavior   AnimatedSource 6:   �� 6g   (� 53   
   -   5        $��TestControlButton��53      $��2��6+   .��54      � .+�55      �� -   5        $
��Stop��55      $��0,0,153,180q�6;   $y�	Collapsed=�6P   �� 56      -   5        $
��Move��56      $��153,180,0,0q�6;   $y�	Collapsed=�6P   57      58      (� 5:   
   -   5        $��BubbleImage��5:      $��2��6%   .��5;      � .+�5<      �� -   5        $ RequestBubble��5<      $"��/Resources/RequestBubble.png��6/   $y�	Collapsed=�6U    J� RenderTransformOrigin$ 3.174,-1.997&�6l   $��143,19,10,153q�6�   $��47��6�   $��26��6�   �� 5=      -   5        $ 
ChatBubble��5=      $��/Resources/ChatBubble.png��6,   $ 3.174,-1.997&�6O   $��143,19,10,153q�6t   $��47��6�   $��26��6�   5>      5?      (� 5B   
   -	   5        $��BubbleButton��5B      $��2��6&   .��5C      � .+�5D      �� -
   5         ��Name$ RequestButton��5D      #���   6L   �� 5E      -   5        $ 
ChatButton��5E      #���   6F   5F      5G      (� 5H   
   -   5        $��Arrow��5H      $��2��6   .��5I      � .+�5J      �� -   5        $ 
ArrowImage��5J      $��/Resources/Arrow.png��6,   #���  6J   $y�	Collapsed=�6q   5K      5L      &   HandyControl.Controls.NotifyIcon  5M   
   -   5          Name$
 Ling��5M   W      IsBlink . $y�Visible=�6'      Icon$ /Resources/Stay.gif��6<      Text$
 Ling��6e   6q   5N      5O      