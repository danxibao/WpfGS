   M S B A M L   `   `   `  ����@  <WpfGS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WpfGSWpfGS  5      | xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting 5      �rclr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit)System.Windows.Controls.DataVisualization 5      ��clr-namespace:System.Windows.Controls.DataVisualization.Charting.Primitives;assembly=System.Windows.Controls.DataVisualization.Toolkit=System.Windows.Controls.DataVisualization.Charting.Primitives 5	          WpfGS.TransmissionView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:WpfGS     5      �tk{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5      �datavisrclr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit     5      �chartingprimitives�clr-namespace:System.Windows.Controls.DataVisualization.Charting.Primitives;assembly=System.Windows.Controls.DataVisualization.Toolkit     5	        � 	Resources  5        WpfGS.RegImageConverter   RegImageConverter��%   &	          5   
   6"   5      .��5      �� .+�5   
   �� $��  透射校正��6   $	��Top=�6"   $��36(�67   � 5   
   $	��500��6   .+�5   
   R� -   5        $��treeview��5   #   $��0��6   $
��Left=�5      $	��Top=�61   $	��436��6I   $	��201��6V   $��	10,10,0,0q�6b     5       �� DataType   
WpfGS.Node �� ItemsSource �� Orientation �� 	Converter � ToolTip
 � Tag��%  (��       5   +   *, +)7   �� 6   "  6+    6K   �� *Nodes+.R�5      �� $ 
Horizontal=�6!   $��0,2,0,2q�6:   .+�5      �� $��
0, 0, 5, 0q�6a   ��6    �� 8   6a   �� 5      ��6$   �� *Name+ 6:   �� *path+ 6S   �� 5      5      5       � ContextMenu 5      �� 5      .@�5      m� -   5        $��treeOpen��5   +   $��打开��6   6;   m� 5       -   5        $��treeNew��5    +   $��编辑��6   6:   5#      5%      &   System.Windows.Controls.DataGrid  5'   
   -   5        $��datagrid��5'         CanUserAddRows .  	   CanUserDeleteRows	 .  $
��Left=�5)      $��216,276,0,0q�6(   $	��Top=�6=   $	��170��6U   $	��180��6b   ��5'   $   �� *ECPara+5)   b     5+   
   -   5        
  Name$
 	datagrid2��5+       .  	 .  $
��Left=�5-   
   $��401,276,0,0q�6%   $	��Top=�6:   $	��170��6R   $	��180��6_   ��5+   '   �� *evsr+5-   _   >  8System.Windows.Controls.DataVisualization.Charting.Chart  5/   
   -   5        $��	lineChart��5/      $	��Top=�6%   $��216,10,10,0q�6=   $	��261��6R    J� SnapsToDevicePixels .    Series. 51      C  =System.Windows.Controls.DataVisualization.Charting.LineSeries  H  BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   DependentValuePath$ Value��6      IndependentValuePath$	 Key��68      IsSelectionEnabled .    ItemsSource 6S   �� 6k   ��53      �� 54       �� 
TargetType"  6"   .R�55      �� #��z��6   #��z��6G   $��0q�6r    �� Padding$ 0q�6�   $��0,0,8,0q�6�   ._�56      �  � RowDefinitions 57      �� 58   "   $
��Auto �60   �� 59   "   $��* �60   .+�5<      M  GSystem.Windows.Controls.DataVisualization.Charting.Primitives.EdgePanel  $��	ChartArea��6;      ChartAreaStyle#��z 6L   $��1��6u   $��0,0,0,0q�6�   .+�5=   "   �  K� ZIndex$ -1��6'      PlotAreaStyle#��z 69   �� 5>   "   $ 10��6)   ���	#FF919191$��1q�6S   5?      5@      5A      5B      5D      �� 5F   
   -   5        $��	删除行��5F      $
��Left=�6   $��269,451,0,0q�6:   $	��Top=�6O   $��61��6g   5G      �� 5H   
   -   5        $��	添加行��5H      $
��Left=�6   $��335,451,0,0q�6:   $	��Top=�6O   $��61��6g   5I      �� 5J   
   -	   5        $��	删除行��5J      $
��Left=�6   $��454,451,0,0q�6:   $	��Top=�6O   $��61��6g   5K   
   �� 5L   
   -
   5        $��	添加行��5L      $
��Left=�6   $��520,451,0,0q�6:   $	��Top=�6O   $��61��6g   5M   
   �� 5O      -   5        $��实验测定��5O      $
��Left=�6$   $��626,280,0,0q�6?   $	��Top=�6T   $��61��6l   5P      �� 5Q      -   5        $��来自文件��5Q      $
��Left=�6$   $��626,304,0,0q�6?   $	��Top=�6T   $��61��6l   5R      �� 5S      -   5        $��保存文件��5S      $
��Left=�6$   $��626,427,0,0q�6?   $	��Top=�6T   $��61��6l   5T      5U      5V      5W      