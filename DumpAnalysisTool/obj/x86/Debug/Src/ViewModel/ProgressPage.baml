   M S B A M L   `   `   `  ����K  GDumpAnalysisTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DumpAnalysisTool.Src.PageDumpAnalysisTool.Src.Page  5      1    +DumpAnalysisTool.Src.ViewModel.ProgressPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :parent'clr-namespace:DumpAnalysisTool.Src.Page     5        N� 
Background
  ����� N� Title$ ProgressPage��5	      .��5      � .+�5   
   ��  %� IsVirtualizing . $
��Left=�6?   $	��366��6Z   $��	10,10,0,0q�6g   $	��Top=�6z   $	��660��6�   $
��Bold"�6�   
���   ���6�   �� *
LogEntry+��5      �� 5      .R�5      �  � ColumnDefinitions 5      �� 5      $��70 �6/   �� 5      $��* �6/   .+�5      �� $��0��6$   
���������64   �� *strDateTime+6Q   �� 5      $��1��6$   
���������64   �� *	Message+6M   5      5      5      �� 5   
   -   5        $��btStop��5      $
��STOP��6   $��Right=�6.   $��Bottom=�6J   $��75��6e   $��37��6p   $��	0,0,10,10q�6�   � 5   
   $��Center=�6   $��
10,0,90,10q�63   $��Bottom=�6G   $	��580��6b   $��37��6n    � Value 6z   �� *
Progress+ � Maximum 6�   �� *	Maximum+ � Minimum 6�   �� *	Minimum+5      5      