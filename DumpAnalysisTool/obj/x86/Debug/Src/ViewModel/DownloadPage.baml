   M S B A M L   `   `   `  ����K  GDumpAnalysisTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:DumpAnalysisTool.Src.PageDumpAnalysisTool.Src.Page  5      T PMahApps.Metro, Version=1.0.0.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5      1    +DumpAnalysisTool.Src.ViewModel.DownloadPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :parent'clr-namespace:DumpAnalysisTool.Src.Page     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5        N� 
Background  �#FFF N� Title$ DownloadPage��5
      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��11* �6   �� 5      $
��191* �6   �� 5      $
��467* �6   �� 5      $	��11* �6    � RowDefinitions 5   
   �� 5      $	��26* �6   �� 5      $	��26* �6   �� 5      $	��26* �6   �� 5      $	��26* �6   �� 5      $	��26* �6   �� 5      $	��26* �6   �� 5      $	��97* �6   .+�5    
   �� $��Location��6   $��1��6#   $��0��63   $
��Left=�6A   $��	10,10,0,8q�6\   $	��Top=�6o   $	��171��6�   $��34��6�   �� 5!   
   $��0��6   $��2��6    $
��Left=�60   $��	10,13,0,0q�6K   $	��Top=�6^   $	��447��6v    �� SelectedValuePath$ Id��6�    �� DisplayMemberPath$ Value��6�    �� SelectedValue 6�   ��  �� Path$ SelectedCountry���6�   �� $ CountryList��� 5#   
   $
��Date��6   $��1��6   $��1��6/   $
��Left=�6<   $��	10,10,0,8q�6W   $	��Top=�6k   $	��171��6�   $��34��6�   (  "System.Windows.Controls.DatePicker  5$   
   -   5        $��dpDatePicker��5$      $	��447��6)   $
��Left=�66   $��
10,10,0,11q�6Q   $��2��6e   $��1��6u      SelectedDate 6�   �� *DownloadDate+�� 5&   
   $��
FTP Server��6   $��1��6%   $��2��65   $
��Left=�6B   $��	10,10,0,8q�6]   $	��Top=�6p   $	��171��6�   $��34��6�   �� 5'   
   $��2��6   $��2��6"   $
��Left=�6/   $��23��6J   $��	10,11,0,0q�6V   	 �� TextWrapping$
	 Wrap=�6j   $	��Top=�6�   $	��447��6�   ��6~   �� *SERVER_ADDRESS+6�   �� 5)   
   $��FTP ID��6   $��1��6!   $��3��61   $
��Left=�6>   $��	10,10,0,8q�6Y   $	��Top=�6l   $	��171��6�   $��34��6�   �� 5*   
   $��2��6   $��3��6"   $
��Left=�6/   $��23��6J   $��	10,11,0,0q�6V   $
	 Wrap=�6i   $	��Top=�6�   $	��447��6�   ��6}   �� *	SERVER_ID+6�   �� 5,   
   $��FTP PASSWORD��6   $��1��6'   $��4��67   $
��Left=�6D   $��	10,10,0,8q�6_   $	��Top=�6r   $	��171��6�   $��34��6�   �� 5-   
   $��2��6   $��4��6"   $
��Left=�6/   $��23��6J   $��	10,11,0,0q�6V   $
	 Wrap=�6i   $	��Top=�6�   $	��447��6�   ��6}   �� *SERVER_PASSWORD+6�   �� 5/   
   $��Download Path��6   $��1��6(   $��5��68   $
��Left=�6E   $��	10,10,0,8q�6`   $	��Top=�6s   $	��171��6�   $��34��6�   �� 50   
   $��2��6   $��5��6"   $
��Left=�6/   $��23��6J   $��	10,11,0,0q�6V   $
	 Wrap=�6i   $	��Top=�6�   $	��396��6�   ��6}   �� *DOWNLOAD_PATH+6�   �� 51   
   -   5        $	��...��51      $��2��6   $
��Left=�6/   $��
411,11,0,0q�6J   $��5��6^   $	��Top=�6k   $��46��6�   6�   58      59      